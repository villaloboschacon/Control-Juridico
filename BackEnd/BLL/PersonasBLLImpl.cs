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
        private SCJ_BDEntities context;

        public bool Comprobar(string cedula, string idPersona)
        {
            int id = 0;
            try
            {
                id = Int32.Parse(idPersona);
            }
            catch (Exception)
            {

            }
            try
            {
                List<Persona> lista;
                    using (unidad = new UnidadDeTrabajo<Persona>(new SCJ_BDEntities()))
                    {
                        Expression<Func<Persona, bool>> consulta = (d => d.cedula.Equals(cedula) && d.idPersona.Equals(id));
                        lista = unidad.genericDAL.Find(consulta).ToList();
                        if (lista.Count() == 1)
                        {
                            return true;
                        }
                        else
                        {
                            consulta = (d => d.cedula.Equals(cedula));
                            lista = unidad.genericDAL.Find(consulta).ToList();
                            if (lista.Count() == 0)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
            }
            catch (Exception ex)
            {
                throw new NotImplementedException();
            }
        }
        public bool SaveChanges()
        {
            using (unidad = new UnidadDeTrabajo<Persona>(new SCJ_BDEntities()))
            {
                this.unidad.Complete();
                return true;
            }
        }
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
        public List<Persona> Consulta(int idtipo)
        {
            try
            {
                List<Persona> listapersonas;
                using (unidad = new UnidadDeTrabajo<Persona>(new SCJ_BDEntities()))
                {
                    Expression<Func<Persona, bool>> consulta = (d => d.idTipo.Equals(idtipo));
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

        public List<Persona> buscaPorNombre(string filtro)
        {
            try
            {
                List<Persona> listapersonas;
                using (context = new SCJ_BDEntities())
                {
                    listapersonas = context.SP_BuscaPersonaNombre(filtro).ToList();
                    return listapersonas;
                }             
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }

        public List<Persona> buscarNombre(string filtro)
        {
            try
            {
                List<Persona> listapersonas;
                using (context = new SCJ_BDEntities())
                {
                    listapersonas = context.Personas.Where(x => x.nombreCompleto.Contains(filtro) || filtro == null).ToList(); 
                    return listapersonas;
                }
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }

        public bool Eliminar(Persona persona)
        {
               this.Remove(persona);
                return true;
        }
    }
}
