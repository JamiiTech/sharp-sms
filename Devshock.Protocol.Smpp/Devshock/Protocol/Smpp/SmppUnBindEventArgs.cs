using System;
using Devshock.Protocol.SmppPdu;

namespace Devshock.Protocol.Smpp {
  [CLSCompliant(true)]
  public class SmppUnBindEventArgs : EventArgs {
    readonly Guid _ConnGuid;
    readonly SmppUnBindReq _Pdu;

    public SmppUnBindEventArgs(Guid ConnGuid, SmppUnBindReq Pdu) {
      _Pdu = Pdu;
      _ConnGuid = ConnGuid;
    }

    public Guid ConnGuid {
      get { return _ConnGuid; }
    }

    public SmppUnBindReq Pdu {
      get { return _Pdu; }
    }
  }
}