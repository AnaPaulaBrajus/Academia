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
    public class DocenteCursoLogic
    {
        private Data.Database.DocenteCursoAdapter _DocenteCursoData = new Data.Database.DocenteCursoAdapter();

        public Data.Database.DocenteCursoAdapter DocenteCursoData {
            get { return _DocenteCursoData; }
            set { _DocenteCursoData = value; }
        }

        public Business.Entities.DocenteCurso GetOne(int ID)
        {
            return DocenteCursoData.GetOne(ID);
        }

        public List<Business.Entities.DocenteCurso> GetAll()
        {
            return DocenteCursoData.GetAll();
        }

        public void Delete(int ID)
        {
            DocenteCursoData.Delete(ID);
        }

        public void Save(Business.Entities.DocenteCurso d)
        {
            DocenteCursoData.Save(d);
        }

        public void Update(Business.Entities.DocenteCurso d)
        {
            DocenteCursoData.Update(d);
        }

        public DataTable GetCursos()
        {
            return DocenteCursoData.GetCursos();
        }

        public DataTable GetCursosDocente(int id)
        {
            return DocenteCursoData.GetCursosDocente(id);
        }

        public DataTable GetDocentes()
        {
            return DocenteCursoData.GetDocentes();
        }
    }
}