using System;
using Devshock.Protocol.SmppPdu;

namespace Devshock.Protocol.Smpp {
  [CLSCompliant(true)]
  public class SmppAsyncQuerySmResEventArgs : EventArgs {
    internal Exception _ProcessException;
    internal SmppQuerySmReq _RequestPdu;
    internal SmppQuerySmRes _ResponsePdu;
    internal object _State;

    public SmppAsyncQuerySmResEventArgs() {}

    public SmppAsyncQuerySmResEventArgs(SmppQuerySmRes ResponsePdu) {
      _ResponsePdu = ResponsePdu;
    }

    public Exception ProcessException {
      get { return _ProcessException; }
    }

    public SmppQuerySmReq RequestPdu {
      get { return _RequestPdu; }
      set { _RequestPdu = value; }
    }

    public SmppQuerySmRes ResponsePdu {
      get { return _ResponsePdu; }
    }

    public object State {
      get { return _State; }
      set { _State = value; }
    }
  }
}