using System;
using System.Runtime.InteropServices;

namespace Devshock.Protocol.Smpp {
  [StructLayout(LayoutKind.Sequential), CLSCompliant(true)]
  public struct SmppQueueCounter {
    readonly int _WaitingRequest;
    readonly int _WaitingResponse;

    public SmppQueueCounter(int WaitingRequest, int WaitingResponse) {
      _WaitingRequest = WaitingRequest;
      _WaitingResponse = WaitingResponse;
    }

    public int WaitingRequest {
      get { return _WaitingRequest; }
    }

    public int WaitingResponse {
      get { return _WaitingResponse; }
    }
  }
}