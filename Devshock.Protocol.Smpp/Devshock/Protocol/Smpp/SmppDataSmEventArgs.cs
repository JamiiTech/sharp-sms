using System;
using Devshock.Protocol.SmppPdu;

namespace Devshock.Protocol.Smpp {
  [CLSCompliant(true)]
  public class SmppDataSmEventArgs : EventArgs {
    public readonly SmppDataSmReq Pdu;

    public SmppDataSmEventArgs(SmppDataSmReq Pdu) {
      this.Pdu = Pdu;
    }
  }
}