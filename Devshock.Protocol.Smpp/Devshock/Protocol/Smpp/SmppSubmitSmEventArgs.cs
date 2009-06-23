using System;
using Devshock.Protocol.SmppPdu;

namespace Devshock.Protocol.Smpp {
  [CLSCompliant(true)]
  public class SmppSubmitSmEventArgs : EventArgs {
    readonly Guid _ConnGuid;
    readonly SmppSubmitSmReq _Pdu;

    public SmppSubmitSmEventArgs(Guid ConnGuid, SmppSubmitSmReq Pdu) {
      _ConnGuid = ConnGuid;
      _Pdu = Pdu;
    }

    public Guid ConnGuid {
      get { return _ConnGuid; }
    }

    public SmppSubmitSmReq Pdu {
      get { return _Pdu; }
    }
  }
}