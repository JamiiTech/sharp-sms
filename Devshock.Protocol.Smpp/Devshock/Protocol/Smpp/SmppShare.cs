using System.Collections;
using System.Timers;

namespace Devshock.Protocol.Smpp {
  sealed class SmppShare {
    readonly SmppInternalConnection[] availablePool;
    readonly Timer tmCheckIdleConn = new Timer(60000.0);
    internal ArrayList PendingBind = new ArrayList(10);

    public SmppShare(SmppSettings Settings) {
      this.Settings = Settings;
      availablePool = new SmppInternalConnection[Settings.Recycling.MaxSize];
      for (int i = 0; i < availablePool.Length; i++)
        availablePool[i] = new SmppInternalConnection(Settings);
      tmCheckIdleConn.AutoReset = false;
      tmCheckIdleConn.Enabled = true;
      tmCheckIdleConn.Elapsed += tmCheckIdleConn_Elapsed;
    }

    public SmppSettings Settings { get; set; }

    internal SmppInternalConnection GetConnection() {
      if (availablePool.Length == 0)
        throw new SmppException("There are not SmppConnections available");
      int index = 0;
      lock (availablePool.SyncRoot) {
        for (int i = 1; i < (availablePool.Length - 1); i++)
          if ((availablePool[i].PendingResponse.Count + availablePool[i].PendingQueue.Count) <
              (availablePool[index].PendingResponse.Count + availablePool[index].PendingQueue.Count))
            index = i;
      }
      return availablePool[index];
    }

    internal SmppInternalConnection[] GetConnections() {
      return availablePool;
    }

    void tmCheckIdleConn_Elapsed(object sender, ElapsedEventArgs e) {
      try {
        lock (availablePool.SyncRoot) {
          for (int i = 0; i < availablePool.Length; i++)
            if (availablePool[i].IsTooOld())
              availablePool[i].BeginUnBind();
        }
      } catch {}
      tmCheckIdleConn.Enabled = true;
    }

    public void UnBindConnections() {
      lock (availablePool.SyncRoot) {
        for (int i = 0; i < availablePool.Length; i++)
          try {
            availablePool[i].BeginUnBind();
          } catch {}
      }
    }
  }
}