﻿using System;
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
        void load_data ()
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
    }
}