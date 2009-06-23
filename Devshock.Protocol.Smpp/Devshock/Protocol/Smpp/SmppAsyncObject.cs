using System.Threading;
using System.Timers;
using Devshock.Common;
using Devshock.Protocol.SmppPdu;
using Timer=System.Timers.Timer;

namespace Devshock.Protocol.Smpp {
  class SmppAsyncObject {
    #region SmppAsyncCompleted enum

    public enum SmppAsyncCompleted {
      Disconnection = 3,
      Response = 1,
      Timeout = 2
    }

    #endregion

    #region SmppAsyncState enum

    public enum SmppAsyncState {
      Disposing = 3,
      Enabled = 1,
      Moving = 2
    }

    #endregion

    public SmppAsyncState AsyncState;
    public object Callback;
    public SmppCompletionCallbackHandler CompletionCallback;
    public ManualResetEvent mre;
    public ByteBuilder PduRes;
    public ISmppPdu Request;
    public object State;
    public object SyncRoot;
    public int Timeout;
    public Timer tmTimeout;

    public SmppAsyncObject() {
      Timeout = 0x7530;
      SyncRoot = new object();
      AsyncState = SmppAsyncState.Enabled;
    }

    public SmppAsyncObject(int Key, ManualResetEvent mre) {
      Timeout = 0x7530;
      SyncRoot = new object();
      AsyncState = SmppAsyncState.Enabled;
      this.mre = mre;
    }

    public SmppAsyncObject(object CallBack, object State, ISmppPdu Request, int Timeout) {
      this.Timeout = 0x7530;
      SyncRoot = new object();
      AsyncState = SmppAsyncState.Enabled;
      Callback = CallBack;
      this.State = State;
      this.Request = Request;
      this.Timeout = Timeout;
    }

    public void DisposeTimer() {
      if (tmTimeout != null) {
        tmTimeout.Enabled = false;
        tmTimeout.Dispose();
        tmTimeout = null;
      }
    }

    public void StartTimer() {
      if (tmTimeout == null) {
        tmTimeout = new Timer(Timeout);
        tmTimeout.Elapsed += Timeout_Elapsed;
        tmTimeout.AutoReset = false;
      }
      tmTimeout.Enabled = true;
    }

    void Timeout_Elapsed(object sender, ElapsedEventArgs e) {
      if (CompletionCallback != null) {
        lock (SyncRoot) {
          if (AsyncState == SmppAsyncState.Enabled)
            AsyncState = SmppAsyncState.Moving;
        }
        if (AsyncState == SmppAsyncState.Moving)
          CompletionCallback(this, SmppAsyncCompleted.Timeout);
      }
    }
  }
}