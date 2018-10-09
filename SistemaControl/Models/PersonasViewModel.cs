using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SistemaControl.Models
{
    public class PersonasViewModel
    {
        public int SelectedPersonaId { get; set; }
        public SelectList PersonaIdTemplate { get; set; }

        [Display(Name = "Identificador")]
        [Key]
        public int idPersona { get; set; }
        [Display(Name = "tipo:")]
        public int idTipo { get; set; }
        public virtual ICollection<Tipo> tipos { get; set; }

        [Display(Name = "Cédula:")]
        [Required(ErrorMessage = "Debe digitar la Cedula")]
        public string cedula { get; set; }

        [Display(Name = "Nombre Completo:")]
        [Required(ErrorMessage = "Debe digitar el Nombre")]
        public string nombreCompleto { get; set; }

        [Display(Name = "Representante Legal:")]
        public string RepresentanteLegal { get; set; }

        [Display(Name = "Representante Social:")]
        public string RepresentanteSocial { get; set; }

        [Display(Name = "Correo:")]
        [Required(ErrorMessage = "Debe digitar el Correo")]
        public string correo { get; set; }

        [Display(Name = "Observación:")]
        public string observacion { get; set; }
    }


    public class Tipo
    {
        public int Id { get; set; }
        public String Name { get; set; }
    }
}