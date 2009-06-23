using NUnit.Framework;
using Rai;

namespace Goods.Tests {
  /// <summary>
  /// Базовый класс для Unit-тестов с базой данных RAI
  /// </summary>
  public class Test_RaiDB {
    protected DbDataContext db;

    /// <summary>
    /// Перед выполнением каждого теста открывается соединение с БД и начинается транзакция
    /// </summary>
    [SetUp]
    public void Setup() {
      db = new DbDataContext();
      db.Connection.Open();
      db.Transaction = db.Connection.BeginTransaction();
    }

    /// <summary>
    /// После выполнения каждого теста откатывается транзакция (чтобы не сохранять изменения внесённые тестом)
    /// и закрывается соединение с БД
    /// </summary>
    [TearDown]
    public void TearDown() {
      db.Transaction.Rollback();
      db.Connection.Close();
      db.Dispose();
    }
  }
}