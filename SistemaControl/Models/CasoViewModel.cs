using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BackEnd.Model;

namespace SistemaControl.Models
{
    public class CasoViewModel
    {
        [Key]
        public int idCaso { get; set; }

        public int idTipo { get; set; }

        public int? idPersona { get; set; }

        public int? idUsuario { get; set; }

        public int? idEstado { get; set; }

        public int? tipoLitigante { get; set; }
       
      
        [RegularExpression("[0-9]{2}-[0-9]{6}-[0-9]{4}-[a-zñA-ZÑ]{1,12}(-[0-9]{1}|)$", ErrorMessage = "Formato incorrecto. *DOS NUMEROS*-*SEIS NÚMEROS*-*CUATRO NUMEROS*-*MATERIA*. \n Ejemplo:19-161616-8989-Penal.")]
        [Required(ErrorMessage = "*Debe de digitar el número de caso.")]
        [Remote("ComprobarCaso", "Caso", AdditionalFields = "idCaso")]
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

        public static explicit operator CasoViewModel(Caso v)
        {
            CasoViewModel casoViewModel = new CasoViewModel();
            casoViewModel.idCaso = v.idCaso;
            casoViewModel.idEstado = v.idEstado;
            casoViewModel.idPersona = v.idPersona;
            casoViewModel.idTipo = v.idTipo;
            casoViewModel.idUsuario = v.idUsuario;
            casoViewModel.materia = v.materia;
            casoViewModel.numeroCaso = v.numeroCaso;
            casoViewModel.observacion = v.observacion;
            casoViewModel.tipoLitigante = v.tipoLitigante;
            casoViewModel.descripcion = v.descripcion;
            return casoViewModel;
        }
    }
}