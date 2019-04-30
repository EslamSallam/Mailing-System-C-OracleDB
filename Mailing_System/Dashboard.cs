using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mailing_System
{
    public partial class Dashboard : Form
    {
        public static Dashboard dashInctance = null;
        public Dashboard()
        {
            InitializeComponent();
            dashInctance = this;
        }

        
        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            profile1.Visible = false;
            profile1.BringToFront();
            bunifuTransition1.ShowSync(profile1);
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            
            mailbox1.Visible = false;
            mailbox1.BringToFront();
            bunifuTransition1.ShowSync(mailbox1);
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            draftbox1.Visible = false;
            draftbox1.BringToFront();
            bunifuTransition1.ShowSync(draftbox1);
        }

        internal Image imagevalidator(bool v)
        {
            if (v)
            {
                return imageList1.Images[1];
            }
            return imageList1.Images[0];
        }

        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
            sentbox1.Visible = false;
            sentbox1.BringToFront();
            bunifuTransition1.ShowSync(sentbox1);
        }

        private void bunifuFlatButton5_Click(object sender, EventArgs e)
        {
            seeContacts1.Visible = false;
            seeContacts1.BringToFront();
            bunifuTransition1.ShowSync(seeContacts1);
        }

        private void bunifuFlatButton7_Click(object sender, EventArgs e)
        {
            sendmail1.Visible = false;
            sendmail1.BringToFront();
            bunifuTransition1.ShowSync(sendmail1);
        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {

        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuImageButton4_Click(object sender, EventArgs e)
        {

        }

        private void bunifuImageButton4_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bunifuImageButton1_Click_1(object sender, EventArgs e)
        {
            string str = WindowState.ToString();
                if (str.Equals("Normal"))
                    WindowState = FormWindowState.Maximized;
                else
                    WindowState = FormWindowState.Normal;
        }

        private void bunifuImageButton2_Click_1(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            label2.Text = User.firstname.First().ToString().ToUpper() + User.firstname.Substring(1) + " " + User.lastname.First().ToString().ToUpper() + User.lastname.Substring(1);
        }

        private void bunifuFlatButton6_Click(object sender, EventArgs e)
        {
            userReports1.Visible = false;
            userReports1.BringToFront();
            bunifuTransition1.ShowSync(userReports1);
        }
    }
}