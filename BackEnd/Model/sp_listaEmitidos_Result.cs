//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BackEnd.Model
{
    using System;
    
    public partial class sp_listaEmitidos_Result
    {
        public int idDocumento { get; set; }
        public int idTipo { get; set; }
        public int idOrigen { get; set; }
        public int tipoOrigen { get; set; }
        public int idEstado { get; set; }
        public Nullable<long> idReferencia { get; set; }
        public string numeroDocumento { get; set; }
        public string numeroIngreso { get; set; }
        public System.DateTime fecha { get; set; }
        public string asunto { get; set; }
        public string descripcion { get; set; }
        public string ubicacion { get; set; }
        public string observacion { get; set; }
        public string parte { get; set; }
        public string texto { get; set; }
        public byte[] adjunto { get; set; }
    }
}
