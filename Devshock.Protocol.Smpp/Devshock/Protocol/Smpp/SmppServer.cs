using System;
using System.Reflection;
using Devshock.Common;
using Devshock.Net;
using Devshock.Protocol.SmppPdu;
using log4net;

namespace Devshock.Protocol.Smpp {
  [CLSCompliant(true)]
  public class SmppServer {
    protected static readonly ILog log =
      LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

    readonly SocketServer TcpServer;
    SmppServerSettings _Settings;

    public SmppServer() : this(new SmppServerSettings()) {}

    public SmppServer(SmppServerSettings Settings) {
      TcpServer = new SocketServer();
      _Settings = Settings;
      ConfigureServer();
    }

    public SmppServer(int LocalPort) : this(new SmppServerSettings(LocalPort)) {}

    public SmppServerSettings Settings {
      get { return _Settings; }
      set { _Settings = value; }
    }

    public event SmppBindHandler OnBindReq;

    public event SmppCloseConnectionHandler OnCloseConnection;

    public event SmppDeliverSmResHandler OnDeliverSmRes;

    public event SmppEnquireLinkHandler OnEnquireLinkReq;

    public event SmppEnquireLinkResHandler OnEnquireLinkRes;

    public event SmppOpenConnectionHandler OnOpenConnection;

    public event SmppQuerySmHandler OnQuerySmReq;

    public event SmppSubmitSmHandler OnSubmitSmReq;

    public event SmppUnBindHandler OnUnBindReq;

    public int BindRes(Guid ConnGuid, SmppBindRes ResponsePdu) {
      return TcpServer.Send(ConnGuid, ResponsePdu.ToByteArray());
    }

    void ConfigureServer() {
      TcpServer.evDataReceived += TcpServer_evDataReceived;
      TcpServer.evOpenConnection += TcpServer_evOpenConnection;
      TcpServer.evCloseConnection += TcpServer_evCloseConnection;
      TcpServer.evClassInformation += TcpServer_evClassInformation;
    }

    public int DeliverSmReq(Guid ConnGuid, SmppDeliverSmReq RequestPdu) {
      return TcpServer.Send(ConnGuid, RequestPdu.ToByteArray());
    }

    public int EnquireLinkReq(Guid ConnGuid, SmppEnquireLinkReq RequestPdu) {
      return TcpServer.Send(ConnGuid, RequestPdu.ToByteArray());
    }

    public int EnquireLinkRes(Guid ConnGuid, SmppEnquireLinkRes EnquireLinkResObj) {
      return TcpServer.Send(ConnGuid, EnquireLinkResObj.ToByteArray());
    }

    public SmppConnProperties GetProperties(Guid ConnGuid) {
      return new SmppConnProperties(TcpServer[ConnGuid].RemoteHost);
    }

    void ProcessPdu(Guid ConnGuid, ByteBuilder bb) {
      var header = new SmppHeader(bb);
      if (bb.Count > 0x10)
        bb.ToArray(0x10, bb.Count - 0x10);
      if (((header.CommandId == 2) || (header.CommandId == 1)) || (header.CommandId == 9))
        try {
          var pdu = new SmppBindReq(bb);
          if (OnBindReq != null)
            OnBindReq(this, new SmppBindEventArgs(ConnGuid, pdu));
        } catch {}
      else {
        int commandId = header.CommandId;
        if (commandId <= -2147483627)
          switch (commandId) {
            case -2147483643: {
              var res = new SmppDeliverSmRes(bb);
              if (OnDeliverSmRes != null)
                OnDeliverSmRes(this, new SmppDeliverSmResEventArgs(ConnGuid, res));
              return;
            }
            case -2147483627: {
              var res2 = new SmppEnquireLinkRes(bb);
              if (OnEnquireLinkRes != null) {
                OnEnquireLinkRes(this, new SmppEnquireLinkResEventArgs(ConnGuid, res2));
                return;
              }
              break;
            }
          }
        else
          switch (commandId) {
            case 3: {
              var req4 = new SmppQuerySmReq(bb);
              if (OnQuerySmReq != null)
                OnQuerySmReq(this, new SmppQuerySmEventArgs(req4, ConnGuid));
              return;
            }
            case 4: {
              var req2 = new SmppSubmitSmReq(bb);
              if (OnSubmitSmReq != null)
                OnSubmitSmReq(this, new SmppSubmitSmEventArgs(ConnGuid, req2));
              return;
            }
            case 5:
              return;

            case 6: {
              var req5 = new SmppUnBindReq(bb);
              if (OnUnBindReq != null)
                OnUnBindReq(this, new SmppUnBindEventArgs(ConnGuid, req5));
              return;
            }
            case 0x15: {
              var req3 = new SmppEnquireLinkReq(bb);
              if (OnEnquireLinkReq != null)
                OnEnquireLinkReq(this, new SmppEnquireLinkEventArgs(ConnGuid, req3));
              return;
            }
            default:
              return;
          }
      }
    }

    public int QuerySmRes(Guid ConnGuid, SmppQuerySmRes ResponsePdu) {
      return TcpServer.Send(ConnGuid, ResponsePdu.ToByteArray());
    }

    public void Start() {
      TcpServer.Settings.Capacity = _Settings.Capacity;
      TcpServer.Settings.ConnectionBacklog = _Settings.ConnectionBacklog;
      TcpServer.Settings.LocalHost = _Settings.LocalHost;
      TcpServer.Settings.LocalPort = _Settings.LocalPort;
      TcpServer.Settings.PacketSize = _Settings.PacketSize;
      TcpServer.StartServer();
    }

    public void Stop() {
      TcpServer.StopServer();
    }

    public int SubmitSmRes(Guid ConnGuid, SmppSubmitSmRes ResponsePdu) {
      return TcpServer.Send(ConnGuid, ResponsePdu.ToByteArray());
    }

    void TcpServer_evClassInformation(object sender, SocketServer.ClassInformationEventArgs e) {
      log.Info("Devshock: " + e.Description);
    }

    void TcpServer_evCloseConnection(object sender, SocketServer.CloseConnectionEventArgs e) {
      if (OnCloseConnection != null)
        OnCloseConnection(this, new SmppConnectionEventArgs(e.ConnGuid));
    }

    void TcpServer_evDataReceived(object sender, SocketServer.DataReceivedEventArgs e) {
      while (e.Session.dataIn.Count >= 4) {
        int count = SmppConverter.FromByteArrayToInt32(e.Session.PeekBytes(4));
        if (count > _Settings.MaxPduLength) {
          TcpServer.Disconnect(e.Session.ConnGuid);
          return;
        }
        if (count > e.Session.dataIn.Count)
          break;
        ProcessPdu(e.Session.ConnGuid, new ByteBuilder(e.Session.dataIn.ToArray(0, count)));
        e.Session.dataIn.RemoveRange(0, count);
      }
    }

    void TcpServer_evOpenConnection(object sender, SocketServer.OpenConnectionEventArgs e) {
      if (OnOpenConnection != null)
        OnOpenConnection(this, new SmppConnectionEventArgs(e.ConnGuid));
    }

    public int UnBind(Guid ConnGuid, SmppUnBindRes RequestPdu) {
      int num = 0;
      try {
        num = TcpServer.Send(ConnGuid, RequestPdu.ToByteArray());
      } catch {}
      TcpServer.Disconnect(ConnGuid);
      return num;
    }
  }
}