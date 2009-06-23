using System;
using Devshock.Common;
using Devshock.Protocol.Smpp;

namespace Devshock.Protocol.SmppPdu {
  [CLSCompliant(true)]
  public class SmppHeader : ISmppBasic {
    int _CommandId;
    int _CommandLength;
    int _CommandStatus;
    int _SequenceNumber;

    public SmppHeader() : this(0x10, 0, 0, 0) {}

    internal SmppHeader(ByteBuilder bb) {
      _CommandLength = SmppConverter.FromByteArrayToInt32(bb.ToArray(0, 4));
      _CommandId = SmppConverter.FromByteArrayToInt32(bb.ToArray(4, 4));
      _CommandStatus = SmppConverter.FromByteArrayToInt32(bb.ToArray(8, 4));
      _SequenceNumber = SmppConverter.FromByteArrayToInt32(bb.ToArray(12, 4));
    }

    public SmppHeader(int CommandId) : this(0x10, CommandId, 0, 0) {}

    public SmppHeader(int CommandId, int SequenceNumber) : this(0x10, CommandId, 0, SequenceNumber) {}

    public SmppHeader(int CommandLength, int CommandId, int CommandStatus, int SequenceNumber) {
      _CommandLength = CommandLength;
      _CommandId = CommandId;
      _CommandStatus = CommandStatus;
      _SequenceNumber = SequenceNumber;
    }

    public int CommandId {
      get { return _CommandId; }
      set { _CommandId = value; }
    }

    public int CommandLength {
      get { return _CommandLength; }
      set { _CommandLength = value; }
    }

    public int CommandStatus {
      get { return _CommandStatus; }
      set { _CommandStatus = value; }
    }

    public int SequenceNumber {
      get { return _SequenceNumber; }
      set { _SequenceNumber = value; }
    }

    #region ISmppBasic Members

    public byte[] ToByteArray() {
      var builder = new ByteBuilder(0x10);
      builder.AddRange(SmppConverter.FromInt32ToByteArray(_CommandLength));
      builder.AddRange(SmppConverter.FromInt32ToByteArray(_CommandId));
      builder.AddRange(SmppConverter.FromInt32ToByteArray(_CommandStatus));
      builder.AddRange(SmppConverter.FromInt32ToByteArray(_SequenceNumber));
      return builder.ToArray();
    }

    #endregion
  }
}