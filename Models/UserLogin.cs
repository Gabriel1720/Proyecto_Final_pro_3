using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Proyecto_final_pro_3.Models
{
    public class UserLogin
    {
        [Required(ErrorMessage = "Campo obligatorio")]
        [EmailAddress (ErrorMessage = "Su correo no es valido")]
        public string Correo { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        [PasswordPropertyText]
        public string Password { get; set; }


    }
}
