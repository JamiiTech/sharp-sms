using NUnit.Framework;
using Rai;

namespace Goods.Tests {
  /// <summary>
  /// ������� ����� ��� Unit-������ � ����� ������ RAI
  /// </summary>
  public class Test_RaiDB {
    protected DbDataContext db;

    /// <summary>
    /// ����� ����������� ������� ����� ����������� ���������� � �� � ���������� ����������
    /// </summary>
    [SetUp]
    public void Setup() {
      db = new DbDataContext();
      db.Connection.Open();
      db.Transaction = db.Connection.BeginTransaction();
    }

    /// <summary>
    /// ����� ���������� ������� ����� ������������ ���������� (����� �� ��������� ��������� �������� ������)
    /// � ����������� ���������� � ��
    /// </summary>
    [TearDown]
    public void TearDown() {
      db.Transaction.Rollback();
      db.Connection.Close();
      db.Dispose();
    }
  }
}