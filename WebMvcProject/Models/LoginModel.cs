using System.ComponentModel.DataAnnotations;

namespace WebMvcProject.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage="UserName Boş Geçilemez!")]
        public string UserName { get; set; }


        [Required(ErrorMessage = "Password Boş Geçilemez!")]
        [MinLength(8,ErrorMessage ="Password Min 8 Karakter Uzunluğunda Olmalıdır!")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
