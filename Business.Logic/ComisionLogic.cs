using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Database;
using System.Data;
using Business.Entities;

namespace Business.Logic
{
    public class ComisionLogic : BusinessLogic
    {
        private ComisionAdapter _comisionData;

        public ComisionAdapter ComisionData
        {
            get { return _comisionData; }
            set { _comisionData = value; }
        }

        public ComisionLogic()
        {
            ComisionData = new ComisionAdapter();
        }

        public Comision GetOne(int id)
        {
            return ComisionData.GetOne(id);
        }

        public List<Comision> GetAll()
        {
            return ComisionData.GetAll();
        }

        public void Delete(int id)
        {
            ComisionData.Delete(id);
        }

        public void Save(Comision com)
        {
            ComisionData.Save(com);
        }

        public DataTable GetComisiones()
        {
            return ComisionData.GetComisiones();
        }
    }
}
