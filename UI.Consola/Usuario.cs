using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Entities;
using Business.Logic;

namespace UI.Consola
{
    public class Usuario
    {
        UsuarioLogic _usuarioNegocio;
        public UsuarioLogic UsuarioNegocio
        {
            get { return _usuarioNegocio; }
            set { _usuarioNegocio = value; }
        }

        public Usuario()
        {
            UsuarioNegocio = new UsuarioLogic();
        }

        public void Menu()
        {
            ConsoleKeyInfo opcion;
            do
            {
                System.Console.WriteLine("Menu de opciones: 1– Listado General 2– Consulta 3– Agregar 4 - Modificar 5 - Eliminar 6 - Salir");
                opcion = Console.ReadKey();
                switch (opcion.Key)
                {
                    case ConsoleKey.NumPad1:
                        ListadoGeneral();
                        break;
                    case ConsoleKey.NumPad2:
                        Consultar();
                        break;
                    case ConsoleKey.NumPad3:
                        Agregar();
                        break;
                    case ConsoleKey.NumPad4:
                        Modificar();
                        break;
                    case ConsoleKey.NumPad5:
                        Eliminar();
                        break;
                    case ConsoleKey.NumPad6:
                        Console.WriteLine("Fin");
                        break;
                }
            } while (opcion.Key != ConsoleKey.NumPad6);
            
        }

        public void ListadoGeneral()
        {
            Console.Clear();
            foreach (Business.Entities.Usuario usr in UsuarioNegocio.GetAll()) 
            {
                MostrarDatos(usr);
            }
        }
        //consultar Business.Entities

        public void Consultar()
        {
            try
            {
                Console.Clear();
                Console.Write("Ingrese el ID del usuario a consultar:");
                int ID = int.Parse(Console.ReadLine());
                MostrarDatos(UsuarioNegocio.GetOne(ID));
            }
            catch (FormatException fe)
            {
                Console.WriteLine();
                Console.WriteLine("La ID ingresada debe ser un numero entero"+ fe.Message);            }
            catch (Exception e)
            {
                Console.WriteLine();
                Console.WriteLine(e.Message);  
            }
            finally
            {
                Console.WriteLine("Presione una tecla para continuar");
                Console.ReadKey();
            }
        }

        public void Agregar()
        {
            Business.Entities.Usuario usuario = new Business.Entities.Usuario();

            Console.Clear();
            Console.Write("Ingrese Nombre: ");
            usuario.Nombre = Console.ReadLine();
            Console.Write("Ingrese Apellido: ");
            usuario.Apellido = Console.ReadLine();
            Console.Write("Ingrese Nombre de Usuario: ");
            usuario.NombreUsuario = Console.ReadLine();
            Console.Write("Ingrese Clave: ");
            usuario.Clave = Console.ReadLine();
            Console.Write("Ingrese Email: ");
            usuario.Email = Console.ReadLine();
            Console.Write("Ingrese Habilitacion de Usuario (1-Si/otro-No): ");
            usuario.Habilitado = (Console.ReadLine() == "1");
            usuario.State = BusinessEntity.States.New;
            UsuarioNegocio.Save(usuario);
            Console.WriteLine();
            Console.WriteLine("ID. {0}", usuario.ID);

        }

        public void Modificar()
        {
            try
            {
                Console.Clear();
                Console.Write("Ingrese el ID del usuario a modificar: ");
                int ID = int.Parse(Console.ReadLine());
                Business.Entities.Usuario usuario = UsuarioNegocio.GetOne(ID);
                Console.Write("Ingrese Nombre: ");
                usuario.Nombre = Console.ReadLine();
                Console.Write("Ingrese Apellido: ");
                usuario.Apellido = Console.ReadLine();
                Console.Write("Ingrese Nombre de Usuario: ");
                usuario.NombreUsuario = Console.ReadLine();
                Console.Write("Ingrese Clave: ");
                usuario.Clave = Console.ReadLine();
                Console.Write("Ingrese Email: ");
                usuario.Email = Console.ReadLine();
                Console.Write("Ingrese Habilitacion del Usuario (1-Si/otro-No): ");
                usuario.Habilitado= (Console.ReadLine() == "1");
                usuario.State = BusinessEntity.States.Modified;
                UsuarioNegocio.Save(usuario);
            }

            catch (FormatException)
            {
                Console.WriteLine();
                Console.WriteLine("La ID ingresada debe ser un numero entero");
            }

            catch (Exception e)
            {
                Console.WriteLine();
                Console.WriteLine(e.Message);
            }

            finally
            {
                Console.WriteLine("Presione una tecla para continuar");
                Console.ReadKey();
            }
        }

        public void Eliminar()
        {
            try
            {
                Console.Clear();
                Console.Write("Ingrese el ID del usuario a eliminar: ");
                int ID = int.Parse(Console.ReadLine());
                UsuarioNegocio.Delete(ID);
            }

            catch (FormatException)
            {
                Console.WriteLine();
                Console.WriteLine("La ID ingresada debe ser un numero entero");
            }

            catch (Exception e)
            {
                Console.WriteLine();
                Console.WriteLine(e.Message);
            }

            finally
            {
                Console.WriteLine("Presione una tecla para continuar");
                Console.ReadKey();
            }
        }

        public void MostrarDatos(Business.Entities.Usuario usr)
        {
            Console.WriteLine("Usuario: {0}", usr.ID);
            Console.WriteLine("\t\tApellido: {0}", usr.Apellido);
            Console.WriteLine("\t\tNombre: {0}", usr.Nombre);
            Console.WriteLine("\t\tNombre de usuario: {0}", usr.NombreUsuario);
            Console.WriteLine("\t\tClave: {0}", usr.Clave);
            Console.WriteLine("\t\tEmail: {0}", usr.Email);
            Console.WriteLine("\t\tHabilitado: {0}", usr.Habilitado);
            Console.WriteLine();
        }
    }
}
