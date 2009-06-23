using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Goods;

namespace CDS {
  // NOTE: If you change the class name "MeService" here, you must also update the reference to "MeService" in Web.config.
  public class MeService : IMeService {
    //todo: жесткий хак. Доработать ConnectionManager
    static readonly string conStr = ConnectionManager.ReadConnStr("MPSConnectionString");

    #region IMeService Members

    public string GetFilePathByHash(string hash, int idMeFileType) {
      var mng = new ConnectionManager(conStr);
      SqlCommand cmd = mng.CreateCommand("GetFilePathByHash");
      cmd.Parameters.AddWithValue("@hash", hash);
      cmd.Parameters.AddWithValue("@idMeFileType", idMeFileType);
      cmd.CommandType = CommandType.StoredProcedure;
      mng.Open();
      object o = cmd.ExecuteScalar();
      string ret;
      if (DBNull.Value.Equals(o))
        ret = string.Empty;
      else
        ret = (string) o;
      mng.Close();
      return ret;
    }

    public List<MeFileType> GetFilesDescrByHash(string hash) {
      //ConnectionManager mng = new ConnectionManager(conStr);
      //SqlCommand cmd = mng.CreateCommand("GetFilesDescrByHash");
      var con = new SqlConnection(conStr);
      SqlCommand cmd = con.CreateCommand();
      cmd.CommandText = "GetFilesDescrByHash";
      try {
        hash = (new Guid(hash)).ToString();
      } catch {}
      cmd.Parameters.AddWithValue("@hash", hash);
      cmd.CommandType = CommandType.StoredProcedure;
      //mng.Open();
      con.Open();
      SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
      var ret = new List<MeFileType>(3);
      while (rdr.Read())
        ret.Add(new MeFileType(rdr.GetInt32(0), rdr.GetString(1)));
      rdr.Close();
      //mng.Close();
      con.Close();
      return ret;
    }

    #endregion
  }
}