using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;

namespace pbo_project
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private static NpgsqlConnection koneksi()
        {
            return new NpgsqlConnection("server=localhost;port=5432;user id=postgres;password=Bagus383`;database=kasir;");
        }
        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            using (NpgsqlConnection con = koneksi())
            {
                con.Open();
                NpgsqlCommand cmd = new NpgsqlCommand("select * from public.akun where username = '" + username.Text + "'and password = '" + password.Text + "'", con);
                NpgsqlDataAdapter adp = new NpgsqlDataAdapter();
                DataTable dt = new DataTable();
                NpgsqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    if (dr[3].ToString() == "admin")
                    {
                        splash sp = new splash();
                        sp.Show();
                        con.Close();
                        this.Hide();
                    }
                    else
                    {
                        splash sp = new splash();
                        sp.Show();
                        con.Close();
                        this.Hide();
                    }
                }
                else
                {
                    pop_up_salah pop_up = new pop_up_salah();
                    pop_up.Show();
                }

            }

        }
    }
}
