using System;
using System.Reflection;
using Devshock.Protocol.SmppPdu;
using log4net;

namespace Devshock.Protocol.Smpp {
  [CLSCompliant(true)]
  public class SmppConnection {
    protected static readonly ILog log =
      LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

    SmppInternalConnection _InternalConnection;
    SmppSettings _Settings;

    public SmppConnection() : this(new SmppSettings()) {}

    public SmppConnection(SmppSettings ConnectionSettings) {
      _Settings = ConnectionSettings;
    }

    public SmppConnection(string ConnectionString) : this(new SmppSettings(ConnectionString)) {}

    public bool Connected {
      get {
        if (_InternalConnection != null)
          return _InternalConnection.Connected;
        return false;
      }
    }

    public Exception LastException {
      get {
        if (_InternalConnection != null)
          return _InternalConnection.LastException;
        return null;
      }
    }

    public DateTime LastPduTime {
      get { return _InternalConnection.LastReceptionTime; }
    }

    public SmppQueueCounter PendingPdu {
      get {
        return new SmppQueueCounter(_InternalConnection.PendingQueue.Count,
                                    _InternalConnection.PendingResponse.Count);
      }
    }

    public SmppSettings Settings {
      get { return _Settings; }
      set { _Settings = value; }
    }

    public event SmppDeliverSmHandler OnDeliverSmReq;

    public event SmppEnquireLinkHandler OnEnquireLinkReq;

    public event SmppUnBindHandler OnUnBindReq;

    void _InternalConnection_OnDeliverSmReq(object sender, SmppDeliverSmEventArgs e) {
      RaiseDeliverSmReq(e);
    }

    void _InternalConnection_OnEnquireLinkReq(object sender, SmppEnquireLinkEventArgs e) {
      RaiseEnquireLinkReq(e);
    }

    void _InternalConnection_OnUnBindReq(object sender, SmppUnBindEventArgs e) {
      RaiseUnBindReq(e);
    }

    public void BeginBind(SmppBindResHandler Callback) {
      BeginBind(Callback, null);
    }

    public void BeginBind(SmppBindResHandler Callback, object State) {
      if ((_InternalConnection != null) && _InternalConnection.Connected)
        throw new SmppException("Error Connecting: The connection is already Open.");
      switch (_Settings.Recycling.Method) {
        case SmppSettings.RecyclingMethod.Pooling:
          _InternalConnection = SmppPoolManager.GetConnection(_Settings);
          break;

        case SmppSettings.RecyclingMethod.Sharing:
          _InternalConnection = SmppShareManager.GetConnection(_Settings);
          break;

        default:
          _InternalConnection = new SmppInternalConnection(_Settings);
          break;
      }
      SetEvents();
      if ((_InternalConnection.Connected && (_InternalConnection.LastBindRes != null)) &&
          ((_InternalConnection.LastBindRes.Header != null) &&
           (_InternalConnection.LastBindRes.Header.CommandStatus == 0)))
        Callback(this, new SmppAsyncBindResEventArgs(_InternalConnection.LastBindRes));
      else
        _InternalConnection.BeginBind(Callback, State);
    }

    public void BeginCancelSm(SmppCancelSmReq RequestPdu, SmppCancelSmResHandler Callback) {
      _InternalConnection.BeginCancelSm(RequestPdu, Callback, null);
    }

    public void BeginCancelSm(SmppCancelSmReq RequestPdu, SmppCancelSmResHandler Callback, object State) {
      _InternalConnection.BeginCancelSm(RequestPdu, Callback, State);
    }

    public void BeginEnquireLink(SmppEnquireLinkReq RequestPdu, SmppAsyncEnquireLinkResHandler Callback) {
      _InternalConnection.BeginEnquireLink(RequestPdu, Callback, null);
    }

    public void BeginEnquireLink(SmppEnquireLinkReq RequestPdu, SmppAsyncEnquireLinkResHandler Callback,
                                 object State) {
      _InternalConnection.BeginEnquireLink(RequestPdu, Callback, State);
    }

    public void BeginQuerySm(SmppQuerySmReq RequestPdu, SmppQuerySmResHandler Callback) {
      _InternalConnection.BeginQuerySm(RequestPdu, Callback, null);
    }

