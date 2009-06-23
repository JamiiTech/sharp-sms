using System;
using System.Collections;
using System.Threading;
using System.Timers;
using Timer=System.Timers.Timer;

namespace Devshock.Protocol.Smpp {
  sealed class SmppPool {
    readonly ArrayList idlePool;
    readonly ArrayList inUsePool;
    readonly Timer tmCheckIdleConn = new Timer(60000.0);
    SmppSettings _Settings;

    public SmppPool(SmppSettings Settings) {
      _Settings = Settings;
      inUsePool = new ArrayList();
      idlePool = new ArrayList(Settings.Recycling.MinSize);
      lock (idlePool.SyncRoot) {
        lock (inUsePool.SyncRoot) {
          for (int i = 0; i < Settings.Recycling.MinSize; i++) {
            var connection = new SmppInternalConnection(Settings);
            idlePool.Add(connection);
          }
        }
      }
      tmCheckIdleConn.AutoReset = false;
      tmCheckIdleConn.Enabled = true;
      tmCheckIdleConn.Elapsed += tmCheckIdleConn_Elapsed;
    }

    public SmppSettings Settings {
      get { return _Settings; }
      set { _Settings = value; }
    }

    void CheckOutConnection(SmppInternalConnection conn) {}

    public SmppInternalConnection GetConnection() {
      SmppInternalConnection pooledConnection = null;
      int tickCount = Environment.TickCount;
      int timeout = _Settings.Timeout;
      while ((pooledConnection == null) && ((Environment.TickCount - tickCount) < timeout)) {
        pooledConnection = GetPooledConnection();
        if (pooledConnection == null)
          Thread.Sleep(new TimeSpan(0x2710L));
      }
      if (pooledConnection == null)
        throw new Exception(
          "error connecting: Timeout expired.  The timeout period elapsed prior to obtaining a connection from the pool.  This may have occurred because all pooled connections were in use and max pool size was reached.");
      return pooledConnection;
    }

    SmppInternalConnection GetPooledConnection() {
      SmppInternalConnection connection = null;
      lock (idlePool.SyncRoot) {
        lock (inUsePool.SyncRoot) {
          if ((idlePool.Count == 0) && (inUsePool.Count == _Settings.Recycling.MaxSize))
            return null;
          if (idlePool.Count > 0) {
            int index = idlePool.Count - 1;
            while (index >= 0) {
              connection = idlePool[index] as SmppInternalConnection;
              inUsePool.Add(connection);
              idlePool.RemoveAt(index);
              return connection;
            }
            return connection;
          }
          connection = new SmppInternalConnection(_Settings);
          inUsePool.Add(connection);
          return connection;
        }
      }
      return connection;
    }

    public void ReleaseConnection(SmppInternalConnection connection) {
      if (connection != null)
        lock (idlePool.SyncRoot) {
          lock (inUsePool.SyncRoot) {
            connection.ReleasedDateTime = DateTime.Now;
            inUsePool.Remove(connection);
            idlePool.Add(connection);
          }
        }
    }

    void tmCheckIdleConn_Elapsed(object sender, ElapsedEventArgs e) {
      try {
        lock (idlePool.SyncRoot) {
          for (int i = 0; i < idlePool.Count; i++) {
            var connection = (SmppInternalConnection) idlePool[i];
            if (connection.IsTooOld()) {
              idlePool.RemoveAt(i);
              connection.BeginUnBind();
            }
          }
        }
      } catch {}
      tmCheckIdleConn.Enabled = true;
    }

    public void UnBindConnections() {
      int count = inUsePool.Count;
      int num2 = idlePool.Count;
      lock (idlePool.SyncRoot) {
        for (int i = 0; i < idlePool.Count; i++) {
          var connection = (SmppInternalConnection) idlePool[i];
          idlePool.RemoveAt(i);
          try {
            connection.BeginUnBind();
          } catch {}
        }
      }
    }
  }
}