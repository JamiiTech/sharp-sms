using CDS.UI.Controls;

namespace CDS.UI
{
	partial class MainForm
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
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
      this.btnSystemWork = new System.Windows.Forms.Button();
      this.btnNewMe = new System.Windows.Forms.Button();
      this.dsMeTypes = new CDS.UI.DSMeTypes();
      this.dataGridView1 = new System.Windows.Forms.DataGridView();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.publishing1 = new Publishing();
      this.mePropertiesEditor = new CDS.UI.Controls.MePropertiesEditor();
      this.splitContainer2 = new System.Windows.Forms.SplitContainer();
      this.button1 = new System.Windows.Forms.Button();
      this.panel1 = new System.Windows.Forms.Panel();
      this.meSSEditor = new CDS.UI.Controls.MeSSEditor();
      this.fileEditor1 = new CDS.UI.FileEditor();
      this.radioButtonFiles = new System.Windows.Forms.RadioButton();
      this.radioButtonSS = new System.Windows.Forms.RadioButton();
      this.btnOpen = new System.Windows.Forms.Button();
      this.btnSaveAll = new System.Windows.Forms.Button();
      this.xml = new CDS.UI.Controls.XML();
      this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
      this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
      this.cBoxMeType = new System.Windows.Forms.ComboBox();
      this.label1 = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.dsMeTypes)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.splitContainer2.Panel1.SuspendLayout();
      this.splitContainer2.Panel2.SuspendLayout();
      this.splitContainer2.SuspendLayout();
      this.panel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // btnSystemWork
      // 
      this.btnSystemWork.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.btnSystemWork.Location = new System.Drawing.Point(132, 631);
      this.btnSystemWork.Name = "btnSystemWork";
      this.btnSystemWork.Size = new System.Drawing.Size(108, 23);
      this.btnSystemWork.TabIndex = 3;
      this.btnSystemWork.Text = "Обслуживание...";
      this.btnSystemWork.UseVisualStyleBackColor = true;
      this.btnSystemWork.Click += new System.EventHandler(this.btnSystemWork_Click);
      // 
      // btnNewMe
      // 
      this.btnNewMe.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.btnNewMe.Location = new System.Drawing.Point(12, 631);
      this.btnNewMe.Name = "btnNewMe";
      this.btnNewMe.Size = new System.Drawing.Size(102, 23);
      this.btnNewMe.TabIndex = 2;
      this.btnNewMe.Text = "Новый МЭ";
      this.btnNewMe.UseVisualStyleBackColor = true;
      this.btnNewMe.Click += new System.EventHandler(this.btnNewMe_Click);
      // 
      // dsMeTypes
      // 
      this.dsMeTypes.DataSetName = "DSMeTypes";
      this.dsMeTypes.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
      // 
      // dataGridView1
      // 
      this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)));
      dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
      dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
      dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
      this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
      dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
      dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
      this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
      this.dataGridView1.Location = new System.Drawing.Point(12, 52);
      this.dataGridView1.MultiSelect = false;
      this.dataGridView1.Name = "dataGridView1";
      dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
      dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
      dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
      this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.dataGridView1.Size = new System.Drawing.Size(246, 566);
      this.dataGridView1.TabIndex = 0;
      this.dataGridView1.CurrentCellChanged += new System.EventHandler(this.dataGridView1_CurrentCellChanged);
      this.dataGridView1.Resize += new System.EventHandler(this.dataGridView1_Resize);
      // 
      // splitContainer1
      // 
      this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.splitContainer1.Location = new System.Drawing.Point(264, 0);
      this.splitContainer1.Name = "splitContainer1";
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.Controls.Add(this.publishing1);
      this.splitContainer1.Panel1.Controls.Add(this.mePropertiesEditor);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
      this.splitContainer1.Size = new System.Drawing.Size(820, 666);
      this.splitContainer1.SplitterDistance = 293;
      this.splitContainer1.TabIndex = 2;
      // 
      // publishing1
      // 
      this.publishing1.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.publishing1.IdMe = 0;
      this.publishing1.IdSellSection = 3;
      this.publishing1.Location = new System.Drawing.Point(0, 424);
      this.publishing1.Name = "publishing1";
      this.publishing1.Size = new System.Drawing.Size(291, 240);
      this.publishing1.TabIndex = 6;
      this.publishing1.Load += new System.EventHandler(this.publishing1_Load);
      // 
      // mePropertiesEditor
      // 
      this.mePropertiesEditor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.mePropertiesEditor.AutoScroll = true;
      this.mePropertiesEditor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.mePropertiesEditor.Location = new System.Drawing.Point(0, 0);
      this.mePropertiesEditor.Name = "mePropertiesEditor";
      this.mePropertiesEditor.Size = new System.Drawing.Size(291, 418);
      this.mePropertiesEditor.TabIndex = 5;
      // 
      // splitContainer2
      // 
      this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer2.Location = new System.Drawing.Point(0, 0);
      this.splitContainer2.Name = "splitContainer2";
      this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer2.Panel1
      // 
      this.splitContainer2.Panel1.Controls.Add(this.button1);
      this.splitContainer2.Panel1.Controls.Add(this.panel1);
      this.splitContainer2.Panel1.Controls.Add(this.radioButtonFiles);
      this.splitContainer2.Panel1.Controls.Add(this.radioButtonSS);
      this.splitContainer2.Panel1.Controls.Add(this.btnOpen);
      this.splitContainer2.Panel1.Controls.Add(this.btnSaveAll);
      // 
      // splitContainer2.Panel2
      // 
      this.splitContainer2.Panel2.Controls.Add(this.xml);
      this.splitContainer2.Size = new System.Drawing.Size(523, 666);
      this.splitContainer2.SplitterDistance = 262;
      this.splitContainer2.TabIndex = 0;
      // 
      // button1
      // 
      this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.button1.Enabled = false;
      this.button1.Location = new System.Drawing.Point(190, 225);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(75, 23);
      this.button1.TabIndex = 12;
      this.button1.Text = "Удалить...";
      this.button1.UseVisualStyleBackColor = true;
      // 
      // panel1
      // 
      this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.panel1.Controls.Add(this.meSSEditor);
      this.panel1.Controls.Add(this.fileEditor1);
      this.panel1.Location = new System.Drawing.Point(-1, -1);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(521, 219);
      this.panel1.TabIndex = 11;
      // 
      // meSSEditor
      // 
      this.meSSEditor.AutoScroll = true;
      this.meSSEditor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.meSSEditor.Location = new System.Drawing.Point(19, 8);
      this.meSSEditor.Name = "meSSEditor";
      this.meSSEditor.Size = new System.Drawing.Size(151, 154);
      this.meSSEditor.TabIndex = 6;
      // 
      // fileEditor1
      // 
      this.fileEditor1.AutoScroll = true;
      this.fileEditor1.BackColor = System.Drawing.SystemColors.Control;
      this.fileEditor1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.fileEditor1.Location = new System.Drawing.Point(218, 37);
      this.fileEditor1.Name = "fileEditor1";
      this.fileEditor1.Size = new System.Drawing.Size(153, 104);
      this.fileEditor1.TabIndex = 11;
      // 
      // radioButtonFiles
      // 
      this.radioButtonFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.radioButtonFiles.AutoSize = true;
      this.radioButtonFiles.Location = new System.Drawing.Point(391, 228);
      this.radioButtonFiles.Name = "radioButtonFiles";
      this.radioButtonFiles.Size = new System.Drawing.Size(62, 17);
      this.radioButtonFiles.TabIndex = 10;
      this.radioButtonFiles.Text = "Файлы";
      this.radioButtonFiles.UseVisualStyleBackColor = true;
      // 
      // radioButtonSS
      // 
      this.radioButtonSS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.radioButtonSS.AutoSize = true;
      this.radioButtonSS.Checked = true;
      this.radioButtonSS.Location = new System.Drawing.Point(292, 228);
      this.radioButtonSS.Name = "radioButtonSS";
      this.radioButtonSS.Size = new System.Drawing.Size(83, 17);
      this.radioButtonSS.TabIndex = 9;
      this.radioButtonSS.TabStop = true;
      this.radioButtonSS.Text = "Скриншоты";
      this.radioButtonSS.UseVisualStyleBackColor = true;
      this.radioButtonSS.CheckedChanged += new System.EventHandler(this.radioButtonSS_CheckedChanged);
      // 
      // btnOpen
      // 
      this.btnOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.btnOpen.Location = new System.Drawing.Point(8, 225);
      this.btnOpen.Name = "btnOpen";
      this.btnOpen.Size = new System.Drawing.Size(75, 23);
      this.btnOpen.TabIndex = 8;
      this.btnOpen.Text = "Добавить...";
      this.btnOpen.UseVisualStyleBackColor = true;
      this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
      // 
      // btnSaveAll
      // 
      this.btnSaveAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.btnSaveAll.Location = new System.Drawing.Point(99, 225);
      this.btnSaveAll.Name = "btnSaveAll";
      this.btnSaveAll.Size = new System.Drawing.Size(75, 23);
      this.btnSaveAll.TabIndex = 7;
      this.btnSaveAll.Text = "Сохранить...";
      this.btnSaveAll.UseVisualStyleBackColor = true;
      this.btnSaveAll.Click += new System.EventHandler(this.btnSaveAll_Click);
      // 
      // xml
      // 
      this.xml.Location = new System.Drawing.Point(4, 4);
      this.xml.Name = "xml";
      this.xml.Size = new System.Drawing.Size(396, 347);
      this.xml.TabIndex = 0;
      this.xml.Value = "";
      this.xml.Load += new System.EventHandler(this.xml_Load);
      // 
      // folderBrowserDialog
      // 
      this.folderBrowserDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
      // 
      // openFileDialog1
      // 
      this.openFileDialog1.FileName = "openFileDialog1";
      // 
      // cBoxMeType
      // 
      this.cBoxMeType.DataSource = this.dsMeTypes;
      this.cBoxMeType.DisplayMember = "MeTypes.name";
      this.cBoxMeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cBoxMeType.FormattingEnabled = true;
      this.cBoxMeType.Location = new System.Drawing.Point(14, 25);
      this.cBoxMeType.Name = "cBoxMeType";
      this.cBoxMeType.Size = new System.Drawing.Size(245, 21);
      this.cBoxMeType.TabIndex = 1;
      this.cBoxMeType.ValueMember = "MeTypes.id";
      this.cBoxMeType.SelectedValueChanged += new System.EventHandler(this.cBoxMeType_SelectedValueChanged);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(12, 8);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(165, 13);
      this.label1.TabIndex = 4;
      this.label1.Text = "Выберите тип Медиаэлемента:";
      // 
      // MainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1084, 666);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.cBoxMeType);
      this.Controls.Add(this.btnSystemWork);
      this.Controls.Add(this.btnNewMe);
      this.Controls.Add(this.dataGridView1);
      this.Controls.Add(this.splitContainer1);
      this.Name = "MainForm";
      this.Text = "Редактирование медиаэлементов";
      ((System.ComponentModel.ISupportInitialize)(this.dsMeTypes)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.ResumeLayout(false);
      this.splitContainer2.Panel1.ResumeLayout(false);
      this.splitContainer2.Panel1.PerformLayout();
      this.splitContainer2.Panel2.ResumeLayout(false);
      this.splitContainer2.ResumeLayout(false);
      this.panel1.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataGridView dataGridView1;
		private MePropertiesEditor mePropertiesEditor;
		private MeSSEditor meSSEditor;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.SplitContainer splitContainer2;
		private System.Windows.Forms.Button btnSaveAll;
		private System.Windows.Forms.Button btnOpen;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private CDS.UI.Controls.XML xml;
		private Publishing publishing1;
		private System.Windows.Forms.RadioButton radioButtonFiles;
		private System.Windows.Forms.RadioButton radioButtonSS;
		private FileEditor fileEditor1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button button1;
		private DSMeTypes dsMeTypes;
		private System.Windows.Forms.Button btnNewMe;
		private System.Windows.Forms.Button btnSystemWork;
    private System.Windows.Forms.ComboBox cBoxMeType;
    private System.Windows.Forms.Label label1;
	}
}

