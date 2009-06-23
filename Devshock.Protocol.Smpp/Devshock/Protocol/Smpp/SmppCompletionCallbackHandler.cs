namespace Devshock.Protocol.Smpp {
  delegate void SmppCompletionCallbackHandler(
    SmppAsyncObject AsyncObject, SmppAsyncObject.SmppAsyncCompleted CompletionReason);
}