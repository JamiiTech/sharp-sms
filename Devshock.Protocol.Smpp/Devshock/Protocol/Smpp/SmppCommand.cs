using System;
using System.Runtime.InteropServices;

namespace Devshock.Protocol.Smpp {
  [StructLayout(LayoutKind.Sequential, Size = 1), CLSCompliant(true)]
  public struct SmppCommand {
    public const int BindReceiverReq = 1;
    public const int BindReceiverRes = -2147483647;
    public const int BindTransmitterReq = 2;
    public const int BindTransmitterRes = -2147483646;
    public const int BindTransceiverReq = 9;
    public const int BindTransceiverRes = -2147483639;
    public const int OutBind = 11;
    public const int UnBindReq = 6;
    public const int UnBindRes = -2147483642;
    public const int EnquireLinkReq = 0x15;
    public const int EnquireLinkRes = -2147483627;
    public const int Alert_otification = 0x102;
    public const int GenericNack = -2147483648;
    public const int SubmitSmReq = 4;
    public const int SubmitSmRes = -2147483644;
    public const int DataSmReq = 0x103;
    public const int DataSmRes = -2147483389;
    public const int DeliverSmReq = 5;
    public const int DeliverySmRes = -2147483643;
    public const int CancelSmReq = 8;
    public const int CancelSmRes = -2147483640;
    public const int QuerySmReq = 3;
    public const int QuerySmRes = -2147483645;
    public const int ReplaceSmReq = 7;
    public const int ReplaceSmRes = -2147483641;
  }
}