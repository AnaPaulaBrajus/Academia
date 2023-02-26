using Business.Logic;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI.Desktop
{
    public partial class formReportePlanes : Form
    {
        public formReportePlanes()
        {
            InitializeComponent();
        }

        private void formReportePlanes_Load(object sender, EventArgs e)
        {
            PlanLogic planLogic = new PlanLogic();
            ReportDataSource rds = new ReportDataSource("ReportePlanes", planLogic.GetAll());
            this.reportViewer1.LocalReport.DataSources.Add(rds);
            this.reportViewer1.RefreshReport();
        }
    }
}

