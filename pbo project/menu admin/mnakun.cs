using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;

namespace pbo_project
{
    public partial class mnakun : Form
    {
        public mnakun()
        {
            InitializeComponent();
            timer1.Start();
            date_form();
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
            NpgsqlCommand cmd = new NpgsqlCommand("select * from akun", con);
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Data_Akun.DataSource = dt;
        }
        void date_form()
        {
            Date.Text = DateTime.Now.ToShortDateString();
        }

        private void kryptonButton6_Click(object sender, EventArgs e)
        {
            try
            {
                NpgsqlConnection con = koneksi();
                con.Open();
                NpgsqlCommand cmd = new NpgsqlCommand("insert into public.akun (username,password,jabatan)values('" + user.Text + "','" + pw.Text + "','" + rl.Text + "')", con);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                con.Close();
                user.Text = "";
                pw.Text = "";
                rl.Text = "";
                pop_up_user_berhasil pop = new pop_up_user_berhasil();
                pop.Show();
                load_data();

            }
            catch (Exception ex)
            {
                pop_up_user_salah pop = new pop_up_user_salah();
                pop.Show();
            }
        }

        private void kryptonButton4_Click(object sender, EventArgs e)
        {
            Login lg = new Login();
            lg.Show();
            this.Close();
        }

        private void kryptonButton8_Click(object sender, EventArgs e)
        {
            try
            {
                NpgsqlConnection con = koneksi();
                    con.Open();
                    NpgsqlCommand cmd = new NpgsqlCommand("delete from akun where id_akun = '" + this.id_akun + "' ", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    load_data();
                    pop_up.pop_up_delete pop = new pop_up.pop_up_delete();
                    pop.Show();
                
            }
            catch (Exception ex)
            {
                pop_up.pop_up_delete_gagal pop = new pop_up.pop_up_delete_gagal();
                pop.Show();
            }
        }
        int id_akun;
        private void Data_Akun_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            id_akun = Convert.ToInt32(Data_Akun.SelectedCells[0].Value);
            user.Text = Data_Akun.SelectedCells[1].Value.ToString();
            pw.Text = Data_Akun.SelectedCells[2].Value.ToString();
            rl.Text = Data_Akun.SelectedCells[3].Value.ToString();
        }

        private void kryptonButton9_Click(object sender, EventArgs e)
        {
            try
            {
                NpgsqlConnection con = koneksi();
                con.Open();
                NpgsqlCommand cmd = new NpgsqlCommand("update akun set username = '" + user.Text + "' , password = '" + pw.Text + "', jabatan = '" + rl.Text + "' where id_akun = '" + this.id_akun + "' ", con);
                cmd.ExecuteNonQuery();
                con.Close();
                load_data();
                pop_up.pop_up_scs_edit pop = new pop_up.pop_up_scs_edit();
                pop.Show();
            }
            catch (Exception ex)
            {
                pop_up.pop_up_fail_edit pop = new pop_up.pop_up_fail_edit();
                pop.Show();
            }
        }

        private void kryptonTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(kryptonTextBox1.Text != "")
            {
                NpgsqlConnection con = koneksi();
                con.Open();
                NpgsqlCommand cmd = new NpgsqlCommand("select * from akun where username like '" + kryptonTextBox1.Text + "'",con);
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                Data_Akun.DataSource = dt;
            }
            else if (kryptonTextBox1.Text == "")
            {
                load_data();
            }
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            barang brg = new barang();
            brg.Show();
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            menu_admin.jenis_barang jns = new menu_admin.jenis_barang();
            jns.Show();
            this.Close();
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            return;
        }

        private void kryptonButton7_Click(object sender, EventArgs e)
        {
            menu_admin.supplier splr = new menu_admin.supplier();
            splr.Show();
            this.Close();
        }
    }
}
