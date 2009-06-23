using System;
using Devshock.Common;
using Devshock.Protocol.Smpp;

namespace Devshock.Protocol.SmppPdu {
  [CLSCompliant(true)]
  public class SmppBindReq : ISmppBasic, ISmppPdu {
    BodyPdu _Body;
    SmppHeader _Header;

    public SmppBindReq() {}

    internal SmppBindReq(ByteBuilder bb) {
      int startPosition = 0x10;
      _Header = new SmppHeader(bb);
      _Body = new BodyPdu(bb, ref startPosition);
    }

    public SmppBindReq(SmppHeader Header, BodyPdu Body) {
      _Header = Header;
      _Body = Body;
    }

    public SmppBindReq(int ConnectionMode, BodyPdu Body) : this(new SmppHeader(0, ConnectionMode, 0, 0), Body) {}

    public SmppBindReq(int CommandId, int SequenceNumber, BodyPdu Body)
      : this(new SmppHeader(CommandId, SequenceNumber), Body) {}

    public BodyPdu Body {
      get { return _Body; }
      set { _Body = value; }
    }

    #region ISmppBasic Members

    public byte[] ToByteArray() {
      var builder = new ByteBuilder(300);
      byte[] c = _Body.ToByteArray();
      _Header.CommandLength = 0x10 + c.Length;
      byte[] buffer2 = _Header.ToByteArray();
      builder.AddRange(buffer2);
      builder.AddRange(c);
      return builder.ToArray();
    }

    #endregion

    #region ISmppPdu Members

    public void BuildPdu(byte[] a) {}

    public SmppHeader Header {
      get { return _Header; }
      set { _Header = value; }
    }

    #endregion

    #region Nested type: BodyPdu

    [CLSCompliant(true)]
    public class BodyPdu : ISmppBasic {
      byte _AddressNpi;
      string _AddressRange;
      byte _AddressTon;
      byte _InterfaceVersion;
      string _Password;
      string _SystemId;
      string _SystemType;

      public BodyPdu() : this(string.Empty, string.Empty, string.Empty, 0, 0, 0, string.Empty) {}

      internal BodyPdu(ByteBuilder PduByteArray, ref int StartPosition) {
        _SystemId = string.Empty;
        _Password = string.Empty;
        _SystemType = string.Empty;
        _AddressRange = string.Empty;
        _SystemId = SmppDataCoding.BaseEncoding.GetString(PduByteArray.ReadBytesUntil(ref StartPosition, 0));
        _Password = SmppDataCoding.BaseEncoding.GetString(PduByteArray.ReadBytesUntil(ref StartPosition, 0));
        _SystemType = SmppDataCoding.BaseEncoding.GetString(PduByteArray.ReadBytesUntil(ref StartPosition, 0));
        _InterfaceVersion = PduByteArray.ReadByte(ref StartPosition);
        _AddressTon = PduByteArray.ReadByte(ref StartPosition);
        _AddressNpi = PduByteArray.ReadByte(ref StartPosition);
        _AddressRange = SmppDataCoding.BaseEncoding.GetString(PduByteArray.ReadBytesUntil(ref StartPosition, 0));
      }

      public BodyPdu(string SystemId, string Password)
        : this(SystemId, Password, string.Empty, 0, 0, 0, string.Empty) {}

      public BodyPdu(string SystemId, string Password, string SystemType, byte InterfaceVersion, byte AddressTon,
                     byte AddressNpi, string AddressRange) {
        _SystemId = string.Empty;
        _Password = string.Empty;
        _SystemType = string.Empty;
        _AddressRange = string.Empty;
        _SystemId = SystemId;
        _Password = Password;
        _SystemType = SystemType;
        _InterfaceVersion = InterfaceVersion;
        _AddressTon = AddressTon;
        _AddressNpi = AddressNpi;
        _AddressRange = AddressRange;
      }

      public byte AddressNpi {
        get { return _AddressNpi; }
        set { _AddressNpi = value; }
      }

      public string AddressRange {
        get { return _AddressRange; }
        set { _AddressRange = value; }
      }

      public byte AddressTon {
        get { return _AddressTon; }
        set { _AddressTon = value; }
      }

      public byte InterfaceVersion {
        get { return _InterfaceVersion; }
        set { _InterfaceVersion = value; }
      }

      public string Password {
        get { return _Password; }
        set { _Password = value; }
      }

      public string SystemId {
        get { return _SystemId; }
        set { _SystemId = value; }
      }

      public string SystemType {
        get { return _SystemType; }
        set { _SystemType = value; }
      }

      #region ISmppBasic Members

      public byte[] ToByteArray() {
        var builder = new ByteBuilder(90);
        builder.AddRange(SmppConverter.SmppNullEnd(SmppDataCoding.BaseEncoding.GetBytes(_SystemId)));
        builder.AddRange(SmppConverter.SmppNullEnd(SmppDataCoding.BaseEncoding.GetBytes(_Password)));
        builder.AddRange(SmppConverter.SmppNullEnd(SmppDataCoding.BaseEncoding.GetBytes(_SystemType)));
        builder.Add(_InterfaceVersion);
        builder.Add(_AddressTon);
        builder.Add(_AddressNpi);
        builder.AddRange(SmppConverter.SmppNullEnd(SmppDataCoding.BaseEncoding.GetBytes(_AddressRange)));
        return builder.ToArray();
      }

      #endregion
    }

    #endregion
  }
}