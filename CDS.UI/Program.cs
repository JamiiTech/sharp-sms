using System;
using System.Windows.Forms;
using CDS.UI.Properties;

namespace CDS.UI {
  static class Program {
    /// <summary>
    /// Основная программа
    /// </summary>
    [STAThread]
    static void Main() {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new MainForm()); // Открываем главную форму
      Settings.Default.Save(); // Сохраняем все настройки
    }
  }
}