using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BackEnd.Model;
using BackEnd.DAL;
namespace BackEnd.BLL
{

    public class PersonasBLLImpl:BLLGenericoImpl<Persona>, IPersonasBLL
    {
        private UnidadDeTrabajo<Persona> unidad;

        public bool Agregar(Persona oPersona)
        {
            try
            {
                return Add(oPersona);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Actualizar(Persona oPersona)
        {
            try
            {
                return Update(oPersona);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Eliminar(Persona oPersona)
        {
            try
            {
                return Remove(oPersona);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Persona> Consulta(int iTipo)
        {
            try
            {
                using (unidad = new UnidadDeTrabajo<Persona>(new SCJ_BDEntities()))
                {
                    Expression<Func<Persona, bool>> consulta = (oPersona => oPersona.idTipo.Equals(iTipo));
                    return unidad.genericDAL.Find(consulta).ToList();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Persona> Consulta(int iTipo, string sFiltro, string sCampo)
        {
            try
            {
                using (unidad = new UnidadDeTrabajo<Persona>(new SCJ_BDEntities()))
                {
                    switch (sCampo)
                    {                      
                        case "Cédula":
                            Expression<Func<Persona, bool>> consultaCedula = (oPersona => oPersona.idTipo.Equals(iTipo) && oPersona.cedula.Contains(sFiltro));
                            return unidad.genericDAL.Find(consultaCedula).ToList();
                        case "Nombre Completo":
                            Expression<Func<Persona, bool>> consultaNombre = (oPersona => oPersona.idTipo.Equals(iTipo) && oPersona.nombreCompleto.Contains(sFiltro));
                            return unidad.genericDAL.Find(consultaNombre).ToList();
                        case "Correo Electrónico":
                            Expression<Func<Persona, bool>> consultaCorreo = (oPersona => oPersona.idTipo.Equals(iTipo) && oPersona.correo.Contains(sFiltro));
                            return unidad.genericDAL.Find(consultaCorreo).ToList();
                        default:
                            Expression<Func<Persona, bool>> consultaDefault = (oPersona => oPersona.idTipo.Equals(iTipo));
                            return unidad.genericDAL.Find(consultaDefault).ToList();
                    }     
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Persona> GetPersonas()
        {
            try
            {
                using (unidad = new UnidadDeTrabajo<Persona>(new SCJ_BDEntities()))
                {
                    return unidad.genericDAL.GetAll().ToList();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        public Persona GetPersona(int iId)
        {
            try
            {
                using (unidad = new UnidadDeTrabajo<Persona>(new SCJ_BDEntities()))
                {
                    return unidad.genericDAL.Get(iId);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        public bool Comprobar(string sCedula, string sIdPersona)
        {
            try
            {
                List<Persona> personas;
                int iIdPersona = Int32.Parse(sIdPersona);
                using (unidad = new UnidadDeTrabajo<Persona>(new SCJ_BDEntities()))
                {
                    Expression<Func<Persona, bool>> consulta = (d => d.cedula.Equals(sCedula) && d.idPersona.Equals(iIdPersona));
                    personas = unidad.genericDAL.Find(consulta).ToList();
                    if (personas.Count() == 1)
                    {
                        return true;
                    }
                    else
                    {
                        consulta = (d => d.cedula.Equals(sCedula));
                        personas = unidad.genericDAL.Find(consulta).ToList();
                        if (personas.Count() == 0)
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
            catch (Exception)
            {
                return false;
            }
        }

        public bool SaveChanges()
        {
            try
            {
                using (unidad = new UnidadDeTrabajo<Persona>(new SCJ_BDEntities()))
                {
                    this.unidad.Complete();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        

    }
}
