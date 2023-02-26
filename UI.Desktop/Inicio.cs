using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Business.Entities;

namespace UI.Desktop
{
    public partial class Inicio : Form
    {
        static class Global
        {
            public static int ID;
        }

        public Inicio(Business.Entities.Personas.TipoPersona tipo, int id)
        {
            InitializeComponent();
            Global.ID = id;
            switch(tipo)
            {
                case Business.Entities.Personas.TipoPersona.Alumno:
                    {
                        tsmiUsuarios.Visible = false;
                        tsmiPlanes.Visible = false;
                        tsmiMaterias.Visible = false;
                        tsmiEspecialidades.Visible = false;
                        tsmiComisiones.Visible = false;
                        tsmiUsuarios.Visible = false;
                        tsmiPersonas.Visible = false;
                        tsmiCursos.Visible = false;
                        tsmiInscripcionDocentes.Visible = false;
                        tsmiCargarNotas.Visible = false;
                        tsmiReportePlanes.Visible = false;
                        tsmiReporteCursos.Visible = false;
                        tsmiReportes.Visible = false;
                        break;
                    }
                case Business.Entities.Personas.TipoPersona.Docente:
                    {
                        tsmiUsuarios.Visible = false;
                        tsmiPlanes.Visible = false;
                        tsmiMaterias.Visible = false;
                        tsmiEspecialidades.Visible = false;
                        tsmiComisiones.Visible = false;
                        tsmiUsuarios.Visible = false;
                        tsmiPersonas.Visible = false;
                        tsmiCursos.Visible = false;
                        tsmiInscripcionDocentes.Visible = false;
                        inscripcionesDeAlumnosToolStripMenuItem.Visible = false;
                        tsmiReportePlanes.Visible = false;
                        tsmiReporteCursos.Visible = false;
                        tsmiReportes.Visible = false;
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        private void tsmiUsuarios_Click(object sender, EventArgs e)
        {
            Usuarios formUsuario = new Usuarios();
            formUsuario.ShowDialog();
        }

        private void tsmiPersonas_Click(object sender, EventArgs e)
        {
            Personas formPersonas = new Personas();
            formPersonas.ShowDialog();
        }

        private void tsmiPlanes_Click(object sender, EventArgs e)
        {
            Planes formPlan = new Planes();
            formPlan.ShowDialog();
        }

        private void tsmiEspecialidades_Click(object sender, EventArgs e)
        {
            Especialidades formEspecialidad = new Especialidades();
            formEspecialidad.ShowDialog();
        }

        private void tsmiMaterias_Click(object sender, EventArgs e)
        {
            Materias formMateria = new Materias();
            formMateria.ShowDialog();
        }

        private void tsmiComisiones_Click(object sender, EventArgs e)
        {
            Comisiones formComision = new Comisiones();
            formComision.ShowDialog();
        }

        private void tsmCursos_Click(object sender, EventArgs e)
        {
            Cursos formCursos = new Cursos();
            formCursos.ShowDialog();
        }

        private void tsmiInscripcionDocentes_Click(object sender, EventArgs e)
        {
            DocentesCursos formInsc = new DocentesCursos();
            formInsc.ShowDialog();

        }

        private void inscripcionesDeAlumnosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InscripcionesAlumnos formAlumnoCurso = new InscripcionesAlumnos(Global.ID);
            formAlumnoCurso.ShowDialog();
        }

        private void tsmiCargarNotas_Click(object sender, EventArgs e)
        {
            ElegirCurso eleg = new ElegirCurso(Global.ID);
            eleg.ShowDialog();
        }

        private void tsmiReportePlanes_Click(object sender, EventArgs e)
        {
            formReportePlanes rp = new formReportePlanes();
            rp.ShowDialog();
        }

        private void tsmiReporteCursos_Click(object sender, EventArgs e)
        {
            formReporteCursos rc = new formReporteCursos();
            rc.ShowDialog();
        }
    }
}
