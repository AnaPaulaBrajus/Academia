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
    public partial class DocentesCursos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadGrid();
            }
        }
        DocenteCursoLogic _logic;

        private DocenteCursoLogic Logic {
            get {
                if (_logic == null)
                {
                    _logic = new DocenteCursoLogic();
                }
                return _logic;
            }
        }

        private void LoadGrid()
        {
            this.gridView.DataSource = this.Logic.GetAll();
            this.gridView.DataBind();
        }

        public enum FormModes
        {
            Alta,
            Baja,
            Modificacion
        }

        public FormModes FormMode {
            get { return (FormModes)this.ViewState["FormMode"]; }
            set { this.ViewState["FormMode"] = value; }
        }

        private DocenteCurso Entity {
            get;
            set;
        }

        private int SelectedID {
            get {
                if (this.ViewState["SelectedID"] != null)
                {
                    return (int)this.ViewState["SelectedID"];
                }
                else return 0;
            }
            set {
                this.ViewState["SelectedID"] = value;
            }
        }

        private bool IsEntitySelected {
            get {
                return (this.SelectedID != 0);
            }
        }


        protected void gridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SelectedID = (int)this.gridView.SelectedValue;
        }

        private void LoadForm(int id)
        {
            this.Entity = this.Logic.GetOne(id);
            this.ddlCargo.SelectedValue = this.Entity.Cargo.ToString();
            this.ddlCurso.SelectedValue = this.Entity.IdCurso.ToString();
            this.ddlDocente.SelectedValue = this.Entity.IdDocente.ToString();
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

        private void LoadEntity(DocenteCurso dcurso)
        {
            dcurso.IdCurso = Convert.ToInt32(this.ddlCurso.SelectedValue);
            dcurso.IdDocente = Convert.ToInt32(this.ddlDocente.SelectedValue);
            dcurso.Cargo = (DocenteCurso.TipoCargo)Enum.Parse(typeof(DocenteCurso.TipoCargo), this.ddlCargo.SelectedValue);
        }

        private void SaveEntity(DocenteCurso dcurso)
        {
            this.Logic.Save(dcurso);
        }

        protected void aceptarLinkButton_Click(object sender, EventArgs e)
        {
            switch (FormMode)
            {
                case FormModes.Baja:
                    DeleteEntity(SelectedID);
                    LoadGrid();
                    break;
                case FormModes.Modificacion:
                    this.Entity = new DocenteCurso();
                    this.Entity.ID = this.SelectedID;
                    this.Entity.State = BusinessEntity.States.Modified;
                    this.LoadEntity(this.Entity);
                    this.SaveEntity(this.Entity);
                    this.LoadGrid();
                    break;
                case FormModes.Alta:
                    this.Entity = new DocenteCurso();
                    this.LoadEntity(this.Entity);
                    this.SaveEntity(this.Entity);
                    this.LoadGrid();
                    break;
                default: break;
            }
            this.formPanel.Visible = false;
        }

        private void EnableForm(bool enable)
        {
            this.ddlCargo.Enabled = enable;
            this.ddlCurso.Enabled = enable;
            this.ddlDocente.Enabled = enable;

            DocenteCursoLogic dcursos = new DocenteCursoLogic();
            ddlCurso.SelectedValue = null;
            ddlCurso.DataSource = dcursos.GetCursos();
            ddlCurso.DataValueField = "id_curso";
            ddlCurso.DataTextField = "id_curso";
            ddlCurso.DataBind();

            ddlDocente.SelectedValue = null;
            this.ddlDocente.DataSource = dcursos.GetDocentes();
            ddlDocente.DataValueField = "id_persona";
            ddlDocente.DataTextField = "apenom";
            ddlDocente.DataBind();

            ddlCargo.SelectedValue = null;
            this.ddlCargo.DataSource = Enum.GetNames(typeof(DocenteCurso.TipoCargo));
            ddlCargo.DataBind();

            ActualizarComboCurso();
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
            ClearForm();
            EnableForm(true);
        }

        private void ClearForm()
        {

        }

        protected void cancelarLinkButton_Click(object sender, EventArgs e)
        {
            LoadGrid();
            formPanel.Visible = false;
        }

        public void ActualizarComboCurso()
        {
            CursoLogic cl = new CursoLogic();
            Curso cur = cl.GetOne(Convert.ToInt32(this.ddlCurso.SelectedValue));
            MateriaLogic ml = new MateriaLogic();
            Materia mat = ml.GetOne(cur.IdMateria);
            lblMateria.Text = mat.Descripcion;
            ComisionLogic col = new ComisionLogic();
            Comision com = col.GetOne(cur.IdComision);
            lblComision.Text = com.Descripcion;
            lblAnio.Text = cur.AnioCalendario.ToString();
            lblCupo.Text = cur.Cupo.ToString();
            lblComision.DataBind();
            lblMateria.DataBind();
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