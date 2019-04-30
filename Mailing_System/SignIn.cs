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
    public partial class SignIn : UserControl
    {
        string ordb = "data source=orcl; user id=scott; password=tiger;";
        OracleConnection conn;
        public SignIn()
        {
            InitializeComponent();
        }

        private void SignIn_Load(object sender, EventArgs e)
        {
            conn = new OracleConnection(ordb);
            conn.Open();
        }
        private void btnSignUp_Click(object sender, EventArgs e)
        {
            Form1.instance.showSignUp();
        }

        private void btnSignIn_Click(object sender, EventArgs e)
        {
            OracleCommand cmd;
            OracleDataReader dr;
            
            if (Validation.validateUserName(bunifuTextBox2.Text) && Validation.validatePassword(bunifuTextBox5.Text))
            {
                cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "select * from user_ where email=:e and password1=:p";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("e", bunifuTextBox2.Text);
                cmd.Parameters.Add("p", bunifuTextBox5.Text);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    DateTime d = Convert.ToDateTime(dr[4]);
                    User.email = dr[0].ToString();

                    User.firstname = dr[1].ToString();

                    User.lastname = dr[2].ToString();
                    User.password = dr[3].ToString();
                    User.birthdate = dr[4].ToString();
                    User.gender = dr[5].ToString();
                    User.age = (DateTime.Now.Year - d.Year).ToString();
                    Form1.instance.showProfile();
                }
                else
                {
                    label3.Text = "Email or Password is incorrect.";
                    return;
                }
                dr.Close();
            }
        }

        private void bunifuImageButton4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            Form.ActiveForm.WindowState = FormWindowState.Minimized;
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            string str = Form.ActiveForm.WindowState.ToString();
            if (str.Equals("Normal"))
                Form.ActiveForm.WindowState = FormWindowState.Maximized;
            else
                Form.ActiveForm.WindowState = FormWindowState.Normal;
        }
    }
}
