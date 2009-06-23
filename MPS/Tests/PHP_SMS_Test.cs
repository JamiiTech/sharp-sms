using System.Text;
using NUnit.Framework;

namespace MPS.Tests {
  [TestFixture]
  public class PHP_SMS_Test {
    /// <summary>
    /// Передача параметров в POST-запросе и получение ответа
    /// </summary>
    [Test]
    public void doPost() {
      Assert.AreEqual("utf-8", Encoding.UTF8.HeaderName);
      Assert.AreEqual(Encoding.UTF8, Encoding.GetEncoding(Encoding.UTF8.HeaderName));
      Encoding e = Encoding.GetEncoding("Windows-1251");
      var p = new PHP_SMS();
      p.Vars["sn"] = "1";
      p.Vars["num"] = "<br>";
      p.Vars["sms_text"] = "Привет";
      p.URL = "http://www.i-rai.com:83/test.php";
      p.Encoding = Encoding.UTF8;
      Assert.AreEqual("Привет мир!  sn = 1  num = " + p.Vars["num"] + "  sms_text = " + p.Vars["sms_text"],
                      p.doPost());

      // Испытываем скрипт index.php в кодировке Windows-1251
      var p1 = PHP_SMS.Get_by_Prefix("index");
      Assert.AreEqual("Привет, мир! Windows-1251 sn = 4141  num = 79117117850  sms_text = " + "Привет", 
        MessageProcessing.GetAnswer(p1, "4141", "Привет", "79117117850"));

      PHP_SMS pp = PHP_SMS.Get_by_Prefix("supsex");
      // Assert.IsTrue(MessageProcessing.GetAnswer(pp, "4141", "test", "79117117850").StartsWith("Пароль: "));
    //  Assert.AreEqual("", MessageProcessing.GetAnswer(pp, "4141", "test", "79117117850"));

      PHP_SMS p2 = PHP_SMS.Get_by_Prefix("test");
  //    Assert.AreEqual("", MessageProcessing.GetAnswer(p2, "4141", "test", "79117117850"));
      Assert.IsTrue(MessageProcessing.GetAnswer(p2, "4141", "test", "79117117850").StartsWith("\nhttp://test.i-rai.com/psi1.php?pass="));

    }

    /// <summary>
    /// Получение URL по префиксу
    /// </summary>
    [Test]
    public void find_by_Prefix() {
      Assert.AreEqual("http://www.i-rai.com:83/test.php", PHP_SMS.Get_by_Prefix("test_denis").URL);
      Assert.AreEqual("http://supsex.ru/get_sms.php", PHP_SMS.Get_by_Prefix("supsex").URL);
      Assert.AreEqual("http://supsex.ru/get_sms.php", PHP_SMS.Get_by_Prefix("Supsex").URL);
      Assert.AreEqual("http://supsex.ru/get_sms.php", PHP_SMS.Get_by_Prefix("SUPSEX").URL);
      Assert.AreEqual("http://test.i-rai.com/get_sms.php", PHP_SMS.Get_by_Prefix("test").URL);
      Assert.IsNull(PHP_SMS.Get_by_Prefix("Несуществующий_префикс"));
    }

    /// <summary>
    /// Получить префикс
    /// </summary>
    [Test]
    public void get_Prefix() {
      Assert.AreEqual("AA", PHP_SMS.Get_Prefix("AA 1"));
      Assert.AreEqual("AB", PHP_SMS.Get_Prefix("AB"));
      Assert.AreEqual("BB", PHP_SMS.Get_Prefix(" BB\t\r\n2"));
      Assert.AreEqual("BB", PHP_SMS.Get_Prefix("\tBB\t\r\n2"));
      Assert.AreEqual("", PHP_SMS.Get_Prefix(""));
      Assert.AreEqual("", PHP_SMS.Get_Prefix("   "));
      Assert.AreEqual("", PHP_SMS.Get_Prefix(" \r\n  \t  "));
    }
  }
}