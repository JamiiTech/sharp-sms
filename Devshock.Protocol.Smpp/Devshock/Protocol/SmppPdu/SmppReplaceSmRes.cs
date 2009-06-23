using System;

namespace Devshock.Protocol.SmppPdu {
  [CLSCompliant(true)]
  public class SmppReplaceSmRes : ISmppBasic {
    SmppHeader _Header;

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