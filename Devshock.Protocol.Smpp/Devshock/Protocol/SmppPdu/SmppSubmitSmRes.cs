using System;
using Devshock.Common;
using Devshock.Protocol.Smpp;

namespace Devshock.Protocol.SmppPdu {
  [CLSCompliant(true)]
  public class SmppSubmitSmRes : ISmppBasic {
    BodyPdu _Body;
    SmppHeader _Header;
    SmppTlv _Tlv;

    public SmppSubmitSmRes() : this(0) {}

    internal SmppSubmitSmRes(ByteBuilder bb) {
      if (bb.Count >= 0x10) {
        _Header = new SmppHeader(bb);
        if (bb.Count != 0x10) {
          int lastBytePosition = 0x10;
          _Body = new BodyPdu(bb, ref lastBytePosition);
          if (bb.Count >= lastBytePosition)
            _Tlv = new SmppTlv(bb, lastBytePosition);
        }
      }
    }

    public SmppSubmitSmRes(int SequenceNumber) {
      _Header = new SmppHeader(-2147483644, SequenceNumber);
      _Body = new BodyPdu();
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
      byte[] c = null;
      byte[] buffer2 = _Body.ToByteArray();
      byte[] arrayObj = null;
      _Header.CommandLength = 0x10 + buffer2.Length;
      if (_Tlv != null) {
        arrayObj = _Tlv.ToByteArray();
        _Header.CommandLength += arrayObj.Length;
      }
      c = _Header.ToByteArray();
      var builder = new ByteBuilder((c.Length + buffer2.Length) + SmppConverter.GetArrayLength(arrayObj));
      builder.AddRange(c);
      builder.AddRange(buffer2);
      if (arrayObj != null)
        builder.AddRange(arrayObj);
      return builder.ToArray();
    }

    #endregion

    #region Nested type: BodyPdu

    [CLSCompliant(true)]
    public class BodyPdu : ISmppBasic {
      string _MessageId;

      public BodyPdu() {
        _MessageId = string.Empty;
      }

      internal BodyPdu(ByteBuilder bb, ref int LastBytePosition) {
        _MessageId = string.Empty;
        _MessageId = SmppDataCoding.BaseEncoding.GetString(bb.ReadBytesUntil(ref LastBytePosition, 0));
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