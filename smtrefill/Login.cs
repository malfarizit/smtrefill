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
    public partial class Login : UserControl
    {
        MySqlCommand cmd;
        MySqlDataAdapter ad;
        MySqlConnection con = new MySqlConnection(@"server=localhost;uid=root;pwd=;database=refillcheck");
        DataTable dt = new DataTable();
        Thread th;
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public void openForm()
        {
            Application.Run(new Form1());
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select * from user_login WHERE Username= '" + txtPass.Text +"'";
            //cmd.Parameters.AddWithValue("@Part_Number", txtPartNumber.Text);
            //cmd.Parameters.AddWithValue("@Feeder", txtFeeder.Text);
            cmd.ExecuteNonQuery();
            ad = new MySqlDataAdapter(cmd);
            ad.Fill(dt);
            if (dt.Rows.Count == 1)
            {
                this.Hide();
                th = new Thread(openForm);
                th.SetApartmentState(ApartmentState.STA);
                th.Start();
            }


            else
            {
                MessageBox.Show("Invalid Password!");
                //this.Close();
                //th = new Thread(opensearchform);
                //th.SetApartmentState(ApartmentState.STA);
                //th.Start();
            }
            con.Close();
        }
    }
}
