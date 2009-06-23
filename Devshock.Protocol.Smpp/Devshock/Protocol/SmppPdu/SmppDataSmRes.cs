using System;
using Devshock.Common;
using Devshock.Protocol.Smpp;

namespace Devshock.Protocol.SmppPdu {
  [CLSCompliant(true)]
  public class SmppDataSmRes : ISmppBasic {
    public BodyPdu Body { get; set; }

    public SmppHeader Header { get; set; }

    #region ISmppBasic Members

    public byte[] ToByteArray() {
      var builder = new ByteBuilder(200);
      builder.AddRange(Header.ToByteArray());
      builder.AddRange(Body.ToByteArray());
      return builder.ToArray();
    }

    #endregion

    #region Nested type: BodyPdu

    [CLSCompliant(true)]
    public class BodyPdu : ISmppBasic {
      public string MessageId = string.Empty;

      #region ISmppBasic Members

      public byte[] ToByteArray() {
        var builder = new ByteBuilder(100);
        builder.AddRange(SmppConverter.SmppNullEnd(SmppDataCoding.BaseEncoding.GetBytes(MessageId)));
        return builder.ToArray();
      }

      #endregion
    }

    #endregion
  }
}