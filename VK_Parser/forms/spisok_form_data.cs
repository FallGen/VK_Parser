using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json;
using System.Data.OleDb;

namespace VK_Parser
{
    public partial class spisok_form_data : Form
    {
        private OleDbConnection connection_DB;
        public int global_rowindex_Data = -1;
        public static string way_DB = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source = Data_VK_parser.accdb";
        public int[] array_snimok;
        public spisok_form_data()
        {
            InitializeComponent();
            connection_DB = new OleDbConnection(way_DB);
            connection_DB.Open();
        }

        public string[,] main_spisik;
        public string global_id_Data = null;
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        //public void qwe()
        //{
        //    dataGridView1.RowCount = 0;
        //    for (int i = 0; i < main_spisik.GetLength(1); i++)
        //    {
        //        dataGridView1.RowCount++;
        //        for (int j = 0; j < main_spisik.GetLength(0); j++)
        //            dataGridView1[j, i].Value = Convert.ToString(main_spisik[j, i]);
        //    }
        //}

        public int local_lenght;
        private void spisok_form_Load(object sender, EventArgs e)
        {
            label1.Text = "id профиля: " + global_id_Data;

            OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT id_снимок, дата FROM снимок WHERE id_профиль=" + global_id_Data, connection_DB);
            DataTable table = new DataTable();
            adapter.Fill(table);

            array_snimok = new int[table.Rows.Count];

            for (int i = 0; i < table.Rows.Count; i++)
            {
                array_snimok[i] = Convert.ToInt32(table.Rows[i]["id_снимок"]);
                CB_otobrajenie_date.Items.Add(table.Rows[i]["дата"]);
                CB_sravnenie_date1.Items.Add(table.Rows[i]["дата"]);
                CB_sravnenie_date2.Items.Add(table.Rows[i]["дата"]);
            }

            CB_sravnenie_date1.SelectedIndex = 0;
            CB_otobrajenie_date.SelectedIndex = 0;
            update_dgv();
        }

        public void update_dgv()
        {
            if (RB_friends.Checked)
            {
                load_people("список_друзей", CB_otobrajenie_date.SelectedIndex);
            }

            if (RB_followers.Checked)
            {
                load_people("список_подписчиков", CB_otobrajenie_date.SelectedIndex);
            }

        }

        public void load_people(string name_catalog, int index_date)
        {
            try
            {
                OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT id_user, фамилия, имя, пол, город, nickname, id_профиль FROM " + name_catalog + " WHERE id_профиль=" + global_id_Data + " AND id_снимок= " + array_snimok[index_date], connection_DB);
                DataTable table = new DataTable();
                adapter.Fill(table);

                string[] array = { "", "id_user", "фамилия", "имя", "пол", "город", "nickname" };

                main_spisik = new string[7, table.Rows.Count];

                dataGridView1.RowCount = 0;
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    dataGridView1.RowCount++;
                    for (int j = 1; j < array.Length; j++)
                    {
                        dataGridView1[0, i].Value = main_spisik[0, i] = Convert.ToString(i + 1);
                        dataGridView1[j, i].Value = Convert.ToString(table.Rows[i][array[j]]);
                        main_spisik[j, i] = Convert.ToString(table.Rows[i][array[j]]);
                    }
                }

                int value_man = 0; int value_girl = 0;

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    if (main_spisik[4, i] == "мужской") value_man++;
                    if (main_spisik[4, i] == "женский") value_girl++;
                }

