

namespace SRI.Models.ViewModels
{
    using SRI.Models.Enums;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class FuncionarioVM
    {

        [Display(Name = "Apellido")]
        [Required(ErrorMessage = "Ingrese el apellido")]
        public string apellido { get; set; }

        [Display(Name = "Nombre ")]
        [Required(ErrorMessage = "Ingrese el nombre")]
        public string nombre { get; set; }

        [Display(Name = "Cedula ")]
        [Required(ErrorMessage = "Ingrese la cedula")]
        [Key]
        public string ci { get; set; }
        
        [Display(Name = "Mail ")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Ingrese el mail")]
        public string mail { get; set; }

        [Display(Name = "Celular ")]
        [Required(ErrorMessage = "Ingrese el celular")]
        public string celular { get; set; }

        [Display(Name = "Contraseņa ")]
        [Required(ErrorMessage = "Ingrese la contraseņa")]
        public string password { get; set; }

        [Display(Name = "Horario")]
        public virtual Int32 horario_id { get; set; }

        [Display(Name = "Horario")]
        public string horario_string { get; set; }


        [Display(Name = "Rol")]
        public Rol rol { get; set; }

        public static explicit operator FuncionarioVM(Funcionario v)
        {
            FuncionarioVM funcionarioVM = new FuncionarioVM();

            funcionarioVM.nombre = v.nombre;
            funcionarioVM.apellido = v.apellido;
            funcionarioVM.celular = v.celular;
            funcionarioVM.ci = v.ci;
            funcionarioVM.mail = v.mail;
            funcionarioVM.password = v.password;
            funcionarioVM.rol = (Rol)v.rol;
            funcionarioVM.horario_id = v.Horario.Id;
            funcionarioVM.horario_string = v.Horario.hora_inicio.ToString("HH:mm") + " - " + v.Horario.hora_fin.ToString("HH:mm");


            return funcionarioVM;
        }
    }
}
