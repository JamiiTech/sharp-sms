using System;
using Devshock.Protocol.SmppPdu;

namespace Devshock.Protocol.Smpp {
  [CLSCompliant(true)]
  public class SmppDeliverSmEventArgs : EventArgs {
    readonly SmppDeliverSmReq _Pdu;

    public SmppDeliverSmEventArgs(SmppDeliverSmReq Pdu) {
      _Pdu = Pdu;
    }

    public SmppDeliverSmReq Pdu {
      get { return _Pdu; }
    }
  }
}