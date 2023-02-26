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

namespace UI.Desktop
{
    public partial class OlvidasteContrasenia : Form
    {
        public OlvidasteContrasenia()
        {
            InitializeComponent();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            UsuarioLogic usuarioActual = new UsuarioLogic();
            if (ValidarLogic.ValidarCantCaractContraseña(txtContraseniaNueva.Text))
            {
                var usuario = usuarioActual.ActualizarContraseña(txtNombreUsuario.Text, txtContraseniaVieja.Text, txtContraseniaNueva.Text);
                if (usuario is null)
                {
                    MessageBox.Show("No se encontró el usuario");
                }
                else
                {
                    MessageBox.Show("La contraseña se modificó con éxito");
                    Close();
                }
            }
            else { MessageBox.Show("La nueva contraseña debe tener mas de 8 caracteres"); }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }


}

