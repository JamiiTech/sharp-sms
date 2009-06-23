namespace Manager
{
  partial class SMS_Series_Form
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
      this.SMSGroupBox = new System.Windows.Forms.GroupBox();
      this.SMSDataGrid = new System.Windows.Forms.DataGridView();
      this.SMS = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Error = new System.Windows.Forms.ErrorProvider(this.components);
      this.DeleteMap = new System.Windows.Forms.Button();
      this.SaveChanges = new System.Windows.Forms.Button();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.chooseSellSection = new Manager.Controls.ChooseSellSection();
      this.panel1 = new System.Windows.Forms.Panel();
      this.CreateNewS = new System.Windows.Forms.Button();
      this.SN_Code = new Manager.Controls.SN_Code_Edit();
      this.UpdateProcessSMS = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.groupBox3 = new System.Windows.Forms.GroupBox();
      this.SN_Code_Publish = new Manager.Controls.SN_Code_Edit();
      this.PublishSeries = new System.Windows.Forms.Button();
      this.smsSeries = new System.Windows.Forms.ComboBox();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.ProcessSMS_Result = new System.Windows.Forms.TextBox();
      this.MePropetryGrid = new System.Windows.Forms.PropertyGrid();
      this.toolTip = new System.Windows.Forms.ToolTip(this.components);
      this.SMSGroupBox.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.SMSDataGrid)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.Error)).BeginInit();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.panel1.SuspendLayout();
      this.groupBox3.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.SuspendLayout();
      // 
      // SMSGroupBox
      // 
      this.SMSGroupBox.Controls.Add(this.SMSDataGrid);
      this.SMSGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
      this.SMSGroupBox.Location = new System.Drawing.Point(0, 89);
      this.SMSGroupBox.Name = "SMSGroupBox";
      this.SMSGroupBox.Size = new System.Drawing.Size(592, 483);
      this.SMSGroupBox.TabIndex = 7;
      this.SMSGroupBox.TabStop = false;
      this.SMSGroupBox.Text = "Все SMS из БД с данным номером и кодом";
      // 
      // SMSDataGrid
      // 
      dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
      dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
      dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.SMSDataGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
      this.SMSDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.SMSDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SMS});
      dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
      dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
      dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
      this.SMSDataGrid.DefaultCellStyle = dataGridViewCellStyle5;
      this.SMSDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
      this.SMSDataGrid.Location = new System.Drawing.Point(3, 16);
      this.SMSDataGrid.Name = "SMSDataGrid";
      dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
      dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
      dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.SMSDataGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
      this.SMSDataGrid.Size = new System.Drawing.Size(586, 464);
      this.SMSDataGrid.TabIndex = 9;
      this.SMSDataGrid.UserAddedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.SMSDataGrid_UserAddedRow);
      // 
      // SMS
      // 
      this.SMS.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
      this.SMS.HeaderText = "SMS";
      this.SMS.Name = "SMS";
      // 
      // Error
      // 
      this.Error.ContainerControl = this;
      // 
      // DeleteMap
      // 
      this.DeleteMap.Location = new System.Drawing.Point(16, 370);
      this.DeleteMap.Name = "DeleteMap";
      this.DeleteMap.Size = new System.Drawing.Size(116, 23);
      this.DeleteMap.TabIndex = 8;
      this.DeleteMap.Text = "Удалить код";
      this.DeleteMap.UseVisualStyleBackColor = true;
      this.DeleteMap.Click += new System.EventHandler(this.DeleteMapButton_Click);
      // 
      // SaveChanges
      // 
      this.SaveChanges.Location = new System.Drawing.Point(16, 192);
      this.SaveChanges.Name = "SaveChanges";
      this.SaveChanges.Size = new System.Drawing.Size(127, 23);
      this.SaveChanges.TabIndex = 12;
      this.SaveChanges.Text = "Сохранить изменения";
      this.SaveChanges.UseVisualStyleBackColor = true;
      this.SaveChanges.Click += new System.EventHandler(this.SubmitChanges_Click);
      // 
      // splitContainer1
      // 
      this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer1.Location = new System.Drawing.Point(0, 0);
      this.splitContainer1.Name = "splitContainer1";
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.Controls.Add(this.SMSGroupBox);
      this.splitContainer1.Panel1.Controls.Add(this.chooseSellSection);
      this.splitContainer1.Panel1.Controls.Add(this.panel1);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.UpdateProcessSMS);
      this.splitContainer1.Panel2.Controls.Add(this.label1);
      this.splitContainer1.Panel2.Controls.Add(this.groupBox3);
      this.splitContainer1.Panel2.Controls.Add(this.smsSeries);
      this.splitContainer1.Panel2.Controls.Add(this.SaveChanges);
      this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
      this.splitContainer1.Panel2.Controls.Add(this.DeleteMap);
      this.splitContainer1.Panel2.Controls.Add(this.MePropetryGrid);
      this.splitContainer1.Size = new System.Drawing.Size(1115, 572);
      this.splitContainer1.SplitterDistance = 592;
      this.splitContainer1.TabIndex = 13;
      // 
      // chooseSellSection
      // 
      this.chooseSellSection.Dock = System.Windows.Forms.DockStyle.Top;
      this.chooseSellSection.Location = new System.Drawing.Point(0, 26);
      this.chooseSellSection.Name = "chooseSellSection";
      this.chooseSellSection.sellChannel = null;
      this.chooseSellSection.sellSection = null;
      this.chooseSellSection.Size = new System.Drawing.Size(592, 63);
      this.chooseSellSection.TabIndex = 12;
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.CreateNewS);
      this.panel1.Controls.Add(this.SN_Code);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel1.Location = new System.Drawing.Point(0, 0);
      this.panel1.Margin = new System.Windows.Forms.Padding(6);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(592, 26);
      this.panel1.TabIndex = 11;
      // 
      // CreateNewS
      // 
      this.CreateNewS.Location = new System.Drawing.Point(413, 3);
      this.CreateNewS.Name = "CreateNewS";
      this.CreateNewS.Size = new System.Drawing.Size(153, 23);
      this.CreateNewS.TabIndex = 14;
      this.CreateNewS.Text = "Создать новую серию SMS";
      this.CreateNewS.UseVisualStyleBackColor = true;
      this.CreateNewS.Click += new System.EventHandler(this.CreateNewS_Click);
      // 
      // SN_Code
      // 
      this.SN_Code.Dock = System.Windows.Forms.DockStyle.Left;
      this.SN_Code.Location = new System.Drawing.Point(0, 0);
      this.SN_Code.Name = "SN_Code";
      this.SN_Code.Size = new System.Drawing.Size(388, 26);
      this.SN_Code.TabIndex = 13;
      // 
      // UpdateProcessSMS
      // 
      this.UpdateProcessSMS.Location = new System.Drawing.Point(363, 370);
      this.UpdateProcessSMS.Name = "UpdateProcessSMS";
      this.UpdateProcessSMS.Size = new System.Drawing.Size(115, 23);
      this.UpdateProcessSMS.TabIndex = 18;
      this.UpdateProcessSMS.Text = "Обновить SMS";
      this.UpdateProcessSMS.UseVisualStyleBackColor = true;
      this.UpdateProcessSMS.Click += new System.EventHandler(this.RefreshSMS_Click);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(19, 234);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(139, 13);
      this.label1.TabIndex = 17;
      this.label1.Text = "Выбрать серию по имени:";
      // 
      // groupBox3
      // 
      this.groupBox3.Controls.Add(this.SN_Code_Publish);
      this.groupBox3.Controls.Add(this.PublishSeries);
      this.groupBox3.Location = new System.Drawing.Point(16, 271);
      this.groupBox3.Name = "groupBox3";
      this.groupBox3.Size = new System.Drawing.Size(439, 79);
      this.groupBox3.TabIndex = 16;
      this.groupBox3.TabStop = false;
      this.groupBox3.Text = "Опубликовать эту серию на другой номер и код";
      // 
      // SN_Code_Publish
      // 
      this.SN_Code_Publish.Location = new System.Drawing.Point(6, 19);
      this.SN_Code_Publish.Name = "SN_Code_Publish";
      this.SN_Code_Publish.Size = new System.Drawing.Size(398, 23);
      this.SN_Code_Publish.TabIndex = 19;
      // 
      // PublishSeries
      // 
      this.PublishSeries.Location = new System.Drawing.Point(302, 48);
      this.PublishSeries.Name = "PublishSeries";
      this.PublishSeries.Size = new System.Drawing.Size(102, 23);
      this.PublishSeries.TabIndex = 18;
      this.PublishSeries.Text = "Опубликовать";
      this.PublishSeries.UseVisualStyleBackColor = true;
      this.PublishSeries.Click += new System.EventHandler(this.CreateMap_Click);
      // 
      // smsSeries
      // 
      this.smsSeries.FormattingEnabled = true;
      this.smsSeries.Location = new System.Drawing.Point(164, 231);
      this.smsSeries.Name = "smsSeries";
      this.smsSeries.Size = new System.Drawing.Size(176, 21);
      this.smsSeries.TabIndex = 14;
      this.smsSeries.SelectedIndexChanged += new System.EventHandler(this.smsSeries_SelectedIndexChanged);
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.ProcessSMS_Result);
      this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.groupBox1.Location = new System.Drawing.Point(0, 417);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(519, 155);
      this.groupBox1.TabIndex = 7;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Результат хранимой процедуры ProcessSMS - пример SMS, которая вернётся, если посл" +
          "ать такой запрос";
      // 
      // ProcessSMS_Result
      // 
      this.ProcessSMS_Result.Dock = System.Windows.Forms.DockStyle.Fill;
      this.ProcessSMS_Result.Location = new System.Drawing.Point(3, 16);
      this.ProcessSMS_Result.Multiline = true;
      this.ProcessSMS_Result.Name = "ProcessSMS_Result";
      this.ProcessSMS_Result.Size = new System.Drawing.Size(513, 136);
      this.ProcessSMS_Result.TabIndex = 1;
      // 
      // MePropetryGrid
      // 
      this.MePropetryGrid.Dock = System.Windows.Forms.DockStyle.Top;
      this.MePropetryGrid.HelpVisible = false;
      this.MePropetryGrid.Location = new System.Drawing.Point(0, 0);
      this.MePropetryGrid.Name = "MePropetryGrid";
      this.MePropetryGrid.Size = new System.Drawing.Size(519, 186);
      this.MePropetryGrid.TabIndex = 5;
      // 
      // SMS_Series_Form
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1115, 572);
      this.Controls.Add(this.splitContainer1);
      this.Name = "SMS_Series_Form";
      this.Text = "Серии текстовых SMS";
      this.Load += new System.EventHandler(this.SMS_Series_Form_Load);
      this.SMSGroupBox.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.SMSDataGrid)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.Error)).EndInit();
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.Panel2.PerformLayout();
      this.splitContainer1.ResumeLayout(false);
      this.panel1.ResumeLayout(false);
      this.groupBox3.ResumeLayout(false);
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.GroupBox SMSGroupBox;
    internal System.Windows.Forms.ErrorProvider Error;
    internal System.Windows.Forms.Button DeleteMap;
    private System.Windows.Forms.DataGridView SMSDataGrid;
    private System.Windows.Forms.Button SaveChanges;
    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.PropertyGrid MePropetryGrid;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.TextBox ProcessSMS_Result;
    private System.Windows.Forms.ToolTip toolTip;
    private System.Windows.Forms.DataGridViewTextBoxColumn SMS;
    private System.Windows.Forms.ComboBox smsSeries;
    private System.Windows.Forms.GroupBox groupBox3;
    private System.Windows.Forms.Button PublishSeries;
    internal Controls.SN_Code_Edit SN_Code;
    private Controls.SN_Code_Edit SN_Code_Publish;
    private Controls.ChooseSellSection chooseSellSection;
    private System.Windows.Forms.Button CreateNewS;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button UpdateProcessSMS;
  }
}

