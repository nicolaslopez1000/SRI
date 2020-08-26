using SRI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SRI.Models.Classes
{
    public class IncidentesPorDiaEmocion
    {
        [DataType(DataType.Date)]
        public DateTime fecha { get; set; }

        public int cantidadPositivos { get; set; }

        public int cantidadNegativos { get; set; }

        public int cantidadNeutrales { get; set; }

        public List<IncidenteVM> incidentes { get; set; }


    }
}