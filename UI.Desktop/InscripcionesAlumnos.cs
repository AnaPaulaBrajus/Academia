using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Business.Entities;
using Business.Logic;

namespace UI.Desktop
{
    public partial class InscripcionesAlumnos : UI.Desktop.ApplicationForm
    {
        public InscripcionesAlumnos(int id)
        {
            Global.ID = id;
            InitializeComponent();
            dgvInscripciones.AutoGenerateColumns = false;
        }
        static class Global
        {
            public static int ID;
        }

        private void tsbNuevo_Click(object sender, EventArgs e)
        {
            InscripcionAlumnoDesktop formInsc = new InscripcionAlumnoDesktop(ApplicationForm.ModoForm.Alta,Global.ID);
            formInsc.ShowDialog();
            this.Listar();
        }

        private void tsbEditar_Click(object sender, EventArgs e)
        {
            int ID = ((AlumnoInscripcion)this.dgvInscripciones.SelectedRows[0].DataBoundItem).ID;
            InscripcionAlumnoDesktop formInsc = new InscripcionAlumnoDesktop(ID, ApplicationForm.ModoForm.Modificacion,Global.ID);
            formInsc.ShowDialog();
            this.Listar();
        }

        private void tsbEliminar_Click(object sender, EventArgs e)
        {
            int ID = ((AlumnoInscripcion)this.dgvInscripciones.SelectedRows[0].DataBoundItem).ID;
            InscripcionAlumnoDesktop formInsc = new InscripcionAlumnoDesktop(ID, ApplicationForm.ModoForm.Baja,Global.ID);
            formInsc.ShowDialog();
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

        public void Listar()
        {
            AlumnoInscripcionLogic alumno = new AlumnoInscripcionLogic();
            this.dgvInscripciones.DataSource = alumno.GetInscAlumno(Global.ID);
        }

        private void InscripcionesAlumnos_Load(object sender, EventArgs e)
        {
            Listar();
        }
    }
}
