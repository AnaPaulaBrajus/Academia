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
    public partial class InscripcionAlumnoDesktop : UI.Desktop.ApplicationForm
    {

        public InscripcionAlumnoDesktop()
        {
            InitializeComponent();
        }

        static class Global
        {
            public static int ID;
        }

        public InscripcionAlumnoDesktop(ModoForm modo, int idPersona) : this()
        {
            Modo = modo;
            Global.ID = idPersona;
        }

        public InscripcionAlumnoDesktop(int ID, ModoForm modo, int idPersona) : this()
        {
            AlumnoInscripcionLogic inscripcion = new AlumnoInscripcionLogic();
            InscActual = inscripcion.GetOne(ID);
            Modo = modo;
            Global.ID = idPersona;
            MapearDeDatos();
        }

        protected AlumnoInscripcion inscActual;
        public AlumnoInscripcion InscActual {
            get { return inscActual; }
            set { inscActual = value; }
        }

        public override void MapearDeDatos()
        {
            txtIDInscripcion.Text = InscActual.ID.ToString();
            cmbCurso.SelectedValue = InscActual.IdCurso.ToString();

            if (Modo == ModoForm.Alta || Modo == ModoForm.Modificacion)
            {
                btnAceptar.Text = "Guardar";
                if (Modo == ModoForm.Modificacion)
                {
                    this.Text = "Modificar especialidad";
                }
            }

            if (Modo == ModoForm.Baja)
            {
                txtIDInscripcion.ReadOnly = true;
                cmbCurso.Enabled = false;
                btnAceptar.Text = "Eliminar";
                this.Text = "Eliminar especialidad";
            }

            if (Modo == ModoForm.Consulta)
            {
                btnAceptar.Text = "Aceptar";
            }
        }
        public override void MapearADatos()
        {
            this.InscActual = new AlumnoInscripcion();
            CursoLogic c = new CursoLogic();
            Curso curso = c.GetOne(Convert.ToInt32(cmbCurso.SelectedValue));
            switch (Modo)
            {
                case ModoForm.Alta:
                    if (curso.Cupo != 0)
                    {
                        this.InscActual.IdAlumno = Global.ID;
                        this.InscActual.IdCurso = Convert.ToInt32(this.cmbCurso.SelectedValue);
                        this.InscActual.Condicion = "Inscripto";
                        this.InscActual.Nota = 0;
                        c.ActualizarCupo(curso, -1);
                        this.InscActual.State = BusinessEntity.States.New;
                        break;
                    }
                    else
                    {
                        MessageBox.Show("No hay cupo en este curso.");
                        break;
                    }
                case ModoForm.Modificacion:
                    this.InscActual.ID = Convert.ToInt32(this.txtIDInscripcion.Text);
                    this.InscActual.IdAlumno = Global.ID;
                    this.InscActual.IdCurso = Convert.ToInt32(this.cmbCurso.SelectedValue);
                    this.InscActual.Condicion = "Inscripto";
                    this.InscActual.Nota = 0;
                    this.InscActual.State = BusinessEntity.States.Modified;
                    break;
                case ModoForm.Baja:
                    this.InscActual.ID = Convert.ToInt32(this.txtIDInscripcion.Text);
                    this.InscActual.IdAlumno = Global.ID;
                    this.InscActual.IdCurso = Convert.ToInt32(this.cmbCurso.SelectedValue);
                    this.InscActual.Condicion = "Inscripto";
                    this.InscActual.Nota = 0;
                    this.InscActual.State = BusinessEntity.States.Deleted;
                    c.ActualizarCupo(curso, 1);
                    break;
                case ModoForm.Consulta:
                    this.InscActual.State = BusinessEntity.States.Unmodified;
                    break;
            }
        }

        public override void GuardarCambios()
        {
            MapearADatos();
            if(InscActual.IdCurso != 0)
            {
                AlumnoInscripcionLogic inscActual = new AlumnoInscripcionLogic();
                inscActual.Save(InscActual);
            }
        }
        public override bool Validar()
        {
            return true;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (Validar())
            {
                GuardarCambios();
                Close();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            GuardarCambios();
            this.Close();
        }

        private void InscripcionAlumnoDesktop_Load(object sender, EventArgs e)
        {
            AlumnoInscripcionLogic a = new AlumnoInscripcionLogic();
            cmbCurso.DataSource = a.GetCursos(Global.ID);
            cmbCurso.ValueMember = "id_curso";
            cmbCurso.DisplayMember = "id_curso";

            if(Modo == ModoForm.Baja || Modo == ModoForm.Modificacion)
            {
                cmbCurso.SelectedValue = InscActual.IdCurso.ToString();
            }

            CursoLogic c = new CursoLogic();
            Curso cur = c.GetOne(Convert.ToInt32(cmbCurso.SelectedValue));
            MateriaLogic m = new MateriaLogic();
            Materia mat = m.GetOne(cur.IdMateria);
            lblMateria.Text = mat.Descripcion;
            ComisionLogic com = new ComisionLogic();
            Comision comi = com.GetOne(cur.IdComision);
            lblComision.Text = comi.Descripcion;
        }

        private void cmbCurso_SelectionChangeCommitted(object sender, EventArgs e)
        {
            CursoLogic c = new CursoLogic();
            Curso cur = c.GetOne(Convert.ToInt32(cmbCurso.SelectedValue));
            MateriaLogic m = new MateriaLogic();
            Materia mat = m.GetOne(cur.IdMateria);
            lblMateria.Text = mat.Descripcion;
            ComisionLogic com = new ComisionLogic();
            Comision comi = com.GetOne(cur.IdComision);
            lblComision.Text = comi.Descripcion;
        }
    }

}
