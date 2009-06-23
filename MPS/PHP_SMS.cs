using System;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using Rai;
using Raidb;

namespace MPS {
  /// <summary>
  /// Обработка SMS путём вызова обработчиков на PHP
  /// </summary>
  public class PHP_SMS {
    /// <summary>
    /// Метод отправки данных PHP скрипту
    /// </summary>
    const string METHOD = "POST";

    /// <summary>
    /// Кодировка в которой работает PHP скрипт (UTF-8, Windows-1251 или ещё какая)
    /// </summary>
    public Encoding Encoding;

    /// <summary>
    /// URL страница-обработчика (вызывается для обработки SMS)
    /// </summary>
    public string URL;

    /// <summary>
    /// Переменные для передачи в POST запрос на PHP страницу
    /// </summary>
    public NameValueCollection Vars = new NameValueCollection();

    /// <summary>
    /// Получить URL обработчика из БД по префиксу
    /// </summary>
    /// <param name="prefix">Префикс SMS</param>
    /// <returns></returns>
    public static PHP_SMS Get_by_Prefix(string prefix) {
      using (var db = new DbDataContext()) {
        PHP_SM handler = db.PHP_SMs.SingleOrDefault(t => t.prefix == prefix);
        if (handler == null)
          return null;
        return new PHP_SMS {URL = handler.URL, Encoding = Encoding.GetEncoding(handler.Encoding.Trim())};
      }
    }

    /// <summary>
    /// Выполнение POST запроса
    /// </summary>
    /// <returns>Результат запроса</returns>
    public string doPost() {
      var wc = new WebClient {BaseAddress = URL, Encoding = Encoding};
      return Encoding.GetString(wc.UploadValues(URL, METHOD, Vars));
    }

    public string UploadValues() {
      var builder = new StringBuilder();
      foreach (string name in Vars) {
        string encodedName = HttpUtility.UrlEncode(name, Encoding);
        string encodedValue = HttpUtility.UrlEncode(Vars[name], Encoding);
        builder.Append(encodedName);
        builder.Append('=');
        builder.Append(encodedValue);
        builder.Append('&');
      }
      builder.Remove(builder.Length - 1, 1);
      byte[] bytes = Encoding.ASCII.GetBytes(builder.ToString());
      var client = new WebClient();
      client.Headers["Content-Type"] = String.Format("application/x-www-form-urlencoded; charset={0}",
                                                     Encoding.HeaderName);
      return Encoding.GetString(client.UploadData(URL, METHOD, bytes));
    }

    public static string Get_Prefix(string smsText) {
      string s = smsText.Trim();
      // Разбиваем строку по разделителям
      string[] d = s.Split(' ', '\t', (char) 13, (char) 10);
      return d[0];
    }
  }
}