    public void BeginQuerySm(SmppQuerySmReq RequestPdu, SmppQuerySmResHandler Callback, object State) {
      _InternalConnection.BeginQuerySm(RequestPdu, Callback, State);
    }

    public void BeginSubmitSm(SmppSubmitSmReq RequestPdu, SmppSubmitSmResHandler Callback) {
      _InternalConnection.BeginSubmitSm(RequestPdu, Callback, null);
    }

    public void BeginSubmitSm(SmppSubmitSmReq RequestPdu, SmppSubmitSmResHandler Callback, object State) {
      _InternalConnection.BeginSubmitSm(RequestPdu, Callback, State);
    }

    public SmppBindRes Bind() {
      if ((_InternalConnection != null) && _InternalConnection.Connected)
        throw new SmppException("Error Connecting: The connection is already Open.");
      switch (_Settings.Recycling.Method) {
        case SmppSettings.RecyclingMethod.Pooling:
          _InternalConnection = SmppPoolManager.GetConnection(_Settings);
          break;

        case SmppSettings.RecyclingMethod.Sharing:
          _InternalConnection = SmppShareManager.GetConnection(_Settings);
          break;

        default:
          _InternalConnection = new SmppInternalConnection(_Settings);
          break;
      }
      SetEvents();
      SmppBindRes res = null;
      if ((_InternalConnection.Connected && (_InternalConnection.LastBindRes != null)) &&
          ((_InternalConnection.LastBindRes.Header != null) &&
           (_InternalConnection.LastBindRes.Header.CommandStatus == 0)))
        return _InternalConnection.LastBindRes;
      res = _InternalConnection.Bind();
      _InternalConnection.LastBindRes = res;
      return res;
    }

    public SmppCancelSmRes CancelSm(SmppCancelSmReq RequestPdu) {
      return _InternalConnection.CancelSm(RequestPdu);
    }

    public void DeliverSmRes(SmppDeliverSmRes DeliverSmResObj) {
      _InternalConnection.DeliverSmRes(DeliverSmResObj);
    }

    public SmppEnquireLinkRes EnquireLink(SmppEnquireLinkReq RequestPdu) {
      return _InternalConnection.EnquireLink(RequestPdu);
    }

    public SmppQuerySmRes QuerySm(SmppQuerySmReq RequestPdu) {
      return _InternalConnection.QuerySm(RequestPdu);
    }

    void RaiseDeliverSmReq(SmppDeliverSmEventArgs e) {
      if (OnDeliverSmReq != null)
        OnDeliverSmReq(this, e);
    }

    void RaiseEnquireLinkReq(SmppEnquireLinkEventArgs e) {
      if (OnEnquireLinkReq != null)
        OnEnquireLinkReq(this, e);
    }

    void RaiseUnBindReq(SmppUnBindEventArgs e) {
      if (OnUnBindReq != null)
        OnUnBindReq(this, e);
    }

    void SetEvents() {
      if (_InternalConnection != null) {
        _InternalConnection.OnDeliverSmReq += _InternalConnection_OnDeliverSmReq;
        _InternalConnection.OnEnquireLinkReq += _InternalConnection_OnEnquireLinkReq;
        _InternalConnection.OnUnBindReq += _InternalConnection_OnUnBindReq;
      }
    }

    public SmppSubmitSmRes SubmitSm(SmppSubmitSmReq RequestPdu) {
      return _InternalConnection.SubmitSm(RequestPdu);
    }

    public void UnBind() {
      try {
        switch (_Settings.Recycling.Method) {
          case SmppSettings.RecyclingMethod.Pooling:
            SmppPoolManager.ReleaseConnection(_InternalConnection);
            break;

          case SmppSettings.RecyclingMethod.None:
            _InternalConnection.BeginUnBind();
            goto Label_0034;
        }
        Label_0034:
        UnSetEvents();
      } catch (Exception exception) {
        log.Error("Devshock", exception);
      }
    }

    void UnSetEvents() {
      if (_InternalConnection != null) {
        _InternalConnection.OnDeliverSmReq -= _InternalConnection_OnDeliverSmReq;
        _InternalConnection.OnEnquireLinkReq -= _InternalConnection_OnEnquireLinkReq;
      }
    }
  }
}