using System;
using System.Collections;
using System.Collections.Specialized;
using System.Net.Sockets;
using System.Reflection;
using System.Threading;
using Devshock.Common;
using Devshock.Net;
using Devshock.Protocol.SmppPdu;
using log4net;

namespace Devshock.Protocol.Smpp {
  class SmppInternalConnection {
    protected static readonly ILog log =
      LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

    readonly SockClient TcpConnection;

    Exception _LastException;
    int _SequenceNumber;
    internal HybridDictionary AsyncPdu;
    bool Binding;
    internal Guid ClientGuid;
    internal bool ConnectionSuccess;
    internal int ConsecutiveTimeouts;
    internal SmppBindRes LastBindRes;
    internal DateTime LastReceptionTime;
    internal ArrayList PendingBind;
    internal ArrayList PendingQueue;
    internal ListDictionary PendingResponse;
    internal DateTime ReleasedDateTime;
    internal SmppSettings Settings;

    public SmppInternalConnection() : this(new SmppSettings()) {}

    public SmppInternalConnection(SmppSettings Settings) {
      TcpConnection = new SockClient();
      PendingResponse = new ListDictionary();
      PendingQueue = new ArrayList(15);
      PendingBind = new ArrayList(10);
      ReleasedDateTime = DateTime.Now;
      LastReceptionTime = DateTime.Now;
      ClientGuid = Guid.NewGuid();
      this.Settings = Settings;
      TcpConnection.DataReceived += TcpConnection_DataReceived;
      TcpConnection.CloseConnection += TcpConnection_CloseConnection;
    }

    internal bool Connected {
      get {
        if (TcpConnection == null)
          return false;
        return TcpConnection.Connected;
      }
    }

    public int CountPending {
      get {
        if (PendingResponse != null)
          return PendingResponse.Count;
        return 0;
      }
    }

    public Exception LastException {
      get { return _LastException; }
    }

    public event SmppDeliverSmHandler OnDeliverSmReq;

    public event SmppDisconnectedHandler OnDisconnected;

    public event SmppEnquireLinkHandler OnEnquireLinkReq;

    public event SmppUnBindHandler OnUnBindReq;

    void AddPendingResponse(int TransactionKey, SmppAsyncObject AsyncObject) {
      lock (PendingResponse.SyncRoot) {
        PendingResponse.Add(TransactionKey, AsyncObject);
      }
    }

    internal void AsyncConnect(object state) {
      Exception e = null;
      try {
        TcpConnection.Connect();
      } catch (Exception exception2) {
        e = exception2;
      }
      TcpConnection_OpenConnection(e);
    }

    internal void AsyncNotifyConnect(object state) {
      TcpConnection_OpenConnection(null);
    }

    internal void BeginBind(SmppBindResHandler Callback, object State) {
      lock (this) {
        var req = new SmppBindReq(Settings.ConnectionMode.Value, GetSequenceNumber(), Settings.BindParams);
        var obj2 = new SmppAsyncObject();
        obj2.Callback = Callback;
        obj2.Request = req;
        obj2.State = State;
        obj2.Timeout = Settings.Timeout;
        PendingBind.Add(obj2);
        if (!Connected) {
          if (Binding)
            goto Label_0142;
          TcpConnection.RemoteHost = Settings.RemoteHost;
          TcpConnection.RemotePort = Settings.RemotePort;
          TcpConnection.LocalHost = Settings.LocalHost;
          TcpConnection.LocalPort = Settings.LocalPort;
          TcpConnection.BufferSize = Settings.SocketBufferSize;
          ConnectionSuccess = false;
          Binding = true;
          try {
            ThreadPool.QueueUserWorkItem(AsyncConnect);
            goto Label_0142;
          } catch {
            Binding = false;
            PendingBind.Remove(obj2);
            throw;
          }
        }
        ThreadPool.QueueUserWorkItem(AsyncNotifyConnect);
        Label_0142:
        ;
      }
    }

    internal void BeginCancelSm(SmppCancelSmReq RequestPdu, SmppCancelSmResHandler Callback, object State) {
      InitializeAsyncTransac(RequestPdu, Callback, State);
    }

