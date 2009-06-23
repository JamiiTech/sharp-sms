using System.Collections.Generic;
using System.ServiceModel;

namespace CDS {
  // NOTE: If you change the interface name "IMeService" here, you must also update the reference to "IMeService" in Web.config.
  [ServiceContract]
  public interface IMeService {
    [OperationContract]
    List<MeFileType> GetFilesDescrByHash(string hash);

    //[OperationContract]
    //public string GetFileTypes(int idMeType);

    [OperationContract]
    string GetFilePathByHash(string hash, int idMeFileType);

/*
        [OperationContract]
        int CreateMe(int idMeType, string mask);


        [OperationContract]
        int AddMe(int idMeType, string mask);
*/
  }
}