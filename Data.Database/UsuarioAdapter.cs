using System;
using System.Collections.Generic;
using System.Text;
using Business.Entities;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Data.Database
{
    public class UsuarioAdapter:Adapter
    {

        public List<Usuario> GetAll()
        {
            List<Usuario> usuarios = new List<Usuario>();

            try
            {
                this.OpenConnection();
                SqlCommand cmdUsuarios = new SqlCommand("select * from usuarios", sqlConn);
                SqlDataReader drUsuarios = cmdUsuarios.ExecuteReader();
                while (drUsuarios.Read())
                {
                    Usuario usr = new Usuario();
                    usr.ID = (int)drUsuarios["id_usuario"];
                    usr.NombreUsuario = (string)drUsuarios["nombre_usuario"];
                    usr.Clave = (string)drUsuarios["clave"];
                    usr.Habilitado = (bool)drUsuarios["habilitado"];
                    usr.Nombre = (string)drUsuarios["nombre"];
                    usr.Apellido = (string)drUsuarios["apellido"];
                    usr.Email = (string)drUsuarios["email"];
                    usr.IdPersona = (int)drUsuarios["id_persona"];

                    usuarios.Add(usr);
                }

                drUsuarios.Close();
            }

            catch(Exception Ex)
            {
                Exception ExcepcionManejada =
                new Exception("Error al recuperar lista de usuarios", Ex);
            }
            finally { this.CloseConnection(); }

            return usuarios;
        }

        public Usuario GetOne(int ID)
        {
            Usuario usr = new Usuario();
            try
            {
                this.OpenConnection();
                SqlCommand cmdUsuarios = new SqlCommand("select * from usuarios where id_usuario = @id", sqlConn);
                cmdUsuarios.Parameters.Add("@id", SqlDbType.Int).Value = ID;
                SqlDataReader drUsuarios = cmdUsuarios.ExecuteReader();
                if (drUsuarios.Read())
                {
                    usr.ID = (int)drUsuarios["id_usuario"];
                    usr.NombreUsuario = (string)drUsuarios["nombre_usuario"];
                    usr.Clave = (string)drUsuarios["clave"];
                    usr.Habilitado = (bool)drUsuarios["habilitado"];
                    usr.Nombre = (string)drUsuarios["nombre"];
                    usr.Apellido = (string)drUsuarios["apellido"];
                    usr.Email = (string)drUsuarios["email"];
                    usr.IdPersona = (int)drUsuarios["id_persona"];
                }
                drUsuarios.Close();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al recuperar datos de usuario", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
            return usr;
        }

        public Usuario ExisteUsuario(string nombre, string contrase�a)
        {
            Usuario usr = new Usuario();
            try
            {
                this.OpenConnection();
                SqlCommand cmdUsuarios = new SqlCommand("select * from usuarios where nombre_usuario = @nombre and clave = @contrase�a ", sqlConn);
                cmdUsuarios.Parameters.Add("@nombre", SqlDbType.VarChar,50).Value = nombre;
                cmdUsuarios.Parameters.Add("@contrase�a", SqlDbType.VarChar, 50).Value = contrase�a;
                SqlDataReader drUsuarios = cmdUsuarios.ExecuteReader();
                if (drUsuarios.Read())
                {
                    usr.ID = (int)drUsuarios["id_usuario"];
                    usr.NombreUsuario = (string)drUsuarios["nombre_usuario"];
                    usr.Clave = (string)drUsuarios["clave"];
                    usr.Habilitado = (bool)drUsuarios["habilitado"];
                    usr.Nombre = (string)drUsuarios["nombre"];
                    usr.Apellido = (string)drUsuarios["apellido"];
                    usr.Email = (string)drUsuarios["email"];
                    usr.IdPersona = (int)drUsuarios["id_persona"];

                }
                else { usr = null; }
                drUsuarios.Close();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al recuperar datos de usuario", Ex);
            }
            finally
            {
                this.CloseConnection();
            }
            return usr;
        }

        public void Delete(int ID)
        {
            try
            {
                this.OpenConnection();
                SqlCommand cmdDelete =
                    new SqlCommand("delete usuarios where id_usuario=@id", sqlConn);
                cmdDelete.Parameters.Add("@id", SqlDbType.Int).Value = ID;
                cmdDelete.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al eliminar usuario", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
        }

        public void Save(Usuario usuario)
        {
            if (usuario.State == BusinessEntity.States.Deleted)
            {
                this.Delete(usuario.ID);
            }
            else if (usuario.State == BusinessEntity.States.New)
            {
                this.Insert(usuario);
            }
            else if (usuario.State == BusinessEntity.States.Modified)
            {
                this.Update(usuario);
            }
            usuario.State = BusinessEntity.States.Unmodified;
        }

        protected void Update(Usuario usuario)
        {
            try
            {
                this.OpenConnection();
                SqlCommand cmdSave = new SqlCommand(
                    "UPDATE usuarios SET nombre_usuario = @nombre_usuario, clave = @clave," +
                    "habilitado = @habilitado, nombre = @nombre, apellido = @apellido, email = @email, id_persona = @idpersona " +
                    "WHERE id_usuario = @id", sqlConn);

                cmdSave.Parameters.Add("@id", SqlDbType.Int).Value = usuario.ID;
                cmdSave.Parameters.Add("@nombre_usuario", SqlDbType.VarChar, 50).Value = usuario.NombreUsuario;
                cmdSave.Parameters.Add("@clave", SqlDbType.VarChar, 50).Value = usuario.Clave;
                cmdSave.Parameters.Add("@nombre", SqlDbType.VarChar, 50).Value = usuario.Nombre;
                cmdSave.Parameters.Add("apellido", SqlDbType.VarChar, 50).Value = usuario.Apellido;
                cmdSave.Parameters.Add("@email", SqlDbType.VarChar, 50).Value = usuario.Email;
                cmdSave.Parameters.Add("@habilitado", SqlDbType.Bit).Value = usuario.Habilitado;
                cmdSave.Parameters.Add("@idpersona", SqlDbType.Int).Value = usuario.IdPersona; 
                cmdSave.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al modificar los datos del usuario", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
        }

        public void ActualizarDatos(string nombre, string apellido, string mail, int id)
        {
            try
            {
                this.OpenConnection();
                SqlCommand cmdSave = new SqlCommand(
                    "UPDATE usuarios SET " +
                    "nombre = @nombre, apellido = @apellido, email = @email " +
                    "WHERE id_persona = @id", sqlConn);

                cmdSave.Parameters.Add("@id", SqlDbType.Int).Value = id;
                cmdSave.Parameters.Add("@nombre", SqlDbType.VarChar, 50).Value = nombre;
                cmdSave.Parameters.Add("apellido", SqlDbType.VarChar, 50).Value = apellido;
                cmdSave.Parameters.Add("@email", SqlDbType.VarChar, 50).Value = mail;
                cmdSave.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al modificar los datos del usuario", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
        }

        protected void Insert(Usuario usuario)
        {
            try
            {
                this.OpenConnection();
                SqlCommand cmdSave = new SqlCommand(
                    "insert into usuarios(nombre_usuario,clave,habilitado,nombre,apellido,email,id_persona)" +
                    "values(@nombre_usuario,@clave,@habilitado,@nombre,@apellido,@email,@idpersona)" +
                    "select @@identity", sqlConn);

                cmdSave.Parameters.Add("@nombre_usuario", SqlDbType.VarChar, 50).Value = usuario.NombreUsuario;
                cmdSave.Parameters.Add("@clave", SqlDbType.VarChar, 50).Value = usuario.Clave;
                cmdSave.Parameters.Add("@nombre", SqlDbType.VarChar, 50).Value = usuario.Nombre;
                cmdSave.Parameters.Add("apellido", SqlDbType.VarChar, 50).Value = usuario.Apellido;
                cmdSave.Parameters.Add("@email", SqlDbType.VarChar, 50).Value = usuario.Email;
                cmdSave.Parameters.Add("@habilitado", SqlDbType.Bit).Value = usuario.Habilitado;
                cmdSave.Parameters.Add("@idpersona", SqlDbType.Int).Value = usuario.IdPersona;
                usuario.ID = Decimal.ToInt32((decimal)cmdSave.ExecuteScalar());
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al crear el usuario", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
        }

        public Usuario ActualizarContrase�a(string us, string conActual, string conNueva)
        {
            Usuario usuario = new Usuario();
            usuario = ExisteUsuario(us, conActual);
            if(!(usuario is null))
            {
                try
                {
                    OpenConnection();
                    SqlCommand cmdAct = new SqlCommand("update usuarios set clave = @clave_nueva, cambia_clave = 1 " + // ver cambia clave
                                                        "where id_usuario = @id", sqlConn);

                    cmdAct.Parameters.Add("@clave_nueva", SqlDbType.VarChar, 50).Value = conNueva;
                    cmdAct.Parameters.Add("@id", SqlDbType.Int).Value = usuario.ID;
                    cmdAct.ExecuteNonQuery();
                }
                catch
                {
                    Exception ExcepcionManejada = new Exception("Error al modificar la contrase�a");
                    throw ExcepcionManejada;
                }
                finally
                {
                    CloseConnection();
                }
            }
            return usuario;
        }

    }
}
