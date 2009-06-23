using System;
using Devshock.Protocol.SmppPdu;

namespace Devshock.Protocol.Smpp {
  [CLSCompliant(true)]
  public class SmppAsyncSubmitSmResEventArgs : EventArgs {
    internal Exception _ProcessException;
    internal SmppSubmitSmReq _RequestPdu;
    internal SmppSubmitSmRes _ResponsePdu;
    internal object _State;

    public SmppAsyncSubmitSmResEventArgs() {}

    public SmppAsyncSubmitSmResEventArgs(SmppSubmitSmRes ResponsePdu) {
      _ResponsePdu = ResponsePdu;
    }

    public Exception ProcessException {
      get { return _ProcessException; }
    }

    public SmppSubmitSmReq RequestPdu {
      get { return _RequestPdu; }
      set { _RequestPdu = value; }
    }

    public SmppSubmitSmRes ResponsePdu {
      get { return _ResponsePdu; }
    }

    public object State {
      get { return _State; }
      set { _State = value; }
    }
  }
}