using System;
using Devshock.Common;
using Devshock.Protocol.Smpp;

namespace Devshock.Protocol.SmppPdu {
  [CLSCompliant(true)]
  public class SmppDataSmReq : ISmppBasic {
    BodyPdu _Body;
    SmppHeader _Header;
    SmppTlv _Tlv;

    public SmppDataSmReq(BodyPdu Body) : this(new SmppHeader(0, 0x103, 0, 0), Body, null) {}

    public SmppDataSmReq(BodyPdu Body, SmppTlv Tlv) : this(new SmppHeader(0, 0x103, 0, 0), Body, Tlv) {}

    public SmppDataSmReq(SmppHeader Header, BodyPdu Body, SmppTlv Tlv) {
      _Header = Header;
      _Body = Body;
      _Tlv = Tlv;
    }

    public BodyPdu Body {
      get { return _Body; }
      set { _Body = value; }
    }

    public SmppHeader Header {
      get { return _Header; }
      set { _Header = value; }
    }

    public SmppTlv Tlv {
      get { return _Tlv; }
      set { _Tlv = value; }
    }

    #region ISmppBasic Members

    public byte[] ToByteArray() {
      var builder = new ByteBuilder(300);
      builder.AddRange(_Header.ToByteArray());
      builder.AddRange(_Body.ToByteArray());
      if (_Tlv != null)
        builder.AddRange(_Tlv.ToByteArray());
      return builder.ToArray();
    }

    #endregion

    #region Nested type: BodyPdu

    [CLSCompliant(true)]
    public class BodyPdu : ISmppBasic {
      SmppDataCoding _DataCoding = SmppDataCoding.Default;
      string _DestinationAddress = string.Empty;
      byte _DestinationAddressNpi;
      byte _DestinationAddressTon;
      BitBuilder _EsmClass = new BitBuilder();
      BitBuilder _RegisteredDelivery = new BitBuilder();
      SmppServiceType _ServiceType = SmppServiceType.Default;
      string _SourceAddress = string.Empty;
      byte _SourceAddressNpi;
      byte _SourceAddressTon;

      public SmppDataCoding DataCoding {
        get { return _DataCoding; }
        set { _DataCoding = value; }
      }

      public string DestinationAddress {
        get { return _DestinationAddress; }
        set { _DestinationAddress = value; }
      }

      public byte DestinationAddressNpi {
        get { return _DestinationAddressNpi; }
        set { _DestinationAddressNpi = value; }
      }

      public byte DestinationAddressTon {
        get { return _DestinationAddressTon; }
        set { _DestinationAddressTon = value; }
      }

      public BitBuilder EsmClass {
        get { return _EsmClass; }
        set { _EsmClass = value; }
      }

      public BitBuilder RegisteredDelivery {
        get { return _RegisteredDelivery; }
        set { _RegisteredDelivery = value; }
      }

      public SmppServiceType ServiceType {
        get { return _ServiceType; }
        set { _ServiceType = value; }
      }

      public string SourceAddress {
        get { return _SourceAddress; }
        set { _SourceAddress = value; }
      }

      public byte SourceAddressNpi {
        get { return _SourceAddressNpi; }
        set { _SourceAddressNpi = value; }
      }

      public byte SourceAddressTon {
        get { return _SourceAddressTon; }
        set { _SourceAddressTon = value; }
      }

      #region ISmppBasic Members

      public byte[] ToByteArray() {
        var builder = new ByteBuilder(100);
        builder.AddRange(SmppConverter.SmppNullEnd(SmppDataCoding.BaseEncoding.GetBytes(_ServiceType.Value)));
        builder.Add(_SourceAddressTon);
        builder.Add(_SourceAddressNpi);
        builder.AddRange(SmppConverter.SmppNullEnd(SmppDataCoding.BaseEncoding.GetBytes(_SourceAddress)));
        builder.Add(_DestinationAddressTon);
        builder.Add(_DestinationAddressNpi);
        builder.AddRange(SmppConverter.SmppNullEnd(SmppDataCoding.BaseEncoding.GetBytes(_DestinationAddress)));
        builder.Add(_EsmClass.Value);
        builder.Add(_RegisteredDelivery.Value);
        builder.Add(_DataCoding.Value);
        return builder.ToArray();
      }

      #endregion
    }

    #endregion
  }
}