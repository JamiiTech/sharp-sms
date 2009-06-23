using System;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Text;

namespace Goods {
  public class Report {
    /// <summary>
    /// Отправка отчёта по почте через mail.ru
    /// Для отправки используется e-mail: stdenbot@mail.ru
    /// </summary>
    /// <param name="subject">Тема письма</param>
    /// <param name="body">Содержание письма</param>
    public static void MailReport(string subject, string body) {
      try {
        const string логин = "stdenbot";
        const string пароль = "gbplfceifvb";
        var Message = new MailMessage {
                                        Subject = subject + " (" + Environment.MachineName + ")",
                                        Body = body,
                                        BodyEncoding = Encoding.UTF8,
                                        //.GetEncoding("Windows-1251"),
                                        From = new MailAddress(логин + "@mail.ru") // мейл отправителя
                                      };
        Message.To.Add(new MailAddress("super.denis@gmail.com")); // мейл получателя
        Message.To.Add(new MailAddress("prgcscmyline@mail.ru"));
        var Smtp = new SmtpClient {
                                    Host = "smtp.mail.ru",
                                    //                                  Port = 465,
                                    //                                  EnableSsl = true,
                                    Credentials = new NetworkCredential(логин, пароль)
                                  };
        //Smtp.Host = "хост или IP адрес"; //например для gmail (smtp.gmail.com), mail.ru(smtp.mail.ru)
        Smtp.Send(Message); //отправка
      }
      catch (Exception ex) {
        Console.WriteLine("Не удалось отправить e-mail: " + ex.Message);
      }
    }

    public static string Subject(MethodBase method) {
      return "#"+method.DeclaringType.Name + "." + method.Name;
    }

    public static void MailReport(MethodBase method, string body)
    {
      MailReport(Subject(method),body);
    }
  }
}