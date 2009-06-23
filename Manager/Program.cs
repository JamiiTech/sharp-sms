using System;
using System.Windows.Forms;
using Manager.Properties;

namespace Manager {
  static class Program {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main() {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new SMS_Series_Form());
      Settings.Default.Save(); // Сохраняем все настройки
    }
  }
}