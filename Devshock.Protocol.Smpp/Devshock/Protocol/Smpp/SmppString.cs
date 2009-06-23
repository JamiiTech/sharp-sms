using System;
using System.Text;

namespace Devshock.Protocol.Smpp {
  [CLSCompliant(true)]
  public class SmppString {
    SmppDataCoding _DataCoding;
    byte[] _Value;
    string strValue;

    public SmppDataCoding DataCoding {
      get { return _DataCoding; }
      set { _DataCoding = value; }
    }

    public int Length {
      get {
        ApplyValue();
        return _Value.Length;
      }
    }

    public byte[] Value {
      get {
        ApplyValue();
        return _Value;
      }
      set { _Value = value; }
    }

    void ApplyValue() {
      if (_Value == null)
        _Value = Encoding.GetEncoding(DataCoding.CodePage).GetBytes(strValue);
    }

    public override bool Equals(object obj) {
      return _Value.Equals(obj);
    }

    public override int GetHashCode() {
      return _Value.GetHashCode();
    }

    public void SetValue(string value) {
      strValue = value;
    }

    public void SetValue(byte[] value) {
      _Value = value;
    }

    public void SetValue(string value, SmppDataCoding DataCoding) {
      _DataCoding = DataCoding;
      SetValue(value);
    }

    public void SetValue(byte[] value, SmppDataCoding DataCoding) {
      _DataCoding = DataCoding;
      _Value = value;
    }

    public override string ToString() {
      return Encoding.GetEncoding(DataCoding.CodePage).GetString(_Value);
    }
  }
}