using SRI.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SRI.Models.ViewModels
{
    public class FiltradoVM
    {

        [Display(Name = "Mes de filtrado")]
        public DateTime mes { get; set; }

        [Display(Name = "Cedula funcionario")]
        public string funcionario_ci { get; set; }

        [Display(Name = "Tipo de incidente")]
        public TipoIncidente tipoIncidente { get; set; }

    }
}