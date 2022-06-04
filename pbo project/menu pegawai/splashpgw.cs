using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pbo_project
{
    public partial class splashpgw : Form
    {
        public splashpgw()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            progressBar1.Increment(2);
            if (progressBar1.Value == 100)
            {
                this.Close();
                timer1.Enabled = false;
                menu_pegawai_1 mn = new menu_pegawai_1();
                mn.Show();
            }
        }
    }
}
