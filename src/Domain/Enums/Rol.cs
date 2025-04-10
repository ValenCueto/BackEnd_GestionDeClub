using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum Rol
    {
        [Display(Name = "Admin")]
        Admin,
        [Display(Name = "Client")]
        Client,
        [Display(Name = "Gerente")]
        Gerente,
        [Display(Name = "CM")]
        CM
    }
}
