using System;
using Devshock.Protocol.SmppPdu;

namespace Devshock.Protocol.Smpp {
  [CLSCompliant(true)]
  public class SmppQuerySmEventArgs : EventArgs {
    readonly Guid _ConnGuid;
    readonly SmppQuerySmReq _Pdu;

    public SmppQuerySmEventArgs(SmppQuerySmReq Pdu, Guid ConnGuid) {
      _Pdu = Pdu;
      _ConnGuid = ConnGuid;
    }

    public Guid ConnGuid {
      get { return _ConnGuid; }
    }

    public SmppQuerySmReq Pdu {
      get { return _Pdu; }
    }
  }
}