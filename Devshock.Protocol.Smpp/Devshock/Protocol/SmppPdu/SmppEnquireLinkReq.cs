using System;
using Devshock.Common;

namespace Devshock.Protocol.SmppPdu {
  [CLSCompliant(true)]
  public class SmppEnquireLinkReq : ISmppBasic, ISmppPdu {
    SmppHeader _Header;

    public SmppEnquireLinkReq() : this(new SmppHeader(0x15, 0)) {}

    internal SmppEnquireLinkReq(ByteBuilder bb) {
      _Header = new SmppHeader(bb);
    }

    public SmppEnquireLinkReq(SmppHeader Header) {
      _Header = Header;
    }

    #region ISmppBasic Members

    public byte[] ToByteArray() {
      return _Header.ToByteArray();
    }

    #endregion

    #region ISmppPdu Members

    public void BuildPdu(byte[] a) {}

    public SmppHeader Header {
      get { return _Header; }
      set { _Header = value; }
    }

    #endregion
  }
}