namespace VK_Parser
{
    partial class settings_form
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
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.authorization = new System.Windows.Forms.CheckBox();
            this.button4 = new System.Windows.Forms.Button();
            this.time = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.CB_autosave_time = new System.Windows.Forms.ComboBox();
            this.CB_smart_save = new System.Windows.Forms.CheckBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Enabled = false;
            this.checkBox1.Location = new System.Drawing.Point(12, 12);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(283, 17);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "запускать при старте logs и json в отдельном окне";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(352, 246);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 42;
            this.button1.Text = "назад";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button2.Location = new System.Drawing.Point(271, 246);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 43;
            this.button2.Text = "применить";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Enabled = false;
            this.button3.Location = new System.Drawing.Point(301, 8);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(126, 23);
            this.button3.TabIndex = 44;
            this.button3.Text = "открыть logs и json";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // authorization
            // 
            this.authorization.AutoSize = true;
            this.authorization.Location = new System.Drawing.Point(12, 35);
            this.authorization.Name = "authorization";
            this.authorization.Size = new System.Drawing.Size(174, 17);
            this.authorization.TabIndex = 46;
            this.authorization.Text = "оставаться авторизованным";
            this.authorization.UseVisualStyleBackColor = true;
            this.authorization.CheckedChanged += new System.EventHandler(this.authorization_CheckedChanged);
            // 
            // button4
            // 
            this.button4.Enabled = false;
            this.button4.Location = new System.Drawing.Point(301, 35);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(126, 23);
            this.button4.TabIndex = 47;
            this.button4.Text = "смена пользователя";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // time
            // 
            this.time.FormattingEnabled = true;
            this.time.Items.AddRange(new object[] {
            "+0",
            "+1",
            "+2",
            "+3",
            "+4",
            "+5",
            "+6",
            "+7"});
            this.time.Location = new System.Drawing.Point(12, 71);
            this.time.Name = "time";
            this.time.Size = new System.Drawing.Size(145, 21);
            this.time.TabIndex = 48;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 13);
            this.label1.TabIndex = 49;
            this.label1.Text = "часовой пояс (+ от МСК)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(145, 13);
            this.label2.TabIndex = 51;
            this.label2.Text = "время автоснимка (раз в..)";
            // 
            // CB_autosave_time
            // 
            this.CB_autosave_time.FormattingEnabled = true;
            this.CB_autosave_time.Items.AddRange(new object[] {
            "30 сек",
            "1 мин",
            "3 мин",
            "5 мин",
            "10 мин",
            "30 мин",
            "60 мин",
            "12 часов",
            "1 день",
            "10 сек (не рекомендуется)"});
            this.CB_autosave_time.Location = new System.Drawing.Point(12, 111);
            this.CB_autosave_time.Name = "CB_autosave_time";
            this.CB_autosave_time.Size = new System.Drawing.Size(145, 21);
            this.CB_autosave_time.TabIndex = 50;
            // 
            // CB_smart_save
            // 
            this.CB_smart_save.AutoSize = true;
            this.CB_smart_save.Location = new System.Drawing.Point(12, 138);
            this.CB_smart_save.Name = "CB_smart_save";
            this.CB_smart_save.Size = new System.Drawing.Size(119, 17);
            this.CB_smart_save.TabIndex = 52;
            this.CB_smart_save.Text = "умное сохранение";
            this.CB_smart_save.UseVisualStyleBackColor = true;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F);
            this.linkLabel1.Location = new System.Drawing.Point(137, 137);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(14, 16);
            this.linkLabel1.TabIndex = 53;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "?";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(165, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 55;
            this.label3.Text = "тема";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "old thema (16.06.22)",
            "new thema (26.11.22)"});
            this.comboBox1.Location = new System.Drawing.Point(165, 71);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(145, 21);
            this.comboBox1.TabIndex = 54;
            // 
            // settings_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(439, 281);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.CB_smart_save);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.CB_autosave_time);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.time);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.authorization);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.checkBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "settings_form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "настройки VK_Parser";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        public System.Windows.Forms.CheckBox authorization;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.ComboBox time;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.ComboBox CB_autosave_time;
        public System.Windows.Forms.CheckBox CB_smart_save;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.ComboBox comboBox1;
    }
}