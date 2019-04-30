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
    public partial class SeeContacts : UserControl
    {
        OracleCommandBuilder builder;
        OracleDataAdapter adapter1;
        OracleDataAdapter adapter2;
        DataSet djs;
        public SeeContacts()
        {
            InitializeComponent();
        }

        private void SeeContacts_Load(object sender, EventArgs e)
        {
            

        }
        private void btnUp_Click(object sender, EventArgs e)
        {

            builder = new OracleCommandBuilder(adapter2);
            adapter2.Update(djs.Tables[1]);
        }

        private void bunifuDataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewRow row = bunifuDataGridView1.Rows[e.RowIndex];

                row.Cells[2].Value = DateTime.Now;
                

            }
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            string constr = "User Id=scott;Password=tiger;Data Source=orcl";
            adapter1 = new OracleDataAdapter("select * from user_ where EMAIL=:e", constr);
            adapter2 = new OracleDataAdapter("select * from CONTACTS", constr);
            djs = new DataSet();
            adapter1.SelectCommand.Parameters.Add("e", User.email);
            adapter1.Fill(djs, "U");
            adapter2.Fill(djs, "C");

            djs.Relations.Add("fk", djs.Tables[0].Columns["EMAIL"], djs.Tables[1].Columns["USER_EMAIL"], false);
            BindingSource b_Master = new BindingSource(djs, "U");
            BindingSource b_child = new BindingSource(b_Master, "fk");
            bunifuDataGridView1.DataSource = b_child;
        }
    }
}
