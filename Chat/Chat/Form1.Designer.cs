namespace Chat
{
    partial class Chatrr
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
            this.NickBox = new System.Windows.Forms.TextBox();
            this.RegBtn = new System.Windows.Forms.Button();
            this.MessageBox = new System.Windows.Forms.TextBox();
            this.EntrBtn = new System.Windows.Forms.Button();
            this.TxtBox = new System.Windows.Forms.TextBox();
            this.DiscBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // NickBox
            // 
            this.NickBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.NickBox.Location = new System.Drawing.Point(12, 34);
            this.NickBox.Name = "NickBox";
            this.NickBox.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.NickBox.Size = new System.Drawing.Size(259, 38);
            this.NickBox.TabIndex = 0;
            // 
            // RegBtn
            // 
            this.RegBtn.Location = new System.Drawing.Point(270, 34);
            this.RegBtn.Name = "RegBtn";
            this.RegBtn.Size = new System.Drawing.Size(102, 39);
            this.RegBtn.TabIndex = 1;
            this.RegBtn.Text = "Register";
            this.RegBtn.UseVisualStyleBackColor = true;
            this.RegBtn.Click += new System.EventHandler(this.RegBtn_Click);
            // 
            // MessageBox
            // 
            this.MessageBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MessageBox.Location = new System.Drawing.Point(12, 475);
            this.MessageBox.Name = "MessageBox";
            this.MessageBox.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.MessageBox.Size = new System.Drawing.Size(349, 41);
            this.MessageBox.TabIndex = 2;
            // 
            // EntrBtn
            // 
            this.EntrBtn.Location = new System.Drawing.Point(357, 475);
            this.EntrBtn.Name = "EntrBtn";
            this.EntrBtn.Size = new System.Drawing.Size(127, 41);
            this.EntrBtn.TabIndex = 3;
            this.EntrBtn.Text = "Enter";
            this.EntrBtn.UseVisualStyleBackColor = true;
            this.EntrBtn.Click += new System.EventHandler(this.EntrBtn_Click);
            // 
            // TxtBox
            // 
            this.TxtBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TxtBox.Location = new System.Drawing.Point(12, 112);
            this.TxtBox.Multiline = true;
            this.TxtBox.Name = "TxtBox";
            this.TxtBox.ReadOnly = true;
            this.TxtBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TxtBox.Size = new System.Drawing.Size(475, 357);
            this.TxtBox.TabIndex = 4;
            // 
            // DiscBtn
            // 
            this.DiscBtn.Location = new System.Drawing.Point(378, 33);
            this.DiscBtn.Name = "DiscBtn";
            this.DiscBtn.Size = new System.Drawing.Size(106, 39);
            this.DiscBtn.TabIndex = 5;
            this.DiscBtn.Text = "Disconnect";
            this.DiscBtn.UseVisualStyleBackColor = true;
            this.DiscBtn.Click += new System.EventHandler(this.DiscBtn_Click);
            // 
            // Chatrr
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(496, 524);
            this.Controls.Add(this.DiscBtn);
            this.Controls.Add(this.TxtBox);
            this.Controls.Add(this.EntrBtn);
            this.Controls.Add(this.MessageBox);
            this.Controls.Add(this.RegBtn);
            this.Controls.Add(this.NickBox);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(514, 571);
            this.MinimumSize = new System.Drawing.Size(514, 571);
            this.Name = "Chatrr";
            this.Text = "Chatrr";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Chatrr_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox NickBox;
        private System.Windows.Forms.Button RegBtn;
        private System.Windows.Forms.TextBox MessageBox;
        private System.Windows.Forms.Button EntrBtn;
        private System.Windows.Forms.TextBox TxtBox;
        private System.Windows.Forms.Button DiscBtn;
    }
}

