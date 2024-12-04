using System.ComponentModel.DataAnnotations;

namespace E_Lab_Backend.Dto
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Email alani zorunludur.")]
        [EmailAddress(ErrorMessage = "Lutfen gecerli bir e-mail giriniz.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Sifre alani zorunludur.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Sifre en az 6 karakter icermelidir.")]
        public string Password { get; set; }
    }
    public class RefreshTokenDto
    {
        public string Token { get; set; }
    }

}
