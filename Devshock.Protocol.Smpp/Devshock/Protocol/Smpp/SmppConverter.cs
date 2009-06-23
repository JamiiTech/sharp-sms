using System;
using Devshock.Common;
using Devshock.Protocol.SmppPdu;

namespace Devshock.Protocol.Smpp {
  [CLSCompliant(true)]
  public class SmppConverter {
    internal static byte Asc(int value) {
      return SmppDataCoding.BaseEncoding.GetBytes(value.ToString())[0];
    }

    internal static byte Asc(string strValue) {
      return SmppDataCoding.BaseEncoding.GetBytes(strValue)[0];
    }

    internal static byte BooleanToByte(bool value) {
      if (value)
        return 1;
      return 0;
    }

    internal static char Chr(byte CharCode) {
      return Convert.ToChar(CharCode);
    }

    internal static short FromByteArrayToInt16(byte[] number) {
      if (BitConverter.IsLittleEndian)
        Array.Reverse(number);
      return BitConverter.ToInt16(number, 0);
    }

    internal static int FromByteArrayToInt32(byte[] number) {
      if (BitConverter.IsLittleEndian)
        Array.Reverse(number);
      return BitConverter.ToInt32(number, 0);
    }

    internal static byte[] FromInt16ToByteArray(short number) {
      byte[] bytes = BitConverter.GetBytes(number);
      if (BitConverter.IsLittleEndian)
        Array.Reverse(bytes);
      return bytes;
    }

    internal static byte[] FromInt32ToByteArray(int number) {
      byte[] bytes = BitConverter.GetBytes(number);
      if (BitConverter.IsLittleEndian)
        Array.Reverse(bytes);
      return bytes;
    }

    internal static int GetArrayLength(Array ArrayObj) {
      if (ArrayObj != null)
        return ArrayObj.Length;
      return 0;
    }

    public static int GetCommandIdReq(int CommandIdRes) {
      return ((0x7fffffff + CommandIdRes) + 1);
    }

    public static int GetCommandIdRes(int CommandIdReq) {
      return ((CommandIdReq - 0x7fffffff) - 1);
    }

    internal static byte[] GetPduByteArray(ref SmppHeader Header, ISmppBasic Body, SmppTlv Tlv) {
      byte[] c = null;
      byte[] buffer2 = null;
      byte[] arrayObj = null;
      buffer2 = Body.ToByteArray();
      Header.CommandLength = 0x10 + buffer2.Length;
      if (Tlv != null) {
        arrayObj = Tlv.ToByteArray();
        Header.CommandLength += arrayObj.Length;
      }
      c = Header.ToByteArray();
      var builder = new ByteBuilder((c.Length + buffer2.Length) + GetArrayLength(arrayObj));
      builder.AddRange(c);
      builder.AddRange(buffer2);
      if (arrayObj != null)
        builder.AddRange(arrayObj);
      return builder.ToArray();
    }

    internal static byte[] SmppNullEnd(byte[] str) {
      int length = 0;
      if (str != null)
        length = str.Length;
      var destinationArray = new byte[length + 1];
      if (destinationArray.Length > 1)
        Array.Copy(str, 0, destinationArray, 0, length);
      destinationArray[length] = 0;
      return destinationArray;
    }
  }
}