using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Entities;
using Data.Database;

namespace Business.Logic
{
    public class UsuarioLogic : BusinessLogic
    {
        private UsuarioAdapter _usuarioData;
        public UsuarioAdapter UsuarioData {
            get { return _usuarioData; }
            set { _usuarioData = value; }
        }

        public UsuarioLogic()
        {
           UsuarioData = new UsuarioAdapter();
        }

        public Usuario ExisteUsuario(string us, string con)
        {
            return UsuarioData.ExisteUsuario(us, con);
        }

        public void ActualizarDatos(string nom, string ape, string email, int id)
        {
            UsuarioData.ActualizarDatos(nom,ape,email,id);
        }

        public Usuario ActualizarContraseña(string us, string conActual, string conNueva)
        {
            return UsuarioData.ActualizarContraseña(us, conActual, conNueva);
        }

        public Usuario GetOne(int id)
        {
           return UsuarioData.GetOne(id); 
        }

        public List<Usuario> GetAll()
        {
            return UsuarioData.GetAll(); 
        }

        public void Delete(int id)
        {
            UsuarioData.Delete(id);
        }

        public void Save(Usuario usu)
        {
            UsuarioData.Save(usu);
        }

    }
}
