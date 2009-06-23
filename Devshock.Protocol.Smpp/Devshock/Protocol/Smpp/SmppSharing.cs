namespace Devshock.Protocol.Smpp {
  class SmppSharing {
    internal SmppSettings Settings;
    SmppInternalConnection[] SharedConns;

    internal SmppSharing(SmppSettings Settings) {
      this.Settings = Settings;
    }

    internal SmppInternalConnection GetConnection() {
      if (SharedConns.Length == 0)
        throw new SmppException("There are not SmppConnections available");
      int index = 0;
      lock (SharedConns.SyncRoot) {
        for (int i = 1; i < SharedConns.Length; i++)
          if ((SharedConns[i].PendingResponse.Count + SharedConns[i].PendingQueue.Count) <
              (SharedConns[index].PendingResponse.Count + SharedConns[index].PendingQueue.Count))
            index = i;
        if (!SharedConns[index].Connected)
          SharedConns[index].LastBindRes = SharedConns[index].Bind();
      }
      return SharedConns[index];
    }

    internal SmppInternalConnection[] GetConnections() {
      return SharedConns;
    }

    public void UnBindConnections() {
      lock (SharedConns.SyncRoot) {
        for (int i = 0; i < SharedConns.Length; i++)
          try {
            SharedConns[i].BeginUnBind();
          } catch {}
      }
    }
  }
}