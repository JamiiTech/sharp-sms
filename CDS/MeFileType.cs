using System.Runtime.Serialization;

namespace CDS {
  [DataContract]
  public class MeFileType {
    [DataMember] public string Descr;
    [DataMember] public int Id;

    public MeFileType(int Id, string Descr) {
      this.Id = Id;
      this.Descr = Descr;
    }
  }
}