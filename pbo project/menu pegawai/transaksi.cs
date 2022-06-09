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
            NpgsqlConnection con = koneksi();
            con.Open();
            NpgsqlCommand cmd = new NpgsqlCommand("select id_barang,nama,harga,stock from barang", con);
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Data_Barang.DataSource = dt;
        }
        int id_barang;
        int stock;
        private void databarang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Convert.ToInt32(Data_Barang.SelectedCells[3].Value.ToString()) == 0)
            {
                pop_up_kosong pop = new pop_up_kosong();
                pop.Show();
            }
            else
            {
                id_barang = Convert.ToInt32(Data_Barang.SelectedCells[0].Value);
                nama.Text = Data_Barang.SelectedCells[1].Value.ToString();
                harga.Text = Data_Barang.SelectedCells[2].Value.ToString();
                stock = Convert.ToInt32(Data_Barang.SelectedCells[3].Value.ToString());
            }
        }

        private void kryptonButton5_Click(object sender, EventArgs e)
        {
            menu_pegawai.pop_up_add_cust pop = new menu_pegawai.pop_up_add_cust();
            pop.Show();
        }
        int n = 0;
        int total1;
        private void kryptonButton6_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(kuantitas.Text) <= stock)
            {
                n++;
                int total = Convert.ToInt32(kuantitas.Text) * Convert.ToInt32(harga.Text);
                total1 = total + total1;
                DataGridViewRow dr = new DataGridViewRow();
                dr.CreateCells(Data_Transaksi);
                dr.Cells[0].Value = n + 1;
                dr.Cells[1].Value = nama.Text;
                dr.Cells[2].Value = harga.Text;
                dr.Cells[3].Value = kuantitas.Text;
                Data_Transaksi.Rows.Add(dr);
                total_lbl.Text = "Rp. " + total_lbl.ToString();
                load_data();
                nama.Text = "";
                harga.Text = "";
                kuantitas.Text = "";
            }
            else
            {
                MessageBox.Show("Kuantitas melebihi dari stock");
            }
        }
        int id;
        private void get_id(int a)
        {
            NpgsqlConnection con = koneksi();
            con.Open();
            NpgsqlCommand cmd = con.CreateCommand();
            string query = "select id_transaksi from transaksi order by id_transaksi desc limit 1 ";
            cmd.CommandText = query;
            a = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();
        }
        private void update_item()
        {
            get_id(id);
            int jumlah = stock - Convert.ToInt32(kuantitas.Text);
            NpgsqlConnection con = koneksi();
            con.Open();
            NpgsqlCommand cmd = new NpgsqlCommand("update barang set stock ='" + jumlah + "' where id_barang = '" + this.id_barang + "'", con);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }
    }
}
