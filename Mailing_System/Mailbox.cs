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
    public partial class Mailbox : UserControl
    {
        string ordb = "User Id=scott;Password=tiger;Data Source=orcl";
        OracleConnection conn2;
        DataTable dt;

        int id_b;
        public Mailbox()
        {
            InitializeComponent();

        }
        

        private void Mailbox_Load(object sender, EventArgs e)
        {
            //string constr = "data source=orcl; user id=scott; password=tiger;";
            //string cmdstr = @"select MAIL_ID , MAIL_DATE , TO_,FROM_ , MESSAGE from MAIL,MAIL_BOX,USER_ WHERE USER_.EMAIL =:ue and MAIL_BOX.USER_EMAIL=:ue and MAIL.MAIL_ID=:";
            conn2 = new OracleConnection(ordb);
            conn2.Open();

            string constr = "User Id=scott;Password=tiger;Data Source=orcl";
            string cmdstr;

            cmdstr = @"select mail.mail_id,mail.MAIL_DATE,mail.FROM_,mail.SUBJECT,mail.MESSAGE from mail,mail_box where mail_box.user_email=:n and mail.mail_id = mail_box.mail_id ";

            OracleDataAdapter adapter = new OracleDataAdapter(cmdstr, constr);
            adapter.SelectCommand.Parameters.Add("n", OracleDbType.Varchar2).Value = User.email;
            DataSet dataset = new DataSet();
            adapter.Fill(dataset);
            DataTable table = dataset.Tables[0];
            bunifuDataGridView1.DataSource = table;
        }

 

        private void bunifuDataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        

       

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            string constr = "User Id=scott;Password=tiger;Data Source=orcl";
            string cmdstr;

            cmdstr = @"select mail.mail_id,mail.MAIL_DATE,mail.FROM_,mail.SUBJECT,mail.MESSAGE from mail,mail_box where mail_box.user_email=:n and mail.mail_id = mail_box.mail_id ";

            OracleDataAdapter adapter = new OracleDataAdapter(cmdstr, constr);
            adapter.SelectCommand.Parameters.Add("n", OracleDbType.Varchar2).Value = User.email;
            DataSet dataset = new DataSet();
            adapter.Fill(dataset);
            DataTable table = dataset.Tables[0];
            bunifuDataGridView1.DataSource = table;
        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            DateTime d = DateTime.Now;

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn2;
            cmd.CommandText = "deleteMailBox";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("e", User.email);
            cmd.Parameters.Add("cid", id_b);
            cmd.ExecuteNonQuery();

            OracleDataReader dr;
            cmd = new OracleCommand();
            cmd.Connection = conn2;
            cmd.CommandText = "viewMailBox";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("u", User.email);
            cmd.Parameters.Add("rec", OracleDbType.RefCursor, ParameterDirection.Output);
            dr = cmd.ExecuteReader();
            dt = new DataTable();
            if (dr.HasRows)
            {
                dt.Load(dr);

                bunifuDataGridView1.DataSource = dt;
            }
            else
            {
                bunifuDataGridView1.DataSource = null;
            }
            dr.Close();
        }

        private void bunifuDataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            bunifuTextBox1.Enabled = true;
            bunifuTextBox2.Enabled = true;
            richTextBox1.Enabled = true;
            bunifuTextBox1.ReadOnly = false;
            bunifuTextBox2.ReadOnly = false;
            richTextBox1.ReadOnly = false;
            if (e.RowIndex != -1)
            {
                DataGridViewRow row = bunifuDataGridView1.Rows[e.RowIndex];
                id_b = int.Parse(row.Cells[0].Value.ToString());
                bunifuTextBox1.Text = row.Cells[2].Value.ToString();
                bunifuTextBox2.Text = row.Cells[3].Value.ToString();
                richTextBox1.Text = row.Cells[4].Value.ToString();

            }

            bunifuTextBox1.Enabled = false;
            bunifuTextBox2.Enabled = false;
            richTextBox1.Enabled = false;
            bunifuTextBox1.ReadOnly = true;
            bunifuTextBox2.ReadOnly = true;
            richTextBox1.ReadOnly = true;
        }
    }
}
