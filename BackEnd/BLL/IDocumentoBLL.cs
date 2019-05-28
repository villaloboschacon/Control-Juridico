﻿using BackEnd.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.BLL
{
    public interface IDocumentoBLL : IBLLGenerico<Documento>
    {
        ///<summary>
        ///Agrega un documento. 
        ///</summary>
        bool Agregar(Documento oDocumento);

        ///<summary>
        ///Actualiza el documento
        ///</summary>
        bool Actualizar(Documento oDocumento);

        ///<summary>
        ///Elimina el documento.
        ///</summary>
        bool Eliminar(Documento oDocumento);
      
        ///<summary>
        ///Cambia el estado del documento a archivado.
        ///</summary>
        bool ArchivarDocumento(int iIdDocumento);

        ///<summary>
        ///Recupera la lista de entradas filtrandolas por tipo. 
        ///</summary>
       // List<Documento> ConsultaEntradas(int iTipo);

        ///<summary>
        ///Comprueba que el registro del numero del documento no este repetido.
        ///</summary>
        bool Comprobar(string numeroDocumento, string idDocumento,Boolean bNumero ,Boolean bStatus);

        ///<summary>
        ///Guarda el estado del model actual.
        ///</summary>
        bool SaveChanges();

        ///<summary>
        ///Recupera la lista de documentos filtrandolas por nombre de persona, tipo de Documento, si tiene numero de ingreso y campo de busqueda
        ///</summary>
        List<Documento> Consulta(int iTipo, string sFiltro, string sFechaFinal,string sCampo, string sTipoDocumento);   

        ///<summary>
        ///Recupera la lista de documentos de salida. 
        ///</summary>
        List<Documento> GetSalidas(int iTipo);

        ///<summary>
        ///Recupera la lista de documentos de entradas. 
        ///</summary>
        List<Documento> GetEntradas(int iTipo);

        ///<summary>
        ///Recupera la lista de documentos SNI. 
        ///</summary>
        List<Documento> GetDocumentosSNI(int iTipo);

        ///<summary>
        ///Recupera la lista de Expedientes.
        ///</summary>
        List<Documento> GetExpedientes(int iTipo);

        ///<summary>
        ///Recupera la lista de documentos asociados a un numero de referencia. 
        ///</summary>
        List<Documento> GetReferencias(long? iReferencia);
        
        ///<summary>
        ///Recupera el documentos con el id. 
        ///</summary>
        Documento GetDocumento(int iId);

        ///<summary>
        ///Recupera el numero de ingreso del documento.
        ///</summary>
        Nullable<long> GetNumeroIngreso();

        ///<summary>
        ///Recupera el numero de referencia del documento.
        ///</summary>
        Nullable<long> GetNumeroReferencia(int idDocumento);

        ///<summary>
        ///Genera el numero de ingreso para el documento.
        ///</summary>
        Nullable<long> GeneraNumeroIngreso();

        ///<summary>
        ///Genera el numero de referencia para el documento.
        ///</summary>
        Nullable<long> GeneraNumeroReferencia();

        ///<summary>
        ///Recupera la nomenclatura acorde al nombre del departamento.
        ///</summary>
        string GetNomenclatura(string sNombreDepartamento);

        ///<summary>
        ///Busca Entrada por numero de oficio
        ///</summary>
        List<Documento> buscaEntradaPorNumeroDeOficio(string numeroDeOficio, int iTipo);

        ///<summary>
        ///Busca Salida por numero de oficio
        ///</summary>
        List<Documento> buscaSalidaPorNumeroDeOficio(string numeroDeOficio, int iTipo);

        ///<summary>
        ///Busca SNI por numero de oficio
        ///</summary>
        List<Documento> buscaSNIPorNumeroDeOficio(string numeroDeOficio, int iTipo);

        ///<summary>
        ///Busca Expediente por numero de oficio
        ///</summary>
        List<Documento> buscaExpedientePorNumeroDeOficio(string numeroDeOficio, int iTipo);

        ///<summary>
        ///Busca Entrada por numero de ingreso
        ///</summary>
        List<Documento> buscaEntradaPorNumeroDeIngreso(string numeroDeIngreso, int iTipo);

        ///<summary>
        ///Busca Expedientes por numero de inreso
        ///</summary>
        List<Documento> buscaExpedientePorNumeroDeIngreso(string numeroDeIngreso, int iTipo);

        ///<summary>
        ///Busca Entrada por Rango de fechas
        ///</summary>
        List<Documento> buscaEntradaPorRangoDeFechas(DateTime fechaInicio, DateTime fechaFinal, int iTipo);

        ///<summary>
        ///Busca Salida por Rango de fechas
        ///</summary>
        List<Documento> buscaSalidaPorRangoDeFechas(DateTime fechaInicio, DateTime fechaFinal, int iTipo);

        ///<summary>
        ///Busca SNI por numero Rango de fechas
        ///</summary>
        List<Documento> buscaSNIPorRangoDeFechas(DateTime fechaInicio, DateTime fechaFinal, int iTipo);

        ///<summary>
        ///Busca Expediente por Rango de fechas
        ///</summary>
        List<Documento> buscaExpedientePorRangoDeFechas(DateTime fechaInicio, DateTime fechaFinal, int iTipo);

        ///<summary>
        ///Busca Expediente por parte
        ///</summary>
        List<Documento> buscaExpedientePorParte(String parte, int iTipo);


    }

}
