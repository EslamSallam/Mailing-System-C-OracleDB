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
    public partial class Sentbox : UserControl
    {
        int id_s;
        string ordb = "User Id=scott;Password=tiger;Data Source=orcl";
        OracleConnection conn3;
        OracleCommand cmd;
        DataTable dt;
        public Sentbox()
        {
            InitializeComponent();
        }

        private void Sentbox_Load(object sender, EventArgs e)
        {
            conn3 = new OracleConnection(ordb);
            conn3.Open();
        }

       

   

        private void bunifuImageButton1_Click_1(object sender, EventArgs e)
        {
            string constr = "User Id=scott;Password=tiger;Data Source=orcl";
            string cmdstr;

            cmdstr = @"select mail.mail_id,mail.MAIL_DATE,mail.TO_,mail.SUBJECT,mail.MESSAGE from mail,sent_box where sent_box.user_email=:n and mail.mail_id = sent_box.mail_id ";

            OracleDataAdapter adapter = new OracleDataAdapter(cmdstr, constr);
            adapter.SelectCommand.Parameters.Add("n", OracleDbType.Varchar2).Value = User.email;
            DataSet dataset = new DataSet();
            adapter.Fill(dataset);
            DataTable table = dataset.Tables[0];
            bunifuDataGridView1.DataSource = table;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DateTime d = DateTime.Now;

            cmd = new OracleCommand();
            cmd.Connection = conn3;
            cmd.CommandText = "DeleteSentBox";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("e", User.email);
            cmd.Parameters.Add("cd", id_s);
            cmd.ExecuteNonQuery();

            OracleDataReader dr;
            cmd = new OracleCommand();
            cmd.Connection = conn3;
            cmd.CommandText = "viewSentBox";
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

        private void bunifuDataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewRow row = bunifuDataGridView1.Rows[e.RowIndex];
                id_s = int.Parse(row.Cells[0].Value.ToString());

            }
        }
    }
}
