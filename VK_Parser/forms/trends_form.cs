using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace VK_Parser
{
    public partial class trends_form : Form
    {
        private OleDbConnection connection_DB;
        public static string way_DB = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source = Data_VK_parser.accdb";
       // public int[] array_snimok;

        public trends_form()
        {
            InitializeComponent();
            connection_DB = new OleDbConnection(way_DB);
            connection_DB.Open();

        }

        public int[] array_friends;
        public int[] array_followers;
        public int[] array_photos;
        public int[] array_videos;
        public int[] array_audios;
        public int[] array_pades;
        public int[] array_gifts;
        public string[] array_date;

        public string[,] dop_info;

        public int diapozon_from;
        public int diapozon_before;
        public int diapozon;

        public string global_id_Data = null;
        private void trends_form_Load(object sender, EventArgs e)
        {
            label12.Text = "всего снимков: " + array_date.Length;

            diapozon = array_date.Length;

            for (int i = 0; i < diapozon; i++) // заполняем CB "от" до предпоследнего
                CB_from.Items.Add(array_date[i]);

            for (int i = diapozon; i > 0; i--)// заполняем CB "до" от перного до последнего
                CB_before.Items.Add(array_date[i]);


            CB_from.SelectedIndex = 0; // выделить "от" первый элемент
            CB_before.SelectedIndex = 0;

            update_chart();
        }

        public void string_leight(int value)
        {
            array_friends = new int[value];
            array_followers = new int[value];
            array_photos = new int[value];
            array_videos = new int[value];
            array_audios = new int[value];
            array_pades = new int[value];
            array_gifts = new int[value];
            array_date = new string[value];
            
            dop_info = new string[9, value];
        }

        public void update_dgv_dop_info()
        {
            //8 столбиков
            int c = diapozon_before;
            //dataGridView1.RowCount = array_friends.Length;

            dataGridView1.Columns[1].HeaderText = "друзья";
            dataGridView1.Columns[2].HeaderText = "подписчики";
            dataGridView1.Columns[3].HeaderText = "фотографии";
            dataGridView1.Columns[4].HeaderText = "видеозаписи";
            dataGridView1.Columns[5].HeaderText = "аудиозаписи";
            dataGridView1.Columns[6].HeaderText = "сообщества";
            dataGridView1.Columns[7].HeaderText = "подарки";
            dataGridView1.Columns[8].Visible = false;

            dataGridView1.RowCount = 0;
            for (int i = 0; i <= diapozon_before - diapozon_from; i++)
            {
                dataGridView1.RowCount++;

                dataGridView1[0, i].Value = Convert.ToString(array_date[c - i]);
                dataGridView1[1, i].Value = Convert.ToString(array_friends[c - i]);
                dataGridView1[2, i].Value = Convert.ToString(array_followers[c - i]);
                dataGridView1[3, i].Value = Convert.ToString(array_photos[c - i]);
                dataGridView1[4, i].Value = Convert.ToString(array_videos[c - i]);
                dataGridView1[5, i].Value = Convert.ToString(array_audios[c - i]);
                dataGridView1[6, i].Value = Convert.ToString(array_pades[c - i]);
                dataGridView1[7, i].Value = Convert.ToString(array_gifts[c - i]);
            }
        }
        public void update_dgv_osn_info()
        {
            int c = diapozon_before;

            dataGridView1.Columns[1].HeaderText = "статус";
            dataGridView1.Columns[2].HeaderText = "день рождения";
            dataGridView1.Columns[3].HeaderText = "город";
            dataGridView1.Columns[4].HeaderText = "место учебы";
            dataGridView1.Columns[5].HeaderText = "сайт";
            dataGridView1.Columns[6].HeaderText = "семейное положение";
            dataGridView1.Columns[7].HeaderText = "номер телефона";
            dataGridView1.Columns[8].Visible = true;

            dataGridView1.RowCount = 0;
            for (int i = 0; i <= diapozon_before-diapozon_from; i++)
            {
                dataGridView1.RowCount++;
                for (int j = 0; j < dop_info.GetLength(0); j++)
                    dataGridView1[j, i].Value = Convert.ToString(dop_info[j, c]);
                c--;
            }
        }
        public void update_chart()
        {
            chart1.Series[0].Points.Clear();
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            chart1.Series[2].Points.Clear();
            chart1.Series[3].Points.Clear();
            chart1.Series[4].Points.Clear();
            chart1.Series[5].Points.Clear();
            chart1.Series[6].Points.Clear();

            int lenght = array_friends.Length - 1;

            int min_value = 9999;

            for (int i = diapozon_from; i <= diapozon_before; i++)
            {
                if (friends.Checked)
                {
                    chart1.Series[0].Points.AddXY(array_date[i], array_friends[i]);
                    if (min_value > array_friends[i]) min_value = array_friends[i];
                }
                if (followers.Checked)
                {
                    chart1.Series[1].Points.AddXY(array_date[i], array_followers[i]);
                    if (min_value > array_followers[i]) min_value = array_followers[i];
                }
                if (photos.Checked)
                {
                    chart1.Series[2].Points.AddXY(array_date[i], array_photos[i]);
                    if (min_value > array_photos[i]) min_value = array_photos[i];
                }
                if (videos.Checked)
                {
                    chart1.Series[3].Points.AddXY(array_date[i], array_videos[i]);
                    if (min_value > array_videos[i]) min_value = array_videos[i];
                }
                if (audios.Checked)
                {
                    chart1.Series[4].Points.AddXY(array_date[i], array_audios[i]);
                    if (min_value > array_audios[i]) min_value = array_audios[i];
                }
                if (pades.Checked)
                {
                    chart1.Series[5].Points.AddXY(array_date[i], array_pades[i]);
                    if (min_value > array_pades[i]) min_value = array_pades[i];
                }
                if (gifts.Checked)
                {
                    chart1.Series[6].Points.AddXY(array_date[i], array_gifts[i]);
                    if (min_value > array_gifts[i]) min_value = array_gifts[i];
                }
            }

            resize_chart(min_value);

            label3.Text = Convert.ToString(array_friends[lenght]);
            label4.Text = Convert.ToString(array_followers[lenght]);
            label5.Text = Convert.ToString(array_photos[lenght]);
            label6.Text = Convert.ToString(array_videos[lenght]);
            label7.Text = Convert.ToString(array_audios[lenght]);
            label8.Text = Convert.ToString(array_pades[lenght]);
            label9.Text = Convert.ToString(array_gifts[lenght]);
        }

        public void resize_chart(int value)
        {

            int temp = value - (value / 100) * 10;
            if (temp > 90)
                chart1.ChartAreas[0].AxisY.Minimum = temp;
            else
                chart1.ChartAreas[0].AxisY.Minimum = 0;
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }
        public void update_radiobutton()
        {
            egoldsRadioButton1.Checked = true;
            egoldsRadioButton2.Checked = false;
            egoldsRadioButton3.Checked = false;
            update_chart();
        }
        private void egoldsRadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            update_chart();
            chart1.Visible = true;
            dataGridView1.Visible = false;
        }
        private void friends_CheckedChanged(object sender)
        {
            update_radiobutton();
        }
        private void followers_CheckedChanged(object sender)
        {
            update_radiobutton();
        }
        private void gifts_CheckedChanged(object sender)
        {
            update_radiobutton();
        }

        private void pades_CheckedChanged(object sender)
        {
            update_radiobutton();
        }

        private void audios_CheckedChanged(object sender)
        {
            update_radiobutton();
        }

        private void videos_CheckedChanged(object sender)
        {
            update_radiobutton();
        }

        private void photos_CheckedChanged(object sender)
        {
            update_radiobutton();
        }

        private void egoldsRadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            //осн инфо
            update_dgv_osn_info();
            chart1.Visible = false;
            dataGridView1.Visible = true;
        }

        private void egoldsRadioButton3_CheckedChanged(object sender, EventArgs e)
        {
            //доп инфо
            update_dgv_dop_info();
            chart1.Visible = false;
            dataGridView1.Visible = true;
        }

        private void CB_from_SelectionChangeCommitted(object sender, EventArgs e) //от
        {
            diapozon_from = CB_from.SelectedIndex;
            change_checked();
        }

        private void CB_before_SelectedIndexChanged(object sender, EventArgs e) // до
        {
            diapozon_before= diapozon-1 - CB_before.SelectedIndex;
            change_checked();
        }

        private void change_checked()
        {
            if (egoldsRadioButton3.Checked)
                update_dgv_dop_info();
            if (egoldsRadioButton2.Checked)
                update_dgv_osn_info();
            if (egoldsRadioButton1.Checked)
                update_chart();
        }

        private void yt_Button1_Click(object sender, EventArgs e)
        {
            spisok_form_data spisok_form = new spisok_form_data();
            spisok_form.global_id_Data = global_id_Data;
            spisok_form.Show();
        }

        private void yt_Button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("удалить профиль все снимки из базы данных?", "удаление профиля", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    string query = "DELETE FROM снимок WHERE [id_профиль] = " + global_id_Data;
                    OleDbCommand command = new OleDbCommand(query, connection_DB);
                    command.ExecuteNonQuery();
                    this.Close();
                    MessageBox.Show("снимки успешно удалены", "успех");
                }
            }
            catch (Exception E) { };
        }
    }
}
