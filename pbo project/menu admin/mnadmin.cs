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
    public partial class mnadmin : Form
    {
        public mnadmin()
        {
            InitializeComponent();
        }

        private void kryptonButton4_Click(object sender, EventArgs e)
        {
            Login lg = new Login();
            lg.Show();
            this.Close();
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            mnakun acc = new mnakun(); 
            acc.Show();
            this.Close ();
        }
    }
}
