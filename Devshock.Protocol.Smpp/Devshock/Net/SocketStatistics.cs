using System.Runtime.InteropServices;

namespace Devshock.Net {
  [StructLayout(LayoutKind.Sequential)]
  struct SocketStatistics {
    internal int _BytesReceived;
    internal int _BytesSent;

    internal int BytesReceived {
      get { return _BytesReceived; }
    }

    internal int BytesSent {
      get { return _BytesSent; }
    }
  }
}