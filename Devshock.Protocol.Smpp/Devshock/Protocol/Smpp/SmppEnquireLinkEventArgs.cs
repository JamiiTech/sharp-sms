using System;
using Devshock.Protocol.SmppPdu;

namespace Devshock.Protocol.Smpp {
  [CLSCompliant(true)]
  public class SmppEnquireLinkEventArgs : EventArgs {
    readonly Guid _ConnGuid;
    readonly SmppEnquireLinkReq _Pdu;

    public SmppEnquireLinkEventArgs(Guid ConnGuid, SmppEnquireLinkReq Pdu) {
      _Pdu = Pdu;
      _ConnGuid = ConnGuid;
    }

    public Guid ConnGuid {
      get { return _ConnGuid; }
    }

    public SmppEnquireLinkReq Pdu {
      get { return _Pdu; }
    }
  }
}