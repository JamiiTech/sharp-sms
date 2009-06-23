using System;
using System.Text;
using Devshock.Protocol.SmppPdu;

namespace Devshock.Protocol.Smpp {
  [CLSCompliant(true)]
  public class SmppSettings {
    #region RecyclingMethod enum

    public enum RecyclingMethod {
      None = 9,
      Pooling = 0,
      Sharing = 1
    }

    #endregion

    readonly int _MaxPduSize;
    readonly int _SocketBufferSize;

    SmppBindReq.BodyPdu _BindParams;
    int _ConnectionLifetime;
    SmppConnectionMode _ConnectionMode;
    bool _DisconnectOnTimeout;
    string _LocalHost;
    int _LocalPort;
    RecyclingSettings _Recycling;
    string _RemoteHost;
    int _RemotePort;
    int _Timeout;

    public SmppSettings() {
      _RemoteHost = string.Empty;
      _LocalHost = string.Empty;
      _BindParams = new SmppBindReq.BodyPdu();
      _ConnectionMode = SmppConnectionMode.Transmitter;
      _Timeout = 0xea60;
      _ConnectionLifetime = 120;
      _SocketBufferSize = 0x800;
      _DisconnectOnTimeout = true;
      _MaxPduSize = 0x19000;
      _Recycling = new RecyclingSettings();
      WindowSize = 7;
    }

    public SmppSettings(string ConnectionString) {
      _RemoteHost = string.Empty;
      _LocalHost = string.Empty;
      _BindParams = new SmppBindReq.BodyPdu();
      _ConnectionMode = SmppConnectionMode.Transmitter;
      _Timeout = 0xea60;
      _ConnectionLifetime = 120;
      _SocketBufferSize = 0x800;
      _DisconnectOnTimeout = true;
      _MaxPduSize = 0x19000;
      _Recycling = new RecyclingSettings();
      WindowSize = 7;
      foreach (string str in ConnectionString.Split(",".ToCharArray())) {
        string[] strArray2 = str.Split("=".ToCharArray());
        if (strArray2.Length == 2) {
          strArray2[0] = strArray2[0].ToLower();
          switch (strArray2[0]) {
            case "address npi":
              _BindParams.AddressNpi = Convert.ToByte(strArray2[1]);
              break;

            case "address ton":
              _BindParams.AddressTon = Convert.ToByte(strArray2[1]);
              break;

            case "password":
              _BindParams.Password = strArray2[1];
              break;

            case "system id":
              _BindParams.SystemId = strArray2[1];
              break;

            case "system type":
              _BindParams.SystemType = strArray2[1];
              break;

            case "remote host":
              _RemoteHost = strArray2[1];
              break;

            case "remote port":
              _RemotePort = Convert.ToInt32(strArray2[1]);
              break;

            case "local host":
              _LocalHost = strArray2[1];
              break;

            case "local port":
              _LocalPort = Convert.ToInt32(strArray2[1]);
              break;

            case "connection mode":
              _ConnectionMode = SmppConnectionMode.FromValue(Convert.ToByte(strArray2[1]));
              break;

            case "connection timeout":
              _Timeout = Convert.ToInt32(strArray2[1]);
              break;

            case "connection lifetime":
              _ConnectionLifetime = Convert.ToInt32(strArray2[1]);
              break;

            case "disconnect on timeout":
              _DisconnectOnTimeout = Convert.ToBoolean(strArray2[1]);
              break;

            case "max pdu size":
              _MaxPduSize = Convert.ToInt32(strArray2[1]);
              break;
          }
        }
      }
    }

    public SmppBindReq.BodyPdu BindParams {
      get { return _BindParams; }
      set { _BindParams = value; }
    }

    public SmppConnectionMode ConnectionMode {
      get { return _ConnectionMode; }
      set { _ConnectionMode = value; }
    }

    public string ConnectionString {
      get {
        var builder = new StringBuilder(30, 800);
        builder.Append("remote host=" + _RemoteHost + ",");
        builder.Append("remote port=" + _RemotePort + ",");
        builder.Append("local host=" + _LocalHost + ",");
        builder.Append("local port=" + _LocalPort + ",");
        builder.Append("bindparams.system id=" + _BindParams.SystemId + ",");
        builder.Append("bindparams.password=" + _BindParams.Password + ",");
        builder.Append("bindparams.system type=" + _BindParams.SystemType + ",");
        builder.Append("bindparams.interface version=" + _BindParams.InterfaceVersion + ",");
        builder.Append("bindparams.address ton=" + _BindParams.AddressTon + ",");
        builder.Append("bindparams.address npi=" + _BindParams.AddressNpi + ",");
        builder.Append("bindparams.address range=" + _BindParams.AddressRange + ",");
        builder.Append("connection mode=" + _ConnectionMode.Value + ",");
        builder.Append("timeout=" + _Timeout + ",");
        builder.Append("disconnect on timeout=" + _DisconnectOnTimeout.ToString().ToLower() + ",");
        builder.Append("recycling.method=" + _Recycling.Method + ",");
        builder.Append("recycling.max size=" + _Recycling.MaxSize + ",");
        builder.Append("recycling.min size=" + _Recycling.MinSize + ",");
        builder.Append("recycling.idle timeout=" + _Recycling.IdleTimeout + ",");
        return builder.ToString();
      }
    }

    public bool DisconnectOnTimeout {
      get { return _DisconnectOnTimeout; }
      set { _DisconnectOnTimeout = value; }
    }

    public string LocalHost {
      get { return _LocalHost; }
      set { _LocalHost = value; }
    }

    public int LocalPort {
      get { return _LocalPort; }
      set { _LocalPort = value; }
    }

    internal int MaxPduSize {
      get { return _MaxPduSize; }
    }

    public RecyclingSettings Recycling {
      get { return _Recycling; }
      set { _Recycling = value; }
    }

    public string RemoteHost {
      get { return _RemoteHost; }
      set { _RemoteHost = value; }
    }

    public int RemotePort {
      get { return _RemotePort; }
      set { _RemotePort = value; }
    }

    internal int SocketBufferSize {
      get { return _SocketBufferSize; }
    }

    public int Timeout {
      get { return _Timeout; }
      set { _Timeout = value; }
    }

    public int WindowSize { get; set; }

    #region Nested type: RecyclingSettings

    [CLSCompliant(true)]
    public class RecyclingSettings {
      int _IdleTimeout = 0xea60;
      int _MaxSize = 3;
      RecyclingMethod _Method = RecyclingMethod.None;

      public int IdleTimeout {
        get { return _IdleTimeout; }
        set { _IdleTimeout = value; }
      }

      public int MaxSize {
        get { return _MaxSize; }
        set { _MaxSize = value; }
      }

      public RecyclingMethod Method {
        get { return _Method; }
        set { _Method = value; }
      }

      public int MinSize { get; set; }
    }

    #endregion
  }
}