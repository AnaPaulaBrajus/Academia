using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Entities
{
    public class AlumnoInscripcion : BusinessEntity
    {
        private string condicion;
        private int idAlumno;
        private int idCurso;
        private int nota;

        public string Condicion
        {
            get
            {
                return condicion;
            }
            set
            {
                condicion = value;
            }
        }

        public int IdAlumno
        {
            get
            {
                return idAlumno;
            }
            set
            {
                idAlumno = value;
            }
        }

        public int IdCurso
        {
            get
            {
                return idCurso;
            }
            set
            {
                idCurso = value;
            }
        }

        public int Nota
        {
            get
            {
                return nota;
            }
            set
            {
                nota = value;
            }
        }
    }
}