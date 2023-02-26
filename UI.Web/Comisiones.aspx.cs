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
    public partial class Comisiones : System.Web.UI.Page 
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadGrid();
            }
        }

        ComisionLogic _logic;

        private ComisionLogic Logic {
            get {
                if (_logic == null)
                {
                    _logic = new ComisionLogic();
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

        private Business.Entities.Comision Entity {
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
            this.anio_especialidadTextBox.Text = this.Entity.AnioEspecialidad.ToString();
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

        private void LoadEntity(Business.Entities.Comision comision)
        {
            comision.Descripcion = this.descripcionTextBox.Text;
            comision.AnioEspecialidad = Convert.ToInt32(this.anio_especialidadTextBox.Text);
            comision.IdPlan = Convert.ToInt32(this.cmbIdPlan.SelectedValue);
        }

        private void SaveEntity(Business.Entities.Comision comision)
        {
            this.Logic.Save(comision);
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
                    this.Entity = new Business.Entities.Comision();
                    this.Entity.ID = this.SelectedID;
                    this.Entity.State = BusinessEntity.States.Modified;
                    this.LoadEntity(this.Entity);
                    this.SaveEntity(this.Entity);
                    this.LoadGrid();
                    break;
                case FormModes.Alta:
                    Entity = new Business.Entities.Comision();
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
            this.anio_especialidadTextBox.Enabled = enable;
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
            anio_especialidadTextBox.Text = string.Empty;
        }

        protected void cancelarLinkButton_Click(object sender, EventArgs e)
        {
            LoadGrid();
            formPanel.Visible = false;
        }
    }
}