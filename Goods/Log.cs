using System;
using log4net;
using log4net.Core;
using Rai;

namespace Goods {
  /// <summary>
  /// Запись отладочных сообщений в файлы, на экран (через log4net) и в базу данных
  /// </summary>
  public class Log {
    public static string Program;

    public static void Write(string type, string text) {
      using (var db = new DbDataContext())
        db.Log(Program, type, text);
    }

    public static void Write(string type, string format, params object[] args) {
      Write(type, string.Format(format, args));
    }

    public static ILog GetLogger(Type type) {
      return new MyLogger(LogManager.GetLogger(type));
    }

    public static void Write(string type, IFormatProvider provider, string format, object[] args) {
      Write(type, string.Format(format, args, provider));
    }

    public static ILog GetLogger(string s) {
      return new MyLogger(LogManager.GetLogger(s));
    }
  }

  class MyLogger : ILog {
    const string DEBUG = "Debug";
    const string ERROR = "Error";
    const string FATAL = "Fatal";
    const string INFO = "Info";
    const string WARN = "Warn";

    readonly ILog logger;

    public MyLogger(ILog logger) {
      this.logger = logger;
    }

    #region ILog Members

    public ILogger Logger {
      get { return logger.Logger; }
    }

    public bool IsDebugEnabled {
      get { return logger.IsDebugEnabled; }
    }

    public bool IsInfoEnabled {
      get { return logger.IsInfoEnabled; }
    }

    public bool IsWarnEnabled {
      get { return logger.IsWarnEnabled; }
    }

    public bool IsErrorEnabled {
      get { return logger.IsErrorEnabled; }
    }

    public bool IsFatalEnabled {
      get { return logger.IsFatalEnabled; }
    }

    public void Debug(object message) {
      Log.Write(DEBUG, message.ToString());
      logger.Debug(message);
    }

    public void Debug(object message, Exception exception) {
      Log.Write(DEBUG, message + ExceptionString(exception));
      logger.Debug(message, exception);
    }

    public void DebugFormat(string format, params object[] args) {
      Log.Write(DEBUG, format, args);
      logger.DebugFormat(format, args);
    }

    public void DebugFormat(string format, object arg0) {
      Log.Write(DEBUG, format, arg0);
      logger.DebugFormat(format, arg0);
    }

    public void DebugFormat(string format, object arg0, object arg1) {
      Log.Write(DEBUG, format, arg0, arg1);
      logger.DebugFormat(format, arg0, arg1);
    }

    public void DebugFormat(string format, object arg0, object arg1, object arg2) {
      Log.Write(DEBUG, format, arg0, arg1, arg2);
      logger.DebugFormat(format, arg0, arg1, arg2);
    }

    public void DebugFormat(IFormatProvider provider, string format, params object[] args) {
      Log.Write(DEBUG, provider, format, args);
      logger.DebugFormat(provider, format, args);
    }

    public void Info(object message) {
      Log.Write(INFO, message.ToString());
      logger.Info(message);
    }

    public void Info(object message, Exception exception) {
      Log.Write(INFO, message + ExceptionString(exception));
      logger.Info(message, exception);
    }

    public void InfoFormat(string format, params object[] args) {
      Log.Write(INFO, format, args);
      logger.InfoFormat(format, args);
    }

    public void InfoFormat(string format, object arg0) {
      Log.Write(INFO, format, arg0);
      logger.InfoFormat(format, arg0);
    }

    public void InfoFormat(string format, object arg0, object arg1) {
      Log.Write(INFO, format, arg0, arg1);
      logger.InfoFormat(format, arg0, arg1);
    }

    public void InfoFormat(string format, object arg0, object arg1, object arg2) {
      Log.Write(INFO, format, arg0, arg1, arg2);
      logger.InfoFormat(format, arg0, arg1, arg2);
    }

    public void InfoFormat(IFormatProvider provider, string format, params object[] args) {
      Log.Write(INFO, provider, format, args);
      logger.InfoFormat(provider, format, args);
    }

    public void Warn(object message) {
      Log.Write(WARN, message.ToString());
      logger.Warn(message);
    }

    public void Warn(object message, Exception exception) {
      Log.Write(WARN, message + ExceptionString(exception));
      logger.Warn(message, exception);
    }

    public void WarnFormat(string format, params object[] args) {
      Log.Write(WARN, format, args);
      logger.WarnFormat(format, args);
    }

    public void WarnFormat(string format, object arg0) {
      Log.Write(WARN, format, arg0);
      logger.WarnFormat(format, arg0);
    }

    public void WarnFormat(string format, object arg0, object arg1) {
      Log.Write(WARN, format, arg0, arg1);
      logger.WarnFormat(format, arg0, arg1);
    }

    public void WarnFormat(string format, object arg0, object arg1, object arg2) {
      Log.Write(WARN, format, arg0, arg1, arg2);
      logger.WarnFormat(format, arg0, arg1, arg2);
    }

    public void WarnFormat(IFormatProvider provider, string format, params object[] args) {
      Log.Write(WARN, provider, format, args);
      logger.WarnFormat(provider, format, args);
    }

    public void Error(object message) {
      Log.Write(ERROR, message.ToString());
      logger.Error(message);
    }

    public void Error(object message, Exception exception) {
      Log.Write(ERROR, message + ExceptionString(exception));
      logger.Error(message, exception);
    }

    public void ErrorFormat(string format, params object[] args) {
      Log.Write(ERROR, format, args);
      logger.ErrorFormat(format, args);
    }

    public void ErrorFormat(string format, object arg0) {
      Log.Write(ERROR, format, arg0);
      logger.ErrorFormat(format, arg0);
    }

    public void ErrorFormat(string format, object arg0, object arg1) {
      Log.Write(ERROR, format, arg0, arg1);
      logger.ErrorFormat(format, arg0, arg1);
    }

    public void ErrorFormat(string format, object arg0, object arg1, object arg2) {
      Log.Write(ERROR, format, arg0, arg1, arg2);
      logger.ErrorFormat(format, arg0, arg1, arg2);
    }

    public void ErrorFormat(IFormatProvider provider, string format, params object[] args) {
      Log.Write(ERROR, provider, format, args);
      logger.ErrorFormat(provider, format, args);
    }

    public void Fatal(object message) {
      Log.Write(FATAL, message.ToString());
      logger.Fatal(message);
    }

    public void Fatal(object message, Exception exception) {
      Log.Write(FATAL, message + ExceptionString(exception));
      logger.Fatal(message, exception);
    }

    public void FatalFormat(string format, params object[] args) {
      Log.Write(FATAL, format, args);
      logger.FatalFormat(format, args);
    }

    public void FatalFormat(string format, object arg0) {
      Log.Write(FATAL, format, arg0);
      logger.FatalFormat(format, arg0);
    }

    public void FatalFormat(string format, object arg0, object arg1) {
      Log.Write(FATAL, format, arg0, arg1);
      logger.FatalFormat(format, arg0, arg1);
    }

    public void FatalFormat(string format, object arg0, object arg1, object arg2) {
      Log.Write(FATAL, format, arg0, arg1, arg2);
      logger.FatalFormat(format, arg0, arg1, arg2);
    }

    public void FatalFormat(IFormatProvider provider, string format, params object[] args) {
      Log.Write(FATAL, provider, format, args);
      logger.FatalFormat(provider, format, args);
    }

    #endregion

    static string ExceptionString(Exception exception) {
      return "\r\n Exception Message=" + exception.Message + "\r\n" +
             exception.StackTrace;
    }
  }
}