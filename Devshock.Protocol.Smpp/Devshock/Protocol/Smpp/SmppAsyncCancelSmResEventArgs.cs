using System;
using Devshock.Protocol.SmppPdu;

namespace Devshock.Protocol.Smpp {
  [CLSCompliant(true)]
  public class SmppAsyncCancelSmResEventArgs : EventArgs {
    internal Exception _ProcessException;
    internal SmppDataSmReq _RequestPdu;
    internal SmppCancelSmRes _ResponsePdu;
    internal object _State;

    public SmppAsyncCancelSmResEventArgs() {}

    public SmppAsyncCancelSmResEventArgs(SmppCancelSmRes ResponsePdu) {
      _ResponsePdu = ResponsePdu;
    }

    public Exception ProcessException {
      get { return _ProcessException; }
    }

    public SmppDataSmReq RequestPdu {
      get { return _RequestPdu; }
      set { _RequestPdu = value; }
    }

    public SmppCancelSmRes ResponsePdu {
      get { return _ResponsePdu; }
    }

    public object State {
      get { return _State; }
      set { _State = value; }
    }
  }
}