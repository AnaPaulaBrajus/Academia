using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UI.Web
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Business.Entities.Personas.TipoPersona tipo = (Business.Entities.Personas.TipoPersona)Session["tipo"];
            switch (tipo)
            {
                case Business.Entities.Personas.TipoPersona.Alumno:
                    PersonasVisible = false;
                    MateriasVisible = false;
                    UsuariosVisible = false;
                    CursosVisible = false;
                    ComisionesVisible = false;
                    PlanesVisible = false;
                    EspecialidadesVisible = false;
                    DocentesVisible = false;
                    AlumnosInscVisible = true;
                    CargaNotasVisible = false;
                    ReportePlanesVisible = false;
                    ReporteCursosVisible = false;
                    break;
                case Business.Entities.Personas.TipoPersona.Docente:
                    PersonasVisible = false;
                    MateriasVisible = false;
                    UsuariosVisible = false;
                    CursosVisible = false;
                    ComisionesVisible = false;
                    PlanesVisible = false;
                    EspecialidadesVisible = false;
                    DocentesVisible = false;
                    AlumnosInscVisible = false;
                    CargaNotasVisible = true;
                    ReportePlanesVisible = false;
                    ReporteCursosVisible = false;
                    break;
                default: break;
            }
        }

        public bool PersonasVisible {
            get { return personasLink.Visible; }
            set { personasLink.Visible = value; }
        }

        public bool CursosVisible {
            get { return cursosLink.Visible; }
            set { cursosLink.Visible = value; }
        }

        public bool MateriasVisible {
            get { return materiasLink.Visible; }
            set { materiasLink.Visible = value; }
        }

        public bool UsuariosVisible {
            get { return usuariosLink.Visible; }
            set { usuariosLink.Visible = value; }
        }

        public bool PlanesVisible {
            get { return planesLink.Visible; }
            set { planesLink.Visible = value; }
        }

        public bool EspecialidadesVisible {
            get { return especialidadesLink.Visible; }
            set { especialidadesLink.Visible = value; }
        }

        public bool ComisionesVisible {
            get { return comisionesLink.Visible; }
            set { comisionesLink.Visible = value; }
        }

        public bool DocentesVisible {
            get { return docentesCursosLink.Visible; }
            set { docentesCursosLink.Visible = value; }
        }

        public bool AlumnosInscVisible {
            get
            {
                return alumnosInscripcionesLink.Visible;
            }
            set
            {
                alumnosInscripcionesLink.Visible = value;
            }
        }

        public bool CargaNotasVisible
        {
            get { return cargarNotasLink.Visible; }
            set { cargarNotasLink.Visible = value; }
        }

        public bool ReportePlanesVisible
        {
            get { return reportePlanesLink.Visible; }
            set { reportePlanesLink.Visible = value; }
        }

        public bool ReporteCursosVisible
        {
            get { return reporteCursosLink.Visible; }
            set { reporteCursosLink.Visible = value; }
        }
    }
}