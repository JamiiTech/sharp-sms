using System;
using Devshock.Common;
using Devshock.Protocol.Smpp;

namespace Devshock.Protocol.SmppPdu {
  [CLSCompliant(true)]
  public class SmppQuerySmRes : ISmppBasic {
    BodyPdu _Body;
    SmppHeader _Header;

    public SmppQuerySmRes() : this(0) {}

    internal SmppQuerySmRes(ByteBuilder bb) {
      _Header = new SmppHeader(bb);
      int lastBytePosition = 0x10;
      _Body = new BodyPdu(bb, ref lastBytePosition);
    }

    public SmppQuerySmRes(SmppHeader Header) {
      _Header = Header;
      _Body = new BodyPdu();
    }

    public SmppQuerySmRes(int SequenceNumber) : this(new SmppHeader(0x10, -2147483645, 0, SequenceNumber)) {}

    public BodyPdu Body {
      get { return _Body; }
      set { _Body = value; }
    }

    public SmppHeader Header {
      get { return _Header; }
      set { _Header = value; }
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

    #region Nested type: BodyPdu

    [CLSCompliant(true)]
    public class BodyPdu : ISmppBasic {
      byte _ErrorCode;
      SmppDateTime _FinalDate;
      string _MessageId;
      byte _MessageState;

      internal BodyPdu() {
        _MessageId = string.Empty;
      }

      internal BodyPdu(ByteBuilder bb, ref int LastBytePosition) {
        _MessageId = string.Empty;
        _MessageId = SmppDataCoding.BaseEncoding.GetString(bb.ReadBytesUntil(ref LastBytePosition, 0));
        _FinalDate =
          new SmppDateTime(SmppDataCoding.BaseEncoding.GetString(bb.ReadBytesUntil(ref LastBytePosition, 0)));
        _MessageState = bb.ReadByte(ref LastBytePosition);
        _ErrorCode = bb.ReadByte(ref LastBytePosition);
        LastBytePosition--;
      }

      public byte ErrorCode {
        get { return _ErrorCode; }
        set { _ErrorCode = value; }
      }

      public SmppDateTime FinalDate {
        get { return _FinalDate; }
        set { _FinalDate = value; }
      }

      public string MessageId {
        get { return _MessageId; }
        set { _MessageId = value; }
      }

      public byte MessageState {
        get { return _MessageState; }
        set { _MessageState = value; }
      }

      #region ISmppBasic Members

      public byte[] ToByteArray() {
        var builder = new ByteBuilder(30);
        builder.AddRange(SmppConverter.SmppNullEnd(SmppDataCoding.BaseEncoding.GetBytes(_MessageId)));
        if (_FinalDate == null)
          builder.AddRange(SmppConverter.SmppNullEnd(null));
        else
          builder.AddRange(SmppConverter.SmppNullEnd(SmppDataCoding.BaseEncoding.GetBytes(_FinalDate.SmppDate)));
        builder.Add(_MessageState);
        builder.Add(_ErrorCode);
        return builder.ToArray();
      }

      #endregion
    }

    #endregion
  }
}