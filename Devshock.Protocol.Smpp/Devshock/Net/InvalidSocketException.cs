using System;

namespace Devshock.Net {
  class InvalidSocketException : Exception {
    internal InvalidSocketException() {}

    internal InvalidSocketException(string Message) : base(Message) {}
  }
}