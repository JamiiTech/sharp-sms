using System;
using System.Windows.Forms;
using Goods;

namespace CDS.UI {
  public partial class SystemWorks : Form {
    public SystemWorks() {
      InitializeComponent();
    }

    void btnDoWork_Click(object sender, EventArgs e) {
      if (cBoxCleanBlankProperties.Checked) {
        var mng = new ConnectionManager(sqlConnection1);
        try {
          mng.Open();
          cmdCleanBlankProperties.ExecuteNonQuery();
        } finally {
          mng.Close();
        }
      }
    }
  }
}