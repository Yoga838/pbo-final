using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;

namespace pbo_project
{
    public partial class menu_rekap_data : Form
    {
        public menu_rekap_data()
        {
            InitializeComponent();
            load_data();
        }
        private static NpgsqlConnection koneksi()
        {
            return new NpgsqlConnection("server=localhost;port=5432;userid=postgres;password=Bagus383`;database=kasir;");
        }
        void load_data()
        {
            NpgsqlConnection con = koneksi();
            con.Open();
            NpgsqlCommand cmd = new NpgsqlCommand("select b.id_transaksi as id_transaksi, d.username as pegawai, b.nama as nama_pembeli,c.nama as nama_barang, b.tanggal from jumlah a join transaksi b  on (a.id_transaksi = b.id_transaksi) join detail_transaksi c on (b.id_transaksi = c.id_transaksi) join akun d on (a.id_pegawai = d.id_akun)", con);
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Rekap_Data.DataSource = dt;
        }

        private void kryptonTextBox2_Enter(object sender, EventArgs e)
        {
            searching.Text = "";
        }

        private void searching_Leave(object sender, EventArgs e)
        {
            searching.Text = "Cari  Name Customer";
        }

        private void searching_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (searching.Text != "")
            {
                NpgsqlConnection con = koneksi();
                con.Open();
                NpgsqlCommand cmd = new NpgsqlCommand("select b.id_transaksi as id_transaksi, d.username as pegawai, b.nama as nama_pembeli,c.nama as nama_barang, b.tanggal from jumlah a join transaksi b  on (a.id_transaksi = b.id_transaksi) join detail_transaksi c on (b.id_transaksi = c.id_transaksi) join akun d on (a.id_pegawai = d.id_akun) where b.nama ilike '" + searching.Text + "'", con);
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                Rekap_Data.DataSource = dt;
            }
            else
            {
                load_data();
            }
        }
        int day;
        private void kryptonButton6_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "")
            {
                load_data();
                total.Text = "Total";
            }
            else if (comboBox1.Text == "1 hari")
            {
                day = 0;
                NpgsqlConnection con = koneksi();
                con.Open();
                NpgsqlCommand cmd = new NpgsqlCommand("select b.id_transaksi as id_transaksi, d.username as pegawai, b.nama as nama_pembeli,c.nama as nama_barang, b.tanggal from jumlah a join transaksi b  on (a.id_transaksi = b.id_transaksi) join detail_transaksi c on (b.id_transaksi = c.id_transaksi) join akun d on (a.id_pegawai = d.id_akun) where b.tanggal - current_date = '" + day + "'  ", con);
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                Rekap_Data.DataSource = dt;
                total_text();
            }
            else if (comboBox1.Text == "1 minggu")
            {
                day = 7;
                NpgsqlConnection con = koneksi();
                con.Open();
                NpgsqlCommand cmd = new NpgsqlCommand("select b.id_transaksi as id_transaksi, d.username as pegawai, b.nama as nama_pembeli,c.nama as nama_barang, b.tanggal from jumlah a join transaksi b  on (a.id_transaksi = b.id_transaksi) join detail_transaksi c on (b.id_transaksi = c.id_transaksi) join akun d on (a.id_pegawai = d.id_akun) where current_date -b.tanggal  < '" + day + "'  ", con);
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                Rekap_Data.DataSource = dt;
                total_text0();
            }
            else if (comboBox1.Text == "1 bulan")
            {
                day = 30;
                NpgsqlConnection con = koneksi();
                con.Open();
                NpgsqlCommand cmd = new NpgsqlCommand("select b.id_transaksi as id_transaksi, d.username as pegawai, b.nama as nama_pembeli,c.nama as nama_barang, b.tanggal from jumlah a join transaksi b  on (a.id_transaksi = b.id_transaksi) join detail_transaksi c on (b.id_transaksi = c.id_transaksi) join akun d on (a.id_pegawai = d.id_akun) where current_date -b.tanggal  <'" + day + "'  ", con);
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                Rekap_Data.DataSource = dt;
                total_text0();
            }
        }
        void total_text0()
        {
            NpgsqlConnection con = koneksi();
            con.Open();
            NpgsqlCommand cmd = new NpgsqlCommand("select sum (a.jumlah) from jumlah a join transaksi b  on (a.id_transaksi = b.id_transaksi) join detail_transaksi c on (b.id_transaksi = c.id_transaksi) join akun d on (a.id_pegawai = d.id_akun) where current_date - b.tanggal  < '" + day + "' ", con);
            int jumlah = Convert.ToInt32(cmd.ExecuteScalar());
            if (jumlah > 0 )
            {
                total.Text = "Total " + jumlah;
            }
        }
        void total_text()
        {
            try
            {
                NpgsqlConnection con = koneksi();
                con.Open();
                NpgsqlCommand cmd = new NpgsqlCommand("select sum (a.jumlah) from jumlah a join transaksi b  on (a.id_transaksi = b.id_transaksi) join detail_transaksi c on (b.id_transaksi = c.id_transaksi) join akun d on (a.id_pegawai = d.id_akun) where b.tanggal - current_date = '" + this.day + "' ", con);
                int jumlah = Convert.ToInt32(cmd.ExecuteScalar());
                if (jumlah > 0)
                {
                    total.Text = "Total " + jumlah;
                }
            }
            catch (Exception)
            {
                total.Text = "Total 0";
                load_data();
            }
        }

        private void kryptonButton4_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Close();
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            mnakun acc = new mnakun();
            acc.Show();
            this.Close();
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            barang bar = new barang();
            bar.Show();
            this.Close();
        }

        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            menu_admin.jenis_barang jns = new menu_admin.jenis_barang();
            jns.Show();
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
