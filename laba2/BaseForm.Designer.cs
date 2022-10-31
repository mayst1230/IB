namespace Crypto
{
    partial class BaseForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
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
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BaseForm));
            this.label1 = new System.Windows.Forms.Label();
            this.keyTextBox = new System.Windows.Forms.TextBox();
            this.loadKeyButton = new System.Windows.Forms.Button();
            this.keyGenerateButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxImitation = new System.Windows.Forms.TextBox();
            this.encrTextBox = new System.Windows.Forms.TextBox();
            this.decrTextBox = new System.Windows.Forms.TextBox();
            this.fileLoadButton = new System.Windows.Forms.Button();
            this.fileEncryptButton = new System.Windows.Forms.Button();
            this.fileDecryptButton = new System.Windows.Forms.Button();
            this.fileSaveButton = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9F);
            this.label1.Location = new System.Drawing.Point(9, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ключ:";
            // 
            // keyTextBox
            // 
            this.keyTextBox.Location = new System.Drawing.Point(54, 10);
            this.keyTextBox.Name = "keyTextBox";
            this.keyTextBox.Size = new System.Drawing.Size(177, 22);
            this.keyTextBox.TabIndex = 1;
            // 
            // loadKeyButton
            // 
            this.loadKeyButton.Font = new System.Drawing.Font("Arial", 9F);
            this.loadKeyButton.Location = new System.Drawing.Point(246, 10);
            this.loadKeyButton.Name = "loadKeyButton";
            this.loadKeyButton.Size = new System.Drawing.Size(75, 22);
            this.loadKeyButton.TabIndex = 2;
            this.loadKeyButton.Text = "Загрузить";
            this.loadKeyButton.UseVisualStyleBackColor = true;
            this.loadKeyButton.Click += new System.EventHandler(this.loadKeyButton_Click);
            // 
            // keyGenerateButton
            // 
            this.keyGenerateButton.Font = new System.Drawing.Font("Arial", 9F);
            this.keyGenerateButton.Location = new System.Drawing.Point(327, 10);
            this.keyGenerateButton.Name = "keyGenerateButton";
            this.keyGenerateButton.Size = new System.Drawing.Size(93, 22);
            this.keyGenerateButton.TabIndex = 4;
            this.keyGenerateButton.Text = "Генерация";
            this.keyGenerateButton.UseVisualStyleBackColor = true;
            this.keyGenerateButton.Click += new System.EventHandler(this.keyGenerateButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBoxImitation);
            this.groupBox1.Controls.Add(this.encrTextBox);
            this.groupBox1.Controls.Add(this.decrTextBox);
            this.groupBox1.Location = new System.Drawing.Point(16, 58);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(460, 189);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Работа с алгоритмом";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 9F);
            this.label3.Location = new System.Drawing.Point(248, 106);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "Имитовставка:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 9F);
            this.label2.Location = new System.Drawing.Point(245, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(153, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Зашифрованные данные:";
            // 
            // textBoxImitation
            // 
            this.textBoxImitation.Location = new System.Drawing.Point(245, 122);
            this.textBoxImitation.Multiline = true;
            this.textBoxImitation.Name = "textBoxImitation";
            this.textBoxImitation.ReadOnly = true;
            this.textBoxImitation.Size = new System.Drawing.Size(209, 61);
            this.textBoxImitation.TabIndex = 2;
            // 
            // encrTextBox
            // 
            this.encrTextBox.Location = new System.Drawing.Point(245, 32);
            this.encrTextBox.Multiline = true;
            this.encrTextBox.Name = "encrTextBox";
            this.encrTextBox.ReadOnly = true;
            this.encrTextBox.Size = new System.Drawing.Size(209, 67);
            this.encrTextBox.TabIndex = 1;
            // 
            // decrTextBox
            // 
            this.decrTextBox.Location = new System.Drawing.Point(6, 21);
            this.decrTextBox.Multiline = true;
            this.decrTextBox.Name = "decrTextBox";
            this.decrTextBox.Size = new System.Drawing.Size(209, 162);
            this.decrTextBox.TabIndex = 0;
            // 
            // fileLoadButton
            // 
            this.fileLoadButton.Font = new System.Drawing.Font("Arial", 9F);
            this.fileLoadButton.Location = new System.Drawing.Point(12, 253);
            this.fileLoadButton.Name = "fileLoadButton";
            this.fileLoadButton.Size = new System.Drawing.Size(128, 23);
            this.fileLoadButton.TabIndex = 6;
            this.fileLoadButton.Text = "Загрузить файл";
            this.fileLoadButton.UseVisualStyleBackColor = true;
            this.fileLoadButton.Click += new System.EventHandler(this.fileLoadButton_Click);
            // 
            // fileEncryptButton
            // 
            this.fileEncryptButton.Location = new System.Drawing.Point(146, 253);
            this.fileEncryptButton.Name = "fileEncryptButton";
            this.fileEncryptButton.Size = new System.Drawing.Size(99, 23);
            this.fileEncryptButton.TabIndex = 7;
            this.fileEncryptButton.Text = "Шифрование";
            this.fileEncryptButton.UseVisualStyleBackColor = true;
            this.fileEncryptButton.Click += new System.EventHandler(this.fileEncryptButton_Click);
            // 
            // fileDecryptButton
            // 
            this.fileDecryptButton.Location = new System.Drawing.Point(261, 252);
            this.fileDecryptButton.Name = "fileDecryptButton";
            this.fileDecryptButton.Size = new System.Drawing.Size(127, 23);
            this.fileDecryptButton.TabIndex = 8;
            this.fileDecryptButton.Text = "Расшифрование";
            this.fileDecryptButton.UseVisualStyleBackColor = true;
            this.fileDecryptButton.Click += new System.EventHandler(this.fileDecryptButton_Click);
            // 
            // fileSaveButton
            // 
            this.fileSaveButton.Location = new System.Drawing.Point(394, 252);
            this.fileSaveButton.Name = "fileSaveButton";
            this.fileSaveButton.Size = new System.Drawing.Size(82, 23);
            this.fileSaveButton.TabIndex = 9;
            this.fileSaveButton.Text = "Сохранить";
            this.fileSaveButton.UseVisualStyleBackColor = true;
            this.fileSaveButton.Click += new System.EventHandler(this.fileSaveButton_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 292);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(464, 240);
            this.textBox1.TabIndex = 10;
            this.textBox1.Text = resources.GetString("textBox1.Text");
            // 
            // BaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(488, 541);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.fileSaveButton);
            this.Controls.Add(this.fileDecryptButton);
            this.Controls.Add(this.fileEncryptButton);
            this.Controls.Add(this.fileLoadButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.keyGenerateButton);
            this.Controls.Add(this.loadKeyButton);
            this.Controls.Add(this.keyTextBox);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Arial", 9.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "BaseForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ГОСТ 28147-89. Имитовставка";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox keyTextBox;
        private System.Windows.Forms.Button loadKeyButton;
        private System.Windows.Forms.Button keyGenerateButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox encrTextBox;
        private System.Windows.Forms.TextBox decrTextBox;
        private System.Windows.Forms.Button fileLoadButton;
        private System.Windows.Forms.Button fileEncryptButton;
        private System.Windows.Forms.Button fileDecryptButton;
        private System.Windows.Forms.Button fileSaveButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxImitation;
        private System.Windows.Forms.TextBox textBox1;
    }
}

