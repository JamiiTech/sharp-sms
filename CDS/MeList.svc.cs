using System.Data;
using System.Data.SqlClient;
using Goods;

namespace CDS {
  // NOTE: If you change the class name "MeList" here, you must also update the reference to "MeList" in Web.config.
  public class MeList : IMeList {
    readonly SqlDataAdapter daMes;
    readonly ConnectionManager mng;

    string SqlQueryJava =
      @"select  me.id,vName.sVal as name, vDescription.SVal as Description,vShortDesc.sVal as ShortDesc, 
vCopyright.sVal as Copyright
from me
left join MePropValues vName ON Me.id = vName.idMe and vName.idProp = 6
left JOIN MePropValues vDescription ON Me.id = vDescription.idMe and vDescription.idProp = 9
left JOIN MePropValues vShortDesc ON Me.id = vShortDesc.idMe and vShortDesc.idProp = 7
left JOIN MePropValues vCopyright ON Me.id = vCopyright.idMe and vCopyright.idProp = 12
where me.idMeType = @idMeType";

    string SqlQueryRealtones =
      @"select  me.id,vName.sVal as name, vDescription.SVal as Description,vShortDesc.sVal as ShortDesc, 
vCopyright.sVal as Copyright
from me
left join MePropValues vName ON Me.id = vName.idMe and vName.idProp = 43
left JOIN MePropValues vDescription ON Me.id = vDescription.idMe and vDescription.idProp = 44
left JOIN MePropValues vShortDesc ON Me.id = vShortDesc.idMe and vShortDesc.idProp = 45
left JOIN MePropValues vCopyright ON Me.id = vCopyright.idMe and vCopyright.idProp = 12
where me.idMeType = @idMeType";

    public MeList() {
      string conStr = ConnectionManager.ReadConnStr("CDS.UI.Properties.Settings.RAIConnectionString");
      mng = new ConnectionManager(conStr);
      daMes = new SqlDataAdapter(mng.CreateCommand());
      //refreshMes(tBoxSQLQuery.Text);
    }

    //public DataTable GetMeList()
    //{
    //    return GetMeList(string.Empty);
    //}

    #region IMeList Members

    public DataTable GetMeList(int idMeType) {
      var ret = new DataTable("Mes");

      /*
			if (string.Empty != SqlWhere)
			{
				daMes.SelectCommand.CommandText 
					= string.Format("{0} WHERE ({1})", SqlQuery, SqlWhere);
			}
			else
			{
				daMes.SelectCommand.CommandText = SqlQuery;
			}
			 */

      switch (idMeType) {
        case 4:
          daMes.SelectCommand.CommandText = SqlQueryJava;
          break;
        case 2:
          daMes.SelectCommand.CommandText = SqlQueryRealtones;
          break;

        default:
          daMes.SelectCommand.CommandText = SqlQueryJava;
          break;
      }

      daMes.SelectCommand.Parameters.AddWithValue("@idMeType", idMeType);

      try {
        mng.Open();
        daMes.Fill(ret);
      } finally {
        mng.Close();
      }
      return ret;
    }

    #endregion
  }
}