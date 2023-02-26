using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Business.Logic;
using Business.Entities;

namespace UI.Desktop
{
    public partial class DocentesCursos : UI.Desktop.ApplicationForm
    {
        public DocentesCursos()
        {
            InitializeComponent();
            dgvDocenteCurso.AutoGenerateColumns = false;
        }

        public void Listar()
        {
            DocenteCursoLogic d = new DocenteCursoLogic();
            dgvDocenteCurso.DataSource = d.GetAll();
        }

        private void tsbNuevo_Click(object sender, EventArgs e)
        {
            DocenteCursoDesktop formDC = new DocenteCursoDesktop(ApplicationForm.ModoForm.Alta);
            formDC.ShowDialog();
            this.Listar();
        }

        private void tsbEditar_Click(object sender, EventArgs e)
        {
            int ID = ((DocenteCurso)this.dgvDocenteCurso.SelectedRows[0].DataBoundItem).ID;
            DocenteCursoDesktop formDC = new DocenteCursoDesktop(ID, ApplicationForm.ModoForm.Modificacion);
            formDC.ShowDialog();
            this.Listar();
        }

        private void tsbEliminar_Click(object sender, EventArgs e)
        {
            int ID = ((DocenteCurso)this.dgvDocenteCurso.SelectedRows[0].DataBoundItem).ID;
            DocenteCursoDesktop formDC = new DocenteCursoDesktop(ID, ApplicationForm.ModoForm.Baja);
            formDC.ShowDialog();
            this.Listar();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            this.Listar();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DocentesCursos_Load(object sender, EventArgs e)
        {
            Listar();
        }
    }
}
