using System;
using Devshock.Protocol.SmppPdu;

namespace Devshock.Protocol.Smpp {
  [CLSCompliant(true)]
  public class SmppDeliverSmResEventArgs : EventArgs {
    readonly Guid _ConnGuid;
    readonly SmppDeliverSmRes _Pdu;

    public SmppDeliverSmResEventArgs(Guid ConnGuid, SmppDeliverSmRes Pdu) {
      _Pdu = Pdu;
      _ConnGuid = ConnGuid;
    }

    public Guid ConnGuid {
      get { return _ConnGuid; }
    }

    public SmppDeliverSmRes Pdu {
      get { return _Pdu; }
    }
  }
}