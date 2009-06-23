using System;

namespace Devshock.Protocol.Smpp {
  [CLSCompliant(true)]
  public class SmppServerSettings {
    public SmppServerSettings() {
      Capacity = 300;
      LocalHost = string.Empty;
      PacketSize = 0x800;
      ConnectionBacklog = 80;
      MaxPduLength = 0x1000;
    }

    public SmppServerSettings(int LocalPort) {
      Capacity = 300;
      LocalHost = string.Empty;
      PacketSize = 0x800;
      ConnectionBacklog = 80;
      MaxPduLength = 0x1000;
      this.LocalPort = LocalPort;
    }

    public int Capacity { get; set; }

    public int ConnectionBacklog { get; set; }

    public string LocalHost { get; set; }

    public int LocalPort { get; set; }

    public int MaxPduLength { get; set; }

    public int PacketSize { get; set; }
  }
}