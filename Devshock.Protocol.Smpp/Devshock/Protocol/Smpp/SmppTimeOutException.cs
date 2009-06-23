using System;

namespace Devshock.Protocol.Smpp {
  [CLSCompliant(true)]
  public class SmppTimeOutException : Exception {
    public SmppTimeOutException() {}

    public SmppTimeOutException(string Message) : base(Message) {}
  }
}