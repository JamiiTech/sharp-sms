using System;
using System.Runtime.InteropServices;

namespace Devshock.Protocol.Smpp {
  [StructLayout(LayoutKind.Sequential), CLSCompliant(true)]
  public struct SmppServiceType {
    string _Value;

    public string Value {
      get { return _Value; }
    }

    public static SmppServiceType FromValue(string value) {
      var type = new SmppServiceType();
      type._Value = value;
      return type;
    }

    public static SmppServiceType Default {
      get { return FromValue(""); }
    }

    public static SmppServiceType CellularMessaging {
      get { return FromValue("CMT"); }
    }

    public static SmppServiceType CellularPaging {
      get { return FromValue("CPT"); }
    }

    public static SmppServiceType VoiceMailNotification {
      get { return FromValue("VMN"); }
    }

    public static SmppServiceType VoiceMailAlerting {
      get { return FromValue("VMA"); }
    }

    public static SmppServiceType WirelessApplicationProtocol {
      get { return FromValue("WAP"); }
    }

    public static SmppServiceType UnstructuredSupServicesData {
      get { return FromValue("USSD"); }
    }

    public static SmppServiceType CellBroadcastService {
      get { return FromValue("CBS"); }
    }

    public static SmppServiceType GenericUDPTransportService {
      get { return FromValue("GUTS"); }
    }
  }
}