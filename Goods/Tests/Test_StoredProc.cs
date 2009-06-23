using System.Data.SqlClient;
using System.Linq;
using NUnit.Framework;
using Rai;

namespace Goods.Tests {
  /// <summary>
  /// Тестирование хранимых процедур в БД RAI
  /// </summary>
  [TestFixture]
  public class Test_StoredProc : Test_RaiDB {
    const string TestVendorName = "TestVendor1"; // Только английские буквы!!!

    /// <summary>
    /// Тестирование поиска / добавления модели телефона
    /// </summary>
    [Test]
    public void AddPModel() {
      /*
INSERT INTO [RussianFix] ([from],[to]) VALUES ('А','A');
INSERT INTO [RussianFix] ([from],[to]) VALUES ('В','B');
INSERT INTO [RussianFix] ([from],[to]) VALUES ('Е','E');
INSERT INTO [RussianFix] ([from],[to]) VALUES ('К','K');
INSERT INTO [RussianFix] ([from],[to]) VALUES ('М','M');
INSERT INTO [RussianFix] ([from],[to]) VALUES ('Н','H');
INSERT INTO [RussianFix] ([from],[to]) VALUES ('О','O');
INSERT INTO [RussianFix] ([from],[to]) VALUES ('Р','P');
INSERT INTO [RussianFix] ([from],[to]) VALUES ('С','C');
INSERT INTO [RussianFix] ([from],[to]) VALUES ('Т','T');
INSERT INTO [RussianFix] ([from],[to]) VALUES ('У','Y');
INSERT INTO [RussianFix] ([from],[to]) VALUES ('Х','X');
 
INSERT INTO [RussianFix] ([from],[to]) VALUES ('а','a');
INSERT INTO [RussianFix] ([from],[to]) VALUES ('е','e');
INSERT INTO [RussianFix] ([from],[to]) VALUES ('к','k');
INSERT INTO [RussianFix] ([from],[to]) VALUES ('м','m');
INSERT INTO [RussianFix] ([from],[to]) VALUES ('о','o');
INSERT INTO [RussianFix] ([from],[to]) VALUES ('р','p');
INSERT INTO [RussianFix] ([from],[to]) VALUES ('с','c');
INSERT INTO [RussianFix] ([from],[to]) VALUES ('у','y');
INSERT INTO [RussianFix] ([from],[to]) VALUES ('х','x');
       */

      Assert.AreEqual(2699, db.AddPModel("Motorola", "C360"));
      // Проверяем, что пробелы ни на что не влияют
      Assert.AreEqual(257, db.AddPModel(" Motorola ", " C380 "));
      Assert.AreEqual(257, db.AddPModel(" Motorola ", " С380 ")); // С - русская
    }

    /// <summary>
    /// Тестирование поиска / добавления производителя телефонов
    /// </summary>
    [Test]
    public void AddPVendor() {
      Assert.AreEqual(8, db.AddPVendor("Motorola"));
      Assert.AreEqual(9, db.AddPVendor("Nоkia")); // Вторая "о" - русская
      Assert.AreEqual(8, db.AddPVendor("Мotorola")); // Первая М - русская
      Assert.AreEqual(9, db.AddPVendor("Nokia"));
      Assert.AreEqual(8, db.AddPVendor(" Motorola "));
      Assert.AreEqual(9, db.AddPVendor(" Nokia  \r\n".Trim()));
      int idVendor = db.AddPVendor(TestVendorName);
      Assert.AreEqual(TestVendorName, db.PVendors.SingleOrDefault(t => t.id == idVendor).name);
    }

    [Test]
    public void AddItemToBasket() {
      AddItemError("", "", 1, "Не задан короткий номер!");
      AddItemError(null, null, 1, "Не задан короткий номер!");
      AddItemError("33", "", 1, "Не задан код медиаэлемента!");
      AddItemError("44", null, 1, "Не задан код медиаэлемента!");
      //      var r = db.Test();
//      Assert.AreEqual( "11", r.ReturnValue );
    }

    void AddItemError(string sn, string text, int baskets, string expected) {
      try {
        db.AddItemToBasket(sn, text, baskets);
        Assert.Fail("Должно быть исключение \""+expected+"\"");
      } catch(SqlException ex) {
        Assert.AreEqual(expected,ex.Message);
      }
    }

    [Test]
    public void getPModelByUA(){
      var r1 = db.getPModelByUA("Nokia3109c/4.0 (compatible; MSIE 7.0; Windows NT 6.0; S").FirstOrDefault();
      Assert.AreEqual("3109", r1.model);
      Assert.AreEqual("Nokia", r1.vendor);
      Assert.AreEqual(1168, r1.idModel);
      Assert.AreEqual(9, r1.idVendor);

      //   var res = db.getPModelByUA("Mozilla/5.0 (Windows; U; Windows NT 6.0; en-US; rv:1.9.0.4) Gecko/2008102920 Firefox/3.0.4 (.NET CLR 3.5.30729)").FirstOrDefault();
//      Assert.AreEqual("!!!", r1.model);
    }
  }
}