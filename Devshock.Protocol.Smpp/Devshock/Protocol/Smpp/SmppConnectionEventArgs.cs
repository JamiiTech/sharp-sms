using System;

namespace Devshock.Protocol.Smpp {
  [CLSCompliant(true)]
  public class SmppConnectionEventArgs : EventArgs {
    readonly Guid _ConnGuid;

    public SmppConnectionEventArgs(Guid ConnGuid) {
      _ConnGuid = ConnGuid;
    }

    public Guid ConnGuid {
      get { return _ConnGuid; }
    }
  }
}