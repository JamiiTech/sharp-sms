using System;
using Devshock.Common;
using Devshock.Protocol.Smpp;

namespace Devshock.Protocol.SmppPdu {
  [CLSCompliant(true)]
  public class SmppDeliverSmReq : ISmppBasic {
    BodyPdu _Body;
    SmppHeader _Header;
    SmppTlv _Tlv;

    public SmppDeliverSmReq() {
      _Header = new SmppHeader(5);
      _Body = new BodyPdu();
    }

    public SmppDeliverSmReq(ByteBuilder bb) {
      _Header = new SmppHeader(bb);
      int startPosition = 0x10;
      _Body = new BodyPdu(bb, ref startPosition);
      if (bb.Count > startPosition)
        _Tlv = new SmppTlv(bb, startPosition);
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
      string _DestinationAddress;
      byte _DestinationAddressNpi;
      byte _DestinationAddressTon;
      BitBuilder _EsmClass;
      byte _PriorityFlag;
      byte _ProtocolId;
      BitBuilder _RegisteredDelivery;
      byte _ReplaceIfPresent;
      SmppDateTime _ScheduleDeliveryTime;
      SmppServiceType _ServiceType;
      SmppString _ShortMessage;
      byte _SmDefaultMessageId;
      string _SourceAddress;
      byte _SourceAddressNpi;
      byte _SourceAddressTon;
      SmppDateTime _ValidityPeriod;

      public BodyPdu() {
        _ServiceType = SmppServiceType.Default;
        _SourceAddress = string.Empty;
        _DestinationAddress = string.Empty;
        _EsmClass = new BitBuilder();
        _RegisteredDelivery = new BitBuilder();
        _ShortMessage = new SmppString();
      }

      internal BodyPdu(ByteBuilder bb, ref int StartPosition) {
        _ServiceType = SmppServiceType.Default;
        _SourceAddress = string.Empty;
        _DestinationAddress = string.Empty;
        _EsmClass = new BitBuilder();
        _RegisteredDelivery = new BitBuilder();
        _ShortMessage = new SmppString();
        _ServiceType =
          SmppServiceType.FromValue(
            SmppDataCoding.BaseEncoding.GetString(bb.ReadBytesUntil(ref StartPosition, 0)));
        _SourceAddressTon = bb.ReadByte(ref StartPosition);
        _SourceAddressNpi = bb.ReadByte(ref StartPosition);
        _SourceAddress = SmppDataCoding.BaseEncoding.GetString(bb.ReadBytesUntil(ref StartPosition, 0));
        _DestinationAddressTon = bb.ReadByte(ref StartPosition);
        _DestinationAddressNpi = bb.ReadByte(ref StartPosition);
        _DestinationAddress = SmppDataCoding.BaseEncoding.GetString(bb.ReadBytesUntil(ref StartPosition, 0));
        _EsmClass.Value = bb.ReadByte(ref StartPosition);
        _ProtocolId = bb.ReadByte(ref StartPosition);
        _PriorityFlag = bb.ReadByte(ref StartPosition);
        _ScheduleDeliveryTime =
          new SmppDateTime(SmppDataCoding.BaseEncoding.GetString(bb.ReadBytesUntil(ref StartPosition, 0)));
        _ValidityPeriod =
          new SmppDateTime(SmppDataCoding.BaseEncoding.GetString(bb.ReadBytesUntil(ref StartPosition, 0)));
        _RegisteredDelivery.Value = bb.ReadByte(ref StartPosition);
        _ReplaceIfPresent = bb.ReadByte(ref StartPosition);
        _ShortMessage.DataCoding = SmppDataCoding.FromValue(bb.ReadByte(ref StartPosition));
        _SmDefaultMessageId = bb.ReadByte(ref StartPosition);
        byte length = bb.ReadByte(ref StartPosition);
        _ShortMessage.Value = bb.ReadBytes(ref StartPosition, length);
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

      public byte PriorityFlag {
        get { return _PriorityFlag; }
        set { _PriorityFlag = value; }
      }

      public byte ProtocolId {
        get { return _ProtocolId; }
        set { _ProtocolId = value; }
      }

      public BitBuilder RegisteredDelivery {
        get { return _RegisteredDelivery; }
        set { _RegisteredDelivery = value; }
      }

      public byte ReplaceIfPresent {
        get { return _ReplaceIfPresent; }
        set { _ReplaceIfPresent = value; }
      }

      public SmppDateTime ScheduleDeliveryTime {
        get { return _ScheduleDeliveryTime; }
        set { _ScheduleDeliveryTime = value; }
      }

      public SmppServiceType ServiceType {
        get { return _ServiceType; }
        set { _ServiceType = value; }
      }

      public SmppString ShortMessage {
        get { return _ShortMessage; }
        set { _ShortMessage = value; }
      }

      public byte SmDefaultMessageId {
        get { return _SmDefaultMessageId; }
        set { _SmDefaultMessageId = value; }
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

      public SmppDateTime ValidityPeriod {
        get { return _ValidityPeriod; }
        set { _ValidityPeriod = value; }
      }

      #region ISmppBasic Members

      public byte[] ToByteArray() {
        var builder = new ByteBuilder(300);
        builder.AddRange(SmppConverter.SmppNullEnd(SmppDataCoding.BaseEncoding.GetBytes(_ServiceType.Value)));
        builder.Add(_SourceAddressTon);
        builder.Add(_SourceAddressNpi);
        builder.AddRange(SmppConverter.SmppNullEnd(SmppDataCoding.BaseEncoding.GetBytes(_SourceAddress)));
        builder.Add(_DestinationAddressTon);
        builder.Add(_DestinationAddressNpi);
        builder.AddRange(SmppConverter.SmppNullEnd(SmppDataCoding.BaseEncoding.GetBytes(_DestinationAddress)));
        builder.Add(_EsmClass.Value);
        builder.Add(_ProtocolId);
        builder.Add(_PriorityFlag);
        if (_ScheduleDeliveryTime == null)
          builder.AddRange(SmppConverter.SmppNullEnd(null));
        else
          builder.AddRange(
            SmppConverter.SmppNullEnd(SmppDataCoding.BaseEncoding.GetBytes(_ScheduleDeliveryTime.SmppDate)));
        if (_ValidityPeriod == null)
          builder.AddRange(SmppConverter.SmppNullEnd(null));
        else
          builder.AddRange(
            SmppConverter.SmppNullEnd(SmppDataCoding.BaseEncoding.GetBytes(_ValidityPeriod.SmppDate)));
        builder.Add(_RegisteredDelivery.Value);
        builder.Add(_ReplaceIfPresent);
        builder.Add(_ShortMessage.DataCoding.Value);
        builder.Add(_SmDefaultMessageId);
        builder.Add(Convert.ToByte(_ShortMessage.Length));
        builder.AddRange(_ShortMessage.Value);
        return builder.ToArray();
      }

      #endregion
    }

    #endregion
  }
}