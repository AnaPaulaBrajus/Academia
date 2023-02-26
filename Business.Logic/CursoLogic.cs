using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Database;
using Business.Entities;

namespace Business.Logic
{
    public class CursoLogic : BusinessLogic
    {
        private CursoAdapter _cursoAdapter;

        public CursoAdapter CursoData
        {
            get { return _cursoAdapter; }
            set { _cursoAdapter = value; }
        }

        public CursoLogic()
        {
            CursoData = new CursoAdapter();
        }
        public Curso GetOne(int id)
        {
            return CursoData.GetOne(id);
        }

        public List<Curso> GetAll()
        {
            return CursoData.GetAll();
        }

        public void Save(Curso cur)
        {
            CursoData.Save(cur);
        }

        public void Delete(int id)
        {
            CursoData.Delete(id);
        }

        public void ActualizarCupo(Curso curso, int cant)
        {
            CursoData.ActualizarCupo(curso, cant);
        }
    }
}
