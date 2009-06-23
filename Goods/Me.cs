using System;

namespace Goods {
  /// <summary>
  /// Класс самого медиа-элемента
  /// </summary>
  public class Me {
    MeTypeEnum meType;

    public Me(int Id) {
      this.Id = Id;
    }

    public Me() {
    }

    public int Id { get; set; }

    public MeTypeEnum MeType {
      get {
        throw new NotImplementedException();
        return meType;
      }
      set {
        meType = value;
        throw new NotImplementedException();
      }
    }
  }
}