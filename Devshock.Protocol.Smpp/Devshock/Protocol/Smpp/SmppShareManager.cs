using System;
using System.Collections;

namespace Devshock.Protocol.Smpp {
  [CLSCompliant(true)]
  public sealed class SmppShareManager {
    static bool Deactivated;
    static Hashtable Pools;

    internal SmppShareManager() {}

    public static void Deactivate() {
      if (Pools != null) {
        Deactivated = true;
        var array = new string[Pools.Count];
        Pools.Keys.CopyTo(array, 0);
        foreach (string str in array)
          ((SmppShare) Pools[str]).UnBindConnections();
        Deactivated = false;
      }
    }

    internal static SmppInternalConnection GetConnection(SmppSettings Settings) {
      SmppShare share;
      if (Deactivated)
        throw new SmppException("The SmppPoolManager is in process of deactivation");
      if (Pools == null)
        Initialize();
      string connectionString = Settings.ConnectionString;
      lock (Pools.SyncRoot) {
        if (!Pools.Contains(connectionString)) {
          share = new SmppShare(Settings);
          Pools.Add(connectionString, share);
        } else {
          share = Pools[connectionString] as SmppShare;
          share.Settings = Settings;
        }
      }
      return share.GetConnection();
    }

    internal static SmppInternalConnection[] GetConnections(SmppSettings Settings) {
      var share = Pools[Settings.ConnectionString] as SmppShare;
      return share.GetConnections();
    }

    static void Initialize() {
      Pools = new Hashtable();
    }

    public static void Reactivate() {
      Deactivated = false;
    }
  }
}