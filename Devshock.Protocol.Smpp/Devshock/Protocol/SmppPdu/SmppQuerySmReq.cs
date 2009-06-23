using System;
using Devshock.Common;
using Devshock.Protocol.Smpp;

namespace Devshock.Protocol.SmppPdu {
  [CLSCompliant(true)]
  public class SmppQuerySmReq : ISmppBasic, ISmppPdu {
    BodyPdu _Body;
    SmppHeader _Header;

    public SmppQuerySmReq() : this(0) {}

    internal SmppQuerySmReq(ByteBuilder bb) {
      int startPosition = 0x10;
      _Header = new SmppHeader(bb);
      _Body = new BodyPdu(bb, ref startPosition);
    }

    public SmppQuerySmReq(BodyPdu Body) : this(0, Body) {}

    public SmppQuerySmReq(int SequenceNumber) : this(SequenceNumber, new BodyPdu()) {}

    public SmppQuerySmReq(int SequenceNumber, BodyPdu Body) {
      _Header = new SmppHeader(3, SequenceNumber);
      _Body = Body;
    }

    public BodyPdu Body {
      get { return _Body; }
      set { _Body = value; }
    }

    #region ISmppBasic Members

    public byte[] ToByteArray() {
      byte[] c = _Body.ToByteArray();
      _Header.CommandLength = 0x10 + c.Length;
      byte[] buffer2 = _Header.ToByteArray();
      var builder = new ByteBuilder(_Header.CommandLength);
      builder.AddRange(buffer2);
      builder.AddRange(c);
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
      string _MessageId;
      string _SourceAddress;
      byte _SourceAddressNpi;
      byte _SourceAddressTon;

      public BodyPdu() {
        _MessageId = string.Empty;
        _SourceAddress = string.Empty;
      }

      internal BodyPdu(ByteBuilder bb, ref int StartPosition) {
        _MessageId = string.Empty;
        _SourceAddress = string.Empty;
        _MessageId = SmppDataCoding.BaseEncoding.GetString(bb.ReadBytesUntil(ref StartPosition, 0));
        _SourceAddressTon = bb.ReadByte(ref StartPosition);
        _SourceAddressNpi = bb.ReadByte(ref StartPosition);
        _SourceAddress = SmppDataCoding.BaseEncoding.GetString(bb.ReadBytesUntil(ref StartPosition, 0));
      }

      public string MessageId {
        get { return _MessageId; }
        set { _MessageId = value; }
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
        var builder = new ByteBuilder(30);
        builder.AddRange(SmppConverter.SmppNullEnd(SmppDataCoding.BaseEncoding.GetBytes(_MessageId)));
        builder.Add(_SourceAddressTon);
        builder.Add(_SourceAddressNpi);
        builder.AddRange(SmppConverter.SmppNullEnd(SmppDataCoding.BaseEncoding.GetBytes(_SourceAddress)));
        return builder.ToArray();
      }

      #endregion
    }

    #endregion
  }
}