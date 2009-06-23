using System;
using System.Data;
using System.Linq;
using FMK;
using Goods;
using Rai;
using Raidb;

namespace CDS {
  /// <summary>
  /// Один элемент в корзине
  /// </summary>
  public class BasketItem {
    int id;

    int idAbonentBasket;

    public int idMe;
    bool isPurchased;
    bool isRetrieved;
    decimal price;

    SellTypeEnum sellType;

    /// <summary>
    /// Creates new BasketItem in database
    /// </summary>        
    public BasketItem(int idAbonentBasket, int idMe, SellTypeEnum sellType, decimal price) {
      this.idAbonentBasket = idAbonentBasket;
      this.idMe = idMe;
      this.sellType = sellType;
      this.price = price;

      using (var db = new DbDataContext()) {
        var bi = new Raidb.BasketItem {
                                        idAbonentBaskets = idAbonentBasket,
                                        idMe = idMe,
                                        idSellType = ((int) sellType),
                                        isPurchased = false,
                                        isRetrieved = false,
                                        price = price,
                                        dateModified = DateTime.Now,
                                        datePurchased = DateTime.Now
                                      };
        db.BasketItems.InsertOnSubmit(bi);
        db.SubmitChanges();
        id = bi.id;
      }
    }

    public BasketItem(Raidb.BasketItem bi) {
      id = bi.id;
      LoadFromDbBasketItem(bi);
    }

    public int Id {
      get { return id; }
    }

    public int IdAbonentBasket {
      get { return idAbonentBasket; }
      set {
        if (idAbonentBasket == value)
          return;
        idAbonentBasket = value;
      }
    }

    public int IdMe {
      get { return idMe; }
      set {
        if (idMe == value)
          return;
        idMe = value;
      }
    }

    public SellTypeEnum SellType {
      get { return sellType; }
      set {
        if (sellType == value)
          return;
        sellType = value;
      }
    }

    public bool IsPurchased {
      get { return isPurchased; }
      set {
        if (isPurchased == value)
          return;
        isPurchased = value;
        using (var db = new DbDataContext()) {
          Raidb.BasketItem o = db.BasketItems.SingleOrDefault(t => t.id == id);
          if (null != o)
            o.isPurchased = value;
          db.SubmitChanges();
        }
      }
    }

    public bool IsRetrieved {
      get { return isRetrieved; }
      set {
        if (isRetrieved == value)
          return;
        isRetrieved = value;
        using (var db = new DbDataContext()) {
          Raidb.BasketItem o = db.BasketItems.SingleOrDefault(t => t.id == id);
          if (null != o)
            o.isRetrieved = value;
          db.SubmitChanges();
        }
      }
    }

    public decimal Price {
      get { return price; }
      set {
        price = value;
        using (var db = new DbDataContext()) {
          db.BasketItems.Single(t => t.idAbonentBaskets == idAbonentBasket).price = value;
          db.SubmitChanges();
        }
      }
    }

    public string fmk_id {
      get {
        using (var db = new DbDataContext()) {
          MePropValue oId = db.MePropValues.SingleOrDefault(t => t.idMe == idMe && t.idProp == (int) MePropertyEnum.fmk_id);
          return null != oId ? oId.sVal : string.Empty;
        }
      }
    }

    public string fmk_link {
      get {
        using (var db = new DbDataContext()) {
          Raidb.BasketItem o = db.BasketItems.SingleOrDefault(t => t.id == id);
          return null != o ? o.fmk_link : string.Empty;
        }
      }

      set {
        using (var db = new DbDataContext()) {
          Raidb.BasketItem o = db.BasketItems.SingleOrDefault(t => t.id == id);
          o.fmk_link = value;
          db.SubmitChanges();
        }
      }
    }

