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
    public partial class Profile : UserControl
    {
        string ordb = "data source=orcl; user id=scott; password=tiger;";
        OracleConnection conn;
        public Profile()
        {
            InitializeComponent();
        }

        private void Profile_Load(object sender, EventArgs e)
        {
            conn = new OracleConnection(ordb);
            conn.Open();
            OracleCommand cmd;
            OracleDataReader dr;
            if (User.gender == "M" || User.gender == "m")
            {
                bunifuRadioButton1.Checked = true;
                bunifuRadioButton2.Checked = false;
            }
            else
            {
                bunifuRadioButton1.Checked = false;
                bunifuRadioButton2.Checked = true;
            }
            bunifuTextBox1.Text = User.firstname;
            bunifuTextBox3.Text = User.lastname;
            bunifuDatePicker2.Text = User.birthdate;
            label2.Text = "Age : " + User.age;
            cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "select phone from phone_num where email=:e ";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("e", User.email);
            List<string> phNums = new List<string>();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                phNums.Add(dr[0].ToString());
            }
            dr.Close();
            User.Setphone(phNums);
            List<string> phList = User.Getphone();
            for (int i = 0; i < phList.Count; i++) {
                string it = phList[i];
                bunifuDropdown2.Items.Add(it);
            }

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (btnEdit.ButtonText == "Edit")
            {


                bunifuTextBox1.Enabled = true;
                bunifuTextBox2.Enabled = true;
                bunifuTextBox3.Enabled = true;
                bunifuTextBox4.Enabled = true;
                bunifuTextBox5.Enabled = true;
                bunifuTextBox6.Enabled = true;

                bunifuDatePicker2.Enabled = true;
                bunifuRadioButton1.Enabled = true;
                bunifuRadioButton2.Enabled = true;
                label4.Text = "";
                btnEdit.ButtonText = "Apply Edits";
            } else
            {
                OracleCommand cmd;
                OracleDataReader dr;
                if (!Validation.validateFirstName(bunifuTextBox1.Text))
                {
                    label4.Text = "Enter at least 5 chars for First name.";
                    return;
                }
                if (!Validation.validateLastName(bunifuTextBox3.Text))
                {
                    label4.Text = "Enter at least 5 chars for Last name.";
                    return;
                }
                if (!Validation.validateOldPassword(bunifuTextBox5.Text,User.password))
                {
                    if (bunifuTextBox5.Text != "" && bunifuTextBox4.Text != "")
                    {
                        label4.Text = "Enter Your Old Password.";
                        return;
                    }
                }
                if (!Validation.validatePassword(bunifuTextBox4.Text))
                {
                    if (bunifuTextBox5.Text != "" && bunifuTextBox4.Text != "")
                    {
                        label4.Text = "Enter at least 5 chars for NewPassword.";
                        return;
                    } else
                    {
                        bunifuTextBox4.Text = User.password;
                    }
                }
                bool addphone = true;
                if (Validation.validatePhone(bunifuTextBox6.Text))
                {
                    cmd = new OracleCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "select phone from phone_num where phone=:pn and user_email =: e";
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("pn", bunifuTextBox6.Text);
                    cmd.Parameters.Add("e", User.email);
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        label4.Text = "This Phone is taken before.";
                        addphone = false;
                        return;
                    }
                    dr.Close();
                } else if (bunifuTextBox6.Text == "")
                {
                    addphone = false;
                }
                int r;
                if (Validation.validateDeletePhone(bunifuTextBox2.Text, User.Getphone()))
                {
                    cmd = new OracleCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "delete from phone_num where phone=:pn";
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("pn", bunifuTextBox2.Text);
                    cmd.ExecuteNonQuery();
                    bunifuDropdown2.Items.Remove(bunifuTextBox2.Text);
                }
                else if (bunifuTextBox2.Text != "")
                {
                    label4.Text = "This Phone is not in the list.";
                    return;
                }
                string gender = "m";
                if (bunifuRadioButton2.Checked)
                {
                    gender = "f";
                }
                int age = DateTime.Now.Year - bunifuDatePicker2.Value.Year;
                DateTime d = bunifuDatePicker2.Value;
                cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "UpdateUser";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("e", User.email);
                cmd.Parameters.Add("f", bunifuTextBox1.Text);
                cmd.Parameters.Add("l", bunifuTextBox3.Text);
                cmd.Parameters.Add("p", bunifuTextBox4.Text);
                cmd.Parameters.Add("b",d);
                cmd.Parameters.Add("g", gender);
                cmd.ExecuteNonQuery();
                
                if (addphone)
                {
                    cmd = new OracleCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "insert into phone_num values (:ph,:e)";
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("ph", bunifuTextBox6.Text);
                    cmd.Parameters.Add("e", User.email);
                    r = cmd.ExecuteNonQuery();
                    if (r == -1)
                    {
                        label4.Text = "Cannot add this phone.";
                    } else
                    {
                        bunifuDropdown2.Items.Add(bunifuTextBox6.Text);
                    }
                }
                User.firstname = bunifuTextBox1.Text;
                User.lastname = bunifuTextBox3.Text;
                User.password = bunifuTextBox4.Text;
                List<string> ls = new List<string>();
                for (int i=0;i<bunifuDropdown2.Items.Count;i++)
                {
                    ls.Add(bunifuDropdown2.Items[i].ToString());
                }
                User.gender = gender;
                User.age = age.ToString();
                User.Setphone(ls);
                
                bunifuDatePicker2.Value = d;
                label2.Text = "Age : " + age;
                bunifuTextBox1.Enabled = false;
                bunifuTextBox2.Enabled = false;
                bunifuTextBox3.Enabled = false;
                bunifuTextBox4.Enabled = false;
                bunifuTextBox5.Enabled = false;
                bunifuTextBox6.Enabled = false;

                bunifuDatePicker2.Enabled = false;
                bunifuRadioButton1.Enabled = false;
                bunifuRadioButton2.Enabled = false;
                btnEdit.ButtonText = "Edit";

            }
        }

        private void bunifuTextBox1_TextChange(object sender, EventArgs e)
        {
            bunifuTextBox1.IconRight = Dashboard.dashInctance.imagevalidator(Validation.validateFirstName(bunifuTextBox1.Text));
        }

        private void bunifuTextBox3_TextChange(object sender, EventArgs e)
        {
            bunifuTextBox3.IconRight = Dashboard.dashInctance.imagevalidator(Validation.validateLastName(bunifuTextBox3.Text));
        }

        private void bunifuTextBox5_TextChange(object sender, EventArgs e)
        {
            bunifuTextBox5.IconRight = Dashboard.dashInctance.imagevalidator(Validation.validateOldPassword(bunifuTextBox5.Text, User.password));
        }

        private void bunifuTextBox4_TextChange(object sender, EventArgs e)
        {
            bunifuTextBox4.IconRight = Dashboard.dashInctance.imagevalidator(Validation.validatePassword(bunifuTextBox4.Text));
        }

        private void bunifuTextBox6_TextChange(object sender, EventArgs e)
        {
            bunifuTextBox6.IconRight = Dashboard.dashInctance.imagevalidator(Validation.validatePhone(bunifuTextBox6.Text));
        }


        private void bunifuTextBox2_TextChange(object sender, EventArgs e)
        {
            bunifuTextBox2.IconRight = Dashboard.dashInctance.imagevalidator(Validation.validateDeletePhone( bunifuTextBox2.Text, User.Getphone()) );
        }
    }
}
