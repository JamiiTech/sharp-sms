using System.Data;
using System.ServiceModel;

namespace CDS {  
  // NOTE: If you change the interface name "IMeList" here, you must also update the reference to "IMeList" in Web.config.
  /// <summary>
  /// Web-интерфейс, через который можно получать список медиаэлементов заданного типа
  /// </summary>
  [ServiceContract]
  public interface IMeList {
    /// <summary>
    /// Получить список медиаэлементов заданного типа
    /// </summary>
    /// <param name="idMeType">id типа медиаэлементов</param>
    /// <returns>Список медиаэлементов из БД RAI</returns>
    [OperationContract]
    DataTable GetMeList(int idMeType);
  }
}