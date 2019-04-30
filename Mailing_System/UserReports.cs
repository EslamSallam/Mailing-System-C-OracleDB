using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mailing_System
{
    public partial class UserReports : UserControl
    {
        CrystalReport1 cr1;
        CrystalReport2 cr2;
        CrystalReport3 cr3;
        public UserReports()
        {
            InitializeComponent();
        }

        private void btnGenM_Click(object sender, EventArgs e)
        {
            cr1.SetParameterValue(0, User.email);
            crystalReportViewer1.ReportSource = cr1;
        }

        private void btnGenS_Click(object sender, EventArgs e)
        {
            cr2.SetParameterValue(0, User.email);
            crystalReportViewer1.ReportSource = cr2;
        }

        private void btnGenAll_Click(object sender, EventArgs e)
        {
            cr3.SetParameterValue(0, User.email);
            crystalReportViewer1.ReportSource = cr3;
        }

        private void UserReports_Load(object sender, EventArgs e)
        {
            cr1 = new CrystalReport1();
            cr2 = new CrystalReport2();
            cr3 = new CrystalReport3();
        }
    }
}