    internal void BeginEnquireLink(SmppEnquireLinkReq RequestPdu, SmppAsyncEnquireLinkResHandler Callback,
                                   object State) {
      InitializeAsyncTransac(RequestPdu, Callback, State);
    }

    internal void BeginQuerySm(SmppQuerySmReq RequestPdu, SmppQuerySmResHandler Callback, object State) {
      InitializeAsyncTransac(RequestPdu, Callback, State);
    }

    internal void BeginSubmitSm(SmppSubmitSmReq RequestPdu, SmppSubmitSmResHandler Callback, object State) {
      InitializeAsyncTransac(RequestPdu, Callback, State);
    }

    internal void BeginUnBind() {
      if (TcpConnection.Connected) {
        SendResPdu(new SmppUnBindReq().ToByteArray());
        TcpConnection.Disconnect();
      }
      LastBindRes = null;
    }

    internal SmppBindRes Bind() {
      SmppBindRes res = null;
      SmppBindReq requestPdu = null;
      lock (this) {
        try {
          if (Connected)
            throw new Exception("You must disconnect before connect again", new SocketException(0x186a1));
          TcpConnection.RemoteHost = Settings.RemoteHost;
          TcpConnection.RemotePort = Settings.RemotePort;
          TcpConnection.LocalHost = Settings.LocalHost;
          TcpConnection.LocalPort = Settings.LocalPort;
          TcpConnection.BufferSize = Settings.SocketBufferSize;
          ConnectionSuccess = false;
          TcpConnection.Connect();
        } catch (Exception exception) {
          _LastException = exception;
          res = new SmppBindRes(new SmppHeader(Settings.ConnectionMode.Value, GetSequenceNumber()));
          res.Header.CommandStatus = 0x15f95;
          return res;
        }
        requestPdu = new SmppBindReq(Settings.ConnectionMode.Value, GetSequenceNumber(), Settings.BindParams);
        var asyncObject = new SmppAsyncObject();
        SendReqPdu(requestPdu, ref asyncObject);
        res = new SmppBindRes(asyncObject.PduRes);
        if (res.Header.CommandStatus != 0) {
          TcpConnection.Disconnect();
          return res;
        }
        PendingResponse.Clear();
        ConnectionSuccess = true;
      }
      return res;
    }

    internal SmppCancelSmRes CancelSm(SmppCancelSmReq RequestPdu) {
      var asyncObject = new SmppAsyncObject();
      SendReqPdu(RequestPdu, ref asyncObject);
      return new SmppCancelSmRes(asyncObject.PduRes);
    }

    internal void DeliverSmRes(SmppDeliverSmRes DeliverSmResObj) {
      SendResPdu(DeliverSmResObj.ToByteArray());
    }

    internal SmppEnquireLinkRes EnquireLink(SmppEnquireLinkReq RequestPdu) {
      var asyncObject = new SmppAsyncObject();
      SendReqPdu(RequestPdu, ref asyncObject);
      return new SmppEnquireLinkRes(asyncObject.PduRes);
    }

    internal void EnquireLinkRes(SmppEnquireLinkRes EnquireLinkResObj) {
      SendResPdu(EnquireLinkResObj.ToByteArray());
    }

    void FindAndRemoveTransaction(SmppAsyncObject AsyncObject) {
      lock (PendingResponse.SyncRoot) {
        lock (PendingQueue.SyncRoot) {
          AsyncObject.DisposeTimer();
          if (PendingResponse.Contains(AsyncObject.Request.Header.SequenceNumber)) {
            PendingResponse.Remove(AsyncObject.Request.Header.SequenceNumber);
            if ((PendingQueue.Count > 0) && TcpConnection.Connected)
              for (int i = 0; i < PendingQueue.Count; i++) {
                var obj2 = PendingQueue[i] as SmppAsyncObject;
                lock (obj2.SyncRoot) {
                  if (obj2.AsyncState == SmppAsyncObject.SmppAsyncState.Enabled) {
                    PendingQueue.Remove(obj2);
                    obj2.StartTimer();
                    PendingResponse.Add(obj2.Request.Header.SequenceNumber, obj2);
                    SendResPdu(obj2.Request.ToByteArray());
                    goto Label_0142;
                  }
                }
              }
          } else
            PendingQueue.Remove(AsyncObject);
        }
        Label_0142:
        ;
      }
    }

