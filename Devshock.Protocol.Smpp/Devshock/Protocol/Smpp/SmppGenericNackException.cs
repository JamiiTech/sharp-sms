using System;

namespace Devshock.Protocol.Smpp {
  [CLSCompliant(true)]
  public class SmppGenericNackException : Exception {
    public SmppGenericNackException() {}

    public SmppGenericNackException(string Message) : base(Message) {}
  }
}