using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Goods;
using NUnit.Framework;
using Rai;
using Raidb;
using Me=Raidb.Me;

namespace Manager {
  public partial class SMS_Series_Form : Form {
    readonly DbDataContext db;

    Map currentMap;
    MapProperties mapProperties;

    /// <summary>
    /// Список sms    
    /// </summary>
    List<MePropValue> sms_list;

    /// <summary>
    /// Конструктор, которому передаётся контекст 
    /// Чтобы этот самый контекст можно было откатить при модульном тестировании
    /// </summary>
    /// <param name="db"></param>
    public SMS_Series_Form(DbDataContext db) {
      this.db = db;
      InitializeComponent();
      SN_Code.SN.Focus();
      SN_Code.SN.SelectedIndexChanged += Update_PropertyGrid;
      SN_Code.Code.TextChanged += Update_PropertyGrid;
      Error.SetError(SN_Code.SN, "Выберите короткий номер");
      map = null;
      SMSDataGrid.CellValueChanged += texts_CellValueChanged;
      SN_Code_Publish.SN.TextChanged += update_PublishButtonState;
      SN_Code_Publish.Code.TextChanged += update_PublishButtonState;
      LoadSMSSeries();
      chooseSellSection.LoadData(db);
    }

    /// <summary>
    /// Конструктор без параметров создаёт новый контекст базы данных и вызывает конструктор с параметрами
    /// </summary>
    public SMS_Series_Form() : this(new DbDataContext()) {
    }

    /// <summary>
    /// Выбранный элемент map'а
    /// </summary>
    public Map map {
      get { return currentMap; }
      set {
        currentMap = value;
        UpdateButtonsState();
      }
    }

    /// <summary>
    /// Введён корректный код
    /// </summary>
    bool Code_Entered {
      get { return SN_Code.Code.Text != ""; }
    }

    /// <summary>
    /// Выбран корретный короткий номер
    /// </summary>
    bool SN_Exists {
      get { return SN_Code.SN.Text != ""; }
    }

    /// <summary>
    /// Текущая запись Map'а существует
    /// </summary>
    /// <returns></returns>
    bool CurrentMapExists {
      get { return currentMap != null; }
    }

    void update_PublishButtonState(object sender, EventArgs e) {
      // Публиковать можно только если выбран новый номер и код
      PublishSeries.Enabled = (SN_Code_Publish.SN.Text != "") && (SN_Code_Publish.Code.Text != "") &&
                              // И выбран SellSection для публикации
                              chooseSellSection.SelectSellSectionId.SelectedValue != null;
    }

    /// <summary>
    /// Загрузка всех серий SMS в comboBox
    /// </summary>
    void LoadSMSSeries() {
      // Получаем все серии из БД
      IQueryable<Me> series = db.Mes.Where(t => t.idMeType == (int) MeTypeEnum.RandomText);
      // Заполяем ими comboBox
      foreach (Me me in series) {
        // Получаем свойство title у данной серии 
        MePropValue mePropValue = me.MePropValues.FirstOrDefault(t => t.idProp == (int) MePropertyEnum.title);
        // Игнорируем серии SMS без заголовка
        if (mePropValue == null)
          continue;
        string seriesName = mePropValue.sVal;
        smsSeries.Items.Add(new SMS_Series(me, seriesName));
      }
    }

    /// <summary>
    /// Изменение доступности и обновление подписей на кнопках
    /// </summary>
    void UpdateButtonsState() {
      DeleteMap.Enabled = CurrentMapExists;
      PublishSeries.Enabled = !CurrentMapExists && SN_Exists && Code_Entered;
      CreateNewS.Enabled = !CurrentMapExists && SN_Exists && Code_Entered;
      toolTip.SetToolTip(DeleteMap,
                         DeleteMap.Enabled
                           ?
                             string.Format("Удалить Price = {0} code = {1}",
                                           currentMap.idPriceCategory,
                                           currentMap.code)
                           :
                             ""
        );
      toolTip.SetToolTip(PublishSeries,
                         PublishSeries.Enabled
                           ?
                             "Добавить код " + SN_Code.SN.Text + " на номер " + SN_Code.Code.Text
                           :
                             "");
      toolTip.SetToolTip(CreateNewS,
                         CreateNewS.Enabled
                           ?
                             "Создать новую серию SMS с кодом " + SN_Code.SN.Text + " на номер " + SN_Code.Code.Text
                           :
                             "");
      SMSGroupBox.Visible = map != null;
      SMSDataGrid.Visible = map != null;
      chooseSellSection.Visible = map != null;
    }

