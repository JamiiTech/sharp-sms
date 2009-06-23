using System;
using Devshock.Protocol.SmppPdu;

namespace Devshock.Protocol.Smpp {
  [CLSCompliant(true)]
  public class SmppAsyncEnquireLinkResEventArgs : EventArgs {
    internal Exception _ProcessException;
    internal SmppEnquireLinkReq _RequestPdu;
    internal SmppEnquireLinkRes _ResponsePdu;
    internal object _State;

    public SmppAsyncEnquireLinkResEventArgs() {}

    public SmppAsyncEnquireLinkResEventArgs(SmppEnquireLinkRes EnquireLinkResObj) {}

    public Exception ProcessException {
      get { return _ProcessException; }
    }

    public SmppEnquireLinkReq RequestPdu {
      get { return _RequestPdu; }
    }

    public SmppEnquireLinkRes ResponsePdu {
      get { return _ResponsePdu; }
    }

    public object State {
      get { return _State; }
    }
  }
}