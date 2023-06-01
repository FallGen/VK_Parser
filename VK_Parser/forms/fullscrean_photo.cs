using Google.Api;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace VK_Parser.forms
{
    public partial class fullscrean_photo : Form
    {
        public fullscrean_photo()
        {
            InitializeComponent();

            Size resolution = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size;

            pictureBox1.Width = resolution.Width;
            pictureBox1.Height = resolution.Height;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void fullscrean_photo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Space)
            {
                Close();
            }
        }

        private void fullscrean_photo_Deactivate(object sender, EventArgs e)
        {
            Close();
        }
    }
}
