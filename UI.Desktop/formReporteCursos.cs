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
    public partial class formReporteCursos : Form
    {
        public formReporteCursos()
        {
            InitializeComponent();
        }

        private void formReporteCursos_Load(object sender, EventArgs e)
        {
            CursoLogic cursoLogic = new CursoLogic();
            ReportDataSource rds = new ReportDataSource("ReporteCursos", cursoLogic.GetAll());
            this.reportViewer1.LocalReport.DataSources.Add(rds);
            this.reportViewer1.RefreshReport();
        }
    }
}
