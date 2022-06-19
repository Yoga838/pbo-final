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
    public partial class jenis_barang : Form
    {
        public jenis_barang()
        {
            InitializeComponent();
            load_data();
            timer1.Start();
            date_form();
        }
        private static NpgsqlConnection koneksi()
        {
            return new NpgsqlConnection("server=localhost;port=5432;userid=postgres;password=Bagus383`;database=kasir");
        }
        void load_data()
        {
            NpgsqlConnection con = koneksi();
            con.Open();
            NpgsqlCommand cmd = new NpgsqlCommand("select * from jenis", con);
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Data_Jenis.DataSource = dt;
            con.Close();
        }
        void date_form()
        {
            Date.Text = DateTime.Now.ToShortDateString();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {

        }
        int id_jenis;
        private void Data_Jenis_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            id_jenis = Convert.ToInt32(Data_Jenis.SelectedCells[0].Value);
            nama.Text = Data_Jenis.SelectedCells[1].Value.ToString();
            Deskripsi.Text = Data_Jenis.SelectedCells[2].Value.ToString();
        }

        private void kryptonButton6_Click(object sender, EventArgs e)
        {
            try
            {
                NpgsqlConnection con = koneksi();
                con.Open();
                NpgsqlCommand cmd = new NpgsqlCommand("insert into jenis (nama,deskripsi) values  ('" + nama.Text + "','" + Deskripsi.Text + "')", con);
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
                NpgsqlCommand cmd = new NpgsqlCommand("delete from jenis where id_jenis = '" + this.id_jenis + "' ", con);
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
                NpgsqlCommand cmd = new NpgsqlCommand("update jenis set nama='" + nama.Text + "', deskripsi='" + Deskripsi.Text + "' where id_jenis= '" + this.id_jenis + "' ", conn);
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

        private void kryptonTextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (kryptonTextBox2.Text != "")
            {
                NpgsqlConnection con = koneksi();
                con.Open();
                NpgsqlCommand cmd = new NpgsqlCommand("select * from jenis where nama like '" + kryptonTextBox2.Text + "'", con);
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                Data_Jenis.DataSource = dt;
                con.Close();
            }
            else if (kryptonTextBox2.Text == "")
            {
                load_data();
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
            this.Close();
        }

        private void kryptonButton7_Click(object sender, EventArgs e)
        {
            menu_admin.supplier splr = new menu_admin.supplier();
            splr.Show();
            this.Close();
        }

        private void kryptonButton4_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Close();
        }

        private void kryptonButton5_Click(object sender, EventArgs e)
        {
            menu_rekap_data rekap = new menu_rekap_data();
            rekap.Show();
            this.Close();
        }
    }
}
