namespace Library
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
            this.dataGrid = new System.Windows.Forms.DataGridView();
            this.isbnHeader = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.authorHeader = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameHeader = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.publisherHeader = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.yearHeader = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.priceHeader = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.loadBtn = new System.Windows.Forms.Button();
            this.saveBtn = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGrid
            // 
            this.dataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.isbnHeader,
            this.authorHeader,
            this.nameHeader,
            this.publisherHeader,
            this.yearHeader,
            this.priceHeader});
            this.dataGrid.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataGrid.Location = new System.Drawing.Point(0, 0);
            this.dataGrid.Name = "dataGrid";
            this.dataGrid.RowHeadersWidth = 62;
            this.dataGrid.RowTemplate.Height = 28;
            this.dataGrid.Size = new System.Drawing.Size(926, 417);
            this.dataGrid.TabIndex = 0;
            this.dataGrid.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGrid_ColumnHeaderMouseClick);
            this.dataGrid.ColumnHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGrid_ColumnHeaderMouseDoubleClick);
            this.dataGrid.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dataGrid_RowsRemoved);
            // 
            // isbnHeader
            // 
            this.isbnHeader.HeaderText = "ISBN";
            this.isbnHeader.MinimumWidth = 8;
            this.isbnHeader.Name = "isbnHeader";
            this.isbnHeader.Width = 150;
            // 
            // authorHeader
            // 
            this.authorHeader.HeaderText = "Author";
            this.authorHeader.MinimumWidth = 8;
            this.authorHeader.Name = "authorHeader";
            this.authorHeader.Width = 150;
            // 
            // nameHeader
            // 
            this.nameHeader.HeaderText = "Name";
            this.nameHeader.MinimumWidth = 8;
            this.nameHeader.Name = "nameHeader";
            this.nameHeader.Width = 150;
            // 
            // publisherHeader
            // 
            this.publisherHeader.HeaderText = "Publisher";
            this.publisherHeader.MinimumWidth = 8;
            this.publisherHeader.Name = "publisherHeader";
            this.publisherHeader.Width = 150;
            // 
            // yearHeader
            // 
            this.yearHeader.HeaderText = "Year";
            this.yearHeader.MinimumWidth = 8;
            this.yearHeader.Name = "yearHeader";
            this.yearHeader.Width = 150;
            // 
            // priceHeader
            // 
            this.priceHeader.HeaderText = "Price";
            this.priceHeader.MinimumWidth = 8;
            this.priceHeader.Name = "priceHeader";
            this.priceHeader.Width = 150;
            // 
            // loadBtn
            // 
            this.loadBtn.Location = new System.Drawing.Point(12, 432);
            this.loadBtn.Name = "loadBtn";
            this.loadBtn.Size = new System.Drawing.Size(429, 44);
            this.loadBtn.TabIndex = 1;
            this.loadBtn.Text = "Load";
            this.loadBtn.UseVisualStyleBackColor = true;
            this.loadBtn.Click += new System.EventHandler(this.loadBtn_Click);
            // 
            // saveBtn
            // 
            this.saveBtn.Location = new System.Drawing.Point(499, 432);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(415, 44);
            this.saveBtn.TabIndex = 2;
            this.saveBtn.Text = "Save";
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(0, 0);
            this.button1.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(36, 12);
            this.button1.TabIndex = 3;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(926, 516);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.loadBtn);
            this.Controls.Add(this.dataGrid);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(948, 572);
            this.MinimumSize = new System.Drawing.Size(948, 572);
            this.Name = "Form1";
            this.Text = "Library";
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn isbnHeader;
        private System.Windows.Forms.DataGridViewTextBoxColumn authorHeader;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameHeader;
        private System.Windows.Forms.DataGridViewTextBoxColumn publisherHeader;
        private System.Windows.Forms.DataGridViewTextBoxColumn yearHeader;
        private System.Windows.Forms.DataGridViewTextBoxColumn priceHeader;
        private System.Windows.Forms.Button loadBtn;
        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.Button button1;
    }
}

