using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CDS.UI.DSMeTypesTableAdapters;
using CDS.UI.Properties;
using Goods;
using Rai;
using Raidb;

namespace CDS.UI {
  public partial class MainForm : Form {
    readonly SqlCommand cmdFiles;
    readonly SqlCommand cmdXML;
    readonly SqlDataAdapter daSS;

    readonly ConnectionManager mng;
    int idMe;

    public MainForm() {
      mng = new ConnectionManager
        (Settings.Default.RAIConnectionString);
      InitializeComponent();

      var daMeTypes = new MeTypesTableAdapter();
      daMeTypes.Fill(dsMeTypes.MeTypes);

      //ScreenShots
      daSS = new SqlDataAdapter(
        mng.CreateCommand("LoadMeSShots"));
      daSS.SelectCommand.CommandType = CommandType.StoredProcedure;
      daSS.SelectCommand.Parameters.Add("@idMe", SqlDbType.Int);

      cmdXML = mng.CreateCommand("GetMeXMLControl");
      cmdXML.CommandType = CommandType.StoredProcedure;
      cmdXML.Parameters.Add("@idMe", SqlDbType.Int);
      cmdXML.Parameters.Add("@idSellChannel", SqlDbType.Int);
      cmdXML.Parameters.Add("@idSellSection", SqlDbType.Int);

      refreshMes();
      publishing1.onCodeChanged += publishing1_onCodeChanged;
      radioButtonSS_CheckedChanged(null, null);

      cmdFiles = mng.CreateCommand("LoadMeFileInfo");
      cmdFiles.CommandType = CommandType.StoredProcedure;
      cmdFiles.Parameters.Add("@idMe", SqlDbType.Int);
      refreshFileInfo();
    }

    void publishing1_onCodeChanged(object sender, EventArgs e) {
      refreshXML();
    }

    void btnRefreshData_Click(object sender, EventArgs e) {
      refreshMes();
    }

    void refreshMes() {
      dataGridView1.SuspendLayout();
      CDS.MeList client = null;
      try {
        client = new CDS.MeList(); //  MeListClient();
//        client.Open();
        if (null != cBoxMeType.SelectedValue) {
          DataTable dt = client.GetMeList((int) cBoxMeType.SelectedValue);
          dataGridView1.DataSource = dt;
        }
      } finally {
        ResumeLayout();
        if (null != client) {
  //        client.Close();
        }
      }

      #region старая реализация

      /* 
			daMes.SelectCommand.CommandText = sQuery;
			me.Mes.Clear();
			try
			{
				
				mng.Open();
				daMes.Fill(me.Mes);
				dataGridView1.Refresh();
			}
			finally
			{
				mng.Close();
				this.dataGridView1.ResumeLayout();
			}*/

      #endregion
    }

    void refreshCodeInfo(int idMe) {
      publishing1.IdMe = idMe;
    }

    void dataGridView1_CurrentCellChanged(object sender, EventArgs e) {
      DataGridViewRow o = dataGridView1.CurrentRow;
      if (null != o)
        if (null != o.DataBoundItem) {
          DataRow currentRow = ((DataRowView) (o.DataBoundItem)).Row;
          idMe = (int) currentRow["id"];
          mePropertiesEditor.LoadMeProperties(idMe);
          meSSEditor.LoadSS(LoadSs());
          refreshCodeInfo(idMe);
          refreshXML();
          refreshFileInfo();
        }
    }

    void refreshFileInfo() {
      fileEditor1.LoadInfo(loadMeFileInfo());
    }

    List<MeFileInfoSmall> loadMeFileInfo() {
      var ret = new List<MeFileInfoSmall>();
      cmdFiles.Parameters[0].Value = idMe;
      string sStoragePath = FileStorage.GetStoragePath(idMe);
      try {
        mng.Open();
        SqlDataReader rdr = cmdFiles.ExecuteReader();
        string name;
        string type;
        int idFile;
        while (rdr.Read()) {
          name = sStoragePath + @"\" + (string) rdr["filename"];
          type = (string) rdr["typeDesc"];
          idFile = (int) rdr["idFile"];

          var fi = new MeFileInfoSmall(idFile, name, type);
          ret.Add(fi);
        }
        rdr.Close();
        return ret;
      } finally {
        mng.Close();
      }
    }

    void refreshXML() {
      xml.Value = XMLFormatter.FormatXML(LoadXML());
    }

    /// <summary>
    /// Получение сгенерированного XML из базы данных
    /// </summary>
    /// <returns>XML в строке</returns>
    string LoadXML() {
      // Предварительно проверяем все параметры
      var report = new StringBuilder();
      using (var db = new DbDataContext()) {
        Raidb.Me me = db.Mes.SingleOrDefault(t => t.id == idMe);
        if (me.Maps.Count == 0) {
          report.Append("Медиаэлемент idMe=" + idMe + " не опубликован, нажмите на кнопку опубликовать!\n");
          foreach (Map m in me.Maps)
            report.Append(" " + m.code + " SellSection = " + m.SellSection + " SellType =  " + m.SellType);
          //   return report.ToString();
        } else {
          Map map = me.Maps.FirstOrDefault(t => t.idSellSection == publishing1.IdSellSection);
          if (map == null)
            publishing1.IdSellSection = me.Maps.First().idSellSection;
        }
      }

      try {
        mng.Open();
        cmdXML.Parameters["@idMe"].Value = idMe;
        cmdXML.Parameters["@idSellChannel"].Value = publishing1.IdSellChannel;
        cmdXML.Parameters["@idSellSection"].Value = publishing1.IdSellSection;

        object res = cmdXML.ExecuteScalar();
        if (res is string)
          return (string) res;
        else
          return "Ошибка генерации XML " + cmdXML.CommandText + " " + idMe + " " + publishing1.IdSellChannel +
                 " " +
                 publishing1.IdSellSection +
                 "\n\n" + report;
      } catch (Exception ex) {
        return "Ошибка генерации XML: " + ex;
      } finally {
        mng.Close();
      }
    }

