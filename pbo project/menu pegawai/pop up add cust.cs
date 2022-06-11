using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace pbo_project.menu_pegawai
{
    public partial class pop_up_add_cust : Form
    {
        public pop_up_add_cust()
        {
            InitializeComponent();
            timer1.Start();
        }
        static private NpgsqlConnection koneksi ()
        {
            return new NpgsqlConnection ("server=localhost;port=5432;userid=postgres;password=Bagus383`;database=kasir");
        }
        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            string date = DateTime.Now.ToString("yyyy-MM-dd");
            NpgsqlConnection con = koneksi ();
            con.Open ();
            NpgsqlCommand cmd = new NpgsqlCommand("insert into transaksi (nama,tanggal) values ('"+nama.Text+"', '"+date+"')",con);
            cmd.ExecuteNonQuery ();
            con.Close ();
            this.Close();
        }

        private void nama_Enter(object sender, EventArgs e)
        {
            nama.Text = "";
        }

        private void nama_Leave(object sender, EventArgs e)
        {
            if (nama.Text == "")
            {
                nama.Text = "Nama";
            }
        }
    }
}
