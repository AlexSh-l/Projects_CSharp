namespace KnockoffPaint
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.FileSavBtn = new System.Windows.Forms.Button();
            this.FileOpnBtn = new System.Windows.Forms.Button();
            this.FgrOrg = new System.Windows.Forms.ComboBox();
            this.Clrbtn = new System.Windows.Forms.Button();
            this.Lstbtn = new System.Windows.Forms.Button();
            this.Canv = new System.Windows.Forms.PictureBox();
            this.LoadDllBtn = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Canv)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.panel1.Controls.Add(this.LoadDllBtn);
            this.panel1.Controls.Add(this.FileSavBtn);
            this.panel1.Controls.Add(this.FileOpnBtn);
            this.panel1.Controls.Add(this.FgrOrg);
            this.panel1.Controls.Add(this.Clrbtn);
            this.panel1.Controls.Add(this.Lstbtn);
            this.panel1.Location = new System.Drawing.Point(-1, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(973, 100);
            this.panel1.TabIndex = 1;
            // 
            // FileSavBtn
            // 
            this.FileSavBtn.Location = new System.Drawing.Point(533, 11);
            this.FileSavBtn.Name = "FileSavBtn";
            this.FileSavBtn.Size = new System.Drawing.Size(90, 79);
            this.FileSavBtn.TabIndex = 15;
            this.FileSavBtn.Text = "Save file";
            this.FileSavBtn.UseVisualStyleBackColor = true;
            this.FileSavBtn.Click += new System.EventHandler(this.FileSavBtn_Click);
            // 
            // FileOpnBtn
            // 
            this.FileOpnBtn.Location = new System.Drawing.Point(436, 11);
            this.FileOpnBtn.Name = "FileOpnBtn";
            this.FileOpnBtn.Size = new System.Drawing.Size(91, 79);
            this.FileOpnBtn.TabIndex = 14;
            this.FileOpnBtn.Text = "Open file";
            this.FileOpnBtn.UseVisualStyleBackColor = true;
            this.FileOpnBtn.Click += new System.EventHandler(this.FileOpnBtn_Click);
            // 
            // FgrOrg
            // 
            this.FgrOrg.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.FgrOrg.FormattingEnabled = true;
            this.FgrOrg.Items.AddRange(new object[] {
            "Line",
            "Rectangle",
            "Square",
            "Ellipsis",
            "Circle",
            "Polyline"});
            this.FgrOrg.Location = new System.Drawing.Point(141, 39);
            this.FgrOrg.Name = "FgrOrg";
            this.FgrOrg.Size = new System.Drawing.Size(242, 24);
            this.FgrOrg.TabIndex = 12;
            // 
            // Clrbtn
            // 
            this.Clrbtn.Location = new System.Drawing.Point(846, 19);
            this.Clrbtn.Name = "Clrbtn";
            this.Clrbtn.Size = new System.Drawing.Size(102, 63);
            this.Clrbtn.TabIndex = 11;
            this.Clrbtn.Text = "Clear";
            this.Clrbtn.UseVisualStyleBackColor = true;
            this.Clrbtn.Click += new System.EventHandler(this.Clrbtn_Click);
            // 
            // Lstbtn
            // 
            this.Lstbtn.Location = new System.Drawing.Point(673, 19);
            this.Lstbtn.Name = "Lstbtn";
            this.Lstbtn.Size = new System.Drawing.Size(143, 63);
            this.Lstbtn.TabIndex = 10;
            this.Lstbtn.Text = "List";
            this.Lstbtn.UseVisualStyleBackColor = true;
            this.Lstbtn.Click += new System.EventHandler(this.Lstbtn_Click);
            // 
            // Canv
            // 
            this.Canv.Location = new System.Drawing.Point(-1, 100);
            this.Canv.Name = "Canv";
            this.Canv.Size = new System.Drawing.Size(973, 429);
            this.Canv.TabIndex = 3;
            this.Canv.TabStop = false;
            this.Canv.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Canv_MouseClick);
            this.Canv.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Canv_MouseDoubleClick);
            // 
            // LoadDllBtn
            // 
            this.LoadDllBtn.Location = new System.Drawing.Point(14, 19);
            this.LoadDllBtn.Name = "LoadDllBtn";
            this.LoadDllBtn.Size = new System.Drawing.Size(111, 63);
            this.LoadDllBtn.TabIndex = 16;
            this.LoadDllBtn.Text = "LoadDll";
            this.LoadDllBtn.UseVisualStyleBackColor = true;
            this.LoadDllBtn.Click += new System.EventHandler(this.LoadDllBtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(970, 526);
            this.Controls.Add(this.Canv);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(988, 573);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(988, 573);
            this.Name = "Form1";
            this.Text = "Knockoff Paint";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Canv)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox Canv;
        private System.Windows.Forms.Button Lstbtn;
        private System.Windows.Forms.Button Clrbtn;
        private System.Windows.Forms.ComboBox FgrOrg;
        private System.Windows.Forms.Button FileSavBtn;
        private System.Windows.Forms.Button FileOpnBtn;
        private System.Windows.Forms.Button LoadDllBtn;
    }
}

