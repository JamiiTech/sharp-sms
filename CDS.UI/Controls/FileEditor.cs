using System.Collections.Generic;
using System.Windows.Forms;

namespace CDS.UI {
  partial class FileEditor : UserControl {
    public FileEditor() {
      InitializeComponent();
    }

    public void LoadInfo(List<MeFileInfoSmall> info) {
      SuspendLayout();
      Controls.Clear();
      foreach (MeFileInfoSmall item in info) {
        var fi = new FileInfoSmall(item);
        fi.Dock = DockStyle.Left;
        Controls.Add(fi);
      }

      ResumeLayout();
    }

    public List<MeFileInfoSmall> GetSelectedItems() {
      var ret = new List<MeFileInfoSmall>();
      foreach (FileInfoSmall fi in Controls)
        if (fi.IsSelected)
          ret.Add(new MeFileInfoSmall(fi.idFile, fi.Filename, fi.FileType));
      return ret;
    }
  }
}