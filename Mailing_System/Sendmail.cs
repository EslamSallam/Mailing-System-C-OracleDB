using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace Mailing_System
{
    public partial class Sendmail : UserControl
    {

        OracleConnection conn;
        string ordb = "data source=orcl; user id=scott; password=tiger;";

        public Sendmail()
        {
            InitializeComponent();
        }

        private void Sendmail_Load(object sender, EventArgs e)
        {
            conn = new OracleConnection(ordb);
            conn.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "select friend from contacts where USER_EMAIL=:u";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("u", User.email);
            OracleDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                bunifuDropdown2.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            DateTime d = DateTime.Now;
            conn = new OracleConnection(ordb);
            conn.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "MakeDraft";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("u", User.email);
            cmd.Parameters.Add("da", d);
            cmd.Parameters.Add("to", bunifuDropdown2.SelectedItem.ToString());
            cmd.Parameters.Add("sub", bunifuTextBox2.Text);
            cmd.Parameters.Add("mes", richTextBox1.Text);
            cmd.ExecuteNonQuery();
            bunifuTextBox2.Text = "";
            bunifuDropdown2.Text = "";
            richTextBox1.Text = "";
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            DateTime d = DateTime.Now;
            conn = new OracleConnection(ordb);
            conn.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "SENDMAIL";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("u", User.email);
            cmd.Parameters.Add("da", d);
            cmd.Parameters.Add("to", bunifuDropdown2.SelectedItem.ToString());
            cmd.Parameters.Add("sub", bunifuTextBox2.Text);
            cmd.Parameters.Add("mes", richTextBox1.Text);
            cmd.ExecuteNonQuery();
            bunifuTextBox2.Text = "";
            bunifuDropdown2.Text = "";
            richTextBox1.Text = "";
        }
    }
}
