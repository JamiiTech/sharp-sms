using System;
using System.Net;

namespace Devshock.Protocol.Smpp {
  [CLSCompliant(true)]
  public class SmppConnProperties {
    readonly IPAddress _RemoteHost;

    public SmppConnProperties(IPAddress RemoteHost) {
      _RemoteHost = RemoteHost;
    }

    public IPAddress RemoteHost {
      get { return _RemoteHost; }
    }
  }
}