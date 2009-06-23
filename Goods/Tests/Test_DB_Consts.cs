using System.Data.Linq;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using Rai;
using Raidb;

namespace Goods.Tests {
  /// <summary>
  /// Сверка констант с реальными значениями в БД
  /// </summary>
  [TestFixture]
  public class Test_DB_Consts {
    static void CheckValue<T>(string name, int value) {
      FieldInfo constant = typeof (T).GetField(name);
      Assert.IsNotNull(constant,
                       "Нужно добавить константу в enum " + typeof (T).Name + ": " + name + " = " + value + ";");
      Assert.AreEqual(value, constant.GetRawConstantValue(), name + " = " + value + ";");
    }

    [Test]
    public void BasketCollections() {
      using (var db = new DbDataContext()) {
        // Выбираю все значения из таблицы в базе 
        foreach (BasketCollection x in db.BasketCollections)
          CheckValue<BasketCollectionEnum>(x.name, x.id);
      }
    }

    [Test]
    public void MeProperty() {
      using (var db = new DbDataContext()) {
        // Выбираю все значения из таблицы в базе 
        foreach (MeProperty x in db.MeProperties)
          CheckValue<MePropertyEnum>(x.name, x.id);
      }
    }

    [Test]
    public void MeTypes() {
      using (var db = new DbDataContext()) {
        // Выбираем все значения из таблицы в БД и проверяем, что они есть в enum и что значения совпадают
        foreach (MeType x in db.MeTypes)
          CheckValue<MeTypeEnum>(x.name, x.id);
        // Выбираем все значения из enum'а и ищем их в БД, если такого нет => рекомендуем удалить поле
        foreach (FieldInfo fieldInfo in typeof (MeTypeEnum).GetFields()) {
          if (fieldInfo.Name == "value__")
            continue;
          Assert.IsNotNull(db.MeTypes.SingleOrDefault(t => t.name == fieldInfo.Name), "Name = " + fieldInfo.Name);
        }
      }
    }

    [Test]
    public void SellChannels() {
      using (var db = new DbDataContext()) {
        // Проверяем наличие в enum всех значений из таблицы в БД 
        Table<SellChannel> table = db.GetTable<SellChannel>();
        foreach (SellChannel x in table)
          CheckValue<SellChannelEnum>(x.name, x.id);
        // Выбираем все значения из enum'а и ищем их в БД, если такого нет => рекомендуем удалить поле
        foreach (FieldInfo field in typeof (SellChannelEnum).GetFields()) {
          SellChannel channel = table.SingleOrDefault(t => t.name == field.Name);
          if (field.Name == "value__")
            continue;
          Assert.IsNotNull(channel, "Удалите " + field.Name + " - его нет в БД");
          Assert.AreEqual(channel.name, field.Name, "Удалите " + field.Name + " - имя в БД другое");
        }
      }
    }

    [Test]
    public void SellTypes() {
      using (var db = new DbDataContext()) {
        // Выбираю все значения из таблицы в базе 
        foreach (SellType x in db.SellTypes)
          CheckValue<SellTypeEnum>(x.name, x.id);
        // Выбираем все значения из enum'а и ищем их в БД, если такого нет => рекомендуем удалить поле
        foreach (FieldInfo field in typeof (SellTypeEnum).GetFields()) {
          if (field.Name == "value__")
            continue;
          SellType sellType = db.SellTypes.SingleOrDefault(t => t.name == field.Name);
          Assert.IsNotNull(sellType, "Удалите " + field.Name + " - его нет в БД");
          Assert.AreEqual(sellType.name, field.Name, "Удалите " + field.Name + " - имя в БД другое");
        }
      }
    }
  }
}