using System;
using Devshock.Protocol.SmppPdu;

namespace Devshock.Protocol.Smpp {
  [CLSCompliant(true)]
  public class SmppGenericNackEventArgs : EventArgs {
    readonly SmppGenericNackReq _Pdu;

    public SmppGenericNackEventArgs(SmppGenericNackReq Pdu) {
      _Pdu = Pdu;
    }

    public SmppGenericNackReq Pdu {
      get { return _Pdu; }
    }
  }
}