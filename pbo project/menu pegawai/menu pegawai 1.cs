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
    public partial class menu_pegawai_1 : Form
    {
        public menu_pegawai_1()
        {
            InitializeComponent();
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
            transaksi trx = new transaksi();   
            trx.Show();
        }

        private void kryptonButton4_Click(object sender, EventArgs e)
        {
            Login lg = new Login();
            lg.Show();
            this.Close();
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            menu_pegawai.barang barang = new menu_pegawai.barang();
            barang.Show();
            this.Close();
        }

        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            menu_pegawai.menu_rekap_data rekap = new menu_pegawai.menu_rekap_data();
            rekap.Show();
            this.Close();
        }
    }
}
