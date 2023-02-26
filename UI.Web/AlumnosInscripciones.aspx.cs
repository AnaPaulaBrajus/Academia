using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Logic;
using Business.Entities;


namespace UI.Web
{
    public partial class AlumnosInscripciones : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadGrid();
            }
        }

        AlumnoInscripcionLogic _logic;

        private AlumnoInscripcionLogic Logic
        {
            get
            {
                if (_logic == null)
                {
                    _logic = new AlumnoInscripcionLogic();
                }
                return _logic;
            }
        }

        private void LoadGrid()
        {
            this.gridView.DataSource = this.Logic.GetInscAlumno(Convert.ToInt32(Session["idPersona"]));
            this.gridView.DataBind();
        }

        public enum FormModes
        {
            Alta,
            Baja,
            Modificacion
        }

        public FormModes FormMode
        {
            get { return (FormModes)this.ViewState["FormMode"]; }
            set { this.ViewState["FormMode"] = value; }
        }

        private AlumnoInscripcion Entity
        {
            get;
            set;
        }

        private int SelectedID
        {
            get
            {
                if (this.ViewState["SelectedID"] != null)
                {
                    return (int)this.ViewState["SelectedID"];
                }
                else return 0;
            }
            set
            {
                this.ViewState["SelectedID"] = value;
            }
        }

        private bool IsEntitySelected
        {
            get
            {
                return (this.SelectedID != 0);
            }
        }


        protected void gridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SelectedID = (int)this.gridView.SelectedValue;
        }

        private void LoadForm(int id)
        {

        }

        protected void editarLinkButton_Click(object sender, EventArgs e)
        {
            if (this.IsEntitySelected)
            {
                this.formPanel.Visible = true;
                this.FormMode = FormModes.Modificacion;
                this.LoadForm(this.SelectedID);
                EnableForm(true);
            }
        }

        private void LoadEntity(AlumnoInscripcion alumins)
        {

            alumins.IdCurso = Convert.ToInt32(this.ddlCurso.SelectedValue);
            alumins.IdAlumno = Convert.ToInt32(Session["idPersona"]);
            if(FormMode == FormModes.Alta)
            {
                alumins.Condicion = "Inscripto";
                alumins.Nota = 0;
            }
        }

        private void SaveEntity(AlumnoInscripcion alumins)
        {
            this.Logic.Save(alumins);
        }

        protected void aceptarLinkButton_Click(object sender, EventArgs e)
        {
            CursoLogic c = new CursoLogic();
            Curso curso = c.GetOne(Convert.ToInt32(ddlCurso.SelectedValue));
            switch (FormMode)
            {
                case FormModes.Baja:
                    DeleteEntity(SelectedID);
                    c.ActualizarCupo(curso, 1);
                    LoadGrid();
                    break;
                case FormModes.Modificacion:
                    this.Entity = new AlumnoInscripcion();
                    this.Entity.ID = this.SelectedID;
                    this.Entity.State = BusinessEntity.States.Modified;
                    this.LoadEntity(this.Entity);
                    this.SaveEntity(this.Entity);
                    this.LoadGrid();
                    break;
                case FormModes.Alta:
                    this.Entity = new AlumnoInscripcion();
                    if (curso.Cupo != 0)
                    {
                        this.LoadEntity(this.Entity);
                        this.SaveEntity(this.Entity);
                        c.ActualizarCupo(curso, -1);
                        this.LoadGrid();
                    }
                    else Response.Write("No hay cupo en este curso.");
                    break;
                default: break;
            }
            this.formPanel.Visible = false;
        }

        private void EnableForm(bool enable)
        {
            this.ddlCurso.Enabled = enable;

            AlumnoInscripcionLogic dcursos = new AlumnoInscripcionLogic();
            ddlCurso.DataSource = dcursos.GetCursos(Convert.ToInt32(Session["idPersona"]));
            ddlCurso.DataValueField = "id_curso";
            ddlCurso.DataTextField = "id_curso";
            ddlCurso.DataBind();

            ActualizarLabels();
        }

        protected void eliminarLinkButton_Click(object sender, EventArgs e)
        {
            if (this.IsEntitySelected)
            {
                this.formPanel.Visible = true;
                FormMode = FormModes.Baja;
                EnableForm(false);
                LoadForm(SelectedID);
            }
        }

        private void DeleteEntity(int id)
        {
            Logic.Delete(id);
        }

        protected void nuevoLinkButton_Click(object sender, EventArgs e)
        {
            formPanel.Visible = true;
            FormMode = FormModes.Alta;
            EnableForm(true);
        }

        protected void cancelarLinkButton_Click(object sender, EventArgs e)
        {
            LoadGrid();
            formPanel.Visible = false;
        }


        public void ActualizarLabels()
        {
            CursoLogic cl = new CursoLogic();
            Curso cur = cl.GetOne(Convert.ToInt32(this.ddlCurso.SelectedValue));
            MateriaLogic ml = new MateriaLogic();
            Materia mat = ml.GetOne(cur.IdMateria);
            lblMateria.Text = mat.Descripcion;
            ComisionLogic col = new ComisionLogic();
            Comision com = col.GetOne(cur.IdComision);
            lblComision.Text = com.Descripcion;
            lblComision.DataBind();
            lblMateria.DataBind();
            lblAnio.Text = cur.AnioCalendario.ToString();
            lblCupo.Text = cur.Cupo.ToString();
            lblCupo.DataBind();
            lblAnio.DataBind();
        }

        protected void ddlCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            CursoLogic cl = new CursoLogic();
            Curso cur = cl.GetOne(Convert.ToInt32(this.ddlCurso.SelectedValue));
            MateriaLogic ml = new MateriaLogic();
            Materia mat = ml.GetOne(cur.IdMateria);
            lblMateria.Text = mat.Descripcion;
            ComisionLogic col = new ComisionLogic();
            Comision com = col.GetOne(cur.IdComision);
            lblComision.Text = com.Descripcion;
            lblComision.DataBind();
            lblMateria.DataBind();
            lblAnio.Text = cur.AnioCalendario.ToString();
            lblCupo.Text = cur.Cupo.ToString();
            lblCupo.DataBind();
            lblAnio.DataBind();
        }
    }
}