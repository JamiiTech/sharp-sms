using System;
using Devshock.Common;

namespace Devshock.Protocol.SmppPdu {
  [CLSCompliant(true)]
  public class SmppUnBindReq : ISmppBasic, ISmppPdu {
    SmppHeader _Header;

    public SmppUnBindReq() {
      _Header = new SmppHeader(6, 0);
    }

    internal SmppUnBindReq(ByteBuilder bb) {
      _Header = new SmppHeader(bb);
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