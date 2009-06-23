using System;
using Devshock.Common;
using Devshock.Protocol.Smpp;

namespace Devshock.Protocol.SmppPdu {
  [CLSCompliant(true)]
  public class SmppCancelSmReq : ISmppBasic, ISmppPdu {
    BodyPdu _Body;
    SmppHeader _Header;

    public SmppCancelSmReq() {}

    public SmppCancelSmReq(BodyPdu Body) : this(new SmppHeader(0, 8, 0, 0), Body) {}

    public SmppCancelSmReq(SmppHeader Header, BodyPdu Body) {
      _Header = Header;
      _Body = Body;
    }

    public BodyPdu Body {
      get { return _Body; }
      set { _Body = value; }
    }

    #region ISmppBasic Members

    public byte[] ToByteArray() {
      var builder = new ByteBuilder(100);
      builder.AddRange(_Header.ToByteArray());
      builder.AddRange(_Body.ToByteArray());
      return builder.ToArray();
    }

    #endregion

    #region ISmppPdu Members

    public void BuildPdu(byte[] a) {}

    public SmppHeader Header {
      get { return _Header; }
      set { _Header = value; }
    }

    #endregion

    #region Nested type: BodyPdu

    [CLSCompliant(true)]
    public class BodyPdu : ISmppBasic {
      string _DestinationAddress = string.Empty;
      byte _DestinationAddressNpi;
      byte _DestinationAddressTon;
      string _MessageId = string.Empty;
      SmppServiceType _ServiceType = SmppServiceType.Default;
      string _SourceAddress = string.Empty;
      byte _SourceAddressNpi;
      byte _SourceAddressTon;

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

      public string MessageId {
        get { return _MessageId; }
        set { _MessageId = value; }
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
        builder.AddRange(SmppConverter.SmppNullEnd(SmppDataCoding.BaseEncoding.GetBytes(_MessageId)));
        builder.Add(_SourceAddressTon);
        builder.Add(_SourceAddressNpi);
        builder.AddRange(SmppConverter.SmppNullEnd(SmppDataCoding.BaseEncoding.GetBytes(_SourceAddress)));
        builder.Add(_DestinationAddressTon);
        builder.Add(_DestinationAddressNpi);
        builder.AddRange(SmppConverter.SmppNullEnd(SmppDataCoding.BaseEncoding.GetBytes(_DestinationAddress)));
        return builder.ToArray();
      }

      #endregion
    }

    #endregion
  }
}