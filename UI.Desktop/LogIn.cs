using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Business.Logic;
using Business.Entities;

namespace UI.Desktop
{
    public partial class LogIn : Form
    {
        public LogIn()
        {
            InitializeComponent();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            UsuarioLogic usuarioActual = new UsuarioLogic();
            Usuario usuario = usuarioActual.ExisteUsuario(txtUsuario.Text, txtContraseña.Text);
            if (usuario is null)
            {
                MessageBox.Show("No se encontró el usuario");
            }
            else
            {
                PersonaLogic per = new PersonaLogic();
                Business.Entities.Personas persona = per.GetOne(usuario.IdPersona);
                Inicio ini = new Inicio(persona.TPersona, persona.ID);
                ini.ShowDialog();
            }
        }

        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lnklblOlvidaste_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OlvidasteContrasenia formOlvido = new OlvidasteContrasenia();
            formOlvido.ShowDialog();
        }

        private void btnCrearCuentaNueva_Click(object sender, EventArgs e)
        {
            PersonaDesktop formPersona = new PersonaDesktop();
            formPersona.ShowDialog();
        }
    }
}
