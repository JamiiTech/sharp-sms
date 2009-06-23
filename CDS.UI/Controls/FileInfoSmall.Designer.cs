namespace CDS.UI
{
	partial class FileInfoSmall
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.tBoxFilename = new System.Windows.Forms.TextBox();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.tBoxFileType = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(8, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(29, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Тип:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(7, 36);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(32, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "Имя:";
			// 
			// tBoxFilename
			// 
			this.tBoxFilename.Location = new System.Drawing.Point(43, 33);
			this.tBoxFilename.Name = "tBoxFilename";
			this.tBoxFilename.Size = new System.Drawing.Size(219, 20);
			this.tBoxFilename.TabIndex = 3;
			// 
			// checkBox1
			// 
			this.checkBox1.AutoSize = true;
			this.checkBox1.Location = new System.Drawing.Point(10, 63);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(15, 14);
			this.checkBox1.TabIndex = 4;
			this.checkBox1.UseVisualStyleBackColor = true;
			// 
			// tBoxFileType
			// 
			this.tBoxFileType.Location = new System.Drawing.Point(44, 7);
			this.tBoxFileType.Name = "tBoxFileType";
			this.tBoxFileType.Size = new System.Drawing.Size(218, 20);
			this.tBoxFileType.TabIndex = 5;
			// 
			// File
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tBoxFileType);
			this.Controls.Add(this.checkBox1);
			this.Controls.Add(this.tBoxFilename);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Name = "File";
			this.Size = new System.Drawing.Size(273, 87);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox tBoxFilename;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.TextBox tBoxFileType;
	}
}
