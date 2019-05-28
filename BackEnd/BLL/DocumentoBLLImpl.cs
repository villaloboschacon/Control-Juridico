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
       
        public List<Documento> Consulta(int iTipo, string sFiltro, string sFechaFinal, string sCampo, string sTipoDocumento)
        {
            try
            {
                using (unidad = new UnidadDeTrabajo<Documento>(new SCJ_BDEntities()))
                {
                    ITablaGeneralBLL oTablaGeneralBLL = new TablaGeneralBLLImpl();
                    int iEstado = oTablaGeneralBLL.GetIdTablaGeneral("Documentos", "Estado", "Archivado");
                    if (sTipoDocumento == "OficioEntrada")
                    {
                        switch (sCampo)
                        {
                            case "Número de Oficio":
                                return buscaEntradaPorNumeroDeOficio(sFiltro, iTipo);
                                //Expression<Func<Documento, bool>> consultaNumeroOficio = (oDocumento => oDocumento.idTipo.Equals(iTipo) && oDocumento.numeroDocumento.Contains(sFiltro) && oDocumento.idEstado != (iEstado) && oDocumento.numeroIngreso != null);
                                //return unidad.genericDAL.Find(consultaNumeroOficio).ToList();
                            case "Número de Ingreso":
                                return buscaEntradaPorNumeroDeIngreso(sFiltro, iTipo);
                            //Expression<Func<Documento, bool>> consultaNumeroIngreso = (oDocumento => oDocumento.idTipo.Equals(iTipo) && oDocumento.numeroIngreso.Contains(sFiltro) && oDocumento.idEstado != (iEstado) && oDocumento.numeroIngreso != null);
                            //return unidad.genericDAL.Find(consultaNumeroIngreso).ToList();
                            case "Fecha":
                                DateTime date = Convert.ToDateTime(sFiltro);
                                DateTime dateFinal = Convert.ToDateTime(sFechaFinal);
                                return buscaEntradaPorRangoDeFechas(date, dateFinal, iTipo);
                                //Expression<Func<Documento, bool>> consultaFecha = (oDocumento => oDocumento.idTipo.Equals(iTipo) && (oDocumento.fecha >= date && oDocumento.fecha <= dateFinal) && oDocumento.idEstado != (iEstado) && oDocumento.numeroIngreso != null);
                                //return unidad.genericDAL.Find(consultaFecha).ToList();
                            default:
                                return GetEntradas(iTipo);
                                //Expression<Func<Documento, bool>> consultaDefault = (oDocumento => oDocumento.idTipo.Equals(iTipo) && oDocumento.idEstado != (iEstado) && oDocumento.numeroIngreso != null);
                                //return unidad.genericDAL.Find(consultaDefault).ToList();
                        }
                    }
                    else if(sTipoDocumento == "OficioSalida")
                    {
                        switch (sCampo)
                        {
                            case "Número de Oficio":
                                return buscaSalidaPorNumeroDeOficio(sFiltro, iTipo);
                                //Expression<Func<Documento, bool>> consultaNumeroOficio = (oDocumento => oDocumento.idTipo.Equals(iTipo) && oDocumento.numeroDocumento.Contains(sFiltro) && oDocumento.idEstado != (iEstado) && oDocumento.numeroIngreso == null);
                                //return unidad.genericDAL.Find(consultaNumeroOficio).ToList();
                            case "Fecha":
                                DateTime date = Convert.ToDateTime(sFiltro);
                                DateTime dateFinal = Convert.ToDateTime(sFechaFinal);
                                return buscaSalidaPorRangoDeFechas(date, dateFinal, iTipo);
                                //Expression<Func<Documento, bool>> consultaFecha = (oDocumento => oDocumento.idTipo.Equals(iTipo) && (oDocumento.fecha >= date && oDocumento.fecha <= dateFinal) && oDocumento.idEstado != (iEstado) && oDocumento.numeroIngreso == null);
                                //return unidad.genericDAL.Find(consultaFecha).ToList();
                            default:
                                return GetSalidas(iTipo);
                                //Expression<Func<Documento, bool>> consultaDefault = (oDocumento => oDocumento.idTipo.Equals(iTipo) && oDocumento.idEstado != (iEstado) && oDocumento.numeroIngreso == null);
                                //return unidad.genericDAL.Find(consultaDefault).ToList();
                        }
                    }
                    else if (sTipoDocumento == "Expediente")
                    {
                        switch (sCampo)
                        {
                            case "Número de Oficio":
                                return buscaExpedientePorNumeroDeOficio(sFiltro, iTipo);
                            //Expression<Func<Documento, bool>> consultaNumeroOficio = (oDocumento => oDocumento.idTipo.Equals(iTipo) && oDocumento.numeroDocumento.Contains(sFiltro) && oDocumento.idEstado != (iEstado));
                            //return unidad.genericDAL.Find(consultaNumeroOficio).ToList();
                            case "Número de Ingreso":
                                return buscaExpedientePorNumeroDeIngreso(sFiltro, iTipo);
                            ////Expression<Func<Documento, bool>> consultaNumeroIngreso = (oDocumento => oDocumento.idTipo.Equals(iTipo) && oDocumento.numeroIngreso.Contains(sFiltro) && oDocumento.idEstado != (iEstado) );
                            ////return unidad.genericDAL.Find(consultaNumeroIngreso).ToList();
                            case "Parte":
                                return buscaExpedientePorParte(sFiltro, iTipo);
                            //Expression<Func<Documento, bool>> consultaParte = (oDocumento => oDocumento.idTipo.Equals(iTipo) && oDocumento.parte.Contains(sFiltro) && oDocumento.idEstado != (iEstado));
                            //return unidad.genericDAL.Find(consultaParte).ToList();
                            case "Fecha":
                                DateTime date = Convert.ToDateTime(sFiltro);
                                DateTime dateFinal = Convert.ToDateTime(sFechaFinal);
                                return buscaExpedientePorRangoDeFechas(date, dateFinal, iTipo);
                                //Expression<Func<Documento, bool>> consultaFecha = (oDocumento => oDocumento.idTipo.Equals(iTipo) && (oDocumento.fecha >= date && oDocumento.fecha <= dateFinal) && oDocumento.idEstado != (iEstado));
                                //return unidad.genericDAL.Find(consultaFecha).ToList();
                            default:
                                return GetExpedientes(iTipo);
                                //Expression<Func<Documento, bool>> consultaDefault = (oDocumento => oDocumento.idTipo.Equals(iTipo) && oDocumento.idEstado != (iEstado));
                                //return unidad.genericDAL.Find(consultaDefault).ToList();
                        }
                    }
                    else if(sTipoDocumento == "SNI")
                    {
                        switch (sCampo)
                        {
                            case "Número de Oficio":
                                return buscaSNIPorNumeroDeOficio(sFiltro, iTipo);
                            //Expression<Func<Documento, bool>> consultaNumeroOficio = (oDocumento => oDocumento.idTipo.Equals(iTipo) && oDocumento.numeroDocumento.Contains(sFiltro) && oDocumento.idEstado != (iEstado));
                            //return unidad.genericDAL.Find(consultaNumeroOficio).ToList();
                            case "Fecha":
                                DateTime date = Convert.ToDateTime(sFiltro);
                                DateTime dateFinal = Convert.ToDateTime(sFechaFinal);
                                return buscaSalidaPorRangoDeFechas(date, dateFinal, iTipo);
                                //Expression<Func<Documento, bool>> consultaFecha = (oDocumento => oDocumento.idTipo.Equals(iTipo) && (oDocumento.fecha >= date && oDocumento.fecha <= dateFinal) && oDocumento.idEstado != (iEstado));
                                //return unidad.genericDAL.Find(consultaFecha).ToList();
                            default:
                                return GetDocumentosSNI(iTipo);
                                //Expression<Func<Documento, bool>> consultaDefault = (oDocumento => oDocumento.idTipo.Equals(iTipo) && oDocumento.idEstado != (iEstado));
                                //return unidad.genericDAL.Find(consultaDefault).ToList();
                        }
                    }
                    else
                    {
                        Expression<Func<Documento, bool>> consultaDefault = (oDocumento => oDocumento.idTipo.Equals(iTipo) && oDocumento.idEstado != (iEstado));
                        return unidad.genericDAL.Find(consultaDefault).ToList();
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
 
        public bool Comprobar(string sNumeroDocumento, string sIdDocumento, Boolean bNumero,Boolean bStatus)
        {
            try
            {
                using (unidad = new UnidadDeTrabajo<Documento>(new SCJ_BDEntities()))
                {
                    //Nuevo
                    if (bStatus)
                    {
                        //Numero documento
                        if (bNumero)
                        {
                            Expression<Func<Documento, bool>> eConsulta = (oDocumentoConsulta => oDocumentoConsulta.numeroDocumento.Equals(sNumeroDocumento));
                            List<Documento> lista = unidad.genericDAL.Find(eConsulta).ToList();
                            if ((unidad.genericDAL.Find(eConsulta).ToList()).Count == 0)
                            {
                                return true;
                            }
                            return false;
                        }
                        //Numero ingreso
                        else
                        {
                            Expression<Func<Documento, bool>> eConsulta = (oDocumentoConsulta => oDocumentoConsulta.numeroIngreso.Equals(sNumeroDocumento));
                            List<Documento> lista = unidad.genericDAL.Find(eConsulta).ToList();
                            if ((unidad.genericDAL.Find(eConsulta).ToList()).Count == 0)
                            {
                                return true;
                            }
                            return false;
                        }
                    }
                    //Editar
                    else
                    {
                        int iIdDocumento = Int32.Parse(sIdDocumento);
                        Documento documento = Get(iIdDocumento);
                        //Numero documento
                        if (bNumero)
                        {
                            string sNumeroIngresoComprobacion = documento.numeroDocumento;
                            if (sNumeroIngresoComprobacion == sNumeroDocumento)
                            {
                                return true;
                            }
                            else
                            {
                                Expression<Func<Documento, bool>> eConsulta = (oDocumentoConsulta => oDocumentoConsulta.numeroDocumento.Equals(sNumeroDocumento));
                                List<Documento> lista = unidad.genericDAL.Find(eConsulta).ToList();
                                if ((unidad.genericDAL.Find(eConsulta).ToList()).Count == 0)
                                {
                                    return true;
                                }
                                return false;
                            }
                        }
                        //Numero INGRESO
                        else
                        {
                            string sNumeroDocumentoComprobacion = documento.numeroIngreso;
                            if(sNumeroDocumentoComprobacion == sNumeroDocumento)
                            {
                                return true;
                            }
                            else
                            {
                                Expression<Func<Documento, bool>> eConsulta = (oDocumentoConsulta => oDocumentoConsulta.numeroIngreso.Equals(sNumeroDocumento));
                                List<Documento> lista = unidad.genericDAL.Find(eConsulta).ToList();
                                if ((unidad.genericDAL.Find(eConsulta).ToList()).Count == 0)
                                {
                                    return true;
                                }
                                return false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
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

        public List<Documento> GetReferencias(long? referencia)
        {
            try
            {
                using (context = new SCJ_BDEntities())
                {
                    return context.sp_ListaReferencias(referencia).ToList();
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

        public Nullable<long> GetNumeroReferencia(int idDocumento)
        {
            try
            {
                using (context = new SCJ_BDEntities())
                {
                    return context.sp_getNumeroReferencia(idDocumento).FirstOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Documento> GetSalidas(int iTipo)
        {
            try
            {
                using (context = new SCJ_BDEntities())
                {
                    var result = this.context.sp_listaEmitidos(iTipo).ToList();
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

        public List<Documento> GetEntradas(int iTipo)
        {
            try
            {
                using (context = new SCJ_BDEntities())
                {
                    var result = this.context.sp_listaDocumentos(iTipo).ToList();
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

        public List<Documento> GetDocumentosSNI(int iTipo)
        {
            try
            {
                using (context = new SCJ_BDEntities())
                {
                    var result = this.context.sp_listaSNI(iTipo).ToList();
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

        public List<Documento> GetExpedientes(int iTipo)
        {
            try
            {
                using (context = new SCJ_BDEntities())
                {
                    var result = this.context.sp_listaExpedientes(iTipo).ToList();
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

        public List<Documento> buscaEntradaPorNumeroDeOficio(string numeroDeOficio, int iTipo)
        {
            try
            {
                using (context = new SCJ_BDEntities())
                {
                    var result = this.context.sp_buscaDocumentosPorNumeroDeOficio(numeroDeOficio, iTipo).ToList();
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

        public List<Documento> buscaSalidaPorNumeroDeOficio(string numeroDeOficio, int iTipo)
        {
            try
            {
                using (context = new SCJ_BDEntities())
                {
                    var result = this.context.sp_buscaEmitidosPorNumeroDeOficio(numeroDeOficio, iTipo).ToList();
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

        public List<Documento> buscaSNIPorNumeroDeOficio(string numeroDeOficio, int iTipo)
        {
            try
            {
                using (context = new SCJ_BDEntities())
                {
                    var result = this.context.sp_buscaSNIPorNumeroDeOficio(numeroDeOficio, iTipo).ToList();
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
            };
        }

        public List<Documento> buscaExpedientePorNumeroDeOficio(string numeroDeOficio, int iTipo)
        {
            try
            {
                using (context = new SCJ_BDEntities())
                {
                    var result = this.context.sp_buscaExpedientePorNumeroDeOficio(numeroDeOficio, iTipo).ToList();
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

        public List<Documento> buscaEntradaPorNumeroDeIngreso(string numeroDeIngreso, int iTipo)
        {
            try
            {
                using (context = new SCJ_BDEntities())
                {
                    var result = this.context.sp_buscaDocumentosPorNumeroDeIngreso(numeroDeIngreso, iTipo).ToList();
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

        public List<Documento> buscaExpedientePorNumeroDeIngreso(string numeroDeIngreso, int iTipo)
        {
            try
            {
                using (context = new SCJ_BDEntities())
                {
                    var result = this.context.sp_buscaExpedientePorNumeroDeIngreso(numeroDeIngreso, iTipo).ToList();
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

        public List<Documento> buscaEntradaPorRangoDeFechas(DateTime fechaInicio, DateTime fechaFinal, int iTipo)
        {
            try
            {
                using (context = new SCJ_BDEntities())
                {
                    var result = this.context.sp_buscaDocumentosPorRangoDeFechas(fechaInicio,fechaFinal, iTipo).ToList();
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

        public List<Documento> buscaSalidaPorRangoDeFechas(DateTime fechaInicio, DateTime fechaFinal, int iTipo)
        {
            try
            {
                using (context = new SCJ_BDEntities())
                {
                    var result = this.context.sp_buscaEmitidosPorRangoDeFechas(fechaInicio,fechaFinal, iTipo).ToList();
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

        public List<Documento> buscaSNIPorRangoDeFechas(DateTime fechaInicio, DateTime fechaFinal, int iTipo)
        {
            try
            {
                using (context = new SCJ_BDEntities())
                {
                    var result = this.context.sp_buscaSNIPorRangoDeFechas(fechaInicio,fechaFinal, iTipo).ToList();
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

        public List<Documento> buscaExpedientePorRangoDeFechas(DateTime fechaInicio, DateTime fechaFinal, int iTipo)
        {
            try
            {
                using (context = new SCJ_BDEntities())
                {
                    var result = this.context.sp_buscaExpedientePorRangoDeFechas(fechaInicio,fechaFinal, iTipo).ToList();
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

        public List<Documento> buscaExpedientePorParte(string parte, int iTipo)
        {
            try
            {
                using (context = new SCJ_BDEntities())
                {
                    var result = this.context.sp_buscaExpedientePorParte(parte, iTipo).ToList();
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
