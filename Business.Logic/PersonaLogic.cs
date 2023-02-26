using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Database;
using Business.Entities;
using System.Data;

namespace Business.Logic
{
    public class PersonaLogic: BusinessLogic
    {
        private PersonaAdapter _personaData;

        public PersonaAdapter PersonaData
        {
            get
            {
                return _personaData;
            }
            set
            {
                _personaData = value;
            }
        }

        public PersonaLogic()
        {
            PersonaData = new PersonaAdapter();
        }

        public Personas GetOne(int id)
        {
            return PersonaData.GetOne(id);
        }

        public Personas GetLast()
        {
            return PersonaData.GetLast();
        }


        public List<Personas> GetAll()
        {
            return PersonaData.GetAll();
        }

        public void Delete(int id)
        {
            PersonaData.Delete(id);
        }

        public void Save(Personas per)
        {
            PersonaData.Save(per);
        }

        public Array GetTiposPersona()
        {
            return Enum.GetValues(typeof(Personas.TipoPersona));
        }

        public DataTable GetDocentes()
        {
            return PersonaData.GetDocentes();
        }
    }
}
