using System.ComponentModel.DataAnnotations;

namespace DAC.Dtos
{
    public class RegisterUserDto
    {
       
        [Required(ErrorMessage = "Username required")]

        [MaxLength(100, ErrorMessage = "Username too long!")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password required!")]

        [MinLength(4, ErrorMessage = "Password must be at least 4 characters long!")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Rol required")]
      
        public bool IsAdmin { get; set; }
    }
}
