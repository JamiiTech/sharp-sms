using Goods.Tests;
using NUnit.Framework;

namespace Manager.Tests {
  /// <summary>
  /// Тестирование возможностей формы по добавлению SMS
  /// </summary>
  [TestFixture]
  public class SMS_Series_Form_Test : Test_RaiDB {
    /// <summary>
    /// Создание новой серии
    /// </summary>
    [Test]
    public void CreateNewSeries() {
      var form = new CreateNewSMSSeriesForm(db);

      // Нельзя создать новый
      Assert.IsFalse(form.createNewSeries.Enabled);

      // Создаём новую пустую серию SMS
      // Для этого нужно ввести имя
      Assert.IsFalse(form.createNewSeries.Enabled);
      form.newSeriesTitle.Text = "Новая тестовая серия";
      Assert.IsTrue(form.createNewSeries.Enabled);
      form.CreateNewSeries();

      Assert.IsFalse(form.createNewSeries.Enabled);
    }

    [Test]
    public void testForm() {
      var form = new SMS_Series_Form(db);
      Assert.AreEqual("Серии текстовых SMS", form.Text);

      // Сначала в форме ничего не введено
      Assert.IsNull(form.map);
      // Нельзя удалить пустой map
      Assert.IsFalse(form.DeleteMap.Enabled);

      // Надо ввести короткий номер
      form.SN_Code.SN.Text = "5353";

      // Затем выбрать код
      form.SN_Code.Code.Text = "666";

      // Теперь Map не null
      Assert.IsNotNull(form.map);

      // Теперь Map можно удалить
      Assert.IsTrue(form.DeleteMap.Enabled);

      // Удаляем Map (не волнуйтесь! По завершении теста все изменения откатываются!)
      form.DeleteSelectedMap();

      // Теперь Map-удалён
      Assert.IsNull(form.map);

      // Теперь нельзя удалить его, но можно создать новый с тем же коротким номером и кодом
      Assert.IsFalse(form.DeleteMap.Enabled);


      // Теперь её можно удалить, но нельзя сразу же создать такую же
      Assert.IsTrue(form.DeleteMap.Enabled);
    }
  }
}