    SmppAsyncObject FindAsyncObject(int TransactionKey) {
      return (SmppAsyncObject) PendingResponse[TransactionKey];
    }

    int GetSequenceNumber() {
      int num = Interlocked.Increment(ref _SequenceNumber);
      if (num > 0x7ffff1c0)
        _SequenceNumber = Interlocked.Exchange(ref _SequenceNumber, 0);
      return num;
    }

    internal void InitializeAsyncTransac(ISmppPdu RequestObj, object Callback, object State) {
      if (RequestObj.Header.SequenceNumber == 0)
        RequestObj.Header.SequenceNumber = GetSequenceNumber();
      var obj2 = new SmppAsyncObject(Callback, State, RequestObj, Settings.Timeout);
      obj2.CompletionCallback = ProcessAsyncPdu;
      lock (PendingResponse.SyncRoot) {
        lock (PendingQueue.SyncRoot) {
          obj2.StartTimer();
          if (PendingResponse.Count < Settings.WindowSize) {
            PendingResponse.Add(RequestObj.Header.SequenceNumber, obj2);
            try {
              SendResPdu(RequestObj.ToByteArray());
              goto Label_00ED;
            } catch {
              PendingResponse.Remove(RequestObj.Header.SequenceNumber);
              throw;
            }
          }
          PendingQueue.Add(obj2);
        }
        Label_00ED:
        ;
      }
    }

    internal bool IsTooOld() {
      var totalSeconds = (int) DateTime.Now.Subtract(ReleasedDateTime).TotalSeconds;
      return ((Settings.Recycling.Method != SmppSettings.RecyclingMethod.None) &&
              (totalSeconds > Settings.Recycling.IdleTimeout));
    }

    void PendingBindResTrigger(SmppBindRes ResponsePdu, Exception ProcessException) {
      foreach (SmppAsyncObject obj2 in PendingBind) {
        var e = new SmppAsyncBindResEventArgs();
        e._State = obj2.State;
        e._ProcessException = ProcessException;
        e._RequestPdu = obj2.Request as SmppBindReq;
        e._ResponsePdu = ResponsePdu;
        var callback = obj2.Callback as SmppBindResHandler;
        callback(this, e);
      }
      PendingBind.Clear();
    }

    void ProcessAsyncPdu(SmppAsyncObject AsyncObject, SmppAsyncObject.SmppAsyncCompleted CompletionReason) {
      try {
        Exception exception = null;
        object obj2 = null;
        FindAndRemoveTransaction(AsyncObject);
        switch (CompletionReason) {
          case SmppAsyncObject.SmppAsyncCompleted.Response: {
            var header = new SmppHeader(AsyncObject.PduRes);
            if (header.CommandId != -2147483648)
              break;
            exception = new SmppGenericNackException();
            goto Label_008E;
          }
          case SmppAsyncObject.SmppAsyncCompleted.Timeout:
            exception = new SmppTimeOutException();
            Interlocked.Increment(ref ConsecutiveTimeouts);
            if (ConsecutiveTimeouts > Settings.WindowSize)
              BeginUnBind();
            goto Label_008E;

          case SmppAsyncObject.SmppAsyncCompleted.Disconnection:
            exception = new SmppInvalidConnectionStateException();
            goto Label_008E;

          default:
            goto Label_008E;
        }
        obj2 = new SmppSubmitSmRes(AsyncObject.PduRes);
        ConsecutiveTimeouts = 0;
        Label_008E:
        switch (AsyncObject.Request.Header.CommandId) {
          case 3: {
            var args3 = new SmppAsyncQuerySmResEventArgs();
            args3._State = AsyncObject.State;
            args3._ProcessException = exception;
            args3._RequestPdu = AsyncObject.Request as SmppQuerySmReq;
            args3._ResponsePdu = obj2 as SmppQuerySmRes;
            var handler3 = AsyncObject.Callback as SmppQuerySmResHandler;
            handler3(this, args3);
            return;
          }
          case 4: {
            var args = new SmppAsyncSubmitSmResEventArgs();
            args._State = AsyncObject.State;
            args._ProcessException = exception;
            args._RequestPdu = AsyncObject.Request as SmppSubmitSmReq;
            args._ResponsePdu = obj2 as SmppSubmitSmRes;
            var handler = AsyncObject.Callback as SmppSubmitSmResHandler;
            handler(this, args);
            return;
          }
          case 0x15:
            break;

          default:
            return;
        }
        var e = new SmppAsyncEnquireLinkResEventArgs();
        e._State = AsyncObject.State;
        e._ProcessException = exception;
        e._RequestPdu = AsyncObject.Request as SmppEnquireLinkReq;
        e._ResponsePdu = obj2 as SmppEnquireLinkRes;
        var callback = AsyncObject.Callback as SmppAsyncEnquireLinkResHandler;
        callback(this, e);
      } catch (Exception exception2) {
        log.Error("Devshock: " + exception2);
      }
    }

