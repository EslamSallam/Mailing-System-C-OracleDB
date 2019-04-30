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
    public partial class Form1 : Form
    {
        public static Form1 instance = null;
        public Form1()
        {
            instance = this;

            InitializeComponent();

            bunifuFormDock1.SubscribeControlsToDragEvents(new Control[] {
                signUp1,
                signIn1
            }, false);
            

        }

        internal bool AccpetRegistration()
        {
            return true;
        }

        private void signUp1_Load(object sender, EventArgs e)
        {

        }

        internal void showProfile()
        {

            instance.Close();
            
        }

        internal void showSignIn()
        {
            //show signIn tab
            signIn1.Visible = false;
            signIn1.BringToFront();
            bunifuTransition1.ShowSync(signIn1);
            instance.Height = 550;
            instance.Width = 700;
        }

        internal void showSignUp()
        {
            //show Signup tab
            signUp1.Visible = false;
            signUp1.BringToFront();
            bunifuTransition1.ShowSync(signUp1);
            instance.Height = 550;
            instance.Width = 700;
        }

        private void signIn1_Load(object sender, EventArgs e)
        {

        }

        internal Image imagevalidator(bool v)
        {
            if (v)
            {
                return imageList1.Images[1];
            }
            return imageList1.Images[0];
        }
    }
}