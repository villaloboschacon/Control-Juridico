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

        [RegularExpression(@"MA-[a-zñA-ZÑ]{1,6}-[0-9]{4}-2019$", ErrorMessage = "Formato incorrecto.")]
        [Required(ErrorMessage = "*Debe digitar el número de documento.")]
        [StringLength(19, MinimumLength = 14, ErrorMessage = "El número de documento debe tener al menos 11 caracteres.")]
        [Display(Name = "Número de Documento ")]
        [Remote("ComprobarDocumento", "Documento")]
        public string numeroDocumento { get; set; }

        [RegularExpression(@"N.I.[0-9]{4}-2019", ErrorMessage = "Formato incorrecto.")]
        [Required(ErrorMessage = "*Debe digitar el número de ingreso.")]
        [StringLength(14, MinimumLength = 13, ErrorMessage = "El número del documento debe tener al menos 13 caracteres.")]
        [Remote("ComprobarIngreso", "Documento")]
        [Display(Name = "Número de Ingreso")]
        public string numeroIngreso { get; set; }

        //[Range(typeof(DateTime), "10/19/2018", "10/30/2018",
        //ErrorMessage = "El valor {0} debe estar {1} y {2}")]
        //public DateTime fecha { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime fecha { get; set; }

        [StringLength(150, ErrorMessage = "El asunto no puede sobrepasarse de 150 caracteres.")]
        [Display(Name = "Asunto")]
        public string asunto { get; set; }

        [RegularExpression(@"EXP-AD-[a-zA-ZñÑ0-9]+-2019$", ErrorMessage = "Formato incorrecto.")]
        [StringLength(50, ErrorMessage = "El nombre del expediente no puede sobrepasar los 50 caracteres.")]
        [Display(Name = "Parte")]
        public string parte { get; set; }
        [StringLength(750, ErrorMessage = "La descripción no puede sobrepasarse de 750 caracteres.")]
        [Display(Name = "Descripción")]
        public string descripcion { get; set; }
        [StringLength(500,ErrorMessage = "La ubicación no puede sobrepasarse de 500 caracteres.")]
        [Display(Name = "Ubicación")]
        public string ubicacion { get; set; }
        [StringLength(750, ErrorMessage = "La observación no puede sobrepasarse de 750 caracteres.")]
        [Display(Name = "Observación")]
        public string observacion { get; set; }

    }
}