    void ProcessPdu(ByteBuilder bb) {
      var header = new SmppHeader(bb);
      switch (header.CommandId) {
        case 5: {
          var pdu = new SmppDeliverSmReq(bb);
          RaiseDeliverSmReq(new SmppDeliverSmEventArgs(pdu));
          return;
        }
        case 6:
          TcpConnection.Disconnect();
          return;

        case 0x15:
          EnquireLinkRes(new SmppEnquireLinkRes(new SmppHeader(0x10, -2147483627, 0, header.SequenceNumber)));
          RaiseEnquireLinkReq(new SmppEnquireLinkEventArgs(ClientGuid, new SmppEnquireLinkReq(header)));
          return;
      }
    }

    internal SmppQuerySmRes QuerySm(SmppQuerySmReq RequestPdu) {
      var asyncObject = new SmppAsyncObject();
      SendReqPdu(RequestPdu, ref asyncObject);
      return new SmppQuerySmRes(asyncObject.PduRes);
    }

    void RaiseDeliverSmReq(SmppDeliverSmEventArgs e) {
      if (OnDeliverSmReq != null)
        OnDeliverSmReq(this, e);
    }

    internal void RaiseDisconnected() {
      if (OnDisconnected != null)
        OnDisconnected(this);
    }

    void RaiseEnquireLinkReq(SmppEnquireLinkEventArgs e) {
      if (OnEnquireLinkReq != null)
        OnEnquireLinkReq(this, e);
    }

    void RaiseUnBindReq(SmppUnBindEventArgs e) {
      if (OnUnBindReq != null)
        OnUnBindReq(this, e);
    }

    void RemovePendingResponse(int TransactionKey) {
      lock (PendingResponse.SyncRoot) {
        PendingResponse.Remove(TransactionKey);
      }
    }

    void SendReqPdu(ISmppPdu RequestPdu, ref SmppAsyncObject AsyncObject) {
      if (RequestPdu.Header.SequenceNumber == 0)
        RequestPdu.Header.SequenceNumber = GetSequenceNumber();
      AsyncObject.mre = new ManualResetEvent(false);
      AddPendingResponse(RequestPdu.Header.SequenceNumber, AsyncObject);
      if (TcpConnection.Send(RequestPdu.ToByteArray()) <= 0) {
        RemovePendingResponse(RequestPdu.Header.SequenceNumber);
        TcpConnection.Disconnect();
        throw new Exception("Invalid connection State");
      }
      if (!AsyncObject.mre.WaitOne(Settings.Timeout, false) || (AsyncObject.PduRes == null)) {
        RemovePendingResponse(RequestPdu.Header.SequenceNumber);
        if (Settings.DisconnectOnTimeout)
          TcpConnection.Disconnect();
        throw new SmppTimeOutException();
      }
      var header = new SmppHeader(AsyncObject.PduRes);
      if (header.CommandId == -2147483648) {
        RemovePendingResponse(RequestPdu.Header.SequenceNumber);
        throw new SmppGenericNackException(SmppStatusCodes.GetDescription(RequestPdu.Header.CommandStatus));
      }
      RemovePendingResponse(RequestPdu.Header.SequenceNumber);
    }

