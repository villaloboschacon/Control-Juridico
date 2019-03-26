using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using BackEnd.Model;

namespace SistemaControl.Models
{
    public class PersonaViewModel
    {
        [Key]
        public int idPersona { get; set; }


        public int? idTipo { get; set; }

        public int? tipoIdentificacion { get; set; }

        [Required(ErrorMessage = "*Error. Debe de digitar la cédula de la persona")]
        [StringLength(18, ErrorMessage = "Formato Incorrecto. Cédula de persona Fisica: ######### y Cédula de persona Jurídica: ##########")]
        [Remote("ComprobarPersona", "PersonaFisica", AdditionalFields = "idPersona")]
        [Display(Name = "Cédula:")]
        public string cedula { get; set; }

        [Required(ErrorMessage = "Error. Debe de digitar el nombre de la persona.")]
        [StringLength(150, ErrorMessage = "El nombre de la persona no puede contener mas de 150 caracteres.")]
        [Display(Name = "Nombre Completo:")]
        public string nombreCompleto { get; set; }

        [Required(ErrorMessage = "Error. Debe de digitar el nombre del Representante Social.")]
        [StringLength(150, ErrorMessage = "El representante social no puede contener mas de 150 caracteres.")]
        [Display(Name = "Representante Social:")]
        public string representanteSocial { get; set; }

        [Required(ErrorMessage = "Error. Debe de digitar el nombre del Representante Legal.")]
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

        public static explicit operator PersonaViewModel(Persona p)
        {
            PersonaViewModel view = new PersonaViewModel();
            view.idPersona = p.idPersona;
            view.idTipo = p.idTipo;
            view.tipoIdentificacion = p.tipoIdentificacion;
            view.cedula = p.cedula;
            view.nombreCompleto = p.nombreCompleto;
            view.representanteSocial = p.RepresentanteSocial;
            view.representanteLegal = p.RepresentanteLegal;
            view.correo = p.correo;
            view.observacion = p.observacion;
            return view;

        }
    }
}