using System;

namespace Devshock.Protocol.Smpp {
  [CLSCompliant(true)]
  public class SmppInvalidConnectionStateException : Exception {
    public SmppInvalidConnectionStateException() {}

    public SmppInvalidConnectionStateException(string Message) : base(Message) {}
  }
}