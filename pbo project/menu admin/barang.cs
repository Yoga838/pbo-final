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
    public partial class barang : Form
    {
        public barang()
        {
            InitializeComponent();
            load_data();
            timer1.Start();
            date_form();
        }
        private static NpgsqlConnection koneksi ()
        {
            return new NpgsqlConnection("server=localhost;port=5432;userid=postgres;password=Bagus383`;database=kasir");
        }
        void load_data()
        {
            NpgsqlConnection con = koneksi();
            con.Open();
            NpgsqlCommand cmd = new NpgsqlCommand("select * from barang", con);
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Data_Barang.DataSource = dt;
        }
        void date_form()
        {
            Date.Text = DateTime.Now.ToShortDateString();
        }
        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            mnakun acc = new mnakun();
            acc.Show();
            this.Close();
        }
        public int id_barang;
        private void Data_Barang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            id_barang = Convert.ToInt32(Data_Barang.SelectedCells[0].Value);
            nama.Text = Data_Barang.SelectedCells[1].Value.ToString();
            harga.Text = Data_Barang.SelectedCells[2].Value.ToString();
            stock.Text = Data_Barang.SelectedCells[3].Value.ToString();
            supplier.Text = Data_Barang.SelectedCells[4].Value.ToString();
            jenis.Text = Data_Barang.SelectedCells[5].Value.ToString();
        }

        private void kryptonButton6_Click(object sender, EventArgs e)
        {
            try
            {
                NpgsqlConnection con = koneksi();
                con.Open();
                NpgsqlCommand cmd = new NpgsqlCommand("insert into barang (nama,harga,stock,id_supplier,id_jenis) values ('" + nama.Text + "','" + harga.Text + "','" + stock.Text + "','" + supplier.Text + "','" + jenis.Text + "')", con);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                con.Close();
                nama.Text = " ";
                harga.Text = " ";
                stock.Text = " ";
                supplier.Text = " ";
                jenis.Text = " ";
                load_data();
                pop_up.pop_up_barang_scs pop = new pop_up.pop_up_barang_scs();
                pop.Show();
            }
            catch (Exception x)
            {
                pop_up.pop_up_barang_fail pop = new pop_up.pop_up_barang_fail();
                pop.Show();
            }
        }

        private void kryptonButton8_Click(object sender, EventArgs e)
        {
            try
            {
                NpgsqlConnection con = koneksi();
                con.Open();
                NpgsqlCommand cmd = new NpgsqlCommand("delete from barang where id_barang = '" + this.id_barang + "' ", con);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                con.Close();
                load_data();
                pop_up.pop_up_delete pop = new pop_up.pop_up_delete();
                pop.Show();
            }
            catch (Exception)
            {
                pop_up.pop_up_delete_gagal pop = new pop_up.pop_up_delete_gagal();
                pop.Show();
            }
        }

        private void kryptonButton9_Click(object sender, EventArgs e)
        {
            try
            {
                NpgsqlConnection con = koneksi();
                con.Open();
                NpgsqlCommand cmd = new NpgsqlCommand("update barang set nama = '" + nama.Text + "', harga = '" + harga.Text + "', stock = '" + stock.Text + "', id_supplier = '" + supplier.Text + "', id_jenis = '" + jenis.Text + "' where id_barang = '" + this.id_barang + "' ", con);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                con.Close();
                nama.Text = " ";
                harga.Text = " ";
                stock.Text = " ";
                supplier.Text = " ";
                jenis.Text = " ";
                load_data();
                pop_up.pop_up_scs_edit pop = new pop_up.pop_up_scs_edit();
                pop.Show();
            }
            catch (Exception)
            {
                pop_up.pop_up_fail_edit pop = new pop_up.pop_up_fail_edit();
                pop.Show();
            }
        }

        private void kryptonTextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (kryptonTextBox2.Text != "")
            {
                NpgsqlConnection con = koneksi();
                con.Open();
                NpgsqlCommand cmd = new NpgsqlCommand("select * from barang where nama like '" + kryptonTextBox2.Text + "'", con);
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                Data_Barang.DataSource = dt;
            }
            else if (kryptonTextBox2.Text == "")
            {
                load_data();
            }
        }

        private void kryptonButton4_Click(object sender, EventArgs e)
        {
            Login lg = new Login();
            lg.Show();
            this.Close();
        }
    }
}
