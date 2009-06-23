using System;
using System.Windows.Forms;

namespace Manager {
  public partial class MainForm : Form {
    public MainForm() {
      InitializeComponent();
    }

    void выходToolStripMenuItem_Click(object sender, EventArgs e) {
      Close();
    }

    void проверкаТекстовыхSMSToolStripMenuItem_Click(object sender, EventArgs e) {
      new SMS_Series_Form().Show();
    }

    void сгенерироватьКодыToolStripMenuItem_Click(object sender, EventArgs e) {
      new GenCodesForm().Show();
    }
  }
}