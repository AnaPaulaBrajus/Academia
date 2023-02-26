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
    public partial class CursoDesktop : UI.Desktop.ApplicationForm
    {
        protected Curso cursoActual;

        public CursoDesktop()
        {
            InitializeComponent();
        }

        public CursoDesktop(ModoForm modo): this()
        {
            Modo = modo;
        }

        public CursoDesktop(int ID, ModoForm modo): this()
        {
            CursoLogic curso = new CursoLogic();
            CursoActual = curso.GetOne(ID);
            Modo = modo;
            MapearDeDatos();
        }

        public Curso CursoActual
        {
            get { return cursoActual; }
            set { cursoActual = value; }
        }

        public override void MapearDeDatos()
        {
            this.txtID.Text = this.CursoActual.ID.ToString();
            this.txtAnio.Text = this.CursoActual.AnioCalendario.ToString();
            this.txtCupo.Text = this.CursoActual.Cupo.ToString();
            
            if (Modo == ModoForm.Alta || Modo == ModoForm.Modificacion)
            {
                btnAceptar.Text = "Guardar";
                if (Modo == ModoForm.Modificacion)
                {
                    this.Text = "Modificar curso";
                }
            }

            if (Modo == ModoForm.Baja)
            {
                txtID.ReadOnly = true;
                txtCupo.ReadOnly = true;
                txtAnio.ReadOnly = true;
                cmbComision.Enabled = false;
                cmbMateria.Enabled = false;
                btnAceptar.Text = "Eliminar";
                this.Text = "Eliminar curso";
            }

            if (Modo == ModoForm.Consulta)
            {
                btnAceptar.Text = "Aceptar";
            }
        }

        public override void MapearADatos()
        {
            if(Modo == ModoForm.Alta)
            {
                Curso cursoActual = new Curso();
                CursoActual = cursoActual;
                CursoActual.State = BusinessEntity.States.New;
            }

            if (Modo == ModoForm.Alta || Modo == ModoForm.Modificacion)
            {
                CursoActual.AnioCalendario = Convert.ToInt32(txtAnio.Text);
                CursoActual.Cupo = Convert.ToInt32(txtCupo.Text);
                CursoActual.IdMateria= Convert.ToInt32(cmbMateria.SelectedValue);
                CursoActual.IdComision = Convert.ToInt32(cmbComision.SelectedValue);

                if(Modo == ModoForm.Modificacion)
                {
                    CursoActual.State = BusinessEntity.States.Modified;
                }
            }

            if(Modo == ModoForm.Baja)
            {
                CursoActual.State = BusinessEntity.States.Deleted;
            }
        }

        public override bool Validar()
        {
            if (txtAnio.Text == "" || txtCupo.Text == "")
            {
                Notificar("Campos vacíos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else return true;
        }

        public override void GuardarCambios()
        {
            MapearADatos();
            CursoLogic curActual = new CursoLogic();
            curActual.Save(CursoActual);
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

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (Validar())
            {
                GuardarCambios();
                Close();
            }
        }

        private void CursoDesktop_Load(object sender, EventArgs e)
        {
            MateriaLogic mat = new MateriaLogic();
            cmbMateria.DataSource = mat.GetMaterias();
            cmbMateria.DisplayMember = "desc_materia";
            cmbMateria.ValueMember = "id_materia";
            ComisionLogic com = new ComisionLogic();
            cmbComision.DataSource = com.GetComisiones();
            cmbComision.DisplayMember = "desc_comision";
            cmbComision.ValueMember = "id_comision";
            if(Modo == ModoForm.Modificacion || Modo == ModoForm.Baja)
            {
                cmbMateria.SelectedValue = this.CursoActual.IdMateria;
                cmbComision.SelectedValue = this.CursoActual.IdComision;
            }
        }
    }
}
