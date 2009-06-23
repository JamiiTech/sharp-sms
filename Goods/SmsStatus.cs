using System.Runtime.Serialization;

namespace Goods {
  [DataContract]
  public enum SmsStatus : long {
    // глобальный статус
    [EnumMember] NotSent = 0,
    [EnumMember] Sended = 0x1,
    // отправка в smsc
    [EnumMember] Sending = 0x10,
    // запись в хранилище
    [EnumMember] Changed = 0x100,
    // ошибка при отправке
    [EnumMember] CannotSend = 0x11
  }
}