using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;
using Goods;
using log4net;

namespace MPS {
  // NOTE: If you change the class name "MessageProcessing" here, you must also update the reference to "MessageProcessing" in Web.config.

  /// <summary>
  /// Обработка всех входящих / генерация исходящих SMS
  /// </summary>
  public class MessageProcessing : IMessageProcessing {
    readonly string conStrMPS; //Инитятся в конструкторе
    readonly string conStrMTS; //Инитятся в конструкторе
    protected ILog log;

    public MessageProcessing() {
      conStrMPS = ConnectionManager.ReadConnStr("MPS.ConnectionString");
      //Properties.Settings.Default.MPSConnectionString;
      conStrMTS = ConnectionManager.ReadConnStr("MTS.ConnectionString");
      log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType.ToString());
    }

    #region IMessageProcessing Members

    /// <summary>
    /// Обработка одной SMS
    /// </summary>
    /// <param name="inSms">Входящая SMS (все параметры)</param>
    /// <returns>Ответные SMS</returns>
    public Sms[] ProcessSms(Sms inSms) {
      Sms[] ret;
      if (inSms.Text.StartsWith("loop")) {
        ret = makeReply(inSms, 1);
        ret[0].Text = inSms.Text;
      }
      else {
        // Перенаправление на PHP по префиксам
        string prefix = PHP_SMS.Get_Prefix(inSms.Text);
        if (prefix != "") {
          PHP_SMS p = PHP_SMS.Get_by_Prefix(prefix);
          if (p!=null) {
            try {
              Log.Write("PHP_SMS", "Обработка префикса " + prefix);
              ret = makeReply(inSms, 1);
              ret[0].Text = GetAnswer(p, inSms.Destination, inSms.Text, inSms.Source);
              Log.Write("PHP_SMS", "Ответ скрипта " + ret[0].Text);
              return ret;
            }
            catch (Exception ex) {
              ret = makeReply(inSms, 1);
              ret[0].Text = ex.Message;
              return ret;
            }
          }
        }

        // Открывается соединение с БД
        var mng = new ConnectionManager(conStrMPS);
        SqlCommand cmdGenerateLink = mng.CreateCommand("ProcessSms");
        cmdGenerateLink.CommandType = CommandType.StoredProcedure;
        cmdGenerateLink.Parameters.AddWithValue("@text", inSms.Text);
        cmdGenerateLink.Parameters.AddWithValue("@sn", inSms.Destination);
        cmdGenerateLink.Parameters.AddWithValue("@idSmppConfiguration", inSms.idSMPPConfiguration);
        cmdGenerateLink.Parameters.Add("@MSISDN", SqlDbType.NVarChar);
        cmdGenerateLink.Parameters["@MSISDN"].Value = inSms.Source;

        mng.Open();
        object o = cmdGenerateLink.ExecuteScalar();
        mng.Close();

        if (DBNull.Value.Equals(o)) {
          ret = makeReply(inSms, 1);
          ret[0].Text = "Неправильный код заказа. Проверьте правильность ввода кода.";
        }
        else {
          ret = makeReply(inSms, 1);
          // ret[0].Text = "Ссылка на заказанный контент придет в следующей sms.";
          //ret[1].Text = "http://d.i-rai.com/?" + ((Guid)o).ToString("N");
          ret[0].Text = o.ToString();
        }
      }
      return ret;
    }

    /// <summary>
    /// Вызов обработчика на PHP
    /// </summary>
    /// <param name="p"></param>
    /// <param name="sn"></param>
    /// <param name="sms_text"></param>
    /// <param name="num"></param>
    /// <returns></returns>
    public static string GetAnswer(PHP_SMS p, string sn, string sms_text, string num) {
      p.Vars.Clear();
      p.Vars.Add("sn", sn);
      p.Vars.Add("sms_text", sms_text );
      p.Vars.Add("num", num);
      return p.UploadValues();
    }

    #endregion

    Sms[] makeReply(Sms inSms, int replyLength) {
      var ret = new Sms[replyLength];
      for (int i = 0; i < replyLength; i++) {
        var sms = new Sms {
                            Source = inSms.Destination,
                            Destination = inSms.Source,
                            DeliverRoute = inSms.DeliverRoute,
                            AddDeliverroute = inSms.AddDeliverroute
                          };
        ret[i] = sms;
      }
      return ret;
    }

    public void PutSmsToOutQueue(Sms sms) {
      try {
        var mng = new ConnectionManager(conStrMTS);
        SqlCommand cmdPut = mng.CreateCommand("PutSmsToOutQueue");
        cmdPut.CommandType = CommandType.StoredProcedure;
        /*
                 * @smppMessageId,
               @idSMPPConfiguration,
               @idStatus, 
               @source, 
               @destination, 
               @text, 
               @priority,
               @deliverroute
                 */
        cmdPut.Parameters.Add("@smppMessageId", SqlDbType.NVarChar);
        cmdPut.Parameters["@smppMessageId"].Value = DBNull.Value;
        cmdPut.Parameters.Add("@idSMPPConfiguration", SqlDbType.NVarChar);
        cmdPut.Parameters["@idSMPPConfiguration"].Value = sms.idSMPPConfiguration.ToString();
        cmdPut.Parameters.AddWithValue("@idStatus", SmsStatus.NotSent);
        cmdPut.Parameters.AddWithValue("@source", "" + sms.Source);
        cmdPut.Parameters.AddWithValue("@destination", "" + sms.Destination);
        cmdPut.Parameters.Add("@text", SqlDbType.NVarChar);
        cmdPut.Parameters["@text"].Value = "" + sms.Text;
        cmdPut.Parameters.AddWithValue("@priority", DBNull.Value);
        cmdPut.Parameters.AddWithValue("@deliverroute", "" + sms.DeliverRoute);

        mng.Open();
        cmdPut.ExecuteNonQuery();
        mng.Close();
      }
      catch (Exception e) {
        throw new Exception("Can't store message in database!", e);
      }
    }
  }
}