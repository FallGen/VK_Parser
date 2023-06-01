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
    public partial class authorization_form : Form
    {
        public authorization_form()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("текущая локальная авторизация необходима для создание отдельного профиля \n" +
                "пользователя, который расположен в облаке", "помощь", MessageBoxButtons.OK);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
            this.Close();
            Application.Exit();

            this.Close();
            Environment.Exit(0);

        }
        private void TB_pass_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) DialogResult = DialogResult.OK;
        }

        private void TB_log_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) DialogResult = DialogResult.OK;
        }

        private void authorization_form_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
