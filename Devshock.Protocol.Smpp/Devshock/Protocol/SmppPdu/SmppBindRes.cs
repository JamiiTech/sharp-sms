using System;
using Devshock.Common;
using Devshock.Protocol.Smpp;

namespace Devshock.Protocol.SmppPdu {
  [CLSCompliant(true)]
  public class SmppBindRes : ISmppBasic {
    BodyPdu _Body;
    SmppHeader _Header;
    SmppTlv _Tlv;

    public SmppBindRes() : this(new SmppHeader()) {}

    internal SmppBindRes(ByteBuilder bb) {
      int startPosition = 0x10;
      _Header = new SmppHeader(bb);
      if (bb.Count > 0x10)
        _Body = new BodyPdu(bb, ref startPosition);
      if (bb.Count > startPosition)
        _Tlv = new SmppTlv(bb, startPosition);
    }

    public SmppBindRes(SmppHeader Header) {
      _Header = Header;
      _Body = new BodyPdu();
    }

    public SmppBindRes(int CommandId, int CommandStatus, int SequenceNumber)
      : this(new SmppHeader(0x10, CommandId, CommandStatus, SequenceNumber)) {}

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
      string _SystemId;

      public BodyPdu() {
        _SystemId = string.Empty;
      }

      internal BodyPdu(ByteBuilder PduByteArray, ref int StartPosition) {
        _SystemId = string.Empty;
        _SystemId = SmppDataCoding.BaseEncoding.GetString(PduByteArray.ReadBytesUntil(ref StartPosition, 0));
      }

      public string SystemId {
        get { return _SystemId; }
        set { _SystemId = value; }
      }

      #region ISmppBasic Members

      public byte[] ToByteArray() {
        var builder = new ByteBuilder(100);
        builder.AddRange(SmppConverter.SmppNullEnd(SmppDataCoding.BaseEncoding.GetBytes(_SystemId)));
        return builder.ToArray();
      }

      #endregion
    }

    #endregion
  }
}