using System;

namespace Devshock.Protocol.SmppPdu {
  [CLSCompliant(true)]
  public class SmppUnBindRes : ISmppBasic {
    SmppHeader _Header = new SmppHeader(-2147483642, 0);

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