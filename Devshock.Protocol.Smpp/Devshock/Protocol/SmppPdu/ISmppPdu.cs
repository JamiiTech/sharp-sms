namespace Devshock.Protocol.SmppPdu {
  interface ISmppPdu {
    SmppHeader Header { get; set; }
    void BuildPdu(byte[] a);
    byte[] ToByteArray();
  }
}