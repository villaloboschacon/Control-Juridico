using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaControl.Models
{
    public class LoginViewModel
    {
        [AllowHtml]
        [Required(ErrorMessage = "El usuario es obligatorio.")]
        public string Username { get; set; }

        [AllowHtml]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        public string Password { get; set; }
    }
}