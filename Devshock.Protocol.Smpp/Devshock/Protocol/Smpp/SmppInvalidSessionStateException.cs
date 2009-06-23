using System;

namespace Devshock.Protocol.Smpp {
  [CLSCompliant(true)]
  public class SmppInvalidSessionStateException : Exception {
    public SmppInvalidSessionStateException() {}

    public SmppInvalidSessionStateException(string Message) : base(Message) {}
  }
}