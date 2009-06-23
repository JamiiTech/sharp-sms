using System;
using Devshock.Protocol.SmppPdu;

namespace Devshock.Protocol.Smpp {
  [CLSCompliant(true)]
  public class SmppSubmitSmResEventArgs : EventArgs {
    public SmppSubmitSmResEventArgs(SmppSubmitSmRes Pdu) {
      this.Pdu = Pdu;
    }

    public SmppSubmitSmRes Pdu { get; set; }
  }
}