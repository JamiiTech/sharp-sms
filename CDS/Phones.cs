using System.Collections.Generic;
using System.Data.Linq;
using Rai;
using Raidb;

namespace CDS {
  public class Phones {
    public List<MePhoneInfo> GetSupportedPhones(int idMe) {
      using (var db = new DbDataContext()) {
        ISingleResult<GetSupportedPhonesResult> result = db.GetSupportedPhones(idMe);
        var res = new List<MePhoneInfo>();

        foreach (GetSupportedPhonesResult item in result) {
          var i = new MePhoneInfo {
                                    filename = item.filename,
                                    idModel = item.idModel,
                                    idVendor = item.idVendor,
                                    ModelName = item.ModelName,
                                    VendorName = item.VendorName
                                  };
          res.Add(i);
        }
        return res;
      }
    }
  }
}