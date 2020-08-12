using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SRI.Models.Enums
{
    public enum Emocion : int
    {
        positivo = 1,
        neutral = 0,
        negativo = -1
    }
    public enum TipoEmail : int
    {
        to = 2,
        cc = 1
    }

    public enum Rol : int
    {
        [Display(Name = "Administrador")]
        admin = 1,
        [Display(Name = "Funcionario ")]
        funcionario = 0
    }
}