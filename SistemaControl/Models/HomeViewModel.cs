using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaControl.Models
{
    public class HomeViewModel
    {
        public int numeroDocumentos { get; set; }
        public int numEntradas { get; set; }
        public int numSalidas { get; set; }
        public int numExpedientes { get; set; }


        public int numeroCasos { get; set; }
        public int numeroAdministrativos { get; set; }
        public int numeroJudiciales { get; set; }

        public int numeroPersonas { get; set; }
        public int numeroFisicas { get; set; }
        public int numeroJuridicas { get; set; }
    }
}