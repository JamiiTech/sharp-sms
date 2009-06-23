using System;

namespace Devshock.Protocol.Smpp {
  [CLSCompliant(true)]
  public class SmppCallbacks1 {
    SmppDeliverSmHandler _DeliverSmCallback;
    SmppEnquireLinkHandler _EnquireLinkCallback;
    SmppUnBindHandler _UnBindCallback;

    public SmppDeliverSmHandler DeliverSmCallback {
      get { return _DeliverSmCallback; }
      set { _DeliverSmCallback = value; }
    }

    public SmppEnquireLinkHandler EnquireLinkCallback {
      get { return _EnquireLinkCallback; }
      set { _EnquireLinkCallback = value; }
    }

    public SmppEnquireLinkResHandler EnquireLinkResCallback { get; set; }

    public SmppUnBindHandler UnBindCallback {
      get { return _UnBindCallback; }
      set { _UnBindCallback = value; }
    }

    internal void RemoveCallbacks() {
      _DeliverSmCallback = null;
      _EnquireLinkCallback = null;
      _UnBindCallback = null;
      _EnquireLinkCallback = null;
    }
  }
}