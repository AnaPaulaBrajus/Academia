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
    public partial class DocenteCursoDesktop : UI.Desktop.ApplicationForm
    {
        public DocenteCursoDesktop()
        {
            InitializeComponent();
        }

        private DocenteCurso _DocenteCursoActual;
        public DocenteCurso DocenteCursoActual {
            get { return _DocenteCursoActual; }
            set { _DocenteCursoActual = value; }
        }

        public DocenteCursoDesktop(ModoForm modo) : this()
        {
            Modo = modo;
        }

        public DocenteCursoDesktop(int ID, ModoForm modo) : this()
        {
            this.Modo = modo;
            DocenteCursoLogic dcl = new DocenteCursoLogic();
            this.DocenteCursoActual = dcl.GetOne(ID);
            this.MapearDeDatos();
        }

        public void Listar()
        {
            DocenteCursoLogic dc = new DocenteCursoLogic();
            cmbCurso.DataSource = dc.GetCursos();
            cmbCurso.ValueMember = "id_curso";
            cmbCurso.DisplayMember = "id_curso";
            cmbDocentes.DataSource = dc.GetDocentes();
            cmbDocentes.DisplayMember = "apenom";
            cmbDocentes.ValueMember = "id_persona";
            cmbCargo.DataSource = Enum.GetValues(typeof(DocenteCurso.TipoCargo));

            if (ModoForm.Modificacion == Modo || ModoForm.Baja == Modo)
            {
                cmbCurso.SelectedValue = DocenteCursoActual.IdCurso.ToString();
            }

            CursoLogic c = new CursoLogic();
            Curso cur = c.GetOne(Convert.ToInt32(cmbCurso.SelectedValue));
            MateriaLogic m = new MateriaLogic();
            Materia mat = m.GetOne(cur.IdMateria);
            lblMateria.Text = mat.Descripcion;
            ComisionLogic com = new ComisionLogic();
            Comision comi = com.GetOne(cur.IdComision);
            lblComision.Text = comi.Descripcion;
            lblAnioCalendario.Text = cur.AnioCalendario.ToString();
            lblCupo.Text = cur.Cupo.ToString();

        }

        public override void MapearDeDatos()
        {
            this.txtIdDictado.Text = this.DocenteCursoActual.ID.ToString();
            this.cmbDocentes.SelectedValue = this.DocenteCursoActual.IdDocente;
            
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
                txtIdDictado.ReadOnly = true;
                cmbCurso.Enabled = false;
                cmbDocentes.Enabled = false;
                cmbCargo.Enabled = false;
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
            if (Modo == ModoForm.Alta)
            {
                DocenteCurso dc = new DocenteCurso();
                DocenteCursoActual = dc;
                DocenteCursoActual.State = BusinessEntity.States.New;
            }

            if (Modo == ModoForm.Alta || Modo == ModoForm.Modificacion)
            {
                
                this.DocenteCursoActual.IdCurso = Convert.ToInt32(this.cmbCurso.SelectedValue);
                this.DocenteCursoActual.IdDocente = Convert.ToInt32(this.cmbDocentes.SelectedValue);
                this.DocenteCursoActual.Cargo = (DocenteCurso.TipoCargo)(this.cmbCargo.SelectedValue);

                if (Modo == ModoForm.Modificacion)
                {
                    DocenteCursoActual.State = BusinessEntity.States.Modified;
                }
            }

            if (Modo == ModoForm.Baja)
            {
                DocenteCursoActual.State = BusinessEntity.States.Deleted;
            }
        }

        public override void GuardarCambios()
        {
            MapearADatos();
            DocenteCursoLogic dcActual = new DocenteCursoLogic();
            dcActual.Save(DocenteCursoActual);
        }

        private void DocenteCursoDesktop_Load(object sender, EventArgs e)
        {
            Listar();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            GuardarCambios();
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbCurso_SelectionChangeCommitted(object sender, EventArgs e)
        {
            CursoLogic c = new CursoLogic();
            Curso cur = c.GetOne(Convert.ToInt32(cmbCurso.SelectedValue));
            MateriaLogic m = new MateriaLogic();
            Materia mat = m.GetOne(cur.IdMateria);
            lblMateria.Text = mat.Descripcion;
            ComisionLogic com = new ComisionLogic();
            Comision comi = com.GetOne(cur.IdComision);
            lblComision.Text = comi.Descripcion;
            lblAnioCalendario.Text = cur.AnioCalendario.ToString();
            lblCupo.Text = cur.Cupo.ToString();
        }
    }
}

