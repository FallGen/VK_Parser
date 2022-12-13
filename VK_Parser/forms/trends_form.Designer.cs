namespace VK_Parser
{
    partial class trends_form
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series7 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CB_from = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.CB_before = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.egoldsRadioButton1 = new yt_DesignUI.EgoldsRadioButton();
            this.gifts = new yt_DesignUI.EgoldsToggleSwitch();
            this.pades = new yt_DesignUI.EgoldsToggleSwitch();
            this.audios = new yt_DesignUI.EgoldsToggleSwitch();
            this.videos = new yt_DesignUI.EgoldsToggleSwitch();
            this.photos = new yt_DesignUI.EgoldsToggleSwitch();
            this.followers = new yt_DesignUI.EgoldsToggleSwitch();
            this.egoldsRadioButton2 = new yt_DesignUI.EgoldsRadioButton();
            this.friends = new yt_DesignUI.EgoldsToggleSwitch();
            this.egoldsRadioButton3 = new yt_DesignUI.EgoldsRadioButton();
            this.yt_Button1 = new yt_DesignUI.yt_Button();
            this.button1 = new yt_DesignUI.yt_Button();
            this.egoldsFormStyle1 = new yt_DesignUI.Components.EgoldsFormStyle(this.components);
            this.yt_Button2 = new yt_DesignUI.yt_Button();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // chart1
            // 
            this.chart1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Font = new System.Drawing.Font("Verdana", 9F);
            legend1.IsTextAutoFit = false;
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(161, -1);
            this.chart1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 5);
            this.chart1.Name = "chart1";
            series1.BorderWidth = 5;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series1.Legend = "Legend1";
            series1.Name = "друзья";
            series2.BorderWidth = 5;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series2.Legend = "Legend1";
            series2.Name = "подписчики";
            series3.BorderWidth = 5;
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series3.Legend = "Legend1";
            series3.Name = "фотографий";
            series4.BorderWidth = 5;
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series4.Legend = "Legend1";
            series4.Name = "видеозаписей";
            series5.BorderWidth = 5;
            series5.ChartArea = "ChartArea1";
            series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series5.Legend = "Legend1";
            series5.Name = "заудиозаписей";
            series6.BorderWidth = 5;
            series6.ChartArea = "ChartArea1";
            series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series6.Legend = "Legend1";
            series6.Name = "сообществ";
            series6.YValuesPerPoint = 2;
            series7.BorderWidth = 5;
            series7.ChartArea = "ChartArea1";
            series7.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series7.Legend = "Legend1";
            series7.Name = "подарков";
            this.chart1.Series.Add(series1);
            this.chart1.Series.Add(series2);
            this.chart1.Series.Add(series3);
            this.chart1.Series.Add(series4);
            this.chart1.Series.Add(series5);
            this.chart1.Series.Add(series6);
            this.chart1.Series.Add(series7);
            this.chart1.Size = new System.Drawing.Size(1168, 616);
            this.chart1.TabIndex = 7;
            this.chart1.Text = "chart1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 9F);
            this.label3.Location = new System.Drawing.Point(161, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 14);
            this.label3.TabIndex = 12;
            this.label3.Text = "100";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 9F);
            this.label4.Location = new System.Drawing.Point(161, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 14);
            this.label4.TabIndex = 13;
            this.label4.Text = "100";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Verdana", 9F);
            this.label5.Location = new System.Drawing.Point(161, 93);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 14);
            this.label5.TabIndex = 14;
            this.label5.Text = "100";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Verdana", 9F);
            this.label6.Location = new System.Drawing.Point(161, 114);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 14);
            this.label6.TabIndex = 17;
            this.label6.Text = "100";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Verdana", 9F);
            this.label7.Location = new System.Drawing.Point(161, 135);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 14);
            this.label7.TabIndex = 16;
            this.label7.Text = "100";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Verdana", 9F);
            this.label8.Location = new System.Drawing.Point(161, 156);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(31, 14);
            this.label8.TabIndex = 15;
            this.label8.Text = "100";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Verdana", 9F);
            this.label9.Location = new System.Drawing.Point(161, 177);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(31, 14);
            this.label9.TabIndex = 18;
            this.label9.Text = "100";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 11.25F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Tomato;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column1,
            this.Column7,
            this.Column2,
            this.Column8,
            this.Column9,
            this.Column6});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Verdana", 8.25F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Tomato;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.GridColor = System.Drawing.Color.White;
            this.dataGridView1.Location = new System.Drawing.Point(219, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(1079, 616);
            this.dataGridView1.TabIndex = 72;
            this.dataGridView1.Visible = false;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "дата снимка";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column3.Width = 120;
            // 
            // Column4
            // 
            this.Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column4.HeaderText = "статус";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column5
            // 
            this.Column5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Column5.HeaderText = "день рождения";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column5.Width = 105;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.HeaderText = "город";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column7
            // 
            this.Column7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column7.HeaderText = "место учебы";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column2.HeaderText = "сайт";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column8
            // 
            this.Column8.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column8.HeaderText = "семейное положение";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column9
            // 
            this.Column9.HeaderText = "номер телефона";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            this.Column9.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "nickname";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column6.Width = 120;
            // 
            // CB_from
            // 
            this.CB_from.BackColor = System.Drawing.SystemColors.Window;
            this.CB_from.Font = new System.Drawing.Font("Arial", 11.25F);
            this.CB_from.FormattingEnabled = true;
            this.CB_from.Location = new System.Drawing.Point(38, 25);
            this.CB_from.Name = "CB_from";
            this.CB_from.Size = new System.Drawing.Size(143, 25);
            this.CB_from.TabIndex = 84;
            this.CB_from.SelectionChangeCommitted += new System.EventHandler(this.CB_from_SelectionChangeCommitted);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Arial", 11.25F);
            this.label10.Location = new System.Drawing.Point(7, 28);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(23, 17);
            this.label10.TabIndex = 86;
            this.label10.Text = "от";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Arial", 11.25F);
            this.label11.Location = new System.Drawing.Point(7, 59);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(25, 17);
            this.label11.TabIndex = 88;
            this.label11.Text = "до";
            // 
            // CB_before
            // 
            this.CB_before.Font = new System.Drawing.Font("Arial", 11.25F);
            this.CB_before.FormattingEnabled = true;
            this.CB_before.Location = new System.Drawing.Point(38, 56);
            this.CB_before.Name = "CB_before";
            this.CB_before.Size = new System.Drawing.Size(143, 25);
            this.CB_before.TabIndex = 87;
            this.CB_before.SelectedIndexChanged += new System.EventHandler(this.CB_before_SelectedIndexChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Arial", 11.25F);
            this.label12.Location = new System.Drawing.Point(12, 489);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(110, 17);
            this.label12.TabIndex = 89;
            this.label12.Text = "всего снимков:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.CB_from);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.CB_before);
            this.groupBox1.Font = new System.Drawing.Font("Arial", 11.25F);
            this.groupBox1.Location = new System.Drawing.Point(5, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(196, 96);
            this.groupBox1.TabIndex = 91;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "диапазон снимков";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.egoldsRadioButton1);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.gifts);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.pades);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.audios);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.videos);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.photos);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.followers);
            this.groupBox2.Controls.Add(this.egoldsRadioButton2);
            this.groupBox2.Controls.Add(this.friends);
            this.groupBox2.Controls.Add(this.egoldsRadioButton3);
            this.groupBox2.Font = new System.Drawing.Font("Arial", 11.25F);
            this.groupBox2.Location = new System.Drawing.Point(5, 114);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(196, 257);
            this.groupBox2.TabIndex = 92;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "отображение";
            // 
            // egoldsRadioButton1
            // 
            this.egoldsRadioButton1.AutoSize = true;
            this.egoldsRadioButton1.BackColor = System.Drawing.Color.White;
            this.egoldsRadioButton1.Checked = true;
            this.egoldsRadioButton1.Font = new System.Drawing.Font("Verdana", 9F);
            this.egoldsRadioButton1.Location = new System.Drawing.Point(8, 25);
            this.egoldsRadioButton1.Margin = new System.Windows.Forms.Padding(4);
            this.egoldsRadioButton1.Name = "egoldsRadioButton1";
            this.egoldsRadioButton1.Size = new System.Drawing.Size(86, 18);
            this.egoldsRadioButton1.TabIndex = 74;
            this.egoldsRadioButton1.TabStop = true;
            this.egoldsRadioButton1.Text = "графики:";
            this.egoldsRadioButton1.UseVisualStyleBackColor = false;
            this.egoldsRadioButton1.CheckedChanged += new System.EventHandler(this.egoldsRadioButton1_CheckedChanged);
            // 
            // gifts
            // 
            this.gifts.BackColor = System.Drawing.Color.White;
            this.gifts.BackColorOFF = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.gifts.BackColorON = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
            this.gifts.Checked = false;
            this.gifts.Cursor = System.Windows.Forms.Cursors.Hand;
            this.gifts.Font = new System.Drawing.Font("Verdana", 9F);
            this.gifts.Location = new System.Drawing.Point(20, 176);
            this.gifts.Name = "gifts";
            this.gifts.Size = new System.Drawing.Size(105, 15);
            this.gifts.TabIndex = 83;
            this.gifts.Text = "подарки";
            this.gifts.TextOnChecked = "";
            this.gifts.CheckedChanged += new yt_DesignUI.EgoldsToggleSwitch.OnCheckedChangedHandler(this.gifts_CheckedChanged);
            // 
            // pades
            // 
            this.pades.BackColor = System.Drawing.Color.White;
            this.pades.BackColorOFF = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.pades.BackColorON = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
            this.pades.Checked = false;
            this.pades.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pades.Font = new System.Drawing.Font("Verdana", 9F);
            this.pades.Location = new System.Drawing.Point(20, 155);
            this.pades.Name = "pades";
            this.pades.Size = new System.Drawing.Size(128, 15);
            this.pades.TabIndex = 82;
            this.pades.Text = "сообщества";
            this.pades.TextOnChecked = "";
            this.pades.CheckedChanged += new yt_DesignUI.EgoldsToggleSwitch.OnCheckedChangedHandler(this.pades_CheckedChanged);
            // 
            // audios
            // 
            this.audios.BackColor = System.Drawing.Color.White;
            this.audios.BackColorOFF = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.audios.BackColorON = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
            this.audios.Checked = false;
            this.audios.Cursor = System.Windows.Forms.Cursors.Hand;
            this.audios.Font = new System.Drawing.Font("Verdana", 9F);
            this.audios.Location = new System.Drawing.Point(20, 134);
            this.audios.Name = "audios";
            this.audios.Size = new System.Drawing.Size(133, 15);
            this.audios.TabIndex = 81;
            this.audios.Text = "аудиозаписи";
            this.audios.TextOnChecked = "";
            this.audios.CheckedChanged += new yt_DesignUI.EgoldsToggleSwitch.OnCheckedChangedHandler(this.audios_CheckedChanged);
            // 
            // videos
            // 
            this.videos.BackColor = System.Drawing.Color.White;
            this.videos.BackColorOFF = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.videos.BackColorON = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
            this.videos.Checked = false;
            this.videos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.videos.Font = new System.Drawing.Font("Verdana", 9F);
            this.videos.Location = new System.Drawing.Point(20, 113);
            this.videos.Name = "videos";
            this.videos.Size = new System.Drawing.Size(133, 15);
            this.videos.TabIndex = 80;
            this.videos.Text = "видеозаписи";
            this.videos.TextOnChecked = "";
            this.videos.CheckedChanged += new yt_DesignUI.EgoldsToggleSwitch.OnCheckedChangedHandler(this.videos_CheckedChanged);
            // 
            // photos
            // 
            this.photos.BackColor = System.Drawing.Color.White;
            this.photos.BackColorOFF = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.photos.BackColorON = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
            this.photos.Checked = false;
            this.photos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.photos.Font = new System.Drawing.Font("Verdana", 9F);
            this.photos.Location = new System.Drawing.Point(20, 92);
            this.photos.Name = "photos";
            this.photos.Size = new System.Drawing.Size(133, 15);
            this.photos.TabIndex = 79;
            this.photos.Text = "фотографии";
            this.photos.TextOnChecked = "";
            this.photos.CheckedChanged += new yt_DesignUI.EgoldsToggleSwitch.OnCheckedChangedHandler(this.photos_CheckedChanged);
            // 
            // followers
            // 
            this.followers.BackColor = System.Drawing.Color.White;
            this.followers.BackColorOFF = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.followers.BackColorON = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
            this.followers.Checked = false;
            this.followers.Cursor = System.Windows.Forms.Cursors.Hand;
            this.followers.Font = new System.Drawing.Font("Verdana", 9F);
            this.followers.Location = new System.Drawing.Point(20, 71);
            this.followers.Name = "followers";
            this.followers.Size = new System.Drawing.Size(127, 15);
            this.followers.TabIndex = 78;
            this.followers.Text = "подписчики";
            this.followers.TextOnChecked = "";
            this.followers.CheckedChanged += new yt_DesignUI.EgoldsToggleSwitch.OnCheckedChangedHandler(this.followers_CheckedChanged);
            // 
            // egoldsRadioButton2
            // 
            this.egoldsRadioButton2.AutoSize = true;
            this.egoldsRadioButton2.BackColor = System.Drawing.Color.White;
            this.egoldsRadioButton2.Font = new System.Drawing.Font("Verdana", 9F);
            this.egoldsRadioButton2.Location = new System.Drawing.Point(8, 203);
            this.egoldsRadioButton2.Margin = new System.Windows.Forms.Padding(4);
            this.egoldsRadioButton2.Name = "egoldsRadioButton2";
            this.egoldsRadioButton2.Size = new System.Drawing.Size(153, 18);
            this.egoldsRadioButton2.TabIndex = 75;
            this.egoldsRadioButton2.Text = "таблица осн. инфо.";
            this.egoldsRadioButton2.UseVisualStyleBackColor = false;
            this.egoldsRadioButton2.CheckedChanged += new System.EventHandler(this.egoldsRadioButton2_CheckedChanged);
            // 
            // friends
            // 
            this.friends.BackColor = System.Drawing.Color.White;
            this.friends.BackColorOFF = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.friends.BackColorON = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
            this.friends.Checked = true;
            this.friends.Cursor = System.Windows.Forms.Cursors.Hand;
            this.friends.Font = new System.Drawing.Font("Verdana", 9F);
            this.friends.Location = new System.Drawing.Point(20, 50);
            this.friends.Name = "friends";
            this.friends.Size = new System.Drawing.Size(93, 15);
            this.friends.TabIndex = 77;
            this.friends.Text = "друзья";
            this.friends.TextOnChecked = "";
            this.friends.CheckedChanged += new yt_DesignUI.EgoldsToggleSwitch.OnCheckedChangedHandler(this.friends_CheckedChanged);
            // 
            // egoldsRadioButton3
            // 
            this.egoldsRadioButton3.AutoSize = true;
            this.egoldsRadioButton3.BackColor = System.Drawing.Color.White;
            this.egoldsRadioButton3.Font = new System.Drawing.Font("Verdana", 9F);
            this.egoldsRadioButton3.Location = new System.Drawing.Point(8, 229);
            this.egoldsRadioButton3.Margin = new System.Windows.Forms.Padding(4);
            this.egoldsRadioButton3.Name = "egoldsRadioButton3";
            this.egoldsRadioButton3.Size = new System.Drawing.Size(155, 18);
            this.egoldsRadioButton3.TabIndex = 76;
            this.egoldsRadioButton3.Text = "таблица доп. инфо.";
            this.egoldsRadioButton3.UseVisualStyleBackColor = false;
            this.egoldsRadioButton3.CheckedChanged += new System.EventHandler(this.egoldsRadioButton3_CheckedChanged);
            // 
            // yt_Button1
            // 
            this.yt_Button1.BackColor = System.Drawing.Color.Tomato;
            this.yt_Button1.BackColorAdditional = System.Drawing.Color.Gray;
            this.yt_Button1.BackColorGradientEnabled = false;
            this.yt_Button1.BackColorGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.yt_Button1.BorderColor = System.Drawing.Color.Transparent;
            this.yt_Button1.BorderColorEnabled = false;
            this.yt_Button1.BorderColorOnHover = System.Drawing.Color.Tomato;
            this.yt_Button1.BorderColorOnHoverEnabled = false;
            this.yt_Button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.yt_Button1.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.yt_Button1.ForeColor = System.Drawing.Color.White;
            this.yt_Button1.Location = new System.Drawing.Point(10, 377);
            this.yt_Button1.Name = "yt_Button1";
            this.yt_Button1.RippleColor = System.Drawing.Color.Black;
            this.yt_Button1.RoundingEnable = false;
            this.yt_Button1.Size = new System.Drawing.Size(186, 35);
            this.yt_Button1.TabIndex = 90;
            this.yt_Button1.Text = "снимки друзей/подписчиков";
            this.yt_Button1.TextHover = null;
            this.yt_Button1.UseDownPressEffectOnClick = true;
            this.yt_Button1.UseRippleEffect = true;
            this.yt_Button1.UseZoomEffectOnHover = true;
            this.yt_Button1.Click += new System.EventHandler(this.yt_Button1_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Tomato;
            this.button1.BackColorAdditional = System.Drawing.Color.Gray;
            this.button1.BackColorGradientEnabled = false;
            this.button1.BackColorGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.button1.BorderColor = System.Drawing.Color.Transparent;
            this.button1.BorderColorEnabled = false;
            this.button1.BorderColorOnHover = System.Drawing.Color.Tomato;
            this.button1.BorderColorOnHoverEnabled = false;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(10, 567);
            this.button1.Name = "button1";
            this.button1.RippleColor = System.Drawing.Color.Black;
            this.button1.RoundingEnable = false;
            this.button1.Size = new System.Drawing.Size(186, 35);
            this.button1.TabIndex = 61;
            this.button1.Text = "закрыть статистику";
            this.button1.TextHover = null;
            this.button1.UseDownPressEffectOnClick = true;
            this.button1.UseRippleEffect = true;
            this.button1.UseZoomEffectOnHover = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // egoldsFormStyle1
            // 
            this.egoldsFormStyle1.AllowUserResize = false;
            this.egoldsFormStyle1.BackColor = System.Drawing.Color.White;
            this.egoldsFormStyle1.ContextMenuForm = null;
            this.egoldsFormStyle1.ControlBoxButtonsWidth = 18;
            this.egoldsFormStyle1.EnableControlBoxIconsLight = false;
            this.egoldsFormStyle1.EnableControlBoxMouseLight = false;
            this.egoldsFormStyle1.Form = this;
            this.egoldsFormStyle1.FormStyle = yt_DesignUI.Components.EgoldsFormStyle.fStyle.UserStyle;
            this.egoldsFormStyle1.HeaderColor = System.Drawing.Color.Tomato;
            this.egoldsFormStyle1.HeaderColorAdditional = System.Drawing.Color.White;
            this.egoldsFormStyle1.HeaderColorGradientEnable = false;
            this.egoldsFormStyle1.HeaderColorGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.egoldsFormStyle1.HeaderHeight = 18;
            this.egoldsFormStyle1.HeaderImage = null;
            this.egoldsFormStyle1.HeaderTextColor = System.Drawing.Color.White;
            this.egoldsFormStyle1.HeaderTextFont = new System.Drawing.Font("Segoe UI", 9.75F);
            // 
            // yt_Button2
            // 
            this.yt_Button2.BackColor = System.Drawing.Color.Tomato;
            this.yt_Button2.BackColorAdditional = System.Drawing.Color.Gray;
            this.yt_Button2.BackColorGradientEnabled = false;
            this.yt_Button2.BackColorGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.yt_Button2.BorderColor = System.Drawing.Color.Transparent;
            this.yt_Button2.BorderColorEnabled = false;
            this.yt_Button2.BorderColorOnHover = System.Drawing.Color.Tomato;
            this.yt_Button2.BorderColorOnHoverEnabled = false;
            this.yt_Button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.yt_Button2.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.yt_Button2.ForeColor = System.Drawing.Color.White;
            this.yt_Button2.Location = new System.Drawing.Point(10, 509);
            this.yt_Button2.Name = "yt_Button2";
            this.yt_Button2.RippleColor = System.Drawing.Color.Black;
            this.yt_Button2.RoundingEnable = false;
            this.yt_Button2.Size = new System.Drawing.Size(186, 35);
            this.yt_Button2.TabIndex = 93;
            this.yt_Button2.Text = "удалить все снимки";
            this.yt_Button2.TextHover = null;
            this.yt_Button2.UseDownPressEffectOnClick = true;
            this.yt_Button2.UseRippleEffect = true;
            this.yt_Button2.UseZoomEffectOnHover = true;
            this.yt_Button2.Click += new System.EventHandler(this.yt_Button2_Click);
            // 
            // trends_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1298, 614);
            this.Controls.Add(this.yt_Button2);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.yt_Button1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.dataGridView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "trends_form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "статистика снимков";
            this.Load += new System.EventHandler(this.trends_form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private yt_DesignUI.yt_Button button1;
        private yt_DesignUI.Components.EgoldsFormStyle egoldsFormStyle1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private yt_DesignUI.EgoldsToggleSwitch gifts;
        private yt_DesignUI.EgoldsToggleSwitch pades;
        private yt_DesignUI.EgoldsToggleSwitch audios;
        private yt_DesignUI.EgoldsToggleSwitch videos;
        private yt_DesignUI.EgoldsToggleSwitch photos;
        private yt_DesignUI.EgoldsToggleSwitch followers;
        private yt_DesignUI.EgoldsToggleSwitch friends;
        private yt_DesignUI.EgoldsRadioButton egoldsRadioButton3;
        private yt_DesignUI.EgoldsRadioButton egoldsRadioButton2;
        private yt_DesignUI.EgoldsRadioButton egoldsRadioButton1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox CB_before;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox CB_from;
        private System.Windows.Forms.Label label12;
        private yt_DesignUI.yt_Button yt_Button1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private yt_DesignUI.yt_Button yt_Button2;
    }
}