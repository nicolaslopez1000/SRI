using SRI.Models.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SRI.Models.ViewModels
{
    public class FiltradoPorMesDiaTotalModelView
    {
        public int totalPositivos { get; set; }

        public int totalNegativos { get; set; }

        public int totalNeutros { get; set; }

        public List<IncidentesPorDiaEmocion> listaIncidentesGroupedByDay { get; set; }



    }
}