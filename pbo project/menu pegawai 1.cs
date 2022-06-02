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
    }
}
