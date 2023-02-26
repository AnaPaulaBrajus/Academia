using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entities
{
    public class Personas : BusinessEntity
    {
        private string apellido;
        private string nombre;
        private string direccion;
        private string email;
        private DateTime fechaNacimiento;
        private int idPlan;
        private int legajo;
        private string telefono;
        private TipoPersona tipoPersona;

        public string Apellido
        {
            get
            {
                return apellido;
            }
            set
            {
                apellido = value;
            }
        }

        public string Direccion
        {
            get
            {
                return direccion;
            }
            set
            {
                direccion = value;
            }
        }

        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
            }
        }

        public DateTime FechaNacimiento
        {
            get
            {
                return fechaNacimiento;
            }
            set
            {
                fechaNacimiento = value;
            }
        }

        public int IdPlan
        {
            get
            {
                return idPlan;
            }
            set
            {
                idPlan = value;
            }
        }

        public int Legajo
        {
            get
            {
                return legajo;
            }
            set
            {
                legajo = value;
            }
        }

        public string Nombre
        {
            get
            {
                return nombre;
            }
            set
            {
                nombre = value;
            }
        }

        public string Telefono
        {
            get
            {
                return telefono;
            }
            set
            {
                telefono = value;
            }
        }

        public enum TipoPersona
        {
            Alumno,
            Docente,
            Admin
        }
        public TipoPersona TPersona
        {
            get
            {
                return tipoPersona;
            }
            set
            {
                tipoPersona = value;
            }
        }
    }
}
