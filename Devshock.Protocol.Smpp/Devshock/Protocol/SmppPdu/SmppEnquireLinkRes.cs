using System;
using Devshock.Common;

namespace Devshock.Protocol.SmppPdu {
  [CLSCompliant(true)]
  public class SmppEnquireLinkRes : ISmppBasic {
    SmppHeader _Header;

    public SmppEnquireLinkRes() : this(new SmppHeader(-2147483627, 0)) {}

    internal SmppEnquireLinkRes(ByteBuilder bb) {
      _Header = new SmppHeader(bb);
    }

    public SmppEnquireLinkRes(SmppHeader Header) {
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