    void Update_PropertyGrid(object sender, EventArgs e) {
      Error.Clear();
      if (SN_Code.SN.Text.Trim() == "") {
        Error.SetError(SN_Code.SN, "Выберите короткий номер");
        return;
      }
      if (SN_Code.Code.Text.Trim() == "") {
        Error.SetError(SN_Code.Code, "Введите код");
        return;
      }
      map = db.Map_by_sn_code(SN_Code.SN.Text, SN_Code.Code.Text);
      mapProperties = new MapProperties(map);
      MePropetryGrid.SelectedObject = mapProperties;
      SMSDataGrid.Rows.Clear();
      if (map == null) {
        Error.SetError(SN_Code.Code, "По этому номеру и коду ничего не опубликовано");
        DeleteMap.Enabled = false;
        return;
      }
      if (map.Me.idMeType != (int) MeTypeEnum.RandomText) {
        Error.SetError(SN_Code.Code, "Это не серия SMS");
        DeleteMap.Enabled = false;
        SMSDataGrid.Visible = false;
        return;
      }

      SMSDataGrid.Visible = true;

      IQueryable<MePropValue> h = from x in db.MePropValues
                                  where x.idProp == (int) MePropertyEnum.text && x.idMe == map.idMe
                                  select x;
      sms_list = h.ToList();
      foreach (MePropValue b in h) {
        SMSDataGrid.Rows.Add(b.sVal);
      }

      UpdateProcessSMSField();

      chooseSellSection.UpdateSection(map.idSellSection);

      // Пока что изменений не было => сохранять нечего
      SaveChanges.Enabled = false;
    }

    /// <summary>
    /// Изменили ячейку в таблице
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void texts_CellValueChanged(object sender, DataGridViewCellEventArgs e) {
      // Смотрим изменения в DataGridView и сохраняем их в списке     
      string smsText = SMSDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
      // Добавление новой строки
      if (e.RowIndex >= sms_list.Count) {
        var newSms = new MePropValue {
                                       idMe = map.idMe,
                                       idProp = (int) MePropertyEnum.text,
                                       sVal = smsText
                                     };
        db.MePropValues.InsertOnSubmit(newSms);
        sms_list.Add(newSms);
      }
      sms_list[e.RowIndex].sVal = smsText;
      SaveChanges.Enabled = true;
    }

    /// <summary>
    /// Обновить поле ProcessSMS_Result
    /// </summary>
    void UpdateProcessSMSField() {
      foreach (ProcessSmsResult value in db.ProcessSms(SN_Code.SN.Text, SN_Code.Code.Text, 6, ""))
        ProcessSMS_Result.Text = value.sVal;
    }

    /// <summary>
    /// Удаление выбранной записи из Map
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    internal void DeleteMapButton_Click(object sender, EventArgs e) {
      // Если не выбран Map, то этот метод должно быть невозможно вызвать
      Assert.IsNotNull(map);
      // Выводим диалог подтверждения удаления 
      if (MessageBox.Show("Удалить map?", "Подтверждение", MessageBoxButtons.YesNo,
                          MessageBoxIcon.Question) != DialogResult.Yes)
        return;
      DeleteSelectedMap();
      db.SubmitChanges();
      SaveChanges.Enabled = false;
    }

    /// <summary>
    /// Удаляем текущий (выбранный) Map
    /// </summary>
    internal void DeleteSelectedMap() {
      // Если пользователь подтвердил удаление, удаляем из таблицы
      db.Maps.DeleteOnSubmit(map);
      map = null;
      // Изменения есть 
      SaveChanges.Enabled = true;
    }

