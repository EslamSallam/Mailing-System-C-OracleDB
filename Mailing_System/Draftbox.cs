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
    public partial class Draftbox : UserControl
    {
        string ordb = "data source=orcl; user id=scott; password=tiger;";
        OracleConnection conn1;
        DataTable dt;

        int id_m;

        public Draftbox()
        {
            InitializeComponent();
        }

        private void Draftbox_Load(object sender, EventArgs e)
        {
            conn1 = new OracleConnection(ordb);
            conn1.Open();
            OracleCommand cmd = new OracleCommand();
            OracleDataReader dr;
            cmd.Connection = conn1;
            cmd.CommandText = "viewDraftBox";
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
            dr.Close();
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            DateTime d = DateTime.Now;
            
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn1;
            cmd.CommandText = "SENDMAIL";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("u", User.email);
            cmd.Parameters.Add("da", d);
            cmd.Parameters.Add("to", bunifuTextBox1.Text);
            cmd.Parameters.Add("sub", bunifuTextBox2.Text);
            cmd.Parameters.Add("mes", richTextBox1.Text);
            cmd.ExecuteNonQuery();
            bunifuTextBox1.Text = "";
            bunifuTextBox2.Text = "";
            richTextBox1.Text = "";
        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            DateTime d = DateTime.Now;
            
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn1;
            cmd.CommandText = "DeleteDraftBox";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("e", User.email);
            cmd.Parameters.Add("cid", id_m);
            cmd.ExecuteNonQuery();

            OracleDataReader dr;
            cmd = new OracleCommand();
            cmd.Connection = conn1;
            cmd.CommandText = "viewDraftBox";
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
            bunifuTextBox1.Text = "";
            bunifuTextBox2.Text = "";
            richTextBox1.Text = "";
        }

        private void bunifuDataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           

        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            DateTime d = DateTime.Now;

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn1;
            cmd.CommandText = "UpdateDraftBox";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("cid", id_m);
            cmd.Parameters.Add("da", d);
            cmd.Parameters.Add("to", bunifuTextBox1.Text);
            cmd.Parameters.Add("sub", bunifuTextBox2.Text);
            cmd.Parameters.Add("mes", richTextBox1.Text);
            cmd.ExecuteNonQuery();

            OracleDataReader dr;
            cmd = new OracleCommand();
            cmd.Connection = conn1;
            cmd.CommandText = "viewDraftBox";
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
            dr.Close();
        }

        private void bunifuDataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewRow row = bunifuDataGridView1.Rows[e.RowIndex];
                id_m = int.Parse(row.Cells[0].Value.ToString());
                bunifuTextBox1.Text = row.Cells[2].Value.ToString();
                bunifuTextBox2.Text = row.Cells[4].Value.ToString();
                richTextBox1.Text = row.Cells[5].Value.ToString();

            }
        }
    }
}
