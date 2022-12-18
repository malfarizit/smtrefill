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
    public partial class pcb_input : Form
    {
        MySqlCommand cmd;
        MySqlDataAdapter ad;
        MySqlConnection con = new MySqlConnection(@"server=localhost;uid=root;pwd=;database=refillcheck");
        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        Thread th;
        public pcb_input()
        {
            InitializeComponent();
            lblmodel.Text = Form2.model;
        }

        public void clearData()
        {
            textBox1.Text = "";
        }

        public void insertintotraceback()
        {
            con.Open();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "insert into pcb_input values('" + textBox1.Text + "','" + lblmodel.Text + "','" + lbldt.Text + "')"; //this.dateTimePicker1.Text
            cmd.ExecuteNonQuery();
            clearData();
            con.Close();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            DateTime theDate = DateTime.Now;
            lbldt.Text = theDate.ToString("yyyy-MM-dd HH:mm:ss");
            if (e.KeyCode == Keys.Enter)
            {
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "insert into pcb_input values('" + textBox1.Text + "','" + lblmodel.Text + "','" + lbldt.Text + "')";
                //cmd.Parameters.AddWithValue("@Part_Number", txtPartNumber.Text);
                //cmd.Parameters.AddWithValue("@Feeder", txtFeeder.Text);
                cmd.ExecuteNonQuery();
                clearData();
                //ad = new MySqlDataAdapter(cmd);
                //ad.Fill(dt1);
            }
            con.Close();

        }
        
    }
}
