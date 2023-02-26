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
    public partial class Cursos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadGrid();
            }
        }

        CursoLogic _logic;

        private CursoLogic Logic {
            get {
                if (_logic == null)
                {
                    _logic = new CursoLogic();
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

        private Curso Entity {
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
            this.txtAnio.Text = this.Entity.AnioCalendario.ToString();
            this.txtCupo.Text = this.Entity.Cupo.ToString();
            this.ddlComision.SelectedValue = this.Entity.IdComision.ToString();
            this.ddlMateria.SelectedValue = this.Entity.IdMateria.ToString();
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

        private void LoadEntity(Curso curso)
        {
            curso.AnioCalendario = Convert.ToInt32(this.txtAnio.Text);
            curso.Cupo = Convert.ToInt32(this.txtCupo.Text);
            curso.IdMateria = Convert.ToInt32(this.ddlMateria.SelectedValue);
            curso.IdComision = Convert.ToInt32(this.ddlComision.SelectedValue);
        }

        private void SaveEntity(Curso curso)
        {
            this.Logic.Save(curso);
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
                    this.Entity = new Curso();
                    this.Entity.ID = this.SelectedID;
                    this.Entity.State = BusinessEntity.States.Modified;
                    this.LoadEntity(this.Entity);
                    this.SaveEntity(this.Entity);
                    this.LoadGrid();
                    break;
                case FormModes.Alta:
                    this.Entity = new Curso();
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
            this.txtAnio.Enabled = enable;
            this.txtCupo.Enabled = enable;
            this.ddlMateria.Enabled = enable;
            this.ddlComision.Enabled = enable;
            MateriaLogic materias = new MateriaLogic();
            ddlMateria.SelectedValue = null;
            this.ddlMateria.DataSource = materias.GetMaterias();
            ddlMateria.DataValueField ="id_materia";
            ddlMateria.DataTextField ="desc_materia";
            ddlMateria.DataBind();
            ComisionLogic comision = new ComisionLogic();
            ddlComision.SelectedValue = null;
            this.ddlComision.DataSource = comision.GetComisiones();
            ddlComision.DataValueField ="id_comision";
            ddlComision.DataTextField ="desc_comision";
            ddlComision.DataBind();
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
            txtAnio.Text = string.Empty;
            txtCupo.Text = string.Empty;
        }

        protected void cancelarLinkButton_Click(object sender, EventArgs e)
        {
            LoadGrid();
            formPanel.Visible = false;
        }

    }
}