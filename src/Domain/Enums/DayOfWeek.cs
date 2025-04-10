using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum DayOfWeek
    {
        [Display(Name = "Lunes")]
        Lunes,
        [Display(Name = "Martes")]
        Martes,
        [Display(Name = "Miercoles")]
        Miercoles,
        [Display(Name = "Jueves")]
        Jueves,
        [Display(Name = "Viernes")]
        Viernes,
        [Display(Name = "Sabado")]
        Sabado,
        [Display(Name = "Domingo")]
        Domingo

    }
}
