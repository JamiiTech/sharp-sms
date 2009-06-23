using System;
using Devshock.Protocol.SmppPdu;

namespace Devshock.Protocol.Smpp {
  [CLSCompliant(true)]
  public class SmppBindEventArgs : EventArgs {
    readonly Guid _ConnGuid;
    readonly SmppBindReq _Pdu;

    public SmppBindEventArgs(Guid ConnGuid, SmppBindReq Pdu) {
      _Pdu = Pdu;
      _ConnGuid = ConnGuid;
    }

    public Guid ConnGuid {
      get { return _ConnGuid; }
    }

    public SmppBindReq Pdu {
      get { return _Pdu; }
    }
  }
}