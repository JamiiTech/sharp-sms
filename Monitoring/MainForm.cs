using System;
using System.Linq;
using System.Windows.Forms;
using Rai;

namespace Monitoring {
  public partial class MainForm : Form {
    /// <summary>
    /// Время последнего опроса
    /// </summary>
    DateTime LastTime;

    public MainForm() {
      InitializeComponent();
    }

    /// <summary>
    /// Срабатывание таймера
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void timer_Tick(object sender, EventArgs e) {
      LastTime = DateTime.Now;
      UpdateCaption();

      // Получаем данные о работоспособности программы SMPP
      using (var db = new DbDataContext()) {
        // Получить поcледнюю запись о работоспособности сервера
        var tt = db.Logs.Last( t => t.program == "SRV02" );
        // Записать на форму последнее время, когда программа работала
        // Проверяем, сколько прошло времени с последней записи
        // Если времени прошло много => отправляем e-mail
      }
    }

    /// <summary>
    /// Обновить заголовок формы
    /// </summary>
    void UpdateCaption() {
      Text = "Мониторинг работоспособности системы в целом - " + LastTime;
    }
  }
}