using System.Windows.Forms;

namespace CDS.UI {
  partial class FileInfoSmall : UserControl {
    public int idFile;

    public FileInfoSmall() {
      InitializeComponent();
    }

    public FileInfoSmall(MeFileInfoSmall info) {
      InitializeComponent();

      idFile = info.idFile;
      tBoxFilename.Text = info.Name;
      tBoxFileType.Text = info.Type;
    }

    public string FileType {
      get { return tBoxFileType.Text; }
    }

    public string Filename {
      get { return tBoxFilename.Text; }
    }

    public bool IsSelected {
      get { return checkBox1.Checked; }
    }
  }
}