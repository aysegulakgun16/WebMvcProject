using System.ComponentModel.DataAnnotations;

namespace WebMvcProject.Models
{
    public class RegisterModel
    {

        [Required(ErrorMessage = "UserName Boş Geçilemez")]
        public string UserName { get; set; }

        [EmailAddress(ErrorMessage = "E-posta Formatında Giriniz")]
        [Required(ErrorMessage = "E-mail Boş Geçilemez")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password Boş Geçilemez")]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*_.-]).{8,12}$", ErrorMessage = "Güvenli Parola Seçiniz 8-12 Karakter Arası")]
        public string Password { get; set; }


        [Compare("Password",ErrorMessage = "Parola Eşleşmiyor")]
        [Required(ErrorMessage = "Password Boş Geçilemez")]
        public string PasswordAgain { get; set; }

    }
}
