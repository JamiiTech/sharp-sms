using System;
using Devshock.Protocol.SmppPdu;

namespace Devshock.Protocol.Smpp {
  [CLSCompliant(true)]
  public class SmppUnknownPduEventArgs : EventArgs {
    public readonly byte[] BodyByteArray;
    public readonly SmppHeader Header;

    public SmppUnknownPduEventArgs(SmppHeader Header, byte[] BodyByteArray) {
      this.BodyByteArray = BodyByteArray;
      this.Header = Header;
    }
  }
}