    /// <summary>
    /// Сохранить изменения в базе данных
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void SubmitChanges_Click(object sender, EventArgs e) {
      db.SubmitChanges();
      SaveChanges.Enabled = false;
    }

    void SMS_Series_Form_Load(object sender, EventArgs e) {
    }

    /// <summary>
    /// Создание нового кода
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void CreateMap_Click(object sender, EventArgs e) {
      string ShortNumber = SN_Code_Publish.SN.Text;
      PriceCategory pc = db.PriceCategory_by_SN(ShortNumber);
      string Code = SN_Code_Publish.Code.Text;
      // Предотвращение повторной публикации
      if (db.Maps.Count(t => t.idMe == map.Me.id && t.idPriceCategory == pc.id &&
                             t.code == Code) > 0) {
        MessageBox.Show("Серия уже опубликована с кодом " + Code + " на номер " + ShortNumber,
                        "Сообщение",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
        return;
      }
      var map1 = new Map {
                           idBasketCollection = (int) BasketCollectionEnum.main,
                           Me = map.Me,
                           idPriceCategory = pc.id,
                           code = Code,
                           idSellType = (int) SellTypeEnum.Серия_SMS,
                           idSellSection = (int) chooseSellSection.SelectSellSectionId.SelectedValue,
                         };
      db.Maps.InsertOnSubmit(map1);
      MessageBox.Show("Опубликован с кодом " + Code + " на номер " + ShortNumber,
                      "Сообщение",
                      MessageBoxButtons.OK,
                      MessageBoxIcon.Information);
      db.SubmitChanges();
      SaveChanges.Enabled = false;
    }

    /// <summary>
    /// Создание нового элемента с выбранным номером и кодом
    /// </summary>
    public void CreateNewSeries() {
      Assert.IsTrue(SN_Code.Code.Text.Length > 0);
      Assert.IsTrue(SN_Code.SN.Text.Length > 0);
      PriceCategory pc = db.PriceCategory_by_SN(SN_Code.SN.Text);
      var me = new Me {
                        idMeRetrieveWay = (int) MeRetrieveWayEnum.Файлы_не_хранятся,
                        idMeType = (int) MeTypeEnum.RandomText
                      };
      db.Mes.InsertOnSubmit(me);
      map = new Map {
                      idBasketCollection = (int) BasketCollectionEnum.main,
                      Me = me,
                      idPriceCategory = pc.id,
                      code = SN_Code.Code.Text
                    };
      db.Maps.InsertOnSubmit(map);
    }

    void CreateNewS_Click(object sender, EventArgs e) {
      var form = new CreateNewSMSSeriesForm(db);
      // Заносим начальные значения полей в форму создания новой серии
      form.SN_Code.SN.Text = SN_Code.SN.Text;
      form.SN_Code.Code.Text = SN_Code.Code.Text;
      form.ShowDialog();
      // 
      Update_PropertyGrid(null, null);
    }

    void SMSDataGrid_UserAddedRow(object sender, DataGridViewRowEventArgs e) {
    }

    /// <summary>
    /// Выбирают другую SMS-серию
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void smsSeries_SelectedIndexChanged(object sender, EventArgs e) {
      var sms_Series = (SMS_Series) smsSeries.SelectedItem;
      try {
        map = sms_Series.me.Maps.First();
        SN_Code.SN.Text = map.PriceCategory.PriceCategory_Configurations_sns.First().sn;
        SN_Code.Code.Text = map.code;
      }
      catch {
        SN_Code.SN.Text = "";
        SN_Code.Code.Text = "";
      }
    }

    /// <summary>
    /// Генерация новой SMS из ProcessSMS
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void RefreshSMS_Click(object sender, EventArgs e) {
      UpdateProcessSMSField();
    }
  }

  /// <summary>
  /// Серия SMS для списка
  /// </summary>
  class SMS_Series {
    readonly string name;
    public Me me;

    public SMS_Series(Me me, string name) {
      this.me = me;
      this.name = name;
    }

    public override string ToString() {
      return name;
    }
  }
}