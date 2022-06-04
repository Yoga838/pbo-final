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

namespace pbo_project
{
    public partial class transaksi : Form
    {
        public transaksi()
        {
            InitializeComponent();
            load_data();
        }
        private static NpgsqlConnection koneksi ()
        {
            return new NpgsqlConnection("server=localhost;port=5432;user id=postgres;password=Bagus383`;database=kasir;");
        }
        void load_data()
        {
            NpgsqlConnection con = koneksi ();
            con.Open ();
            NpgsqlCommand cmd = new NpgsqlCommand("select id_barang,nama,harga,stock from barang", con);
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            databarang.DataSource = dt;
        }
        int id_barang;
        private void databarang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Convert.ToInt32(databarang.SelectedCells[3].Value.ToString())== 0)
            {
                pop_up_kosong pop = new pop_up_kosong();
                pop.Show();
            }
            else
            {
                id_barang = Convert.ToInt32(databarang.SelectedCells[0].Value);
                textBox1.Text = databarang.SelectedCells[1].Value.ToString();
                textBox2.Text = databarang.SelectedCells[2].Value.ToString();
            }
        }
    }
}
