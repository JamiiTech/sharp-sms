using System;
using Devshock.Common;

namespace Devshock.Protocol.SmppPdu {
  [CLSCompliant(true)]
  public class SmppCancelSmRes : ISmppBasic {
    SmppHeader _Header;

    public SmppCancelSmRes() {}

    internal SmppCancelSmRes(ByteBuilder bb) {
      _Header = new SmppHeader(bb);
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