    void SendResPdu(byte[] PduByteArray) {
      if (TcpConnection.Connected)
        TcpConnection.Send(PduByteArray);
    }

    internal SmppSubmitSmRes SubmitSm(SmppSubmitSmReq RequestPdu) {
      var asyncObject = new SmppAsyncObject();
      SendReqPdu(RequestPdu, ref asyncObject);
      return new SmppSubmitSmRes(asyncObject.PduRes);
    }

    void TcpConnection_CloseConnection(object sender, SockClient.CloseConnectionEventArgs e) {
      try {
        lock (TcpConnection) {
          ConsecutiveTimeouts = 0;
          lock (PendingResponse.SyncRoot) {
            foreach (DictionaryEntry entry in PendingResponse) {
              var asyncObject = (SmppAsyncObject) entry.Value;
              if (asyncObject.mre != null) {
                asyncObject.mre.Set();
                PendingResponse.Remove(entry.Key);
                continue;
              }
              new SmppCompletionCallbackHandler(ProcessAsyncPdu).BeginInvoke(asyncObject,
                                                                             SmppAsyncObject.SmppAsyncCompleted.
                                                                               Disconnection, null, null);
            }
            lock (PendingQueue.SyncRoot) {
              for (int i = 0; i < PendingQueue.Count; i++) {
                var obj3 = PendingQueue[i] as SmppAsyncObject;
                new SmppCompletionCallbackHandler(ProcessAsyncPdu).BeginInvoke(obj3,
                                                                               SmppAsyncObject.SmppAsyncCompleted.
                                                                                 Disconnection, null,
                                                                               null);
              }
            }
          }
        }
      } catch {}
      if (ConnectionSuccess)
        RaiseUnBindReq(new SmppUnBindEventArgs(ClientGuid, new SmppUnBindReq()));
      ConnectionSuccess = false;
    }

    void TcpConnection_DataReceived(object sender, SockClient.DataReceivedEventArgs e) {
      LastReceptionTime = DateTime.Now;
      while (TcpConnection.ReadSize() >= 0x10) {
        int bytes = SmppConverter.FromByteArrayToInt32(TcpConnection.PeekBytes(4));
        if (bytes > Settings.MaxPduSize) {
          TcpConnection.Disconnect();
          return;
        }
        if (bytes > TcpConnection.ReadSize())
          break;
        var bb = new ByteBuilder(TcpConnection.ReadByteArray(bytes, 0));
        var header = new SmppHeader(bb);
        SmppAsyncObject asyncObject = FindAsyncObject(header.SequenceNumber);
        if (asyncObject != null) {
          asyncObject.PduRes = bb;
          if (asyncObject.mre != null)
            asyncObject.mre.Set();
          else if (asyncObject.Callback != null)
            new SmppCompletionCallbackHandler(ProcessAsyncPdu).BeginInvoke(asyncObject,
                                                                           SmppAsyncObject.SmppAsyncCompleted.Response,
                                                                           null, null);
        } else
          ProcessPdu(bb);
      }
    }

    void TcpConnection_OpenConnection(Exception e) {
      SmppBindRes responsePdu = null;
      Exception processException = null;
      lock (this) {
        try {
          if ((e == null) && TcpConnection.Connected) {
            var asyncObject = PendingBind[0] as SmppAsyncObject;
            SendReqPdu(asyncObject.Request, ref asyncObject);
            responsePdu = new SmppBindRes(asyncObject.PduRes);
            if (responsePdu.Header.CommandStatus != 0)
              TcpConnection.Disconnect();
            else {
              LastBindRes = responsePdu;
              PendingResponse.Clear();
              ConnectionSuccess = true;
            }
          } else
            processException = e;
        } catch (Exception exception2) {
          processException = exception2;
        }
        Binding = false;
        PendingBindResTrigger(responsePdu, processException);
      }
    }

    internal void UnBind() {
      if (TcpConnection.Connected) {
        var requestPdu = new SmppUnBindReq();
        var asyncObject = new SmppAsyncObject();
        SendReqPdu(requestPdu, ref asyncObject);
        TcpConnection.Disconnect();
      }
      LastBindRes = null;
    }
  }
}