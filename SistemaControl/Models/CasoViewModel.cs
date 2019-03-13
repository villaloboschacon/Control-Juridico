using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaControl.Models
{
    public class CasoViewModel
    {
        [Key]
        public int idCaso { get; set; }

        public int? idPersona { get; set; }

        public int? idTipo { get; set; }

        public int? idUsuario { get; set; }

        public int? idEstado { get; set; }

        public int? tipoLitigante { get; set; }

        [Required(ErrorMessage = "*Debe de digitar el número de documento.")]
        [Remote("ComprobarCaso", "Caso")]
        [Display(Name = "Número de Caso")]
        public string numeroCaso { get; set; }

        [Required(ErrorMessage = "*Debe de digitar la materia del proceso.")]
        [Display(Name = "Materia")]
        public string materia { get; set; }

        [StringLength(750, ErrorMessage = "La descripción no puede sobrepasarse de 750 caracteres.")]
        [Display(Name = "Descripción")]
        public string descripcion { get; set; }

        [StringLength(750, ErrorMessage = "La observación no puede sobrepasarse de 750 caracteres.")]
        [Display(Name = "Observación")]
        public string observacion { get; set; }
    }
}