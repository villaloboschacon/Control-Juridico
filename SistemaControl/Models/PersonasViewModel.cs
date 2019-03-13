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
        [Key]
        public int idPersona { get; set; }

        public int? idTipo { get; set; }

        [Required(ErrorMessage = "*Error. Debe de digitar correctamente la cédula de la persona")]
        [StringLength(12, ErrorMessage = "Error: Cédula de persona Fisica: X-XXXX-XXXX y Cédula de persona Jurídica: X-XXX-XXXXXX")]
        [Remote("ComprobarCedula", "PersonaFisica")]
        [Display(Name = "Cédula:")]
        public string cedula { get; set; }

        [Required(ErrorMessage = "Error. Debe de digitar el nombre de la persona correctamente.")]
        [StringLength(150, ErrorMessage = "El nombre de la persona no puede contener mas de 150 caracteres.")]
        [Remote("ComprobarNombreCompleto", "PersonaFisica")]
        [Display(Name = "Nombre Completo:")]
        public string nombreCompleto { get; set; }

        [Required(ErrorMessage = "Error. Debe de digitar el nombre del Representante Social correctamente.")]
        [StringLength(150, ErrorMessage = "El representante social no puede contener mas de 150 caracteres.")]
        [Display(Name = "Representante Social:")]
        public string representanteSocial { get; set; }

        [Required(ErrorMessage = "Error. Debe de digitar el nombre del Representante Legal correctamente.")]
        [StringLength(150, ErrorMessage = "El representante legal no puede contener mas de 150 caracteres.")]
        [Display(Name = "Representante Legal:")]
        public string representanteLegal { get; set; }

        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Formato Incorrecto.")]
        [Required(ErrorMessage = "Error. Debe de digitar un correo electrónico para contactar a la persona.")]
        [StringLength(150, ErrorMessage = "El correo electrónico no puede contener mas de 150 caracteres.")]
        [Display(Name = "Correo:")]
        public string correo { get; set; }

        [StringLength(750, ErrorMessage = "La observación no puede sobrepasarse de 750 caracteres.")]
        [Display(Name = "Observación:")]
        public string observacion { get; set; }
        //public int SelectedPersonaId { get; set; }
        //public SelectList PersonaIdTemplate { get; set; }

        //[Display(Name = "Identificador")]
        //[Key]
        //public int idPersona { get; set; }
        //[Display(Name = "tipo:")]
        //public int idTipo { get; set; }
        //public virtual ICollection<Tipo> tipos { get; set; }

        //[Display(Name = "Cédula:")]
        //[Required(ErrorMessage = "Debe digitar la Cedula")]
        //public string cedula { get; set; }

        //[Display(Name = "Nombre Completo:")]
        //[Required(ErrorMessage = "Debe digitar el Nombre")]
        //public string nombreCompleto { get; set; }

        //[Display(Name = "Representante Legal:")]
        //public string RepresentanteLegal { get; set; }

        //[Display(Name = "Representante Social:")]
        //public string RepresentanteSocial { get; set; }

        //[Display(Name = "Correo:")]
        //[Required(ErrorMessage = "Debe digitar el Correo")]
        //public string correo { get; set; }

        //[Display(Name = "Observación:")]
        //public string observacion { get; set; }
    }


    //public class Tipo
    //{
    //    public int Id { get; set; }
    //    public String Name { get; set; }
    //}
}