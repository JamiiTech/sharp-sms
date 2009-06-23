using System;

namespace Devshock.Protocol.SmppPdu {
  [CLSCompliant(true)]
  public class SmppGenericNackReq : ISmppBasic {
    SmppHeader _Header;

    public SmppGenericNackReq() : this(new SmppHeader(0, -2147483648, 0, 0)) {}

    public SmppGenericNackReq(SmppHeader Header) {
      _Header = Header;
    }

    public SmppHeader Header {
      get { return _Header; }
      set { _Header = value; }
    }

    #region ISmppBasic Members

    public byte[] ToByteArray() {
      return _Header.ToByteArray();
    }

    #endregion
  }
}