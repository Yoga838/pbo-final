using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;

namespace pbo_project
{
    public partial class transaksi : Form
    {
        public transaksi()
        {
            InitializeComponent();
            load_data();
        }
        private static NpgsqlConnection koneksi()
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
            con.Close();

        }
        int id_barang;
        int stock;
        private void Data_Barang_CellClick(object sender, DataGridViewCellEventArgs e)
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
            kuantitas.Enabled = true;
        }
        int total1;
        private void kryptonButton6_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(kuantitas.Text) <= stock)
            {
                int total = Convert.ToInt32(kuantitas.Text) * Convert.ToInt32(harga.Text);
                total1 = total + total1;
                DataGridViewRow dr = new DataGridViewRow();
                dr.CreateCells(databarang);
                dr.Cells[0].Value = id_barang;
                dr.Cells[1].Value = nama.Text;
                dr.Cells[2].Value = harga.Text;
                dr.Cells[3].Value = kuantitas.Text;
                databarang.Rows.Add(dr);
                total_lbl.Text = "Rp. " + total1.ToString();
                update_item();
                append_detail();
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
        private void get_id()
        {
            NpgsqlConnection con = koneksi();
            con.Open();
            NpgsqlCommand cmd = con.CreateCommand();
            string query = "select id_transaksi from transaksi order by id_transaksi desc limit 1 ";
            cmd.CommandText = query;
            id = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();
        }
        private void append_detail()
        {
            get_id();
            NpgsqlConnection con = koneksi();
            con.Open();
            NpgsqlCommand cmd = new NpgsqlCommand("insert into detail_transaksi(id_transaksi,id_barang,nama,harga,kuantitas) values ('" + this.id + "', '"+this.id_barang+"' ,'" + nama.Text + "','" + harga.Text + "','" + kuantitas.Text + "')", con);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }
        private void update_item()
        {
            int jumlah = stock - Convert.ToInt32(kuantitas.Text);
            NpgsqlConnection con = koneksi();
            con.Open();
            NpgsqlCommand cmd = new NpgsqlCommand("update barang set stock ='" + jumlah + "' where id_barang = '" + this.id_barang + "'", con);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }
        private void kuantitas_Enter(object sender, EventArgs e)
        {
            kuantitas.Text = "";
        }

        private void kuantitas_Leave(object sender, EventArgs e)
        {
            if (kuantitas.Text == "")
            {
                kuantitas.Text = "Kuantitas";
            }
        }
        int tempid;
        int bckstck;
        string name;
        private void databarang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            tempid = Convert.ToInt32(databarang.SelectedCells[0].Value);
            bckstck = Convert.ToInt32(databarang.SelectedCells[3].Value);
            name = databarang.SelectedCells[1].Value.ToString();

        }
        int newstock;
        void getstock()
        {
            NpgsqlConnection con = koneksi();
            con.Open();
            NpgsqlCommand cmd = con.CreateCommand();
            string query = "select stock from barang where id_barang = '" + this.tempid + "'";
            cmd.CommandText = query;
            newstock = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();
        }

        private void kryptonButton7_Click(object sender, EventArgs e)
        {
            getstock();
            int totalbck = newstock + bckstck;
            int price = Convert.ToInt32(databarang.SelectedCells[2].Value) * bckstck;
            total1 = total1 - price;
            NpgsqlConnection con = koneksi();
            con.Open();
            NpgsqlCommand cmd = new NpgsqlCommand("update barang set stock ='" + totalbck + "' where id_barang = '" + this.tempid + "'", con);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
            total_lbl.Text = "Rp. " + total1.ToString();
            load_data();
            unappend_detail();
            int rowIndex = databarang.CurrentCell.RowIndex;
            databarang.Rows.RemoveAt(rowIndex);
            con.Close();
        }
        private void unappend_detail()
        {
            NpgsqlConnection con = koneksi();
            con.Open();
            NpgsqlCommand cmd = new NpgsqlCommand("Delete from detail_transaksi where nama = '" + this.name + "'and id_transaksi = '" + this.id + "'", con);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }

        private void bayar_Enter(object sender, EventArgs e)
        {
            bayar.Text = "";
        }

        private void bayar_Leave(object sender, EventArgs e)
        {
            if (bayar.Text == "")
            {
                bayar.Text = "bayar";
            }
        }
        int id_acc;
        public static string namapgw;
        void get_acc_info()
        {
            string username = namapgw;
            NpgsqlConnection conn = koneksi();
            conn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand("select id_akun from akun where username = '" + username + "'", conn);
            id_acc = Convert.ToInt32(cmd.ExecuteScalar());
            conn.Close();
        }
        private void kryptonButton8_Click(object sender, EventArgs e)
        {
            int kembalian = Convert.ToInt32(bayar.Text) - total1;
            menu_pegawai.pop_up_bayar.kembali = "kembaliannya " + kembalian.ToString();
            kuantitas.Enabled = false;
            get_acc_info();
            using (NpgsqlConnection conn = koneksi())
            {
                conn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand("insert into jumlah (id_transaksi, id_pegawai, jumlah) values ('" + this.id + "', '" + this.id_acc + "', '" + total1 + "')", conn);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                conn.Close();
            }
            databarang.Rows.Clear();
            total_lbl.Text = "Total";
            this.total1 = 0;
            this.id = 0;
            bayar.Text = "";
            menu_pegawai.pop_up_bayar paying = new menu_pegawai.pop_up_bayar();
            paying.Show();
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
