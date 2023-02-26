using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Business.Logic;
using Business.Entities;

namespace UI.Desktop
{
    public partial class PlanDesktop : UI.Desktop.ApplicationForm
    {
        public PlanDesktop()
        {
            InitializeComponent();
        }

        public PlanDesktop(ModoForm modo) : this()
        {
            Modo = modo;
        }

        public PlanDesktop(int ID, ModoForm modo) : this()
        {
            PlanLogic plan = new PlanLogic();
            planActual = plan.GetOne(ID);
            Modo = modo;
            MapearDeDatos();
        }

        protected Plan planActual;
        public Plan PlanActual
        {
            get { return planActual; }
            set { planActual = value; }
        }

        public override void MapearDeDatos()
        {
            this.txtID.Text = this.PlanActual.ID.ToString();
            this.txtDescripcion.Text = this.PlanActual.Descripcion;

            if (Modo == ModoForm.Alta || Modo == ModoForm.Modificacion)
            {
                btnAceptar.Text = "Guardar";
                if (Modo == ModoForm.Modificacion)
                {
                    this.Text = "Modificar plan";
                }
            }

            if (Modo == ModoForm.Baja)
            {
                txtID.ReadOnly = true;
                txtDescripcion.ReadOnly = true;
                cmbEspecialidad.Enabled = false;
                btnAceptar.Text = "Eliminar";
                this.Text = "Eliminar plan";
            }

            if (Modo == ModoForm.Consulta)
            {
                btnAceptar.Text = "Aceptar";
            }
        }
        public override void MapearADatos()
        {
            if (Modo == ModoForm.Alta)
            {
                Plan planActual = new Plan();
                PlanActual = planActual;
                PlanActual.State = BusinessEntity.States.New;
            }

            if (Modo == ModoForm.Alta || Modo == ModoForm.Modificacion)
            {
                PlanActual.Descripcion = txtDescripcion.Text;
                PlanActual.IdEspecialidad = Convert.ToInt32(cmbEspecialidad.SelectedValue);

                if (Modo == ModoForm.Modificacion)
                {
                    PlanActual.State = BusinessEntity.States.Modified;
                }
            }

            if (Modo == ModoForm.Baja)
            {
                PlanActual.State = BusinessEntity.States.Deleted;
            }
        }

        public override void GuardarCambios()
        {
            MapearADatos();
            PlanLogic planActual = new PlanLogic();
            planActual.Save(PlanActual);
        }
        public override bool Validar()
        {
            if (txtDescripcion.Text == "")
            {
                this.Notificar("Campos vacios", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else
            {
                return true;
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
                this.GuardarCambios();
                this.Close();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PlanDesktop_Load(object sender, EventArgs e)
        {
            EspecialidadLogic esp = new EspecialidadLogic();
            cmbEspecialidad.DataSource = esp.GetEspecialidades();
            cmbEspecialidad.ValueMember = "id_especialidad";
            cmbEspecialidad.DisplayMember = "desc_especialidad";
            if(Modo == ModoForm.Modificacion || Modo == ModoForm.Baja)
            {
                this.cmbEspecialidad.SelectedValue = this.PlanActual.IdEspecialidad;
            }
        }
    }
}
