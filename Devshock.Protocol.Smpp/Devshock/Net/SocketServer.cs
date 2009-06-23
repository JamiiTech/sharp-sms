using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Devshock.Common;

namespace Devshock.Net {
  class SocketServer {
    readonly ManualResetEvent allDone;
    readonly Hashtable Connections;
    SocketSettings _Settings;
    Socket listener;
    bool ServerStarted;

    internal SocketServer() : this(new SocketSettings(0, 0, null, string.Empty)) {}

    internal SocketServer(SocketSettings Settings) {
      _Settings = new SocketSettings();
      allDone = new ManualResetEvent(false);
      _Settings = Settings;
      if (((int) Math.Round(Math.Sqrt(Settings.Capacity))) > 0x7d0)
        Connections = new Hashtable((int) Math.Round(Math.Sqrt(Settings.Capacity)));
      else
        Connections = new Hashtable(0x7d0);
    }

    internal SocketServer(int LocalPort) : this(new SocketSettings(LocalPort, 0, null, string.Empty)) {}

    internal SocketServer(int LocalPort, int Capacity)
      : this(new SocketSettings(LocalPort, Capacity, null, string.Empty)) {}

    internal SocketServer(int LocalPort, int Capacity, string PacketDelimiter)
      : this(
        new SocketSettings(LocalPort, Capacity, Encoding.GetEncoding(0).GetBytes(PacketDelimiter), string.Empty)
        ) {}

    internal SocketServer(int LocalPort, int Capacity, string PacketDelimiter, string LocalHost)
      : this(new SocketSettings(LocalPort, Capacity, Encoding.GetEncoding(0).GetBytes(PacketDelimiter), LocalHost)
        ) {}

    internal int CountConnections {
      get {
        if (Connections != null)
          return Connections.Count;
        return -1;
      }
    }

    public SessionObject this[Guid ConnGuid] {
      get { return GetConnection(ConnGuid); }
    }

    public SocketSettings Settings {
      get { return _Settings; }
      set { _Settings = value; }
    }

    internal event ClassExceptionEventHandler evClassException;

    internal event ClassInformationEventHandler evClassInformation;

    internal event CloseConnectionEventHandler evCloseConnection;

    internal event DataReceivedEventHandler evDataReceived;

    internal event OpenConnectionEventHandler evOpenConnection;

    internal event SendCompleteEventHandler evSendComplete;

    void AcceptCallBack(IAsyncResult ar) {
      allDone.Set();
      try {
        var session = new SessionObject(_Settings.PacketSize, Settings.BufferSize);
        session.mSocket = ((Socket) ar.AsyncState).EndAccept(ar);
        session.ConnGuid = Guid.NewGuid();
        if (!AddSession(session)) {
          session.mSocket.Shutdown(SocketShutdown.Both);
          session.mSocket.Close();
          OnClassInformation(new ClassInformationEventArgs(_Settings.InstaceGuid,
                                                           "The connection attempt was rejected because the limit established has been reached. To avoid this situation you must increase the value of the Capacity property"));
        } else {
          session._ConnectedOn = DateTime.Now;
          session._RemoteHost = ((IPEndPoint) session.mSocket.RemoteEndPoint).Address;
          session._RemotePort = ((IPEndPoint) session.mSocket.RemoteEndPoint).Port;
          OnOpenConnection(new OpenConnectionEventArgs(session.ConnGuid));
          if (session.mSocket.Connected)
            BeginReceive(session);
          else
            RemoveSession(session.ConnGuid);
        }
      } catch (ObjectDisposedException exception) {
        exception.GetType();
      } catch (Exception exception2) {
        OnClassException(new ClassExceptionEventArgs(_Settings.InstaceGuid, exception2));
      }
    }

    bool AddSession(SessionObject Session) {
      bool flag;
      try {
        lock (Connections.SyncRoot) {
          if (Connections.Count >= _Settings.Capacity)
            return false;
          Connections.Add(Session.ConnGuid, Session);
          flag = true;
        }
      } catch {
        flag = false;
      }
      return flag;
    }

    void BeginReceive(SessionObject mClient) {
      try {
        if (mClient.mSocket.Connected)
          mClient.mSocket.BeginReceive(mClient.Buffer, 0, _Settings.PacketSize, SocketFlags.None,
                                       new AsyncCallback(ReadCallBack), mClient);
        else
          RemoveSession(mClient.ConnGuid);
      } catch {
        Disconnect(mClient, mClient.ConnGuid);
      }
    }

