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

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            barang brg = new barang();
            brg.Show();
            this.Close();
        }

        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            menu_admin.jenis_barang jns = new menu_admin.jenis_barang();
            jns.Show();
            this.Close();
        }

        private void kryptonButton5_Click(object sender, EventArgs e)
        {
            menu_rekap_data rekap = new menu_rekap_data();
            rekap.Show();
            this.Close();
        }

        private void kryptonButton7_Click(object sender, EventArgs e)
        {
            menu_admin.supplier splr = new menu_admin.supplier();
            splr.Show();   
            this.Close();
        }
    }
}
