using System;
using System.Text;

namespace Devshock.Net {
  [CLSCompliant(true)]
  public class SocketSettings {
    internal Guid _InstanceGuid;

    public SocketSettings() {
      Capacity = 100;
      BufferSize = 0x800;
      PacketSize = 0x400;
      LocalHost = string.Empty;
      RemoteHost = string.Empty;
      _InstanceGuid = Guid.NewGuid();
      ConnectionBacklog = 80;
      DataCoding = Encoding.Default;
    }

    public SocketSettings(int LocalPort, int Capacity, byte[] PacketDelimiter, string LocalHost) {
      this.Capacity = 100;
      BufferSize = 0x800;
      PacketSize = 0x400;
      this.LocalHost = string.Empty;
      RemoteHost = string.Empty;
      _InstanceGuid = Guid.NewGuid();
      ConnectionBacklog = 80;
      DataCoding = Encoding.Default;
      this.LocalHost = LocalHost;
      this.LocalPort = LocalPort;
      this.Capacity = Capacity;
      if ((PacketDelimiter != null) && (PacketDelimiter.Length > 0))
        this.PacketDelimiter = PacketDelimiter;
    }

    public int BufferSize { get; set; }

    public int Capacity { get; set; }

    public int ConnectionBacklog { get; set; }

    public Encoding DataCoding { get; set; }

    public Guid InstaceGuid {
      get { return _InstanceGuid; }
      set { _InstanceGuid = value; }
    }

    public string LocalHost { get; set; }

    public int LocalPort { get; set; }

    public byte[] PacketDelimiter { get; set; }

    public int PacketSize { get; set; }

    public string RemoteHost { get; set; }

    public int RemotePort { get; set; }
  }
}