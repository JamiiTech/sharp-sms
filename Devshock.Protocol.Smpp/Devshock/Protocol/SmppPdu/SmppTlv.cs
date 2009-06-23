using System;
using System.Collections;
using Devshock.Common;
using Devshock.Protocol.Smpp;

namespace Devshock.Protocol.SmppPdu {
  [CLSCompliant(true)]
  public class SmppTlv : ISmppBasic {
    #region TagCString enum

    public enum TagCString {
      AdditionalStatusInformation = 0x1d,
      DestinationNetworkId = 0x60e,
      ReceiptedMessageId = 30,
      SourceNetworkId = 0x60d
    }

    #endregion

    #region TagInteger enum

    public enum TagInteger {
      AlertOnMsgDelivery = 0x130c,
      CallbackNumberPresenseIndicator = 770,
      CongestionState = 0x428,
      DeliveryFailureReason = 0x425,
      DestBearerType = 7,
      DestinationAddressNpCountry = 0x613,
      DestinationAddressNpResolution = 0x611,
      DestinationAddressSubunit = 5,
      DestinationNetworkType = 6,
      DestinationPort = 0x20b,
      DestinationTelematicsId = 8,
      DisplayTime = 0x1201,
      DpfResult = 0x420,
      ItsReplyType = 0x1380,
      LanguageIndicator = 0x20d,
      MessageState = 0x427,
      MoreMessagesToSend = 0x426,
      MsAvailabilityStatus = 0x422,
      MsMsgWaitFacilities = 0x30,
      NumberOfMessages = 0x304,
      PayloadType = 0x19,
      PrivacyIndicator = 0x201,
      QosTimeToLive = 0x17,
      SarMsgRefNum = 0x20c,
      SarSegmentSeqNum = 0x20f,
      SarTotalSegments = 0x20e,
      ScInterfaceVersion = 0x210,
      SetDpf = 0x421,
      SmsSignal = 0x1203,
      SourceAddrSubunit = 13,
      SourceBearerType = 15,
      SourceNetworkType = 14,
      SourcePort = 0x20a,
      SourceTelematicsId = 10,
      UserMessageReference = 0x204,
      UserResponseCode = 0x205,
      UssdServiceOp = 0x501
    }

    #endregion

    #region TagString enum

    public enum TagString : short {
      BillingIdentification = 0x60b,
      CallbackNumber = 0x381,
      CallbackNumberAtag = 0x303,
      DestinationAddressNpInformation = 0x612,
      DestinationNodeId = 0x610,
      DestinationSubaddress = 0x203,
      ItsSessionInfo = 0x1383,
      MessagePayload = 0x424,
      MsValidity = 0x1204,
      NetworkErrorCode = 0x423,
      SourceNodeId = 0x60f,
      SourceSubAddress = 0x202
    }

    #endregion

    public ArrayList Items;

    public SmppTlv() : this(30) {}

    public SmppTlv(int InitialSize) {
      Items = new ArrayList(10);
      Items = new ArrayList(InitialSize);
    }

    public SmppTlv(byte[] Data) {
      Items = new ArrayList(10);
      if (Data != null) {
        short num3;
        var builder = new ByteBuilder(Data);
        for (int i = 0; i < builder.Count; i += 4 + num3) {
          short tagId = SmppConverter.FromByteArrayToInt16(builder.ToArray(i, 2));
          num3 = SmppConverter.FromByteArrayToInt16(builder.ToArray(i + 2, 2));
          byte[] buffer = builder.ToArray(i + 4, num3);
          var tag = new Tag(tagId);
          tag.SetByteArray(buffer);
          Items.Add(tag);
        }
      }
    }

    internal SmppTlv(ByteBuilder bb, int StartPosition) : this(bb.ToArray(StartPosition, bb.Count - StartPosition)) {}

    #region ISmppBasic Members

    public byte[] ToByteArray() {
      var builder = new ByteBuilder(100);
      if (Items != null)
        foreach (Tag tag in Items) {
          builder.AddRange(SmppConverter.FromInt16ToByteArray(tag.TagId));
          builder.AddRange(SmppConverter.FromInt16ToByteArray(tag.Length));
          builder.AddRange(tag.GetByteArray());
        }
      return builder.ToArray();
    }

    #endregion

    public void AddTlv(Tag CurTlv) {
      Items.Add(CurTlv);
    }

    #region Nested type: Tag

    [CLSCompliant(true)]
    public class Tag {
      byte[] _Value;

      public Tag() {}

      public Tag(short TagId) {
        this.TagId = TagId;
      }

      public short Length {
        get {
          if (_Value != null)
            return Convert.ToInt16(_Value.Length);
          return 0;
        }
      }

      public short TagId { get; set; }

      public int GetByte() {
        return _Value[0];
      }

      public byte[] GetByteArray() {
        return _Value;
      }

      public int GetInt16() {
        return SmppConverter.FromByteArrayToInt16(_Value);
      }

      public int GetInt32() {
        return SmppConverter.FromByteArrayToInt32(_Value);
      }

      public string GetString() {
        if (IsCString(TagId))
          return SmppDataCoding.BaseEncoding.GetString(_Value, 0, _Value.Length - 1);
        return SmppDataCoding.BaseEncoding.GetString(_Value);
      }

      bool IsCString(short TagId) {
        if (((TagCString.DestinationNetworkId.CompareTo(TagId) != 0) &&
             (TagCString.ReceiptedMessageId.CompareTo(TagId) != 0)) &&
            (TagCString.SourceNetworkId.CompareTo(TagId) != 0))
          return false;
        return true;
      }

      public void SetByte(byte value) {
        _Value = new[] {value};
      }

      public void SetByteArray(byte[] value) {
        _Value = value;
      }

      public void SetInt16(short value) {
        _Value = SmppConverter.FromInt16ToByteArray(value);
      }

      public void SetInt32(int value) {
        _Value = SmppConverter.FromInt32ToByteArray(value);
      }

      public void SetString(string value) {
        if (IsCString(TagId))
          _Value = SmppConverter.SmppNullEnd(SmppDataCoding.BaseEncoding.GetBytes(value));
        else
          _Value = SmppDataCoding.BaseEncoding.GetBytes(value);
      }
    }

    #endregion
  }
}