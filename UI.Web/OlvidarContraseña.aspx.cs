using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Logic;

namespace UI.Web
{
    public partial class OlvidarContraseña : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            UsuarioLogic usuarioActual = new UsuarioLogic();
            if (ValidarLogic.ValidarCantCaractContraseña(txtNuevaContraseña.Text))
            {
                var usuario = usuarioActual.ActualizarContraseña(txtUsuario.Text, txtClaveAnterior.Text, txtNuevaContraseña.Text);
                if (usuario != null)
                {
                    Page.Response.Write("La contraseña se modificó con éxito!");
                }
            }   
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            claveAnteriorRequerida.Enabled = false;
            nuevaContraRequerida.Enabled = false; //arreglar
            usuarioRequerido.Enabled = false;
            Response.Redirect("https://localhost:44366/Login.aspx");
        }
    }
}