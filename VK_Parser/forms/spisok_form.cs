using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VK_Parser
{
    public partial class spisok_form : Form
    {
        public spisok_form()
        {
            InitializeComponent();
        }

        public string[,] main_spisik;

  //      public List <string[,]> list_spisok = null;
//        public List <string[,]> filtred_spisok = null;

        
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public int global_rowindex_Data = -1;
        public void qwe()
        {
            dataGridView1.RowCount = 0;
            for (int i = 0; i < main_spisik.GetLength(1); i++)
            {
                dataGridView1.RowCount++;
                for (int j = 0; j < main_spisik.GetLength(0); j++)
                    dataGridView1[j, i].Value = Convert.ToString(main_spisik[j, i]);
            }
        }

        public int local_lenght;
        private void spisok_form_Load(object sender, EventArgs e)
        {
            qwe();
            local_lenght = main_spisik.GetLength(1);
        }

        private void dataGridView1_CellMouseClick_1(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                button2.BackColor = Color.Tomato;
                button2.Enabled = true;
                dataGridView1.Rows[e.RowIndex].Selected = true;
                dataGridView1.RowsDefaultCellStyle.SelectionForeColor = Color.White;

                global_rowindex_Data = e.RowIndex;

            }
            catch (Exception E) { }
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            dataGridView1.RowCount = 0;
            int c = 0;

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
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if ((global_rowindex_Data != -1))
                DialogResult = DialogResult.OK;
            else
                MessageBox.Show("не выбран профиль", "уведомление");
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
