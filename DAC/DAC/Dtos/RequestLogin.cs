using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DAC.Dtos
{
    public class RequestLogin
    {

        [Required(ErrorMessage = "Username required!")]
        [MaxLength(100, ErrorMessage = "Username too long!")]
        public string Uername { get; set; }


        [Required(ErrorMessage = "Password required!")]
        [MinLength(4, ErrorMessage = "Password must be at least 4 characters long!")]
        public string Password { get; set; }
    }
}