    internal void BeginSend(Guid ConnGuid, Stream Buffer) {
      var buffer = new byte[Buffer.Length];
      Buffer.Read(buffer, 0, (int) Buffer.Length);
      BeginSend(ConnGuid, buffer);
    }

    internal void BeginSend(Guid ConnGuid, byte[] Buffer) {
      SessionObject connection = GetConnection(ConnGuid);
      if (((connection == null) || (connection.mSocket == null)) || !connection.mSocket.Connected)
        throw new InvalidSocketException(
          "A request to send data was disallowed because the socket is not connected");
      connection.mSocket.BeginSend(Buffer, 0, Buffer.Length, SocketFlags.None, new AsyncCallback(SendCallBack),
                                   connection);
    }

    internal void BeginSend(Guid ConnGuid, string Buffer) {
      byte[] bytes = _Settings.DataCoding.GetBytes(Buffer);
      BeginSend(ConnGuid, bytes);
    }

    internal stuConnectionProperties ConnectionProperties(Guid ConnGuid) {
      stuConnectionProperties properties = null;
      try {
        SessionObject connection = GetConnection(ConnGuid);
        if (connection != null)
          properties = new stuConnectionProperties(connection.ConnectedOn,
                                                   ((IPEndPoint) connection.mSocket.RemoteEndPoint).Address.
                                                     ToString(),
                                                   ((IPEndPoint) connection.mSocket.RemoteEndPoint).Port);
        return properties;
      } catch {
        return properties;
      }
    }

    internal void Disconnect(Guid ConnGuid) {
      Disconnect(GetConnection(ConnGuid), ConnGuid);
    }

    void Disconnect(SessionObject mClient, Guid ConnGuid) {
      try {
        if (mClient != null)
          if ((mClient.mSocket != null) && mClient.mSocket.Connected)
            mClient.mSocket.Close();
          else
            RemoveSession(mClient.ConnGuid);
        else if (ConnGuid != Guid.Empty)
          RemoveSession(ConnGuid);
      } catch (Exception exception) {
        exception.GetType();
      }
    }

    internal bool Exists(Guid ConnGuid) {
      return Connections.ContainsKey(ConnGuid);
    }

    SessionObject GetConnection(Guid ConnGuid) {
      lock (Connections.SyncRoot) {
        return (SessionObject) Connections[ConnGuid];
      }
    }

    internal IEnumerator GetConnections() {
      ArrayList list;
      lock (Connections.SyncRoot) {
        list = new ArrayList(Connections.Count);
        IEnumerator enumerator = Connections.Keys.GetEnumerator();
        while (enumerator.MoveNext())
          list.Add(enumerator.Current);
      }
      return list.GetEnumerator();
    }

    internal void OnClassException(ClassExceptionEventArgs e) {
      if (evClassException != null)
        evClassException(this, e);
    }

    internal void OnClassInformation(ClassInformationEventArgs e) {
      if (evClassInformation != null)
        evClassInformation(this, e);
    }

    internal void OnCloseConnection(CloseConnectionEventArgs e) {
      if (evCloseConnection != null)
        evCloseConnection(this, e);
    }

    internal void OnDataReceived(DataReceivedEventArgs e) {
      if (evDataReceived != null)
        evDataReceived(this, e);
    }

    internal void OnOpenConnection(OpenConnectionEventArgs e) {
      if (evOpenConnection != null)
        evOpenConnection(this, e);
    }

    internal void OnSendComplete(SendCompleteEventArgs e) {
      if (evSendComplete != null)
        evSendComplete(this, e);
    }

    void ReadCallBack(IAsyncResult ar) {
      Guid empty = Guid.Empty;
      try {
        var asyncState = (SessionObject) ar.AsyncState;
        empty = asyncState.ConnGuid;
        if (asyncState != null)
          if (!asyncState.mSocket.Connected)
            RemoveSession(empty);
          else {
            int num;
            try {
              num = asyncState.mSocket.EndReceive(ar);
            } catch {
              RemoveSession(empty);
              return;
            }
            if (num == 0) {
              asyncState.mSocket.Close();
              RemoveSession(empty);
            } else {
              asyncState.Statistics._BytesReceived += num;
              if ((asyncState.dataIn.Count + num) > _Settings.BufferSize) {
                Disconnect(empty);
                RemoveSession(empty);
                OnClassInformation(new ClassInformationEventArgs(empty,
                                                                 "Client disconnected due a buffer overflow"));
              }
              int count = asyncState.dataIn.Count;
              asyncState.dataIn.AddRange(asyncState.Buffer, num);
              if (_Settings.PacketDelimiter == null) {
                asyncState.dataIn.ToArray(0, num);
                OnDataReceived(new DataReceivedEventArgs(asyncState, count, num));
              } else
                for (int i = asyncState.dataIn.IndexOf(_Settings.PacketDelimiter);
                     i > -1;
                     i = asyncState.dataIn.IndexOf(_Settings.PacketDelimiter)) {
                  asyncState.dataIn.ToArray(0, i + _Settings.PacketDelimiter.Length);
                  OnDataReceived(new DataReceivedEventArgs(asyncState, count, num));
                }
              BeginReceive(asyncState);
            }
          }
      } catch (Exception exception) {
        Disconnect(GetConnection(empty), empty);
        OnClassException(new ClassExceptionEventArgs(empty, exception));
      }
    }