    public string Info {
      get {
        //if (this.sellType == SellType.JavaApp)
        string sTitle = string.Empty;
        using (var db = new DbDataContext()) {
          //var oIdPropTitle = db.MeProperties.SingleOrDefault(t => t.name == "title");
          //if (null != oIdPropTitle)
          MePropValue oTitle = db.MePropValues.SingleOrDefault(t => t.idMe == idMe && t.idProp == (int) MePropertyEnum.title);
          if (null != oTitle)
            sTitle = oTitle.sVal;

          if (string.Empty == sTitle) {
            string sSinger = (from sPropValues in db.MePropValues
                              where sPropValues.idMe == idMe && sPropValues.idProp == (int) MePropertyEnum.singer
                              select sPropValues.sVal).SingleOrDefault();
            string sSong = (from sPropValues in db.MePropValues
                            where sPropValues.idMe == idMe && sPropValues.idProp == (int) MePropertyEnum.song
                            select sPropValues.sVal).SingleOrDefault();

            sTitle = string.Format("{0} - {1}", sSinger, sSong);
          }
        }

        if (string.Empty != sTitle)
          return sTitle;

        return string.Format("IdAbonentBasket: {0}, idMe {1}, sellType: {2}",
                             idAbonentBasket,
                             IdMe,
                             sellType);
      }
    }

    /// <summary>
    /// Файлы для заданного элемента в корзине
    /// </summary>
    public DataTable Files {
      get {
        using (var db = new DbDataContext()) {
          var dbFiles = from meFile in db.MeFiles
                        where meFile.idMe == idMe
                        select new {
                                     meFile.filename,
                                     meFile.MeFileType.name
                                   };
          var ret = new DataTable();
          ret.Columns.Add("filename");
          ret.Columns.Add("type");
          foreach (var item in dbFiles)
            ret.Rows.Add(item.filename, item.name);
          return ret;
        }
      }
    }

    public string GetNewFMKlink() {
      string fmkLink = string.Empty;
      if (string.Empty != fmk_id) {
        var fmkFile = new FMKFile();
        fmkLink = fmkFile.RequestFile(fmk_id);
        fmk_link = fmkLink;
      }
      return fmkLink;
    }

    void LoadFromDbBasketItem(Raidb.BasketItem bi) {
      idMe = bi.idMe;
      SellType = (SellTypeEnum) Enum.ToObject(typeof (SellTypeEnum), bi.idSellType);
      idAbonentBasket = bi.idAbonentBaskets;
      isPurchased = bi.isPurchased;
      isRetrieved = bi.isRetrieved;
      price = bi.price;
      id = bi.id;
    }

    void Reload() {
      using (var db = new DbDataContext()) {
        Raidb.BasketItem bi = db.BasketItems.SingleOrDefault(t => t.id == id);
        if (null != bi)
          LoadFromDbBasketItem(bi);
      }
    }

    public void Delete() {
      using (var db = new DbDataContext()) {
        Raidb.BasketItem bi = db.BasketItems.SingleOrDefault(t => t.id == id);
        if (null != bi) {
          db.BasketItems.DeleteOnSubmit(bi);
          db.SubmitChanges();
        }
      }
    }

    public void Buy() {
      try {
        using (var db = new DbDataContext()) {
          AbonentBasket basket = db.AbonentBaskets.SingleOrDefault(t => t.id == IdAbonentBasket);
          if (null != basket)
            if (!(basket.balans < Price)) {
              db.BuyBasketItem(id, idAbonentBasket);
              Reload();
            }
        }
      }
      catch {
      }
      ;
    }

    public bool CheckFilename(string filename) {
      throw new NotImplementedException("Not tested");
      using (var db = new DbDataContext()) {
        MeFile meFile = db.MeFiles.SingleOrDefault(
          t => t.id == id && t.filename == filename);
        return (meFile != null);
      }
    }

    public string GetFullFileName(string filename) {
      using (var db = new DbDataContext()) {
        MeFile meFile = db.MeFiles.SingleOrDefault(
          t => t.idMe == idMe && t.filename == filename);
        if (meFile == null)
          return string.Empty;
        return FileStorage.GetFullFileName(meFile.id);
      }
    }
  }
}