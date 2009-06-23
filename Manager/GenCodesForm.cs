using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using Goods;
using Rai;
using Raidb;
using Me=Raidb.Me;

namespace Manager {
  public partial class GenCodesForm : Form {
    public readonly DbDataContext db;
    int _code;

    public GenCodesForm() {
      InitializeComponent();

      db = new DbDataContext();
      CSS.LoadData(db);
    }

    public int code {
      get { return _code; }
      set {
        _code = value;
        string s = _code.ToString();
        while (s.Length < 6)
          s = "0" + s;
        СтартовыйКод.Text = s;
      }
    }

    void GenCodes_Load(object sender, EventArgs e) {
    }

    void sellChannelBindingSource_CurrentChanged(object sender, EventArgs e) {
    }

    void SelectSellChannel_SelectedIndexChanged(object sender, EventArgs e) {
    }

    public void X(string Автор, string Произведение) {
      IQueryable<Me> mes = db.Mes.Where(t => t.idMeType == (int) MeTypeEnum.Realtones &&
                                             t.MePropValues.Count(
                                               r => r.sVal == Произведение && r.idProp == (int) MePropertyEnum.song) > 0 &&
                                             t.MePropValues.Count(r => r.sVal == Автор) > 0);

      if (mes.Count() > 1) throw new Exception("Больше одного совпадения МЭ!");
      if (mes.Count() < 1) throw new Exception("МЭ не найден!");
      Debug.Assert(mes.Count() == 1);
      Me me = mes.First();
      // PriceCategory
      PriceCategory pc =
        db.PriceCategory_Configurations_sns.Where(t => t.sn == (string) Короткий_номер.SelectedItem).First().
          PriceCategory;
      if (pc == null) {
        throw new Exception("Не найдена ценовая категория!");
      }

      IEnumerable<Map> maps = CSS.sellSection.Maps.Where(t => t.idMe == me.id && t.idPriceCategory == pc.id);
      if (maps.Count() == 0) {
        // Генерируем код
        Console.WriteLine("Генерируем код");
        bool Занят;
        do {
          Занят = db.Maps.Count(t => t.idPriceCategory == pc.id && t.code == СтартовыйКод.Text) > 0;
          if (Занят) {
            Console.WriteLine("Код - " + СтартовыйКод.Text + " - занят");
            code++;
          }
        } while (Занят);
        db.Опубликовать(Короткий_номер.Text, СтартовыйКод.Text, me, (SellSectionEnum) CSS.sellSection.id);
      }
      else {
        Console.WriteLine(Автор + "\t" + Произведение + "\t" + maps.First().code);
      }
    }
  }
}