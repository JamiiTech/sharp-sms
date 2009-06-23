using System.Linq;
using Goods;
using Raidb;

namespace Manager {
  /// <summary>
  /// Свойства map'а для отображения в PropertyGrid
  /// </summary>
  public class MapProperties {
    public readonly Map map;

    public MapProperties(Map map) {
      this.map = map;
    }

    public int idMe {
      get { return map != null ? map.idMe : -1; }
    }

    public string name {
      get {
        return map != null
                 ? map.Me.MePropValues.SingleOrDefault(t => t.idProp == (int) MePropertyEnum.title).sVal
                 : "МЭ не найден";
      }
    }

    public string retrieveWay {
      get { return (map != null ? ((MeRetrieveWayEnum) map.Me.idMeRetrieveWay).ToString() : ""); }
    }

    public string meType {
      get { return (map != null ? ((MeTypeEnum) map.Me.idMeType).ToString() : ""); }
    }
  }
}