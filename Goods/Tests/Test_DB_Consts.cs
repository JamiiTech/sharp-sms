using System.Data.Linq;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using Rai;
using Raidb;

namespace Goods.Tests {
  /// <summary>
  /// ������ �������� � ��������� ���������� � ��
  /// </summary>
  [TestFixture]
  public class Test_DB_Consts {
    static void CheckValue<T>(string name, int value) {
      FieldInfo constant = typeof (T).GetField(name);
      Assert.IsNotNull(constant,
                       "����� �������� ��������� � enum " + typeof (T).Name + ": " + name + " = " + value + ";");
      Assert.AreEqual(value, constant.GetRawConstantValue(), name + " = " + value + ";");
    }

    [Test]
    public void BasketCollections() {
      using (var db = new DbDataContext()) {
        // ������� ��� �������� �� ������� � ���� 
        foreach (BasketCollection x in db.BasketCollections)
          CheckValue<BasketCollectionEnum>(x.name, x.id);
      }
    }

    [Test]
    public void MeProperty() {
      using (var db = new DbDataContext()) {
        // ������� ��� �������� �� ������� � ���� 
        foreach (MeProperty x in db.MeProperties)
          CheckValue<MePropertyEnum>(x.name, x.id);
      }
    }

    [Test]
    public void MeTypes() {
      using (var db = new DbDataContext()) {
        // �������� ��� �������� �� ������� � �� � ���������, ��� ��� ���� � enum � ��� �������� ���������
        foreach (MeType x in db.MeTypes)
          CheckValue<MeTypeEnum>(x.name, x.id);
        // �������� ��� �������� �� enum'� � ���� �� � ��, ���� ������ ��� => ����������� ������� ����
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
        // ��������� ������� � enum ���� �������� �� ������� � �� 
        Table<SellChannel> table = db.GetTable<SellChannel>();
        foreach (SellChannel x in table)
          CheckValue<SellChannelEnum>(x.name, x.id);
        // �������� ��� �������� �� enum'� � ���� �� � ��, ���� ������ ��� => ����������� ������� ����
        foreach (FieldInfo field in typeof (SellChannelEnum).GetFields()) {
          SellChannel channel = table.SingleOrDefault(t => t.name == field.Name);
          if (field.Name == "value__")
            continue;
          Assert.IsNotNull(channel, "������� " + field.Name + " - ��� ��� � ��");
          Assert.AreEqual(channel.name, field.Name, "������� " + field.Name + " - ��� � �� ������");
        }
      }
    }

    [Test]
    public void SellTypes() {
      using (var db = new DbDataContext()) {
        // ������� ��� �������� �� ������� � ���� 
        foreach (SellType x in db.SellTypes)
          CheckValue<SellTypeEnum>(x.name, x.id);
        // �������� ��� �������� �� enum'� � ���� �� � ��, ���� ������ ��� => ����������� ������� ����
        foreach (FieldInfo field in typeof (SellTypeEnum).GetFields()) {
          if (field.Name == "value__")
            continue;
          SellType sellType = db.SellTypes.SingleOrDefault(t => t.name == field.Name);
          Assert.IsNotNull(sellType, "������� " + field.Name + " - ��� ��� � ��");
          Assert.AreEqual(sellType.name, field.Name, "������� " + field.Name + " - ��� � �� ������");
        }
      }
    }
  }
}