    void RemoveSession(Guid ConnGuid) {
      lock (Connections.SyncRoot) {
        if (Connections.Contains(ConnGuid)) {
          OnCloseConnection(new CloseConnectionEventArgs(ConnGuid));
          Connections.Remove(ConnGuid);
        }
      }
    }

    internal int Send(Guid ConnGuid, string Buffer) {
      return Send(ConnGuid, _Settings.DataCoding.GetBytes(Buffer));
    }

    internal int Send(Guid ConnGuid, byte[] Buffer) {
      int num = 0;
      SessionObject connection = GetConnection(ConnGuid);
      if (((connection == null) || (connection.mSocket == null)) || !connection.mSocket.Connected)
        throw new InvalidSocketException(
          "A request to send data was disallowed because the socket is not connected");
      num = connection.mSocket.Send(Buffer);
      connection.Statistics._BytesSent += num;
      return num;
    }

    internal int Send(Guid ConnGuid, Stream Buffer) {
      var buffer = new byte[Buffer.Length];
      Buffer.Read(buffer, 0, (int) Buffer.Length);
      return Send(ConnGuid, buffer);
    }

    void SendCallBack(IAsyncResult ar) {
      var asyncState = (SessionObject) ar.AsyncState;
      try {
        int bytesSent = 0;
        bytesSent = asyncState.mSocket.EndSend(ar);
        asyncState.Statistics._BytesSent += bytesSent;
        OnSendComplete(new SendCompleteEventArgs(asyncState.ConnGuid, bytesSent));
      } catch {
        OnSendComplete(new SendCompleteEventArgs(asyncState.ConnGuid, -1));
      }
    }

    internal void StartServer() {
      IPAddress any;
      if (ServerStarted)
        throw new Exception("You must stop the server before start again", new SocketException());
      if (_Settings.LocalHost != string.Empty)
        any = IPAddress.Parse(_Settings.LocalHost);
      else
        any = IPAddress.Any;
      var localEP = new IPEndPoint(any, _Settings.LocalPort);
      listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
      listener.Bind(localEP);
      listener.Listen(100);
      ServerStarted = true;
      OnClassInformation(new ClassInformationEventArgs(_Settings.InstaceGuid,
                                                       "Server started on port " + _Settings.LocalPort));
      ThreadPool.QueueUserWorkItem(StartServerThread);
    }

    void StartServerThread(object state) {
      try {
        while (true) {
          allDone.Reset();
          listener.BeginAccept(new AsyncCallback(AcceptCallBack), listener);
          allDone.WaitOne();
        }
      } catch (ObjectDisposedException exception) {
        exception.GetType();
      } catch (ThreadAbortException exception2) {
        exception2.GetType();
      } catch (Exception exception3) {
        OnClassException(new ClassExceptionEventArgs(_Settings.InstaceGuid, exception3));
      } finally {
        ServerStarted = false;
        OnClassInformation(new ClassInformationEventArgs(_Settings.InstaceGuid,
                                                         "Server stoped on port " + _Settings.LocalPort));
      }
    }

    internal void StopServer() {
      try {
        if (listener == null)
          OnClassException(new ClassExceptionEventArgs(_Settings.InstaceGuid,
                                                       new Exception("The server is not started",
                                                                     new SocketException())));
        else {
          listener.Close();
          allDone.Set();
          IEnumerator connections = GetConnections();
          while (connections.MoveNext())
            Disconnect((Guid) connections.Current);
        }
      } catch {}
    }

    #region Nested type: ClassExceptionEventArgs

    internal class ClassExceptionEventArgs : EventArgs {
      internal readonly Exception ClassException;
      internal readonly Guid ConnGuid = Guid.Empty;

      internal ClassExceptionEventArgs(Guid ConnGuid, Exception ClassException) {
        this.ConnGuid = ConnGuid;
        this.ClassException = ClassException;
      }
    }

