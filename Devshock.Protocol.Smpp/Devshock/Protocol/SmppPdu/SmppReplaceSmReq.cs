using System;

namespace Devshock.Protocol.SmppPdu {
  [CLSCompliant(true)]
  public class SmppReplaceSmReq : ISmppBasic {
    public BodyPdu Body { get; set; }

    public SmppHeader Header { get; set; }

    public SmppTlv Tlv { get; set; }

    #region ISmppBasic Members

    public byte[] ToByteArray() {
      return null;
    }

    #endregion

    #region Nested type: BodyPdu

    [CLSCompliant(true)]
    public class BodyPdu : ISmppBasic {
      public string MessageId = string.Empty;
      public byte RegisteredDelivery;
      public string ScheduleDeliveryTime = string.Empty;
      public byte[] ShortMessage;
      public byte SmDefaultMessageId;
      public byte SmLength;
      public string SourceAddress = string.Empty;
      public byte SourceAddressNpi;
      public byte SourceAddressTon;
      public string ValidityPeriod = string.Empty;

      #region ISmppBasic Members

      public byte[] ToByteArray() {
        return null;
      }

      #endregion
    }

    #endregion
  }
}