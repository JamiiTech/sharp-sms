using System;
using System.Runtime.InteropServices;

namespace Devshock.Protocol.Smpp {
  [StructLayout(LayoutKind.Sequential), CLSCompliant(true)]
  public struct SmppConnectionMode {
    byte _Value;

    public int Value {
      get { return _Value; }
    }

    public static SmppConnectionMode FromValue(byte value) {
      var mode = new SmppConnectionMode();
      mode._Value = value;
      return mode;
    }

    public static SmppConnectionMode FromValue(string value) {
      switch (value.ToLower()) {
        case "receiver":
          return FromValue(1);

        case "transmitter":
          return FromValue(2);

        case "transceiver":
          return FromValue(9);
      }
      return FromValue(1);
    }

    public static SmppConnectionMode Receiver {
      get { return FromValue(1); }
    }

    public static SmppConnectionMode Transmitter {
      get { return FromValue(2); }
    }

    public static SmppConnectionMode Transceiver {
      get { return FromValue(9); }
    }
  }
}