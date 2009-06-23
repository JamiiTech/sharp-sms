using System.Reflection;
using NUnit.Framework;

namespace Goods.Tests {
  /// <summary>
  /// Изучение Reflection API и отправки на e-mail
  /// </summary>
  [TestFixture]
  public class Test_Reflection {
    [Test]
    public void Reflect_and_mail() {
      MethodBase method = MethodBase.GetCurrentMethod();
      Assert.AreEqual("Reflect_and_mail", method.Name);
      Assert.AreEqual("Test_Reflection", method.DeclaringType.Name);
      Assert.AreEqual("#Test_Reflection.Reflect_and_mail", Report.Subject(method));
      Report.MailReport(method, "Test!");
    }
  }
}