using System;
using System.Linq;
using System.Windows.Forms;
using Goods;
using Rai;
using Raidb;

namespace Manager {
  public partial class CreateNewSMSSeriesForm : Form {
    /// <summary>
    /// Ссылка на контекст Базы Данных, в котором мы сейчас работаем
    /// </summary>
    readonly DbDataContext db;

    public CreateNewSMSSeriesForm() : this(new DbDataContext()) {
    }

    public CreateNewSMSSeriesForm(DbDataContext db) {
      this.db = db;

      InitializeComponent();

      Error.SetError(newSeriesTitle, "Введите имя новой серии");
      newSeriesTitle.Focus();

      chooseSellSection.LoadData(db);

      SN_Code.SN.TextChanged += UpdateControlsState;
      SN_Code.Code.TextChanged += UpdateControlsState;

      UpdateControlsState();
    }

    /// <summary>
    /// Код введён
    /// </summary>
    bool Code_Entered {
      get { return SN_Code.Code.Text != ""; }
    }

    /// <summary>
    /// Короткий номер выбран
    /// </summary>
    bool SN_Entered {
      get { return SN_Code.SN.Text != ""; }
    }

    /// <summary>
    /// Введено ли имя для новой серии?
    /// </summary>
    bool NewSeriesTitle_Entered {
      get { return newSeriesTitle.Text != ""; }
    }

    /// <summary>
    /// Обновить состояние контролов
    /// </summary>
    void UpdateControlsState() {
      // Проверяем, что введено имя новой серии
      if (NewSeriesTitle_Entered)
        Error.SetError(newSeriesTitle, "");
      else Error.SetError(newSeriesTitle, "Введите имя новой серии");
      // Проверяем, что выбран короткий номер
      if (SN_Entered)
        Error.SetError(SN_Code.SN, "");
      else Error.SetError(SN_Code.SN, "Выберите короткий номер");
      // Проверяем, что введён код
      if (Code_Entered)
        Error.SetError(SN_Code.Code, "");
      else Error.SetError(SN_Code.Code, "Введите код");
      // Если введёно всё, то разрешаем создание новой серии с заданными параметрами
      createNewSeries.Enabled = NewSeriesTitle_Entered && SN_Entered && Code_Entered;
    }


    /// <summary>
    /// Создать новую серию по введённым в форму параметрам
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void CreateSeries_Click(object sender, EventArgs e) {
      // Внутри обработчика вызываем метод, который, собственно, всё и делает
      try {
        CreateNewSeries();
        MessageBox.Show("Серия \"" + newSeriesTitle.Text + "\" создана и опубликована на " +
                        "короткий номер " + SN_Code.SN.Text + " с кодом " + SN_Code.Code.Text,
                        "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
        Close();
      }
      catch (Exception ex) {
        MessageBox.Show(ex.Message, "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    /// <summary>
    /// Создаём новую серию по заполненным в форме данным
    /// </summary>
    public void CreateNewSeries() {
      // Проверяем, что корректно заполнены все поля

      // Имя серии
      if (!NewSeriesTitle_Entered)
        throw new Exception("Не задано имя серии");

      // Проверяем, что серии с такими именем ещё нет в Базе Данных
      MePropValue propValue = db.MePropValues.SingleOrDefault(
        t => t.Me.idMeType == (int) MeTypeEnum.RandomText &&
             t.idProp == (int) MePropertyEnum.title &&
             t.sVal == newSeriesTitle.Text
        );
      if (propValue != null) throw new Exception("Серия с именем \"" + newSeriesTitle.Text + "\" уже есть!");

      // Проверяем, что заполнены SellChannel и SellSection

      // Проверяем имя серии (ищем его в БД)
      new Серия_SMS(db, newSeriesTitle.Text, SN_Code.SN.Text, SN_Code.Code.Text,
                    (int) chooseSellSection.SelectSellSection.SelectedValue
        );
    }

    void Cancel_Click(object sender, EventArgs e) {
      Close();
    }

    void UpdateControlsState(object sender, EventArgs e) {
      UpdateControlsState();
    }
  }
}