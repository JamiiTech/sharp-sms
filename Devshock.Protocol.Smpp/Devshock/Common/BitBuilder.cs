using System;

namespace Devshock.Common {
  [CLSCompliant(true)]
  public class BitBuilder {
    bool[] _Bits;

    public BitBuilder() {
      _Bits = new bool[8];
    }

    public BitBuilder(byte value) {
      _Bits = new bool[8];
      Value = value;
    }

    public bool Bit0 {
      get { return _Bits[0]; }
      set { _Bits[0] = value; }
    }

    public bool Bit1 {
      get { return _Bits[1]; }
      set { _Bits[1] = value; }
    }

    public bool Bit2 {
      get { return _Bits[2]; }
      set { _Bits[2] = value; }
    }

    public bool Bit3 {
      get { return _Bits[3]; }
      set { _Bits[3] = value; }
    }

    public bool Bit4 {
      get { return _Bits[4]; }
      set { _Bits[4] = value; }
    }

    public bool Bit5 {
      get { return _Bits[5]; }
      set { _Bits[5] = value; }
    }

    public bool Bit6 {
      get { return _Bits[6]; }
      set { _Bits[6] = value; }
    }

    public bool Bit7 {
      get { return _Bits[7]; }
      set { _Bits[7] = value; }
    }

    public bool[] Bits {
      get { return _Bits; }
      set { _Bits = value; }
    }

    public byte Value {
      get {
        byte num = 0;
        for (byte i = 0; i < 8; i = (byte) (i + 1))
          num = (byte) (num + Convert.ToByte((BooleanToByte(_Bits[i])*Math.Pow(2.0, i))));
        return num;
      }
      set {
        byte num = 1;
        for (byte i = 0; i < 8; i = (byte) (i + 1)) {
          _Bits[i] = (value & num) > 0;
          num = (byte) (num*2);
        }
      }
    }

    internal static byte BooleanToByte(bool value) {
      if (value)
        return 1;
      return 0;
    }
  }
}