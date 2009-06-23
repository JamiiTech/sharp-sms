using System;
using System.Collections;

namespace Devshock.Protocol.Smpp {
  [CLSCompliant(true)]
  public sealed class SmppPoolManager {
    static bool Deactivated;
    static Hashtable Pools;

    internal SmppPoolManager() {}

    public static void Deactivate() {
      if (Pools != null) {
        Deactivated = true;
        var array = new string[Pools.Count];
        Pools.Keys.CopyTo(array, 0);
        foreach (string str in array)
          ((SmppPool) Pools[str]).UnBindConnections();
        Deactivated = false;
      }
    }

    internal static SmppInternalConnection GetConnection(SmppSettings Settings) {
      SmppPool pool;
      if (Deactivated)
        throw new SmppException("The SmppPoolManager is in process of deactivation");
      if (Pools == null)
        Initialize();
      string connectionString = Settings.ConnectionString;
      lock (Pools.SyncRoot) {
        if (!Pools.Contains(connectionString)) {
          pool = new SmppPool(Settings);
          Pools.Add(connectionString, pool);
        } else {
          pool = Pools[connectionString] as SmppPool;
          pool.Settings = Settings;
        }
      }
      return pool.GetConnection();
    }

    static void Initialize() {
      Pools = new Hashtable();
    }

    public static void Reactivate() {
      Deactivated = false;
    }

    internal static void ReleaseConnection(SmppInternalConnection connection) {
      SmppPool pool = null;
      lock (Pools.SyncRoot) {
        string connectionString = connection.Settings.ConnectionString;
        pool = (SmppPool) Pools[connectionString];
        if (pool == null)
          throw new SmppException("Pooling exception: Unable to find original pool for connection");
      }
      pool.ReleaseConnection(connection);
    }
  }
}