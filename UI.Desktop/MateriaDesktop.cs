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
    public partial class MateriaDesktop : UI.Desktop.ApplicationForm
    {
        public MateriaDesktop()
        {
            InitializeComponent();
        }

        public MateriaDesktop(ModoForm modo) : this()
        {
            Modo = modo;
        }

        public MateriaDesktop(int ID, ModoForm modo) : this()
        {
            MateriaLogic materia = new MateriaLogic();
            materiaActual = materia.GetOne(ID);
            Modo = modo;
            MapearDeDatos();
        }

        protected Materia materiaActual;
        public Materia MateriaActual
        {
            get { return materiaActual; }
            set { materiaActual = value; }
        }

        public override void MapearDeDatos()
        {
            this.txtID.Text = this.MateriaActual.ID.ToString();
            this.txtDescripcion.Text = this.MateriaActual.Descripcion;
            this.txtHsSemanales.Text = this.MateriaActual.HsSemanales.ToString();
            this.txtHsTotales.Text = this.MateriaActual.HsTotales.ToString();
            this.cmbPlanes.SelectedValue = this.MateriaActual.IdPlan.ToString();

            if (Modo == ModoForm.Alta || Modo == ModoForm.Modificacion)
            {
                btnAceptar.Text = "Guardar";
                if (Modo == ModoForm.Modificacion)
                {
                    this.Text = "Modificar materia";
                }
            }

            if (Modo == ModoForm.Baja)
            {
                txtID.ReadOnly = true;
                txtHsTotales.ReadOnly = true;
                txtHsSemanales.ReadOnly = true;
                txtDescripcion.ReadOnly = true;
                cmbPlanes.Enabled = false;
                btnAceptar.Text = "Eliminar";
                this.Text = "Eliminar materia";
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
                Materia materiaActual = new Materia();
                MateriaActual = materiaActual;
                MateriaActual.State = BusinessEntity.States.New;
            }

            if (Modo == ModoForm.Alta || Modo == ModoForm.Modificacion)
            {
                MateriaActual.Descripcion = txtDescripcion.Text;
                MateriaActual.IdPlan = Convert.ToInt32(cmbPlanes.SelectedValue);
                MateriaActual.HsSemanales = Int32.Parse(txtHsSemanales.Text);
                MateriaActual.HsTotales = Int32.Parse(txtHsTotales.Text);

                if (Modo == ModoForm.Modificacion)
                {
                    MateriaActual.State = BusinessEntity.States.Modified;
                }
            }

            if (Modo == ModoForm.Baja)
            {
                MateriaActual.State = BusinessEntity.States.Deleted;
            }
        }

        public override void GuardarCambios()
        {
            MapearADatos();
            MateriaLogic matActual = new MateriaLogic();
            matActual.Save(MateriaActual);
        }
        public override bool Validar()
        {
            if (txtDescripcion.Text == "" || txtHsSemanales.Text == "" || txtHsTotales.Text == "")
            {
                this.Notificar("Campos vacios", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (!ValidarLogic.esEntero(txtHsSemanales.Text))
            {
                this.Notificar("Ingrese la cantidad de horas semanales en numeros enteros", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (!ValidarLogic.esEntero(txtHsTotales.Text))
            {
                this.Notificar("Ingrese la cantidad de horas totales en numeros enteros", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if(Int32.Parse(txtHsSemanales.Text) <= 0 || Int32.Parse(txtHsSemanales.Text) >= 120)
            {
                this.Notificar("La cantidad de horas semanales debe ser mayor o igual a cero y menor o igual a 120", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if(Int32.Parse(txtHsSemanales.Text) >= Int32.Parse(txtHsTotales.Text))
            {
                this.Notificar("La cantidad de horas semanales es mayor a la cantidad total de horas",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return false;
            }
            else return true;
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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MateriaDesktop_Load(object sender, EventArgs e)
        {
            PlanLogic planes = new PlanLogic();
            cmbPlanes.DataSource = planes.GetPlanes();
            cmbPlanes.ValueMember = "id_plan";
            cmbPlanes.DisplayMember = "desc_plan";

            if(Modo== ModoForm.Baja || Modo == ModoForm.Modificacion)
            {
                cmbPlanes.SelectedValue = materiaActual.IdPlan.ToString();
            }
        }
    }
}
