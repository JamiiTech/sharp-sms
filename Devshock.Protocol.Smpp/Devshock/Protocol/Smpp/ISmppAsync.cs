using System;

namespace Devshock.Protocol.Smpp {
  interface ISmppAsync {
    object _Request { get; set; }

    object _Response { get; set; }

    Exception ProcessException { get; set; }

    object State { get; set; }
  }
}