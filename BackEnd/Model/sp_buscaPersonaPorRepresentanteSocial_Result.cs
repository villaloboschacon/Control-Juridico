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
    
    public partial class sp_buscaPersonaPorRepresentanteSocial_Result
    {
        public int idPersona { get; set; }
        public int idTipo { get; set; }
        public string cedula { get; set; }
        public string nombreCompleto { get; set; }
        public string RepresentanteSocial { get; set; }
        public string RepresentanteLegal { get; set; }
        public string correo { get; set; }
        public string observacion { get; set; }
        public Nullable<int> tipoIdentificacion { get; set; }
    }
}
