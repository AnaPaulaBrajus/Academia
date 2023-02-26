using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Business.Logic
{
    public static class ValidarLogic
    {
        public static bool esMailValido(string email)
        {
            string expresion;
            expresion = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, string.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static bool EsConfirmacionValida(string contraseña, string confirmacion)
        {
            if (contraseña == confirmacion)
            {
                return true;
            }
            else return false;
        } 

        public static bool ValidarCantCaractContraseña(string con)
        {
            if (con.Length >= 8)
            {
                return true;
            }
            else return false;
        }

        public static bool esEntero(string s)
        {
            int nro;
            if (Int32.TryParse(s, out nro))
            {
                return true;
            }
            else return false;
        }

        public static bool EstaEntreUnoYDiez(int n)
        {
            if (n <= 10 & n >= 0) return true;
            else return false;
        }
    }
}