    #endregion

    #region Nested type: ClassExceptionEventHandler

    internal delegate void ClassExceptionEventHandler(object sender, ClassExceptionEventArgs e);

    #endregion

    #region Nested type: ClassInformationEventArgs

    internal class ClassInformationEventArgs : EventArgs {
      internal readonly Guid ConnGuid = Guid.Empty;
      internal readonly string Description = string.Empty;

      internal ClassInformationEventArgs(Guid ConnGuid, string Description) {
        this.ConnGuid = ConnGuid;
        this.Description = Description;
      }
    }

    #endregion

    #region Nested type: ClassInformationEventHandler

    internal delegate void ClassInformationEventHandler(object sender, ClassInformationEventArgs e);

    #endregion

    #region Nested type: CloseConnectionEventArgs

    internal class CloseConnectionEventArgs : EventArgs {
      internal readonly Guid ConnGuid = Guid.Empty;

      internal CloseConnectionEventArgs(Guid ConnGuid) {
        this.ConnGuid = ConnGuid;
      }
    }

    #endregion

    #region Nested type: CloseConnectionEventHandler

    internal delegate void CloseConnectionEventHandler(object sender, CloseConnectionEventArgs e);

    #endregion

    #region Nested type: DataReceivedEventArgs

    internal class DataReceivedEventArgs : EventArgs {
      internal readonly int Length;
      internal readonly SessionObject Session;
      internal readonly int StartIndex;

      internal DataReceivedEventArgs(SessionObject Session, int StartIndex, int Length) {
        this.Session = Session;
        this.StartIndex = StartIndex;
        this.Length = Length;
      }
    }

    #endregion

    #region Nested type: DataReceivedEventHandler

    internal delegate void DataReceivedEventHandler(object sender, DataReceivedEventArgs e);

    #endregion

    #region Nested type: OpenConnectionEventArgs

    internal class OpenConnectionEventArgs : EventArgs {
      internal readonly Guid ConnGuid = Guid.Empty;

      internal OpenConnectionEventArgs(Guid ConnGuid) {
        this.ConnGuid = ConnGuid;
      }
    }

    #endregion

    #region Nested type: OpenConnectionEventHandler

    internal delegate void OpenConnectionEventHandler(object sender, OpenConnectionEventArgs e);

    #endregion

    #region Nested type: SendCompleteEventArgs

    internal class SendCompleteEventArgs : EventArgs {
      internal readonly int BytesSent = -1;
      internal readonly Guid ConnGuid = Guid.Empty;

      internal SendCompleteEventArgs(Guid ConnGuid, int BytesSent) {
        this.ConnGuid = ConnGuid;
        this.BytesSent = BytesSent;
      }
    }

    #endregion

    #region Nested type: SendCompleteEventHandler

    internal delegate void SendCompleteEventHandler(object sender, SendCompleteEventArgs e);

    #endregion

    #region Nested type: SessionObject

    internal class SessionObject {
      internal DateTime _ConnectedOn = DateTime.Now;
      internal IPAddress _RemoteHost;
      internal int _RemotePort;
      internal byte[] Buffer;
      internal Guid ConnGuid = Guid.Empty;
      internal ByteBuilder dataIn;
      internal Socket mSocket;
      internal SocketStatistics Statistics;
      internal object Tag;

      internal SessionObject(int inSocketBufferSize, int inServerBufferSize) {
        Buffer = new byte[inSocketBufferSize];
        dataIn = new ByteBuilder(inServerBufferSize);
      }

      internal DateTime ConnectedOn {
        get { return _ConnectedOn; }
      }

      internal IPAddress RemoteHost {
        get { return _RemoteHost; }
      }

      internal int RemotePort {
        get { return _RemotePort; }
      }

      public byte[] PeekBytes(int Size) {
        return dataIn.ToArray(0, Size);
      }
    }

    #endregion

    #region Nested type: stuConnectionProperties

    internal class stuConnectionProperties {
      readonly DateTime _ConnectedOn;
      readonly string _RemoteHost;
      readonly int _RemotePort;

      internal stuConnectionProperties(DateTime ConnectedOn, string RemoteHost, int RemotePort) {
        _ConnectedOn = ConnectedOn;
        _RemoteHost = RemoteHost;
        _RemotePort = RemotePort;
      }

      internal DateTime ConnectedOn {
        get { return _ConnectedOn; }
      }

      internal string RemoteHost {
        get { return _RemoteHost; }
      }

      internal int RemotePort {
        get { return _RemotePort; }
      }
    }

    #endregion
  }
}