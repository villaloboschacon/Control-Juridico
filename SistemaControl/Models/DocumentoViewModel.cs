using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FrontEnd.Models
{
    public class DocumentoViewModel
    {
        [Key]
        public int idDocumento { get; set; }

        [Display(Name = "Tipo Documento:")]
        public int idTipo { get; set; }
        public IEnumerable<TipoDocumento> tiposDocumentos { get; set; }

        [Display(Name = "Origen:")]
        public int idOrigen { get; set; }
        public IEnumerable<Origen> origenes { get; set; }

        [Display(Name = "Tipo Origen:")]
        public int tipoOrigen { get; set; }
        public IEnumerable<TipoOrigen> tiposOrigenes { get; set; }

        [Display(Name = "Estado:")]
        public int idEstado { get; set; }
        public IEnumerable<Estado> estados { get; set; }

        [Display(Name = "Número de Documento:")]
        [Required(ErrorMessage = "Debe digitar el numero de documento")]
        public string numeroDocumento { get; set; }

        [Display(Name = "Número de Ingreso:")]
        [Required(ErrorMessage = "Debe digitar el numero de ingreso")]
        public string numeroIngreso { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public System.DateTime fecha { get; set; }

        [Display(Name = "Asunto:")]
        public string asunto { get; set; }

        [Display(Name = "Descripción:")]
        public string descripcion { get; set; }

        [Display(Name = "Ubicación:")]
        public string ubicacion { get; set; }

        [Display(Name = "Observación:")]
        public string observacion { get; set; }
    }

    public class TipoDocumento
    {
        public int Id { get; set; }
        public String Name { get; set; }
    }

    public class Origen
    {
        public int Id { get; set; }
        public String Name { get; set; }
    }

    public class TipoOrigen
    {
        public int Id { get; set; }
        public String Name { get; set; }
    }

    public class Estado
    {
        public int Id { get; set; }
        public String Name { get; set; }
    }

}