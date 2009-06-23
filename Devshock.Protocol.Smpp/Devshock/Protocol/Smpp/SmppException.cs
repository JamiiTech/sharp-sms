using System;

namespace Devshock.Protocol.Smpp {
  [CLSCompliant(true)]
  public class SmppException : Exception {
    public SmppException() {}

    public SmppException(string Message) : base(Message) {}
  }
}