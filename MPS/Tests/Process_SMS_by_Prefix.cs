using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Goods.Tests;
using NUnit.Framework;

namespace MPS.Tests
{
  /// <summary>
  /// Тестирование получения SMS по коду с префиксом
  /// </summary>
  [TestFixture]
  public class Process_SMS_by_Prefix : Test_RaiDB
  {
    /// <summary>
    /// Просто получаем SMS без префикса
    /// </summary>
    [Test]
    public void Without_Prefix() {
      var sms = db.ProcessSms("5353", "666", 6, "").ToList();
      Assert.AreEqual( 0, db.ProcessSms("5353", "666", 6, "").ReturnValue );
      Assert.IsTrue(sms[0].sVal.StartsWith("http://wap.i-rai.com/fortune/manara_main/?x="));

      var sms2 = db.ProcessSms("5353", "666 test", 6, "").ToList();
      Assert.IsTrue(sms2[0].sVal.StartsWith("http://wap.i-rai.com/fortune/manara_main/?x="));
    }

    /// <summary>
    /// Создаём и получаем SMS с префиксом
    /// </summary>
    [Test]
    public void With_Prefix() {

    }

  }
}
