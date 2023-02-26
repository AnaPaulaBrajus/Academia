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
    public partial class Usuarios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.LoadGrid();
            if (Request.QueryString["nombre"] is null)
            {
                formPanel.Visible = false;
            }
            else
            {
                this.nombreTextBox.Text = Request.QueryString["nombre"];
                nombreTextBox.ReadOnly = true;
                this.apellidoTextBox.Text = Request.QueryString["apellido"];
                apellidoTextBox.ReadOnly = true;
                this.emailTextBox.Text = Request.QueryString["email"];
                emailTextBox.ReadOnly = true;
                this.idPersonaTextBox.Text = Request.QueryString["id_per"];
                idPersonaTextBox.ReadOnly = true;
                formPanel.Visible = true;
                this.FormMode = FormModes.Alta;
            }
        }

        UsuarioLogic _logic;
        private UsuarioLogic Logic
        {
            get
            {
                if (_logic == null)
                {
                    _logic = new UsuarioLogic();
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

        private Usuario Entity
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
            this.nombreTextBox.Text = this.Entity.Nombre;
            this.apellidoTextBox.Text = this.Entity.Apellido;
            this.emailTextBox.Text = this.Entity.Email;
            this.habilitadoCheckBox.Checked = this.Entity.Habilitado;
            this.nombreUsuarioTextBox.Text = this.Entity.NombreUsuario;
            this.idPersonaTextBox.Text = this.Entity.IdPersona.ToString();
            this.claveTextBox.Text = this.Entity.Clave.ToString();
        }

        protected void editarLinkButton_Click(object sender, EventArgs e)
        {
            if (this.IsEntitySelected)
            {
                EnableForm(true);
                this.formPanel.Visible = true;
                this.FormMode = FormModes.Modificacion;
                this.LoadForm(this.SelectedID);
            }
            
        }

        private void LoadEntity(Usuario usuario)
        {
            usuario.Nombre = this.nombreTextBox.Text;
            usuario.Apellido = this.apellidoTextBox.Text;
            usuario.Email = this.emailTextBox.Text;
            usuario.NombreUsuario = this.nombreUsuarioTextBox.Text;
            usuario.Clave = this.claveTextBox.Text;
            usuario.Habilitado = this.habilitadoCheckBox.Checked;
            usuario.IdPersona = Convert.ToInt32(idPersonaTextBox.Text);
        }

        private void SaveEntity(Usuario usuario)
        {
            this.Logic.Save(usuario);
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
                    this.Entity = new Usuario();
                    this.Entity.ID = this.SelectedID;
                    this.Entity.State = BusinessEntity.States.Modified;
                    this.LoadEntity(this.Entity);
                    this.SaveEntity(this.Entity);
                    this.LoadGrid();
                    break;
                case FormModes.Alta:
                    Entity = new Usuario();
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
            this.nombreTextBox.Enabled = enable;
            this.apellidoTextBox.Enabled = enable;
            this.emailTextBox.Enabled = enable;
            this.nombreUsuarioTextBox.Enabled = enable;
            this.claveTextBox.Visible = enable;
            this.claveLabel.Visible = enable;
            this.repetirClaveLabel.Visible = enable;
            this.repetirClaveTextBox.Visible = enable;
            this.habilitadoCheckBox.Visible = enable;
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
            nombreTextBox.Text = string.Empty;
            apellidoTextBox.Text = string.Empty;
            emailTextBox.Text = string.Empty;
            habilitadoCheckBox.Checked = false;
            nombreUsuarioTextBox.Text = string.Empty;
            claveTextBox.Text = string.Empty;
            repetirClaveTextBox.Text = string.Empty;
        }

        protected void cancelarLinkButton_Click(object sender, EventArgs e)
        {
            LoadGrid();
            formPanel.Visible = false;
        }

        protected void ValidarLongitudContraseña_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (args.Value.Length >= 8)
            {
                args.IsValid = true;
            }
            else args.IsValid = false;
        }
    }
}