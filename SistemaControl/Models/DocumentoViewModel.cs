using BackEnd.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaControl.Models
{
    public class DocumentoViewModel
    {
        [Key]
        public int idDocumento { get; set; }

        public int? idTipo { get; set; }

        public int? idOrigen { get; set; }

        public int? tipoOrigen { get; set; }

        public int? idEstado { get; set; }

        [RegularExpression(@"MA-[A-Z]{3}-[0-9]{4}-2018$|EXP-MA-[0-9]{4}$", ErrorMessage = "Formato incorrecto.")]
        [Required(ErrorMessage = "*Debe digitar el número de documento.")]
        [StringLength(17, MinimumLength = 11, ErrorMessage = "* El número de documento debe tener al menos 11 caracteres.")]
        [Display(Name = "Número de Documento ")]
        [Remote("ComprobarDocumento", "Documento")]
        public string numeroDocumento { get; set; }

        [RegularExpression(@"N.I.[0-9]{4}-2018", ErrorMessage = "Formato incorrecto.")]
        [Required(ErrorMessage = "*Debe digitar el número de ingreso.")]
        [StringLength(14, MinimumLength = 13, ErrorMessage = "* El número de documento debe tener al menos 13 caracteres.")]
        [Remote("ComprobarIngreso", "Documento")]
        [Display(Name = "Número de Ingreso")]
        public string numeroIngreso { get; set; }

        //[Range(typeof(DateTime), "10/19/2018", "10/30/2018",
        //ErrorMessage = "El valor {0} debe estar {1} y {2}")]
        [DataType(DataType.Date)]
        //public DateTime fecha { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
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
}