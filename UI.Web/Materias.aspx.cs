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
    public partial class Materias : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadGrid();
            }
        }

        MateriaLogic _logic;

        private MateriaLogic Logic {
            get {
                if (_logic == null)
                {
                    _logic = new MateriaLogic();
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

        private Materia Entity {
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
            this.descripcionTextBox.Text = this.Entity.Descripcion;
            this.hs_semanalesTextBox.Text = this.Entity.HsSemanales.ToString();
            this.hs_totalesTextBox.Text = this.Entity.HsTotales.ToString();
            this.cmbIdPlan.SelectedValue = this.Entity.IdPlan.ToString();
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

        private void LoadEntity(Materia materia)
        {
            materia.Descripcion = this.descripcionTextBox.Text;
            materia.HsSemanales = Convert.ToInt32(this.hs_semanalesTextBox.Text);
            materia.HsTotales = Convert.ToInt32(this.hs_totalesTextBox.Text);
            materia.IdPlan = Convert.ToInt32(this.cmbIdPlan.SelectedValue);
        }

        private void SaveEntity(Materia materia)
        {
            this.Logic.Save(materia);
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
                    this.Entity = new Materia();
                    this.Entity.ID = this.SelectedID;
                    this.Entity.State = BusinessEntity.States.Modified;
                    this.LoadEntity(this.Entity);
                    this.SaveEntity(this.Entity);
                    this.LoadGrid();
                    break;
                case FormModes.Alta:
                    Entity = new Materia();
                    LoadEntity(Entity);
                    SaveEntity(Entity);
                    LoadGrid();
                    break;
                default: break;
            }
            this.formPanel.Visible = false;
        }

        private void EnableForm(bool enable)
        {
            this.descripcionTextBox.Enabled = enable;
            this.hs_semanalesTextBox.Enabled = enable;
            this.hs_totalesTextBox.Enabled = enable;
            this.cmbIdPlan.Enabled = enable;
            PlanLogic planes = new PlanLogic();
            this.cmbIdPlan.DataSource = planes.GetPlanes();
            cmbIdPlan.DataValueField = "id_plan";
            cmbIdPlan.DataTextField = "desc_plan";
            cmbIdPlan.DataBind();
        }

        protected void eliminarLinkButton_Click(object sender, EventArgs e)
        {
            if (this.IsEntitySelected)
            {
                formPanel.Visible = true;
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
            descripcionTextBox.Text = string.Empty;
            hs_semanalesTextBox.Text = string.Empty;
            hs_totalesTextBox.Text = string.Empty;
        }

        protected void cancelarLinkButton_Click(object sender, EventArgs e)
        {
            LoadGrid();
            formPanel.Visible = false;
        }
    }
}