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
    public partial class Form1 : Form
    {
        public static string user;
        MySqlCommand cmd;
        MySqlDataAdapter ad;
        MySqlConnection con = new MySqlConnection(@"server=localhost;uid=root;pwd=;database=refillcheck;convert zero datetime=True");
        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        Thread th;
        //string dt2;
        //public string KeyCode;
        public Form1()
        {
            InitializeComponent();
            txtdate.Text = DateTime.Now.ToShortDateString();
            showdata();
            ShowInformation();

        }

        public void ShowInformation()
        {
            lbluser.Text = Form2.user;
            lblmodel.Text = Form2.model;
            lblline.Text = Form2.line;
        }


        public void openForm()
        {
            Application.Run(new Form2());
        }

        public void openForm1()
        {
            Application.Run(new Form1());
        }

        public void openInput()
        {
            Application.Run(new pcb_input());
        }

        private void txtPartNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * From ohcsydney where Part_Number='" + txtPartNumber.Text + "'";
                ad = new MySqlDataAdapter(cmd);
                ad.Fill(dt);
                if (dt.Rows.Count == 1)
                {
                    //MessageBox.Show("Data Found!");
                    txtFeeder.Focus();
                }

                else
                {
                    MessageBox.Show("Invalid Part Number!");
                    //this.Close();
                    //th = new Thread(opensearchform);
                    //th.SetApartmentState(ApartmentState.STA);
                    //th.Start();
                }
                con.Close();
            }

        }

        private void txtFeeder_KeyDown(object sender, KeyEventArgs a)
        {
            if (a.KeyCode == Keys.Enter)
            {
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Select Part_Number,Feeder from ohcsydney WHERE Part_Number =@Part_Number AND Feeder =@Feeder";
                cmd.Parameters.AddWithValue("@Part_Number", txtPartNumber.Text);
                cmd.Parameters.AddWithValue("@Feeder", txtFeeder.Text);
                cmd.ExecuteNonQuery();
                ad = new MySqlDataAdapter(cmd);
                ad.Fill(dt1);
                if (dt1.Rows.Count == 1)
                {
                    //MessageBox.Show("Data Found!");
                    //txtFeeder.Focus();
                    txtPartLocation.Text = dt.Rows[0]["Part_Loc"].ToString();
                    txtPartLocation.Focus();
                }


                else
                {
                    MessageBox.Show("Invalid Feeder Number!");
                    //this.Close();
                    //th = new Thread(opensearchform);
                    //th.SetApartmentState(ApartmentState.STA);
                    //th.Start();
                }
                con.Close();
            }
        }

        public void clearData()
        {
            txtPartLocation.Text = "";
            txtFeeder.Text = "";
            txtPartNumber.Text = "";
            txtPartNumber.Focus();
        }

        private void txtPartLocation_KeyDown(object sender, KeyEventArgs e)
        {
            DateTime theDate = DateTime.Now;
            txtdate.Text = theDate.ToString("yyyy-MM-dd HH:mm:ss");
            con.Open();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "insert into history values('" + txtid.Text + "','" + txtPartNumber.Text + "','" + txtFeeder.Text + "','" + lblmodel.Text + "','" + txtPartLocation.Text + "','" + lbluser.Text + "','" + lblline.Text + "','" + txtdate.Text + "')"; //this.dateTimePicker1.Text
            cmd.ExecuteNonQuery();
            
            //insertintoactivitylog();
            MessageBox.Show("Recrod Successfully");
            clearData();
            con.Close();
            this.Close();
            th = new Thread(openForm1);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();

        }

        private void labelondate_Click(object sender, EventArgs e)
        {

        }

        public void showdata()
        {
            //con.Open();
            cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from history order by Date DESC";
            ad = new MySqlDataAdapter(cmd);
            ad.Fill(dt2);
            dataGridView1.DataSource = dt2;
            con.Close();
            
        }

        private void btnhistory_Click(object sender, EventArgs e)
        {
            //showdata();
        }

        private void btnOut_Click(object sender, EventArgs e)
        {
            this.Close();
            th = new Thread(openForm);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }

        private void btnInput_Click(object sender, EventArgs e)
        {
            //this.Close();
            th = new Thread(openInput);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }


        //private void textBox1_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        MessageBox.Show("Hi " + textBox1.Text);
        //    }
        //}
    }
}
