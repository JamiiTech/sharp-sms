namespace Manager
{
  partial class GenCodesForm
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
      this.dataGridView1 = new System.Windows.Forms.DataGridView();
      this.Автор = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Произведение = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Код = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Опубликовать = new System.Windows.Forms.DataGridViewCheckBoxColumn();
      this.sellChannelBindingSource = new System.Windows.Forms.BindingSource(this.components);
      this.sellSectionsBindingSource = new System.Windows.Forms.BindingSource(this.components);
      this.label5 = new System.Windows.Forms.Label();
      this.Короткий_номер = new System.Windows.Forms.ComboBox();
      this.label6 = new System.Windows.Forms.Label();
      this.СтартовыйКод = new System.Windows.Forms.TextBox();
      this.CSS = new Manager.Controls.ChooseSellSection();
      ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.sellChannelBindingSource)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.sellSectionsBindingSource)).BeginInit();
      this.SuspendLayout();
      // 
      // dataGridView1
      // 
      this.dataGridView1.AllowUserToOrderColumns = true;
      this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
      dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
      dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
      this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Автор,
            this.Произведение,
            this.Код,
            this.Опубликовать});
      dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
      dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
      dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
      this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle5;
      this.dataGridView1.Location = new System.Drawing.Point(4, 66);
      this.dataGridView1.Name = "dataGridView1";
      dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
      dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
      dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
      this.dataGridView1.Size = new System.Drawing.Size(757, 365);
      this.dataGridView1.TabIndex = 0;
      // 
      // Автор
      // 
      this.Автор.HeaderText = "Автор";
      this.Автор.Name = "Автор";
      // 
      // Произведение
      // 
      this.Произведение.HeaderText = "Произведение";
      this.Произведение.Name = "Произведение";
      this.Произведение.Width = 300;
      // 
      // Код
      // 
      this.Код.HeaderText = "Код";
      this.Код.Name = "Код";
      // 
      // Опубликовать
      // 
      this.Опубликовать.HeaderText = "Опубликовать";
      this.Опубликовать.Name = "Опубликовать";
      // 
      // sellChannelBindingSource
      // 
      this.sellChannelBindingSource.DataSource = typeof(Raidb.SellChannel);
      this.sellChannelBindingSource.CurrentChanged += new System.EventHandler(this.sellChannelBindingSource_CurrentChanged);
      // 
      // sellSectionsBindingSource
      // 
      this.sellSectionsBindingSource.DataMember = "SellSections";
      this.sellSectionsBindingSource.DataSource = this.sellChannelBindingSource;
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(588, 20);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(93, 13);
      this.label5.TabIndex = 10;
      this.label5.Text = "Короткий номер:";
      // 
      // Короткий_номер
      // 
      this.Короткий_номер.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.Короткий_номер.FormattingEnabled = true;
      this.Короткий_номер.Items.AddRange(new object[] {
            "4141",
            "5353"});
      this.Короткий_номер.Location = new System.Drawing.Point(687, 12);
      this.Короткий_номер.Name = "Короткий_номер";
      this.Короткий_номер.Size = new System.Drawing.Size(74, 21);
      this.Короткий_номер.TabIndex = 9;
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(560, 45);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(86, 13);
      this.label6.TabIndex = 11;
      this.label6.Text = "Стартовый код:";
      // 
      // СтартовыйКод
      // 
      this.СтартовыйКод.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Manager.Properties.Settings.Default, "СтартовыйКод", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.СтартовыйКод.Location = new System.Drawing.Point(652, 42);
      this.СтартовыйКод.Name = "СтартовыйКод";
      this.СтартовыйКод.Size = new System.Drawing.Size(100, 20);
      this.СтартовыйКод.TabIndex = 12;
      this.СтартовыйКод.Text = global::Manager.Properties.Settings.Default.СтартовыйКод;
      // 
      // CSS
      // 
      this.CSS.Location = new System.Drawing.Point(-5, -1);
      this.CSS.Name = "CSS";
      this.CSS.Size = new System.Drawing.Size(587, 63);
      this.CSS.TabIndex = 14;
      // 
      // GenCodesForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(763, 434);
      this.Controls.Add(this.label6);
      this.Controls.Add(this.СтартовыйКод);
      this.Controls.Add(this.label5);
      this.Controls.Add(this.Короткий_номер);
      this.Controls.Add(this.dataGridView1);
      this.Controls.Add(this.CSS);
      this.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::Manager.Properties.Settings.Default, "GenCodesFormLocation", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.Location = global::Manager.Properties.Settings.Default.GenCodesFormLocation;
      this.Name = "GenCodesForm";
      this.Text = "Генерирование кодов для музыки";
      this.Load += new System.EventHandler(this.GenCodes_Load);
      ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.sellChannelBindingSource)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.sellSectionsBindingSource)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.DataGridView dataGridView1;
    private System.Windows.Forms.BindingSource sellChannelBindingSource;
    private System.Windows.Forms.BindingSource sellSectionsBindingSource;
    private System.Windows.Forms.DataGridViewTextBoxColumn Автор;
    private System.Windows.Forms.DataGridViewTextBoxColumn Произведение;
    private System.Windows.Forms.DataGridViewTextBoxColumn Код;
    private System.Windows.Forms.DataGridViewCheckBoxColumn Опубликовать;
    private System.Windows.Forms.Label label5;
    public System.Windows.Forms.ComboBox Короткий_номер;
    private System.Windows.Forms.Label label6;
    public System.Windows.Forms.TextBox СтартовыйКод;
    internal Manager.Controls.ChooseSellSection CSS;
  }
}