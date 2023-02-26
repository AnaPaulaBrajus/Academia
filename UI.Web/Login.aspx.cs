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
    public partial class Login : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            UsuarioLogic usuarioActual = new UsuarioLogic();
            var usuario = usuarioActual.ExisteUsuario(txtUsuario.Text, txtClave.Text);
            if (usuario != null)
            {
                PersonaLogic per = new PersonaLogic();
                Business.Entities.Personas persona = per.GetOne(usuario.IdPersona);
                Session.Add("tipo", persona.TPersona);
                Session["idPersona"] = persona.ID;
                Response.Redirect("https://localhost:44366/Default.aspx");
            }
            else
            {
                Page.Response.Write("<script>alert('Usuario y / o contraseña incorrectos')</script>");
            }
        }

        protected void lnkRecordarClave_Click(object sender, EventArgs e)
        {
            Response.Redirect("https://localhost:44366/OlvidarContraseña.aspx");
        }
    }
}