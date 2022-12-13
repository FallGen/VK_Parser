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
    public partial class logs_form : Form
    {
        public logs_form()
        {
            InitializeComponent();
        }


        logs_form form;
        public void show_logs()
        {
            if (form == null || form.IsDisposed)
            {
                form = new logs_form();
                form.Show();
            }
            else
                form.Focus();

        }
    }
}