                label5.Text = Convert.ToString(table.Rows.Count);
                label6.Text = Convert.ToString(value_girl);
                label7.Text = Convert.ToString(value_man);

            }
            catch (Exception E) { MessageBox.Show(E.Message, "ошибка"); }

        }

        private void dataGridView1_CellMouseClick_1(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                global_rowindex_Data = e.RowIndex;
            }
            catch (Exception E) { }
        }//выделение строки

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            int c = 0;

            dataGridView1.RowCount = 0;
            for (int i = 0; i < main_spisik.GetLength(1); i++)
            {
                if (main_spisik[1, i].ToLower().StartsWith(textBox1.Text) || main_spisik[2, i].ToLower().StartsWith(textBox1.Text) || main_spisik[3, i].ToLower().StartsWith(textBox1.Text) || main_spisik[5, i].ToLower().StartsWith(textBox1.Text) || main_spisik[6, i].ToLower().StartsWith(textBox1.Text))
                {
                    dataGridView1.RowCount++;

                    for (int j = 0; j < main_spisik.GetLength(0); j++)
                        dataGridView1[j, c].Value = Convert.ToString(main_spisik[j, i]);
                    c++;
                }
            }
        } //строка поиска
        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void egoldsRadioButton3_CheckedChanged(object sender, EventArgs e)
        {
            GB_otobrajenie.Enabled = true;

            GB_sravnenie.Enabled = false;
            yt_Button1.BackColor = Color.RosyBrown;
            
            update_dgv();
        }

        private void egoldsRadioButton4_CheckedChanged(object sender, EventArgs e)
        {
            GB_otobrajenie.Enabled = false;
            //RB_friends.BackColor = Color.Red;

            GB_sravnenie.Enabled = true;
            yt_Button1.BackColor = Color.Tomato;
            
            update_dgv();
        }

        private void yt_Button1_Click(object sender, EventArgs e)
        {
            sravnenie();
        }


        public void sravnenie()
        {
            // try
            {
                int index_date1 = CB_sravnenie_date1.SelectedIndex;
                int index_date2 = CB_sravnenie_date2.SelectedIndex;

                if (index_date1 > -1 && index_date2 > -1)
                {
                    string name_catalog = (!radioButton1.Checked) ? "список_друзей" : "список_подписчиков";

                    OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT id_user, фамилия, имя, пол, город, nickname, id_профиль FROM " + name_catalog + " WHERE id_профиль=" + global_id_Data + " AND id_снимок= " + array_snimok[index_date1], connection_DB);
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    string[] array = { "", "id_user", "фамилия", "имя", "пол", "город", "nickname" };
                    string[,] temp_array1 = new string[array.Length, table.Rows.Count]; ;

                    for (int i = 0; i < table.Rows.Count; i++)
                        for (int j = 0; j < array.Length; j++)
                            temp_array1[j, i] = Convert.ToString(table.Rows[i][j]);

                    adapter = new OleDbDataAdapter("SELECT id_user, фамилия, имя, пол, город, nickname, id_профиль FROM " + name_catalog + " WHERE id_профиль=" + global_id_Data + " AND id_снимок= " + array_snimok[index_date2], connection_DB);
                    table = new DataTable();
                    adapter.Fill(table);

                    string[,] temp_array2 = new string[array.Length, table.Rows.Count]; ;
                    for (int i = 0; i < table.Rows.Count; i++)
                        for (int j = 0; j < array.Length; j++)
                            temp_array2[j, i] = Convert.ToString(table.Rows[i][j]);

                    if (temp_array1.GetLength(1) > temp_array2.GetLength(1))
                    {
                        main_spisik = temp_array1;
                        temp_array1 = temp_array2;
                    }
                    else
                    {
                        main_spisik = temp_array2;
                    }

                    int lenght = main_spisik.GetLength(1);
                    int n = 0;
                    dataGridView1.RowCount = 0;

                    for (int i = 0; i < lenght; i++)
                    {
                        dataGridView1.RowCount++;
                        for (int c = 0; c < 6; c++)
                        {
                            dataGridView1[0, n].Value = Convert.ToString(n + 1);
                            dataGridView1[c + 1, n].Value = Convert.ToString(main_spisik[c, n]);
                        }
                        n++;
                    }

                    for (int i = lenght - 1; i >= 0; i--)
                    {
                        for (int j = temp_array1.GetLength(1) - 1; j >= 0; j--)
                            if (Convert.ToString(temp_array1[0, j]) == Convert.ToString(dataGridView1[1, i].Value))
                            {
                                dataGridView1.Rows.RemoveAt(i);
                                break;
                            }
                    }

                    int value_man = 0; int value_girl = 0;

                    for (int i = 0; i < dataGridView1.RowCount; i++)
                    {
                        if (Convert.ToString(dataGridView1[4, i].Value) == "мужской") value_man++;
                        if (Convert.ToString(dataGridView1[4, i].Value) == "женский") value_girl++;
                    }

                    label5.Text = Convert.ToString(value_girl+value_man);
                    label6.Text = Convert.ToString(value_girl);
                    label7.Text = Convert.ToString(value_man);
                    //MessageBox.Show("различия между списоками успешно выведены", "успех");
                }
                else
                    MessageBox.Show("не выбран диапазон", "ошибка");
            }
            //catch (Exception E) { }
        }


        private void CB_otobrajenie_date_SelectionChangeCommitted(object sender, EventArgs e)
        {
            update_dgv();
        }

        private void RB_friends_CheckedChanged_1(object sender, EventArgs e)
        {
            update_dgv();
        }

        private void RB_followers_CheckedChanged(object sender, EventArgs e)
        {
            update_dgv();
        }
    }
}
