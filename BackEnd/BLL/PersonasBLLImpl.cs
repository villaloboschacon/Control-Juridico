using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BackEnd.Model;
using BackEnd.DAL;
namespace BackEnd.BLL
{
    public class PersonasBLLImpl:BLLGenericoImpl<Persona>, IPersonasBLL
    {
        private UnidadDeTrabajo<Persona> unidad;

        public List<Persona> getModel()
        {
            try
            {
                List<Persona> listapersonas;
                using (unidad = new UnidadDeTrabajo<Persona>(new SCJ_BDEntities()))
                {
                    listapersonas = unidad.genericDAL.GetAll().ToList();
                }
                return listapersonas;
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }
        public bool Agregar(Persona persona)
        {
            if (persona != null){
                this.Add(persona);
                return true;
            }
            else
            {
                return false;
            }
        }
        public List<Persona> ConsultaPorNombre(string nombre)
        {
            try
            {
                List<Persona> listapersonas;
                using (unidad = new UnidadDeTrabajo<Persona>(new SCJ_BDEntities()))
                {
                    Expression<Func<Persona, bool>> consulta = (d => d.nombreCompleto.Contains(nombre));
                    listapersonas = unidad.genericDAL.Find(consulta).ToList();
                }
                return listapersonas;
            }
            catch(Exception)
            {
                throw new NotImplementedException();
            }
        }

        public bool Modificar(Persona persona)
        {
            this.Update(persona);
            return true;
        }
    }
}
