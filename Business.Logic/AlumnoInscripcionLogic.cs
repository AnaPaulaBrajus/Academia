using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Entities;
using Business;
using System.Data;

namespace Business.Logic
{
    public class AlumnoInscripcionLogic
    {
        private Data.Database.AlumnoInscripcionAdapter _AlumnoInscripcionData = new Data.Database.AlumnoInscripcionAdapter();

        public Data.Database.AlumnoInscripcionAdapter AlumnoInscripcionData
        {
            get { return _AlumnoInscripcionData; }
            set { _AlumnoInscripcionData = value; }
        }

        public Business.Entities.AlumnoInscripcion GetOne(int ID)
        {
            return AlumnoInscripcionData.GetOne(ID);
        }

        public List<AlumnoInscripcion> GetInscAlumno(int ID)
        {
            return AlumnoInscripcionData.GetInscAlumno(ID);
        }
        public List<Business.Entities.AlumnoInscripcion> GetAll()
        {
            return AlumnoInscripcionData.GetAll();
        }

        public void Delete(int ID)
        {
            AlumnoInscripcionData.Delete(ID);
        }

        public void Save(Business.Entities.AlumnoInscripcion alumins)
        {
            AlumnoInscripcionData.Save(alumins);
        }

        public void Update(Business.Entities.AlumnoInscripcion alumins)
        {
            AlumnoInscripcionData.Update(alumins);
        }

        public DataTable GetCursos(int id)
        {
            return AlumnoInscripcionData.GetCursos(id);
        }

        public DataTable GetAlumnos()
        {
            return AlumnoInscripcionData.GetAlumnos();
        }

        public List<AlumnoInscripcion> GetAlumnosCurso(int id)
        {
            return AlumnoInscripcionData.GetAlumnosCurso(id);
        }
    }
}
