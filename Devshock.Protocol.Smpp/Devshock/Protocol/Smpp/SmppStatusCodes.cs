using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace Devshock.Protocol.Smpp {
  [CLSCompliant(true)]
  public class SmppStatusCodes {
    static Hashtable _Codes = new Hashtable(30);
    static Hashtable _StateCodes = new Hashtable(10);

    static SmppStatusCodes() {
      Codes.Add(0, "Command executed successfully");
      Codes.Add(1, "Message Length is invalid");
      Codes.Add(2, "Command Length is invalid");
      Codes.Add(3, "Invalid Command ID");
      Codes.Add(4, "Incorrect BIND Status for given command");
      Codes.Add(5, "ESME Already in Bound State");
      Codes.Add(6, "Invalid Priority Flag");
      Codes.Add(7, "Invalid Registered Delivery Flag");
      Codes.Add(8, "System Error");
      Codes.Add(10, "Invalid Source Address");
      Codes.Add(11, "Invalid Destination Address");
      Codes.Add(12, "Message ID is invalid");
      Codes.Add(13, "Bind Failed");
      Codes.Add(14, "Invalid Password");
      Codes.Add(15, "Invalid System ID");
      Codes.Add(0x11, "Cancel SM Failed");
      Codes.Add(0x13, "Replace SM Failed");
      Codes.Add(20, "Message Queue Full");
      Codes.Add(0x15, "Invalid Service Type");
      Codes.Add(0x33, "Invalid number of destinations");
      Codes.Add(0x34, "Invalid Distribution List name");
      Codes.Add(0x40, "Destination flag is invalid (submit_multi)");
      Codes.Add(0x42,
                "Submit w/replace functionality has been requested where it is either unsupported or inappropriate for the particular MC");
      Codes.Add(0x43, "Invalid esm_class field data");
      Codes.Add(0x44, "Cannot Submit to Distribution List");
      Codes.Add(0x45, "submit_sm, data_sm or submit_multi failed");
      Codes.Add(0x48, "Invalid Source address TON");
      Codes.Add(0x49, "Invalid Source address NPI");
      Codes.Add(80, "Invalid Destination address TON");
      Codes.Add(0x51, "Invalid Destination address NPI");
      Codes.Add(0x53, "Invalid system_type field");
      Codes.Add(0x54, "Invalid replace_if_present flag");
      Codes.Add(0x55, "Invalid number of messages");
      Codes.Add(0x58, "Throttling error (ESME has exceeded allowed message limits)");
      Codes.Add(0x61, "Invalid Scheduled Delivery Time");
      Codes.Add(0x62, "Invalid message validity period (Expiry time)");
      Codes.Add(0x63, "Predefined Message ID is Invalid or specified predefined message was not found");
      Codes.Add(100, "ESME Receiver Temporary App Error Code");
      Codes.Add(0x65, "ESME Receiver Permanent App Error Code");
      Codes.Add(0x66, "ESME Receiver Reject Message Error Code");
      Codes.Add(0x67, "Query_Sm request failed");
      Codes.Add(0xc0, "Error in the optional part of the PDU Body");
      Codes.Add(0xc1, "TLV not allowed");
      Codes.Add(0xc2, "Invalid Parameter Length");
      Codes.Add(0xc3, "Expected TLV missing");
      Codes.Add(0xc4, "Invalid TLV Value");
      Codes.Add(0xfe, "Transaction Delivery Failure");
      Codes.Add(0xff, "Unknown Error");
      Codes.Add(0x100, "ESME Not authorised to use specified service_type");
      Codes.Add(0x101, "ESME Prohibited from using specified operation");
      Codes.Add(0x102, "Specified service_type is unavailable");
      Codes.Add(0x103, "Specified service_type is denied");
      Codes.Add(260, "Invalid Data Coding Scheme");
      Codes.Add(0x105, "Source Address Sub unit is Invalid");
      Codes.Add(0x106, "Destination Address Sub unit is Invalid");
      Codes.Add(0x107, "Broadcast Frequency Interval is invalid");
      Codes.Add(0x108, "Broadcast Alias Name is invalid");
      Codes.Add(0x109, "Broadcast Area Format is invalid");
      Codes.Add(0x10a, "Number of Broadcast Areas is invalid");
      Codes.Add(0x10b, "Broadcast Content Type is invalid");
      Codes.Add(0x10c, "Broadcast Message Class is invalid");
      Codes.Add(0x10d, "Broadcast_sm operation failed");
      Codes.Add(270, "Query_broadcast_sm operation failed");
      Codes.Add(0x10f, "Cancel_broadcast_sm operation failed");
      Codes.Add(0x110, "Number of Repeated Broadcasts is invalid");
      Codes.Add(0x111, "Broadcast Service Group is invalid");
      Codes.Add(0x112, "Broadcast Channel Indicator is invalid");
      Codes.Add(0x15f95, "Local Exception. Check LastException property");
      Codes.Add(-1, "Invalid Error Code for this version. Maybe is a MC custom error code");
      StateCodes.Add(0, "The message is scheduled. Delivery has not yet been initiated");
      StateCodes.Add(1, "The message is in enroute state");
      StateCodes.Add(2, "Message is delivered to destination");
      StateCodes.Add(3, "Message validity period has expired");
      StateCodes.Add(4, "Message has been deleted");
      StateCodes.Add(5, "Message is undeliverable");
      StateCodes.Add(6, "Message is in accepted state");
      StateCodes.Add(7, "Message is in invalid state");
      StateCodes.Add(8, "Message is in a rejected state");
      StateCodes.Add(9, "The message was accepted but not transmitted or broadcast on the network");
      StateCodes.Add(-1, "Invalid State Code for this version. Maybe is a MC custom state code");
    }

    public static Hashtable Codes {
      get { return _Codes; }
      set { _Codes = value; }
    }

    public static int LocalExceptionCode {
      get { return 0x15f95; }
    }

    public static Hashtable StateCodes {
      get { return _StateCodes; }
      set { _StateCodes = value; }
    }

    public static string GetDescription(int ErrorCode) {
      if (Codes.ContainsKey(ErrorCode))
        return Convert.ToString(Codes[ErrorCode]);
      return Convert.ToString(Codes[-1]);
    }

    public static string GetSmStateDescription(byte StateCode) {
      int key = Convert.ToInt32(StateCode);
      if (StateCodes.ContainsKey(key))
        return Convert.ToString(StateCodes[key]);
      return Convert.ToString(StateCodes[-1]);
    }

    public static void SetErrors(Hashtable Table) {
      Codes = Table;
    }

    #region Nested type: CmdStatusList

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    public struct CmdStatusList {
      public static int ESME_ROK {
        get { return 0; }
      }

      public static int ESME_RINVMSGLEN {
        get { return 1; }
      }

      public static int ESME_RINVCMDLEN {
        get { return 2; }
      }

      public static int ESME_RINVCMDID {
        get { return 3; }
      }

      public static int ESME_RINVBNDSTS {
        get { return 4; }
      }

      public static int ESME_RALYBND {
        get { return 5; }
      }

      public static int ESME_RINVPRTFLG {
        get { return 6; }
      }

      public static int ESME_RINVREGDLVFLG {
        get { return 7; }
      }

      public static int ESME_RSYSERR {
        get { return 8; }
      }

      public static int ESME_RINVSRCADR {
        get { return 10; }
      }

      public static int ESME_RINVDSTADR {
        get { return 11; }
      }

      public static int ESME_RINVMSGID {
        get { return 12; }
      }

      public static int ESME_RBINDFAIL {
        get { return 13; }
      }

      public static int ESME_RINVPASWD {
        get { return 14; }
      }

      public static int ESME_RINVSYSID {
        get { return 15; }
      }

      public static int ESME_RCANCELFAIL {
        get { return 0x11; }
      }

      public static int ESME_RREPLACEFAIL {
        get { return 0x13; }
      }

      public static int ESME_RMSGQFUL {
        get { return 20; }
      }

      public static int ESME_RINVSERTYP {
        get { return 0x15; }
      }

      public static int ESME_RINVNUMDESTS {
        get { return 0x33; }
      }

      public static int ESME_RINVDLNAME {
        get { return 0x34; }
      }

      public static int ESME_RINVDESTFLAG {
        get { return 0x40; }
      }

      public static int ESME_RINVSUBREP {
        get { return 0x42; }
      }

      public static int ESME_RINVESMCLASS {
        get { return 0x43; }
      }

      public static int ESME_RCNTSUBDL {
        get { return 0x44; }
      }

      public static int ESME_RSUBMITFAIL {
        get { return 0x45; }
      }

      public static int ESME_RINVSRCTON {
        get { return 0x48; }
      }

      public static int ESME_RINVSRCNPI {
        get { return 0x49; }
      }

      public static int ESME_RINVDSTTON {
        get { return 80; }
      }

      public static int ESME_RINVDSTNPI {
        get { return 0x51; }
      }

      public static int ESME_RINVSYSTYP {
        get { return 0x53; }
      }

      public static int ESME_RINVREPFLAG {
        get { return 0x54; }
      }

      public static int ESME_RINVNUMMSGS {
        get { return 0x55; }
      }

      public static int ESME_RTHROTTLED {
        get { return 0x58; }
      }

      public static int ESME_RINVSCHED {
        get { return 0x61; }
      }

      public static int ESME_RINVEXPIRY {
        get { return 0x62; }
      }

      public static int ESME_RINVDFTMSGID {
        get { return 0x63; }
      }

      public static int ESME_RX_T_APPN {
        get { return 100; }
      }

      public static int ESME_RX_P_APPN {
        get { return 0x65; }
      }

      public static int ESME_RX_R_APPN {
        get { return 0x66; }
      }

      public static int ESME_RQUERYFAIL {
        get { return 0x67; }
      }

      public static int ESME_RINVTLVSTREAM {
        get { return 0xc0; }
      }

      public static int ESME_RTLVNOTALLWD {
        get { return 0xc1; }
      }

      public static int ESME_RINVTLVLEN {
        get { return 0xc2; }
      }

      public static int ESME_RMISSINGTLV {
        get { return 0xc3; }
      }

      public static int ESME_RINVTLVVAL {
        get { return 0xc4; }
      }

      public static int ESME_RDELIVERYFAILURE {
        get { return 0xfe; }
      }

      public static int ESME_RUNKNOWNERR {
        get { return 0xff; }
      }

      public static int ESME_RSERTYPUNAUTH {
        get { return 0x100; }
      }

      public static int ESME_RPROHIBITED {
        get { return 0x101; }
      }

      public static int ESME_RSERTYPUNAVAIL {
        get { return 0x102; }
      }

      public static int ESME_RSERTYPDENIED {
        get { return 0x103; }
      }

      public static int ESME_RINVDCS {
        get { return 260; }
      }

      public static int ESME_RINVSRCADDRSUBUNIT {
        get { return 0x105; }
      }

      public static int ESME_RINVDSTADDRSUBUNIT {
        get { return 0x106; }
      }

      public static int ESME_RINVBCASTFREQINT {
        get { return 0x107; }
      }

      public static int ESME_RINVBCASTALIAS_NAME {
        get { return 0x108; }
      }

      public static int ESME_RINVBCASTAREAFMT {
        get { return 0x109; }
      }

      public static int ESME_RINVNUMBCAST_AREAS {
        get { return 0x10a; }
      }

      public static int ESME_RINVBCASTCNTTYPE {
        get { return 0x10b; }
      }

      public static int ESME_RINVBCASTMSGCLASS {
        get { return 0x10c; }
      }

      public static int ESME_RBCASTFAIL {
        get { return 0x10d; }
      }

      public static int ESME_RBCASTQUERYFAIL {
        get { return 270; }
      }

      public static int ESME_RBCASTCANCELFAIL {
        get { return 0x10f; }
      }

      public static int ESME_RINVBCAST_REP {
        get { return 0x110; }
      }

      public static int ESME_RINVBCASTSRVGRP {
        get { return 0x111; }
      }

      public static int ESME_RINVBCASTCHANIND {
        get { return 0x112; }
      }

      public static int LOCAL_EXCEPTION {
        get { return 0x15f95; }
      }
    }

    #endregion

    #region Nested type: SmStateList

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    public struct SmStateList {
      public static byte SCHEDULED {
        get { return 0; }
      }

      public static byte ENROUTE {
        get { return 1; }
      }

      public static byte DELIVERED {
        get { return 2; }
      }

      public static byte EXPIRED {
        get { return 3; }
      }

      public static byte DELETED {
        get { return 4; }
      }

      public static byte UNDELIVERABLE {
        get { return 5; }
      }

      public static byte ACCEPTED {
        get { return 6; }
      }

      public static byte UNKNOWN {
        get { return 7; }
      }

      public static byte REJECTED {
        get { return 8; }
      }

      public static byte SKIPPED {
        get { return 9; }
      }
    }

    #endregion
  }
}