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
    public partial class Especialidades : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadGrid();
            }
        }

        EspecialidadLogic _logic;
        private EspecialidadLogic Logic
        {
            get
            {
                if (_logic == null)
                {
                    _logic = new EspecialidadLogic();
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

        public FormModes FormMode
        {
            get { return (FormModes)this.ViewState["FormMode"]; }
            set { this.ViewState["FormMode"] = value; }
        }

        private Especialidad Entity
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
            this.Entity = this.Logic.GetOne(id);
            this.descripcionTextBox.Text = this.Entity.Descripcion;
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

        private void LoadEntity(Especialidad especialidad)
        {
            especialidad.Descripcion = this.descripcionTextBox.Text;

        }

        private void SaveEntity(Especialidad especialidad)
        {
            this.Logic.Save(especialidad);
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
                    this.Entity = new Especialidad();
                    this.Entity.ID = this.SelectedID;
                    this.Entity.State = BusinessEntity.States.Modified;
                    this.LoadEntity(this.Entity);
                    this.SaveEntity(this.Entity);
                    this.LoadGrid();
                    break;
                case FormModes.Alta:
                    Entity = new Especialidad();
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
        }

        protected void cancelarLinkButton_Click(object sender, EventArgs e)
        {
            LoadGrid();
            formPanel.Visible = false;
        }

    }
}