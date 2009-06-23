using System;
using System.Collections.Specialized;
using System.Runtime.InteropServices;
using System.Text;

namespace Devshock.Protocol.Smpp {
  [StructLayout(LayoutKind.Sequential), CLSCompliant(true)]
  public struct SmppDataCoding {
    static readonly ListDictionary _DataCodingMap;
    static Encoding _BaseEncoding;
    byte _Value;

    public static Encoding BaseEncoding {
      get { return _BaseEncoding; }
      set { _BaseEncoding = value; }
    }

    static SmppDataCoding() {
      _DataCodingMap = new ListDictionary {
                                            {-1, 0x4e4},
                                            {0, 0x4e4},
                                            {1, 0x6faf},
                                            {3, 0x4e4},
                                            {5, 0xc42c},
                                            {6, 0x6fb3},
                                            {7, 0x4e7},
                                            {8, 0x4b1},
                                            {14, 0x3b5}
                                          };
      _BaseEncoding = Encoding.GetEncoding(0x4e4);
    }

    static void Add(int DataCoding, int CodePage) {
      if (_DataCodingMap.Contains(DataCoding))
        _DataCodingMap[DataCoding] = CodePage;
      else
        _DataCodingMap.Add(DataCoding, CodePage);
    }

    static void Remove(int DataCoding) {
      _DataCodingMap.Remove(DataCoding);
    }

    public byte Value {
      get { return _Value; }
      set { _Value = value; }
    }

    public int CodePage {
      get {
        if (_DataCodingMap.Contains(_Value))
          return Convert.ToInt32(_DataCodingMap[_Value]);
        return Convert.ToInt32(_DataCodingMap[-1]);
      }
    }

    public static SmppDataCoding FromValue(byte value) {
      var coding = new SmppDataCoding {_Value = value};
      return coding;
    }

    public static SmppDataCoding Default {
      get { return FromValue(0); }
    }

    public static SmppDataCoding WesternEuropean {
      get { return FromValue(1); }
    }

    public static SmppDataCoding Latin {
      get { return FromValue(3); }
    }

    public static SmppDataCoding Japanese {
      get { return FromValue(5); }
    }

    public static SmppDataCoding Cyrillic {
      get { return FromValue(6); }
    }

    public static SmppDataCoding Hebrew {
      get { return FromValue(7); }
    }

    public static SmppDataCoding Unicode {
      get { return FromValue(8); }
    }

    public static SmppDataCoding Korean {
      get { return FromValue(14); }
    }
  }
}