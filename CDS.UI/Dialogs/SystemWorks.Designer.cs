namespace CDS.UI
{
	partial class SystemWorks
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SystemWorks));
			this.panel3 = new System.Windows.Forms.Panel();
			this.cBoxCleanBlankProperties = new System.Windows.Forms.CheckBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.label1 = new System.Windows.Forms.Label();
			this.btnDoWork = new System.Windows.Forms.Button();
			this.panel2 = new System.Windows.Forms.Panel();
			this.button3 = new System.Windows.Forms.Button();
			this.cmdCleanBlankProperties = new System.Data.SqlClient.SqlCommand();
			this.sqlConnection1 = new System.Data.SqlClient.SqlConnection();
			this.panel3.SuspendLayout();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel3
			// 
			this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel3.Controls.Add(this.cBoxCleanBlankProperties);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel3.Location = new System.Drawing.Point(0, 65);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(628, 261);
			this.panel3.TabIndex = 8;
			// 
			// cBoxCleanBlankProperties
			// 
			this.cBoxCleanBlankProperties.AutoSize = true;
			this.cBoxCleanBlankProperties.Location = new System.Drawing.Point(24, 28);
			this.cBoxCleanBlankProperties.Name = "cBoxCleanBlankProperties";
			this.cBoxCleanBlankProperties.Size = new System.Drawing.Size(211, 17);
			this.cBoxCleanBlankProperties.TabIndex = 0;
			this.cBoxCleanBlankProperties.Text = "Удалить пустые свойства у всех МЭ";
			this.cBoxCleanBlankProperties.UseVisualStyleBackColor = true;
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.White;
			this.panel1.Controls.Add(this.label1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(628, 65);
			this.panel1.TabIndex = 6;
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(386, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(151, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Обслуживание базы данных";
			// 
			// btnDoWork
			// 
			this.btnDoWork.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnDoWork.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnDoWork.Location = new System.Drawing.Point(460, 13);
			this.btnDoWork.Name = "btnDoWork";
			this.btnDoWork.Size = new System.Drawing.Size(75, 23);
			this.btnDoWork.TabIndex = 0;
			this.btnDoWork.Text = "Выполнить";
			this.btnDoWork.UseVisualStyleBackColor = true;
			this.btnDoWork.Click += new System.EventHandler(this.btnDoWork_Click);
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.button3);
			this.panel2.Controls.Add(this.btnDoWork);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel2.Location = new System.Drawing.Point(0, 326);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(628, 48);
			this.panel2.TabIndex = 7;
			// 
			// button3
			// 
			this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button3.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button3.Location = new System.Drawing.Point(541, 13);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(75, 23);
			this.button3.TabIndex = 1;
			this.button3.Text = "Отмена";
			this.button3.UseVisualStyleBackColor = true;
			// 
			// cmdCleanBlankProperties
			// 
			this.cmdCleanBlankProperties.CommandText = resources.GetString("cmdCleanBlankProperties.CommandText");
			this.cmdCleanBlankProperties.Connection = this.sqlConnection1;
			// 
			// sqlConnection1
			// 
			this.sqlConnection1.ConnectionString = "Data Source=62.140.237.131;Initial Catalog=RAI;Persist Security Info=True;User ID" +
				"=bot;Password=vragi!Proidut";
			this.sqlConnection1.FireInfoMessageEventOnUserErrors = false;
			// 
			// SystemWorks
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(628, 374);
			this.Controls.Add(this.panel3);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.panel2);
			this.Name = "SystemWorks";
			this.Text = "Обслуживание базы данных";
			this.panel3.ResumeLayout(false);
			this.panel3.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.CheckBox cBoxCleanBlankProperties;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnDoWork;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Button button3;
		private System.Data.SqlClient.SqlCommand cmdCleanBlankProperties;
		private System.Data.SqlClient.SqlConnection sqlConnection1;
	}
}