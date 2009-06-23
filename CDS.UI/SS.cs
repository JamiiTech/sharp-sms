using System.Drawing;

namespace CDS.UI {
  public class SS {
    public SS(Image Img, string Fn, int IdFile) {
      this.Img = Img;
      ImagePath = Fn;
      this.IdFile = IdFile;
    }

    public Image Img { get; set; }

    public string ImagePath { get; set; }

    public int IdFile { get; set; }
  }
}