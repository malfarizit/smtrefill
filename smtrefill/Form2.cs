using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using MySql.Data.MySqlClient;

namespace smtrefill
{
    public partial class Form2 : Form
    {
        public static string user, line, model;
        MySqlCommand cmd;
        MySqlDataAdapter ad;
        MySqlConnection con = new MySqlConnection(@"server=localhost;uid=root;pwd=;database=refillcheck");
        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        Thread th;
        public Form2()
        {
            InitializeComponent();
            select_model();
        }
        public void openForm()
        {
            Application.Run(new Form1());
        }

        //public void model_info()
        //{
        //    con.Open();
        //    cmd = con.CreateCommand();
        //    cmd.CommandType = CommandType.Text;
        //    cmd.CommandText = "SELECT Model_Name FROM `model_list` WHERE Description= '" + cmbModel.Text + "'";
        //    cmd.ExecuteNonQuery();
        //    ad = new MySqlDataAdapter(cmd);
        //    ad.Fill(dt1);
        //    if (dt.Rows.Count == 1)
        //    {
        //        model = cmbModel.Text;
        //    }

        //    else
        //    {
        //        MessageBox.Show("Invalid Data!");
        //    }
        //    con.Close();
        //}

        public void select_model()
        {
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select * from model_list";
            //cmd.ExecuteNonQuery();
            MySqlDataReader dz = cmd.ExecuteReader();
            while (dz.Read())
            {
                cmbModel.Items.Add(dz.GetValue(2).ToString());
            }
            con.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select * from user_login WHERE Username= '" + txtPass.Text + "'";
            //cmd.Parameters.AddWithValue("@Part_Number", txtPartNumber.Text);
            //cmd.Parameters.AddWithValue("@Feeder", txtFeeder.Text);
            cmd.ExecuteNonQuery();
            ad = new MySqlDataAdapter(cmd);
            ad.Fill(dt);
            if (dt.Rows.Count == 1)
            {
                user = txtPass.Text;
                model = cmbModel.Text;
                //model_info();
                line = cmbLine.Text;
                this.Close();
                th = new Thread(openForm);
                th.SetApartmentState(ApartmentState.STA);
                th.Start();
            }

            if (txtPass.Text == "")
            {
                MessageBox.Show("Please Type Password");
            }

            else
            {
                MessageBox.Show("Invalid Password!");
                txtPass.Text = "";
                txtPass.Focus();
                //this.Close();
                //th = new Thread(opensearchform);
                //th.SetApartmentState(ApartmentState.STA);
                //th.Start();
            }
            con.Close();
        }
    }
}
