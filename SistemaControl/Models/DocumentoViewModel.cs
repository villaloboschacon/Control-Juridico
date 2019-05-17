﻿using BackEnd.BLL;
using BackEnd.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaControl.Models
{
    ///<summary>
    ///Clase encargada de la vista de la entidad de Documento
    ///</summary>
    public class DocumentoViewModel
    {
        [Key]
        public int idDocumento { get; set; }

        [Required(ErrorMessage = "Tipo requerido.")]
        [Remote("ComprobarDropdownList", "Documento")]
        public int? idTipo { get; set; }
        [Required(ErrorMessage = "Origen requerido.")]
        [Remote("ComprobarDropdownList", "Documento")]
        public int? idOrigen { get; set; }
        [Required(ErrorMessage = "Tipo de origen requerido.")]
        [Remote("ComprobarDropdownList", "Documento")]
        public int? tipoOrigen { get; set; }

        public int? idEstado { get; set; }

        public long? idReferencia { get; set; }

        [RegularExpression(@"(MA-[a-zñA-ZÑ]{1,6}-[0-9]{4}-\b20(1[8-9]|2[0-9]|3[0-9]|4[0-9]|5[0-9]|6[0-9])\b)$", ErrorMessage = "Formato incorrecto. MA-*SIGLAS DEL DEPARTAMENTO*-*####*-*AÑO ACTUAL*. \n Ejemplo:MA-PSI-5463-2019.")]
        [Required(ErrorMessage = "*Debe digitar el número de documento.")]
        [StringLength(19, MinimumLength = 10, ErrorMessage = "El número de documento debe tener al menos 15 caracteres.")]
        [Display(Name = "Número de Documento ")]
        [Remote("ComprobarDocumento", "Documento",AdditionalFields = "idDocumento")]
        public string numeroDocumento { get; set; }

        [RegularExpression("N.I.[0-9]{4}-20(1[8-9]|2[0-9]|3[0-9]|4[0-9]|5[0-9]|6[0-9])$", ErrorMessage = "Formato incorrecto. N.I.-*####*-*AÑO ACTUAL*. \n Ejemplo:N.I.3571-2019.")]
        [Required(ErrorMessage = "*Debe digitar el número de ingreso.")]
        [StringLength(14, MinimumLength = 13, ErrorMessage = "El número del documento debe tener al menos 13 caracteres.")]
        [Display(Name = "Número de Ingreso")]
        [Remote("ComprobarIngreso", "Documento", AdditionalFields = "idDocumento")]
        public string numeroIngreso { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime fecha { get; set; }

        [StringLength(150, ErrorMessage = "El asunto no puede sobrepasarse de 150 caracteres.")]
        [Display(Name = "Asunto")]
        public string asunto { get; set; }

        [RegularExpression(@"EXP-AD-[a-zA-ZñÑ0-9]+-20(1[8-9]|2[0-9]|3[0-9]|4[0-9]|5[0-9]|6[0-9])$", ErrorMessage = "Formato incorrecto. EXP-AD-*NOMBRE DE LA PERSONAS*-*AÑO ACTUAL*.\nEjemplo:EXP-AD-MIGUELSANCHEZFERNANDEZ-2019.")]
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

        [Display(Name = "¿Desea escribir el oficio?")]
        public Boolean check { get; set; }
        [Display(Name = "Oficio")]
        [AllowHtml]
        public string texto { get; set; }

        public static explicit operator DocumentoViewModel(Documento v)
        {
            DocumentoViewModel view = new DocumentoViewModel();
            view.asunto = v.asunto;
            view.descripcion = v.descripcion;
            view.fecha = v.fecha;
            view.idDocumento = v.idDocumento;
            view.idEstado = v.idEstado;
            view.idOrigen = v.idOrigen;
            view.idTipo = v.idTipo;
            view.idReferencia = v.idReferencia;
            view.numeroDocumento = v.numeroDocumento;
            view.numeroIngreso = v.numeroIngreso;
            view.observacion = v.observacion;
            view.parte = v.parte;
            view.tipoOrigen = v.tipoOrigen;
            view.ubicacion = v.ubicacion;
            view.texto = v.texto;
            return view;
        }
    }
}