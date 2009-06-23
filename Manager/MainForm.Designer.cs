namespace Manager
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
      this.mainMenu = new System.Windows.Forms.MenuStrip();
      this.кодыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.сгенерироватьКодыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.проверкаТекстовыхSMSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.mainMenu.SuspendLayout();
      this.SuspendLayout();
      // 
      // mainMenu
      // 
      this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.кодыToolStripMenuItem,
            this.выходToolStripMenuItem});
      this.mainMenu.Location = new System.Drawing.Point(0, 0);
      this.mainMenu.Name = "mainMenu";
      this.mainMenu.Size = new System.Drawing.Size(560, 24);
      this.mainMenu.TabIndex = 0;
      this.mainMenu.Text = "mainMenu";
      // 
      // кодыToolStripMenuItem
      // 
      this.кодыToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.сгенерироватьКодыToolStripMenuItem,
            this.проверкаТекстовыхSMSToolStripMenuItem});
      this.кодыToolStripMenuItem.Name = "кодыToolStripMenuItem";
      this.кодыToolStripMenuItem.Size = new System.Drawing.Size(115, 20);
      this.кодыToolStripMenuItem.Text = "Добавление кодов";
      // 
      // сгенерироватьКодыToolStripMenuItem
      // 
      this.сгенерироватьКодыToolStripMenuItem.Name = "сгенерироватьКодыToolStripMenuItem";
      this.сгенерироватьКодыToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
      this.сгенерироватьКодыToolStripMenuItem.Text = "Сгенерировать коды";
      this.сгенерироватьКодыToolStripMenuItem.Click += new System.EventHandler(this.сгенерироватьКодыToolStripMenuItem_Click);
      // 
      // проверкаТекстовыхSMSToolStripMenuItem
      // 
      this.проверкаТекстовыхSMSToolStripMenuItem.Name = "проверкаТекстовыхSMSToolStripMenuItem";
      this.проверкаТекстовыхSMSToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
      this.проверкаТекстовыхSMSToolStripMenuItem.Text = "Текстовые SMS";
      this.проверкаТекстовыхSMSToolStripMenuItem.Click += new System.EventHandler(this.проверкаТекстовыхSMSToolStripMenuItem_Click);
      // 
      // выходToolStripMenuItem
      // 
      this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
      this.выходToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
      this.выходToolStripMenuItem.Text = "Выход";
      this.выходToolStripMenuItem.Click += new System.EventHandler(this.выходToolStripMenuItem_Click);
      // 
      // MainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(560, 297);
      this.Controls.Add(this.mainMenu);
      this.MainMenuStrip = this.mainMenu;
      this.Name = "MainForm";
      this.Text = "Работа с Базой Данных";
      this.mainMenu.ResumeLayout(false);
      this.mainMenu.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.MenuStrip mainMenu;
    private System.Windows.Forms.ToolStripMenuItem кодыToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem сгенерироватьКодыToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem проверкаТекстовыхSMSToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem;
  }
}