using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Business.Logic;
using Business.Entities;
using Data.Database;

namespace UI.Desktop
{
    public partial class PersonaDesktop : UI.Desktop.ApplicationForm
    {
        public PersonaDesktop()
        {
            InitializeComponent();
        }

        public PersonaDesktop(ModoForm modo) : this()
        {
            Modo = modo;
        }

        public PersonaDesktop(int ID, ModoForm modo) : this()
        {
            PersonaLogic persona = new PersonaLogic();
            personaActual = persona.GetOne(ID);
            Modo = modo;
            MapearDeDatos();
        }

        protected Business.Entities.Personas personaActual;
        public Business.Entities.Personas PersonaActual
        {
            get { return personaActual; }
            set { personaActual = value; }
        }

        public override void MapearADatos()
        {
            if (Modo == ModoForm.Alta)
            {
                Business.Entities.Personas personaActual = new Business.Entities.Personas();
                PersonaActual = personaActual;
                PersonaActual.State = BusinessEntity.States.New;
            }

            if (Modo == ModoForm.Alta || Modo == ModoForm.Modificacion)
            {
                PersonaActual.Nombre = txtNombre.Text;
                PersonaActual.IdPlan = Convert.ToInt32(cmbPlanes.SelectedValue);
                PersonaActual.Apellido = txtApellido.Text;
                PersonaActual.Direccion = txtDireccion.Text;
                PersonaActual.Email = txtEmail.Text;
                PersonaActual.Legajo = Convert.ToInt32(txtLegajo.Text);
                PersonaActual.Telefono = txtTelefono.Text;
                PersonaActual.FechaNacimiento = Convert.ToDateTime(dtpFechaNacimiento.Text);

                if (Modo == ModoForm.Modificacion)
                {
                    PersonaActual.State = BusinessEntity.States.Modified;
                }
            }

            if (Modo == ModoForm.Baja)
            {
                PersonaActual.State = BusinessEntity.States.Deleted;
            }
        }

        public override void MapearDeDatos()
        {
            this.txtID.Text = this.PersonaActual.ID.ToString();
            this.txtNombre.Text = this.PersonaActual.Nombre;
            this.txtApellido.Text = this.PersonaActual.Apellido.ToString();
            this.txtDireccion.Text = this.PersonaActual.Direccion.ToString();
            this.txtEmail.Text = this.PersonaActual.Email.ToString();
            this.txtTelefono.Text = this.PersonaActual.Telefono.ToString();
            this.dtpFechaNacimiento.Text = this.PersonaActual.FechaNacimiento.ToString();
            this.txtLegajo.Text = this.PersonaActual.Legajo.ToString();
            this.cmbTipoPersona.SelectedValue = this.PersonaActual.TPersona.ToString();
            this.cmbPlanes.SelectedValue = this.PersonaActual.IdPlan.ToString();

            if (Modo == ModoForm.Alta || Modo == ModoForm.Modificacion)
            {
                btnAceptar.Text = "Guardar";
                if (Modo == ModoForm.Modificacion)
                {
                    this.Text = "Modificar persona";
                }
            }

            if (Modo == ModoForm.Baja)
            {
                txtID.ReadOnly = true;
                txtTelefono.ReadOnly = true;
                txtNombre.ReadOnly = true;
                dtpFechaNacimiento.Enabled = false;
                txtApellido.ReadOnly = true;
                txtLegajo.ReadOnly = true;
                txtDireccion.ReadOnly = true;
                cmbTipoPersona.Enabled = false;
                txtEmail.ReadOnly = true;
                cmbPlanes.Enabled = false;
                btnAceptar.Text = "Eliminar";
                this.Text = "Eliminar persona";
            }

            if (Modo == ModoForm.Consulta)
            {
                btnAceptar.Text = "Aceptar";
            }
        }

        public override bool Validar()
        {
            if(txtNombre.Text == "" || txtApellido.Text == "" || txtDireccion.Text == "" || txtEmail.Text == "" || txtLegajo.Text == "" || txtTelefono.Text == "")
            {
                Notificar("Campos vacíos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if(ValidarLogic.esMailValido(txtEmail.Text) == false)
            {
                this.Notificar("El mail no es válido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else return true;
        }

        public override void GuardarCambios()
        {
            MapearADatos();
            PersonaLogic perActual = new PersonaLogic();
            perActual.Save(PersonaActual);
            if (Modo == ModoForm.Alta)
            {
                UsuarioDesktop formUsuario = new UsuarioDesktop();
                PersonaLogic p = new PersonaLogic();
                Business.Entities.Personas per = p.GetLast();
                AddOwnedForm(formUsuario);
                formUsuario.txtApellido.Text = txtApellido.Text;
                formUsuario.txtEmail.Text = txtEmail.Text;
                formUsuario.txtNombre.Text = txtNombre.Text;
                formUsuario.txtIdPersona.Text = per.ID.ToString();
                formUsuario.txtNombre.Enabled = false;
                formUsuario.txtEmail.Enabled = false;
                formUsuario.txtApellido.Enabled = false;
                formUsuario.txtIdPersona.Enabled = false;
                formUsuario.ShowDialog();
            }
            if(Modo == ModoForm.Modificacion)
            {
                UsuarioLogic u = new UsuarioLogic();
                u.ActualizarDatos(txtNombre.Text, txtApellido.Text, txtEmail.Text, Convert.ToInt32(txtID.Text));
            }
        }

        public new void Notificar(string titulo, string mensaje, MessageBoxButtons
        botones, MessageBoxIcon icono)
        {
            MessageBox.Show(mensaje, titulo, botones, icono);

        }
        public new void Notificar(string mensaje, MessageBoxButtons botones,
        MessageBoxIcon icono)
        {
            this.Notificar(this.Text, mensaje, botones, icono);
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (Validar())
            {
                GuardarCambios();
                Close();
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void PersonaDesktop_Load(object sender, EventArgs e)
        {
            PersonaLogic per = new PersonaLogic();
            cmbTipoPersona.DataSource = per.GetTiposPersona();
            PlanLogic planes = new PlanLogic();
            cmbPlanes.DataSource = planes.GetPlanes();
            cmbPlanes.ValueMember = "id_plan";
            cmbPlanes.DisplayMember = "desc_plan";
        }
    }
}