    List<SS> LoadSs() {
      var dt = new DataTable();
      daSS.SelectCommand.Parameters[0].Value = idMe;
      try {
        mng.Open();
        daSS.Fill(dt);
      } finally {
        mng.Close();
      }
      var ret = new List<SS>(dt.Rows.Count);

      string sStoragePath = FileStorage.GetStoragePath(idMe);

      if (string.IsNullOrEmpty(sStoragePath))
        return ret;

      //А теперь жесткий хак пути, берем все от последнего слеша
      var sStoragePath1 = new DirectoryInfo(Settings.Default.StoragePath +
                                            sStoragePath.Substring(
                                              sStoragePath.LastIndexOf(@"\")));
      if (!sStoragePath1.Exists)
        ErrShower.ShowError("Отсутствует путь до FileStorage " + sStoragePath1.FullName +
                            "\n Проверьте настройки программы!");
      foreach (DataRow row in dt.Rows)
        try {
          string fn = sStoragePath1.FullName + @"\" + (string) row["filename"];
          if (File.Exists(fn)) {
            Image img = Image.FromFile(fn);
            var ss = new SS(img, fn, (int) row["id"]);
            ret.Add(ss);
          }
        } catch (Exception ex) {
          ErrShower.ShowError("Ошибка загрузки скриншота", ex);
        }
      return ret;
    }

    void btnSaveAll_Click(object sender, EventArgs e) {
      //CDS.UI.Properties.Settings.Default.Reload();
      folderBrowserDialog.SelectedPath = Settings.Default.DirectoryLastSave;
      DialogResult res = folderBrowserDialog.ShowDialog();
      if (DialogResult.OK == res) {
        string SavePath = folderBrowserDialog.SelectedPath;
        Settings.Default.DirectoryLastSave = SavePath;
        Settings.Default.Save();

        string fn = string.Empty;
        string destination = SavePath + @"\";
        List<string> fnList = radioButtonSS.Checked ? GetSelectedSSFilenames() : GetSelectedFilenames();

        foreach (string item in fnList) {
          fn = (new FileInfo(item)).Name;
          File.Copy(item, destination + fn, true);
        }
        var p = new Process {
                              StartInfo = {
                                            FileName = "explorer.exe",
                                            WorkingDirectory = SavePath,
                                            Arguments = SavePath
                                          }
                            };
        p.Start();
      }
    }

    List<string> GetSelectedSSFilenames() {
      var ret = new List<string>();
      List<SS> selectedSS = meSSEditor.GetSelectedSS();
      foreach (SS ss in selectedSS)
        ret.Add(ss.ImagePath);
      return ret;
    }

    List<string> GetSelectedFilenames() {
      var ret = new List<string>();
      List<MeFileInfoSmall> selectedItems = fileEditor1.GetSelectedItems();

      foreach (MeFileInfoSmall item in selectedItems)
        ret.Add(item.Name);
      return ret;
    }

    void radioButtonSS_CheckedChanged(object sender, EventArgs e) {
      panel1.Controls.Clear();
      if (radioButtonSS.Checked) {
        panel1.Controls.Add(meSSEditor);
        meSSEditor.Dock = DockStyle.Fill;
      } else {
        panel1.Controls.Add(fileEditor1);
        fileEditor1.Dock = DockStyle.Fill;
      }
    }

    void button2_Click(object sender, EventArgs e) {
      var IdMes = new List<int>();
      var t = (DataTable) dataGridView1.DataSource;

      foreach (DataRow r in t.Rows) {}
    }

    void btnOpen_Click(object sender, EventArgs e) {
      var f = new AddMeFiles {IdMeType = ((int) cBoxMeType.SelectedValue), IdMe = idMe};
      DialogResult res = f.ShowDialog();
    }

    void cBoxMeType_SelectedValueChanged(object sender, EventArgs e) {
      refreshMes();
    }

    void btnNewMe_Click(object sender, EventArgs e) {
      SqlCommand cmd = mng.CreateCommand("NewMe");
      cmd.CommandType = CommandType.StoredProcedure;
      cmd.Parameters.AddWithValue("@idMeType", cBoxMeType.SelectedValue);
      int idNewMe = 0;
      try {
        mng.Open();
        idNewMe = (int) cmd.ExecuteScalar();
      } finally {
        mng.Close();
      }
      refreshMes();
      selectMe(idNewMe);
    }

    void btnSystemWork_Click(object sender, EventArgs e) {
      DialogResult res = (new SystemWorks()).ShowDialog();
      if (DialogResult.OK != res)
        return;
      int idCurrMe = idMe;
      refreshMes();
      selectMe(idCurrMe);
    }

    bool selectMe(int idMe) {
      foreach (DataGridViewRow row in dataGridView1.Rows)
        if (Convert.ToInt32(row.Cells["id"].Value) == idMe) {
          dataGridView1.CurrentCell = row.Cells[1];
          return true;
        }
      return false;
    }

    void dataGridView1_Resize(object sender, EventArgs e) {
      dataGridView1.PerformLayout();
    }

    void xml_Load(object sender, EventArgs e) {}

    void publishing1_Load(object sender, EventArgs e) {}
  }
}