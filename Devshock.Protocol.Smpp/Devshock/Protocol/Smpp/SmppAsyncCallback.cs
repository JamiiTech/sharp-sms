using System;
using Devshock.Protocol.SmppPdu;

namespace Devshock.Protocol.Smpp {
  [CLSCompliant(true)]
  public class SmppAsyncCallback {
    #region Delegates

    public delegate void OnCompleteSubmitSm(
      object sender, SmppSubmitSmReq SubmitSmReq, SmppSubmitSmRes SubmitSmRes, object State, Exception Ex);

    #endregion
  }
}