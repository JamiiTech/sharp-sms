using System;
using Devshock.Protocol.SmppPdu;

namespace Devshock.Protocol.Smpp {
  [CLSCompliant(true)]
  public class SmppAsyncBindResEventArgs : EventArgs {
    internal Exception _ProcessException;
    internal SmppBindReq _RequestPdu;
    internal SmppBindRes _ResponsePdu;
    internal object _State;

    public SmppAsyncBindResEventArgs() {}

    public SmppAsyncBindResEventArgs(SmppBindRes ResponsePdu) {
      _ResponsePdu = ResponsePdu;
    }

    public Exception ProcessException {
      get { return _ProcessException; }
    }

    public SmppBindReq RequestPdu {
      get { return _RequestPdu; }
    }

    public SmppBindRes ResponsePdu {
      get { return _ResponsePdu; }
    }

    public object State {
      get { return _State; }
    }
  }
}