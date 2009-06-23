using System;
using Devshock.Common;
using Devshock.Protocol.Smpp;

namespace Devshock.Protocol.SmppPdu {
  [CLSCompliant(true)]
  public class SmppDeliverSmRes : ISmppBasic {
    BodyPdu _Body;
    SmppHeader _Header;
    SmppTlv _Tlv;

    internal SmppDeliverSmRes(ByteBuilder bb) {
      int startPosition = 0x10;
      _Header = new SmppHeader(bb);
      _Body = new BodyPdu(bb, ref startPosition);
      _Tlv = new SmppTlv(bb, startPosition);
    }

    public SmppDeliverSmRes(int SequenceNumber, string MessageId) {
      _Header = new SmppHeader(-2147483643, SequenceNumber);
      _Body = new BodyPdu(MessageId);
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
      return SmppConverter.GetPduByteArray(ref _Header, _Body, _Tlv);
    }

    #endregion

    #region Nested type: BodyPdu

    [CLSCompliant(true)]
    public class BodyPdu : ISmppBasic {
      string _MessageId;

      public BodyPdu(string MessageId) {
        _MessageId = string.Empty;
        _MessageId = MessageId;
      }

      internal BodyPdu(ByteBuilder bb, ref int StartPosition) {
        _MessageId = string.Empty;
        _MessageId = SmppDataCoding.BaseEncoding.GetString(bb.ReadBytesUntil(ref StartPosition, 0));
      }

      public string MessageId {
        get { return _MessageId; }
        set { _MessageId = value; }
      }

      #region ISmppBasic Members

      public byte[] ToByteArray() {
        return SmppConverter.SmppNullEnd(SmppDataCoding.BaseEncoding.GetBytes(_MessageId));
      }

      #endregion
    }

    #endregion
  }
}