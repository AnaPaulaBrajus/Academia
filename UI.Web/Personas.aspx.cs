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
    public partial class Personas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                LoadGrid();
                formPanel.Visible = false;
            }
        }

        PersonaLogic _logic;

        private PersonaLogic Logic
        {
            get
            {
                if(_logic == null)
                {
                    _logic = new PersonaLogic();
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
            get { return (FormModes)this.ViewState["FormMode"];}
            set { this.ViewState["FormMode"] = value;  }
        }

        private Business.Entities.Personas Entity
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
            this.direccionTextBox.Text = this.Entity.Direccion;
            this.emailTextBox.Text = this.Entity.Email;
            this.telefonoTextBox.Text = this.Entity.Telefono;
            this.legajoTextBox.Text = this.Entity.Legajo.ToString();
            this.cmbIdPlan.SelectedValue = this.Entity.IdPlan.ToString();
            this.cmbTipoPersona.SelectedValue = this.Entity.TPersona.ToString();
            this.calFechaNacimiento.SelectedDate = this.Entity.FechaNacimiento;

        }

        protected void editarLinkButton_Click(object sender, EventArgs e)
        {
            if(this.IsEntitySelected)
            {
                this.EnableForm(true);
                this.formPanel.Visible = true;
                this.FormMode = FormModes.Modificacion;
                this.LoadForm(this.SelectedID);
            }
        }

        private void LoadEntity(Business.Entities.Personas persona)
        {
            persona.Nombre = this.nombreTextBox.Text;
            persona.Apellido = this.apellidoTextBox.Text;
            persona.Direccion = this.direccionTextBox.Text;
            persona.Email = this.emailTextBox.Text;
            persona.Telefono = this.telefonoTextBox.Text;
            persona.FechaNacimiento = this.calFechaNacimiento.SelectedDate;
            persona.Legajo = Convert.ToInt32(this.legajoTextBox.Text);
            persona.IdPlan = Convert.ToInt32(this.cmbIdPlan.SelectedValue);
            persona.TPersona = (Business.Entities.Personas.TipoPersona)Enum.Parse(typeof(Business.Entities.Personas.TipoPersona), this.cmbTipoPersona.SelectedValue);
        }

        private void GuardarUsuario(Business.Entities.Personas persona)
        {
            this.Logic.Save(persona);
            PersonaLogic p = new PersonaLogic();
            Business.Entities.Personas per = p.GetLast();
            string no = per.Nombre; string ape = per.Apellido; string mail = per.Email; int id = per.ID;
            Response.Redirect("~/Usuarios.aspx?nombre=" + no + "&apellido=" + ape + "&email=" + mail + "&id_per=" + id);
        }

        private void SaveEntity(Business.Entities.Personas persona)
        {
            this.Logic.Save(persona);
        }

        protected void aceptarLinkButton_Click(object sender, EventArgs e)
        {
            if (calFechaNacimiento.SelectedDate == new DateTime(0001,01,01,0,0,0))

            {
                Response.Write("<script>alert('Seleccione una fecha de nacimiento')</script>");
            }
            else
            {
                switch (FormMode)
                {
                    case FormModes.Baja:
                        DeleteEntity(SelectedID);
                        LoadGrid();
                        break;
                    case FormModes.Modificacion:
                        this.Entity = new Business.Entities.Personas();
                        this.Entity.ID = this.SelectedID;
                        this.Entity.State = BusinessEntity.States.Modified;
                        this.LoadEntity(this.Entity);
                        this.SaveEntity(this.Entity);
                        UsuarioLogic u = new UsuarioLogic();
                        u.ActualizarDatos(nombreTextBox.Text, apellidoTextBox.Text, emailTextBox.Text, SelectedID);
                        this.LoadGrid();
                        break;
                    case FormModes.Alta:
                        Entity = new Business.Entities.Personas();
                        LoadEntity(Entity);
                        GuardarUsuario(Entity);
                        LoadGrid();
                        break;
                    default: break;
                }
                this.formPanel.Visible = false;
            }
        }

        private void EnableForm (bool enable)
        {
            this.nombreTextBox.Enabled = enable;
            this.apellidoTextBox.Enabled = enable;
            this.direccionTextBox.Enabled = enable;
            this.emailTextBox.Enabled = enable;
            this.telefonoTextBox.Enabled = enable;
            this.calFechaNacimiento.Enabled = enable;
            this.legajoTextBox.Enabled = enable;
            this.cmbIdPlan.Enabled = enable;
            this.cmbTipoPersona.Enabled = enable;
            PlanLogic planes = new PlanLogic();
            this.cmbIdPlan.DataSource = planes.GetPlanes();
            cmbIdPlan.DataValueField = "id_plan";
            cmbIdPlan.DataTextField = "desc_plan";
            cmbIdPlan.DataBind();
            this.cmbTipoPersona.DataSource = Enum.GetNames(typeof(Business.Entities.Personas.TipoPersona));
            cmbTipoPersona.DataBind();
        }

        protected void eliminarLinkButton_Click(object sender, EventArgs e)
        {
            if(this.IsEntitySelected)
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
            direccionTextBox.Text = string.Empty;
            emailTextBox.Text = string.Empty;
            telefonoTextBox.Text = string.Empty;
            legajoTextBox.Text = string.Empty;
        }//

        protected void cancelarLinkButton_Click(object sender, EventArgs e)
        {
            LoadGrid();
            formPanel.Visible = false;
        }

    }
}