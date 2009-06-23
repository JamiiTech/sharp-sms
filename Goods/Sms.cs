using System.Runtime.Serialization;

namespace Goods {
  /// <summary>
  /// SMS-обертка
  /// </summary>
  [DataContract]
  public class Sms {
    [DataMember] public bool AddDeliverroute;
    public byte[] binaryData;

    [DataMember] public string DeliverRoute = string.Empty;
    [DataMember] public string Destination;

    /// <summary>
    /// Значение из SMPP-протокола, обозначающее кодировку
    /// </summary>
    public byte EncodingID;

    [DataMember] public ulong Id;
    [DataMember] public int idSMPPConfiguration;
    public int msg_ref_num;

    public int segmentCount = 1;
    public int segmentNum = 1;
    [DataMember] public string SMPPMessageId = string.Empty;
    [DataMember] public string Source;
    [DataMember] public SmsStatus Status;
    [DataMember] public string Text;
  }
}