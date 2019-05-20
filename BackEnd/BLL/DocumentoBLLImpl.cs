using BackEnd.DAL;
using BackEnd.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.BLL
{
    public class DocumentoBLLImpl : BLLGenericoImpl<Documento>, IDocumentoBLL
    {
        private UnidadDeTrabajo<Documento> unidad;
        private SCJ_BDEntities context;

        public bool Agregar(Documento oDocumento)
        {
            try
            {
                return Add(oDocumento);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Actualizar(Documento oDocumento)
        {
            try
            {
                return Update(oDocumento);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Eliminar(Documento oDocumento)
        {
            try
            {
                return Remove(oDocumento);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Documento> Consulta(int iTipo, string sFiltro, string sFechaFinal, string sCampo, Boolean bIngreso)
        {
            try
            {
                using (unidad = new UnidadDeTrabajo<Documento>(new SCJ_BDEntities()))
                {
                    ITablaGeneralBLL oTablaGeneralBLL = new TablaGeneralBLLImpl();
                    int iEstado = oTablaGeneralBLL.GetIdTablaGeneral("Documentos", "Estado", "Archivado");
                    if (bIngreso)
                    {
                        switch (sCampo)
                        {
                            case "Número de Oficio":
                                Expression<Func<Documento, bool>> consultaNumeroOficio = (oDocumento => oDocumento.idTipo.Equals(iTipo) && oDocumento.numeroDocumento.Contains(sFiltro) && oDocumento.idEstado != (iEstado) && oDocumento.numeroIngreso != null);
                                return unidad.genericDAL.Find(consultaNumeroOficio).ToList();
                            case "Número de Ingreso":
                                Expression<Func<Documento, bool>> consultaNumeroIngreso = (oDocumento => oDocumento.idTipo.Equals(iTipo) && oDocumento.numeroIngreso.Contains(sFiltro) && oDocumento.idEstado != (iEstado) && oDocumento.numeroIngreso != null);
                                return unidad.genericDAL.Find(consultaNumeroIngreso).ToList();
                            case "Fecha":
                                DateTime date = Convert.ToDateTime(sFiltro);
                                DateTime dateFinal = Convert.ToDateTime(sFechaFinal);
                                Expression<Func<Documento, bool>> consultaFecha = (oDocumento => oDocumento.idTipo.Equals(iTipo) && (oDocumento.fecha >= date && oDocumento.fecha <= dateFinal) && oDocumento.idEstado != (iEstado) && oDocumento.numeroIngreso != null);
                                return unidad.genericDAL.Find(consultaFecha).ToList();
                            default:
                                Expression<Func<Documento, bool>> consultaDefault = (oDocumento => oDocumento.idTipo.Equals(iTipo) && oDocumento.idEstado != (iEstado) && oDocumento.numeroIngreso != null);
                                return unidad.genericDAL.Find(consultaDefault).ToList();
                        }
                    }
                    else {
                        switch (sCampo)
                        {
                            case "Número de Oficio":
                                Expression<Func<Documento, bool>> consultaNumeroOficio = (oDocumento => oDocumento.idTipo.Equals(iTipo) && oDocumento.numeroDocumento.Contains(sFiltro) && oDocumento.idEstado != (iEstado) && oDocumento.numeroIngreso == null);
                                return unidad.genericDAL.Find(consultaNumeroOficio).ToList();
                            case "Número de Ingreso":
                                Expression<Func<Documento, bool>> consultaNumeroIngreso = (oDocumento => oDocumento.idTipo.Equals(iTipo) && oDocumento.numeroIngreso.Contains(sFiltro) && oDocumento.idEstado != (iEstado) && oDocumento.numeroIngreso == null);
                                return unidad.genericDAL.Find(consultaNumeroIngreso).ToList();
                            case "Fecha":
                                DateTime date = Convert.ToDateTime(sFiltro);
                                DateTime dateFinal = Convert.ToDateTime(sFechaFinal);
                                Expression<Func<Documento, bool>> consultaFecha = (oDocumento => oDocumento.idTipo.Equals(iTipo) && (oDocumento.fecha >= date && oDocumento.fecha <= dateFinal) && oDocumento.idEstado != (iEstado) && oDocumento.numeroIngreso == null);
                                return unidad.genericDAL.Find(consultaFecha).ToList();
                            default:
                                Expression<Func<Documento, bool>> consultaDefault = (oDocumento => oDocumento.idTipo.Equals(iTipo) && oDocumento.idEstado != (iEstado) && oDocumento.numeroIngreso == null);
                                return unidad.genericDAL.Find(consultaDefault).ToList();
                        }
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
        }


        public bool ArchivarDocumento(int iIdDocumento)
        {
            try
            {
                using (context = new SCJ_BDEntities())
                {
                    this.context.sp_archivaDocumento(iIdDocumento);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Comprobar(string sNumeroDocumento, int iOpcion,string sIdDocumento)
        {
            try
            {
                using (unidad = new UnidadDeTrabajo<Documento>(new SCJ_BDEntities()))
                {
                    int iId = Int32.Parse(sIdDocumento);
                    Expression<Func<Documento, bool>> eConsulta = (oDocumento => oDocumento.numeroDocumento.Equals(sNumeroDocumento) && oDocumento.idDocumento.Equals(iId));
                    List<Documento> lista = unidad.genericDAL.Find(eConsulta).ToList();
                    if (iOpcion == 1)
                    {
                        if (lista.Count() == 1)
                        {
                            return true;
                        }
                        else
                        {
                            eConsulta = (oDocumento => oDocumento.numeroDocumento.Equals(sNumeroDocumento));
                            lista = unidad.genericDAL.Find(eConsulta).ToList();
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
                    else
                    {
                        if (lista.Count() == 1)
                        {
                            return true;
                        }
                        else
                        {
                            eConsulta = (oDocumento => oDocumento.numeroIngreso.Equals(sNumeroDocumento));
                            lista = unidad.genericDAL.Find(eConsulta).ToList();
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
                using (unidad = new UnidadDeTrabajo<Documento>(new SCJ_BDEntities()))
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


        public List<Documento> GetSalidas()
        {
            try
            {
                using (context = new SCJ_BDEntities())
                {
                    var result = this.context.sp_listaSalidas().ToList();
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

        public List<Documento> GetEntradas()
        {
            try
            {
                using (context = new SCJ_BDEntities())
                {
                    var result = this.context.sp_listaEntradas().ToList();
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

        public List<Documento> GetReferencias(long? referencia)
        {
            try
            {
                using (context = new SCJ_BDEntities())
                {
                    var result = context.sp_ListaReferencias(referencia).ToList();
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

        public Nullable<long> GetNumeroIngreso()
        {
            try
            {
                using (context = new SCJ_BDEntities())
                {
                   return context.sp_ConsultaNumerodeIngreso().FirstOrDefault();
                }          
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Nullable<long> GeneraNumeroIngreso()
        {
            try
            {
                using (context = new SCJ_BDEntities())
                 {
                     return context.sp_GeneraNumerodeIngreso().FirstOrDefault();
                 }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string GetNomenclatura(string sNombreDepartamento)
        {
            try
            {
                using (context = new SCJ_BDEntities())
                {
                    return context.getNomenclatura(sNombreDepartamento).FirstOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public long? GeneraNumeroReferencia()
        {
            try
            {
                using (context = new SCJ_BDEntities())
                {
                    return this.context.sp_GeneraNumerodeReferencia().FirstOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Documento GetDocumento(int iId)
        {
            try
            {
                using (unidad = new UnidadDeTrabajo<Documento>(new SCJ_BDEntities()))
                {
                    return unidad.genericDAL.Get(iId);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
