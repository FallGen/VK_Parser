using System;
using System.Windows.Forms;

namespace VK_Parser
{
    public partial class view_photos_form : Form
    {
        public view_photos_form()
        {
            InitializeComponent();
        }

        public int value = 0;
        public string[,] url_profile;
        public int[] profile_info; // 0 - likes | 1 - comments | 2 - reposts
        //public int likes_profile;

        public string[,] url_wall;

        public string[,] url_saved;

        public string[,] main_url;
        private void view_photos_form_Load(object sender, EventArgs e)
        {
            update_form();
        }

        public void updatePhoto()
        {
            if (main_url != null)
            {
                label6.Visible = false;
                textBox1.Text = (value + 1) + "/" + main_url.GetLength(0);
                pictureBox1.ImageLocation = main_url[value, 0];
                label5.Text = "лайков: " + main_url[value, 1];
                label7.Text = "комментариев: " + main_url[value, 2];
                label8.Text = "репостов: " + main_url[value, 3];
            }
            else
            {
                label6.Visible = true;
                textBox1.Clear();
                label5.Text = "лайков: ";
                label7.Text = "комментариев: ";
                label8.Text = "репостов: ";
                pictureBox1.ImageLocation = null;
            }
        }

        public void update_form()
        {
            if (url_profile != null)
                label1.Text = Convert.ToString(url_profile.GetLength(0));
            else label1.Text = "0";

            if (url_wall != null)
                label3.Text = Convert.ToString(url_wall.GetLength(0));
            else label3.Text = "0";

            if (url_saved != null)
                label4.Text = Convert.ToString(url_saved.GetLength(0));
            else label4.Text = "0";

            main_url = url_profile;
            updatePhoto();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //лево
            if (value == 0)
                value = main_url.GetLength(0) - 1;
            else
                value--;
            updatePhoto();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //право
            if (value == main_url.GetLength(0) - 1)
                value = 0;
            else
                value++;
            updatePhoto();
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void radioButton1_CheckedChanged_1(object sender, EventArgs e)
        {
            //профиль
            main_url = url_profile;
            value = 0;
            updatePhoto();
        }

        private void radioButton2_CheckedChanged_1(object sender, EventArgs e)
        {
            //стена
            main_url = url_wall;
            value = 0;
            updatePhoto();
        }

        private void radioButton3_CheckedChanged_1(object sender, EventArgs e)
        {
            //сохры
            main_url = url_saved;
            value = 0;
            updatePhoto();
        }
    }
}
