using SRI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SRI.Helpers
{
    public class FuncionarioHelper
    {
        private db_SRI db = new db_SRI();

        public IEnumerable<SelectListItem> GetListaHorarios(int selectedItem = -1)
        {
            List<Horario> listaHorarios = db.Horario.Where(x => x.is_eliminado == false).ToList();

            List<SelectListItem> horariosDropDown = new List<SelectListItem>();

            foreach (Horario horario in listaHorarios)
            {
                String texto = (horario.hora_inicio.ToString("HH:mm") + " - " + horario.hora_fin.ToString("HH:mm"));

                horariosDropDown.Add(
                     new SelectListItem
                     {
                         Value = horario.Id.ToString(),
                         Text = texto
                     }
                    );

            }

            SelectList selectList = new SelectList(horariosDropDown, "Value", "Text");

            if (selectedItem != -1)
            {
                var selected = selectList.Where(x => x.Value == selectedItem.ToString()).First();
                selected.Selected = true;
            }

            return selectList;
        }

        public Funcionario GetFuncionarioByMail(String mail)
        {

            var obj = db.Funcionario.Where(a => a.mail.Equals(mail)).FirstOrDefault();

            return obj;
        }

    }
}