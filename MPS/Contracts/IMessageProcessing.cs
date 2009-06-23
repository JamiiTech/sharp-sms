using System.ServiceModel;
using Goods;

namespace MPS {
  // NOTE: If you change the interface name "IMessageProcessing" here, you must also update the reference to "IMessageProcessing" in Web.config.

  /// <summary>
  /// Для взаимодействия по WCF нужен контракт описывающий интерфейс
  /// </summary>
  [ServiceContract]
  public interface IMessageProcessing {
    [OperationContract]
    Sms[] ProcessSms(Sms inSms);

    //string[] ProcessSms(MTS.SMPP.Sms inSms);        
  }
}