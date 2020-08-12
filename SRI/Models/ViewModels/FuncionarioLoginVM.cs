using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SRI.Models.ViewModels
{
    
    public class FuncionarioLoginVM
    {

        [Key]
        [Required(ErrorMessage = "Ingrese contreseña")]
        [Display(Name = "Mail ")]
        public string mail { get; set; }

        [Display(Name = "Contraseña ")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Ingrese su contraseña")]
        public string password { get; set; }




    }
    
}