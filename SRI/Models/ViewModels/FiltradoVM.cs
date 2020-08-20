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
        [DisplayFormat(DataFormatString = "{0:MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime mes { get; set; }

        [Display(Name = "Cedula funcionario")]
        public string ciFuncionario { get; set; }

        [Display(Name = "Tipo de incidente")]
        public TipoIncidente tipoIncidente { get; set; }


    }
}