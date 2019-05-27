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
        private SCJ_BDEntities context;

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

        public List<Persona> buscaPorIdentificacion(string cedula, int iTipo)
        {
            try
            {
                using (context = new SCJ_BDEntities())
                {
                    var result = this.context.sp_buscaPersonaPorIdentificacion(cedula, iTipo).ToList();
                    if (result != null)
                    {
                        return result;
                    }
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Persona> buscaPorNombreCompleto(string nombre, int iTipo)
        {
            try
            {
                using (context = new SCJ_BDEntities())
                {
                    var result = this.context.sp_buscaPersonaPorNombreCompleto(nombre, iTipo).ToList();
                    if (result != null)
                    {
                        return result;
                    }
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Persona> buscaPorRepresentanteSocial(string nombRepresentante)
        {
            try
            {
                using (context = new SCJ_BDEntities())
                {
                    var result = this.context.sp_buscaPersonaPorRepresentanteSocial(nombRepresentante).ToList();
                    if (result != null)
                    {
                        return result;
                    }
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Persona> buscaPorRepresentanteLegal(string nombRepresentante)
        {
            try
            {
                using (context = new SCJ_BDEntities())
                {
                    var result = this.context.sp_buscaPersonaPorRepresentanteLegal(nombRepresentante).ToList();
                    if (result != null)
                    {
                        return result;
                    }
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Persona> buscaPorCorreo(string correo, int iTipo)
        {
            try
            {
                using (context = new SCJ_BDEntities())
                {
                    var result = this.context.sp_buscaPersonaPorCorreo(correo, iTipo).ToList();
                    if (result != null)
                    {
                        return result;
                    }
                    return null;
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
                        case "Cédula Jurídica":
                            return buscaPorIdentificacion(sFiltro, iTipo).ToList();
                        case "Nombre Completo":
                            return buscaPorNombreCompleto(sFiltro, iTipo).ToList();
                        case "Correo Electrónico":
                            return buscaPorCorreo(sFiltro, iTipo).ToList();
                        case "Representante Legal":
                            return buscaPorRepresentanteLegal(sFiltro).ToList();
                        case "Representante Social":
                            return buscaPorRepresentanteSocial(sFiltro).ToList();
                        default:
                            return getPersonas(iTipo);
                    }
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
            using (unidad = new UnidadDeTrabajo<Persona>(new SCJ_BDEntities()))
            {
                this.unidad.Complete();
                return true;
            }
        }
        
        public bool Agregar(Persona persona)
        {
            if (persona != null)
            {
                return Add(persona);
            }
            else
            {
                return false;
            }
        }

        public bool Actualizar(Persona persona)
        {
            return Update(persona);
        }

        public bool Eliminar(Persona persona)
        {
            return Remove(persona);
        }

        public List<Persona> getPersonas(int iTipo)
        {
            try
            {
                using (context = new SCJ_BDEntities())
                {
                    var result = this.context.sp_listaPersonas(iTipo).ToList();
                    if (result != null)
                    {
                        return result;
                    }
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
