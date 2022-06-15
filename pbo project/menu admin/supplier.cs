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

namespace pbo_project.menu_admin
{
    public partial class supplier : Form
    {
        public supplier()
        {
            InitializeComponent();
            load_data();
            timer1.Start();
            date_form();
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
            NpgsqlCommand cmd = new NpgsqlCommand("select * from supplier", con);
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Data_Supplier.DataSource = dt;
        }
        void date_form()
        {
            Date.Text = DateTime.Now.ToShortDateString();
        }

        private void kryptonTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (kryptonTextBox1.Text != "")
            {
                NpgsqlConnection con = koneksi();
                con.Open();
                NpgsqlCommand cmd = new NpgsqlCommand("select * from supplier where nama like '" + kryptonTextBox1.Text + "'", con);
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                Data_Supplier.DataSource = dt;
            }
            else if (kryptonTextBox1.Text == "")
            {
                load_data();
            }
        }
        int id_supplier;
        private void Data_Supplier_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            id_supplier = Convert.ToInt32(Data_Supplier.SelectedCells[0].Value);
            nama.Text = Data_Supplier.SelectedCells[1].Value.ToString();
            alamat.Text = Data_Supplier.SelectedCells[2].Value.ToString();
            kontak.Text = Data_Supplier.SelectedCells[3].Value.ToString();
        }

        private void kryptonButton6_Click(object sender, EventArgs e)
        {
            try
            {
                NpgsqlConnection con = koneksi();
                con.Open();
                NpgsqlCommand cmd = new NpgsqlCommand("insert into supplier (nama,alamat,kontak) values ('" + nama.Text + "', '" + alamat.Text + "', '" + kontak.Text + "')", con);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                con.Close();
                load_data();
                pop_up.pop_up_data_scs pop = new pop_up.pop_up_data_scs();
                pop.Show();
            }
            catch (Exception)
            {
                pop_up.pop_up_data_fail pop = new pop_up.pop_up_data_fail();
                pop.Show();
            }
        }

        private void kryptonButton8_Click(object sender, EventArgs e)
        {
            try
            {
                NpgsqlConnection con = koneksi();
                con.Open();
                NpgsqlCommand cmd = new NpgsqlCommand("delete from supplier where id_supplier = '" + this.id_supplier + "' ", con);
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
                NpgsqlConnection conn = koneksi();
                conn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand("update supplier set nama='" + nama.Text + "', alamat='" + alamat.Text + "', kontak='" + kontak.Text + "' where id_supplier= '" + this.id_supplier + "' ", conn);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                conn.Close();
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

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            mnakun acc = new mnakun();
            acc.Show();
            this.Close();
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            barang brg = new barang();
            brg.Show();
            this.Close ();
        }

        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            jenis_barang jns = new jenis_barang();
            jns.Show();
            this.Close () ;
        }

        private void kryptonButton7_Click(object sender, EventArgs e)
        {
            return;
        }

        private void kryptonButton4_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Close() ;
        }

        private void kryptonButton5_Click(object sender, EventArgs e)
        {
            menu_rekap_data rekap = new menu_rekap_data();
            rekap.Show();
            this.Close();
        }
    }
}
