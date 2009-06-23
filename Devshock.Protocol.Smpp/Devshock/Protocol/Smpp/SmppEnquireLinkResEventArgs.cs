using System;
using Devshock.Protocol.SmppPdu;

namespace Devshock.Protocol.Smpp {
  [CLSCompliant(true)]
  public class SmppEnquireLinkResEventArgs : EventArgs {
    readonly Guid _ConnGuid;
    readonly SmppEnquireLinkRes _Pdu;

    public SmppEnquireLinkResEventArgs(Guid ConnGuid, SmppEnquireLinkRes Pdu) {
      _Pdu = Pdu;
      _ConnGuid = ConnGuid;
    }

    public Guid ConnGuid {
      get { return _ConnGuid; }
    }

    public SmppEnquireLinkRes Pdu {
      get { return _Pdu; }
    }
  }
}