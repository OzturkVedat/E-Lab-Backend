using E_Lab_Backend.Models;
using System.ComponentModel.DataAnnotations;

namespace E_Lab_Backend.Dto
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "Isim soyisim alani zorunludur.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Full name must be between 2 and 100 characters.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Dogum tarihi alani zorunludur.")]
        [DateNotInFuture(ErrorMessage = "Dogum tarihi gelecekte olamaz.")]
        public DateOnly BirthDate { get; set; }

        [Required(ErrorMessage = "Cinsiyet alani zorunludur.")]
        public GenderEnum Gender { get; set; }

        [Required(ErrorMessage = "Email alani zorunludur.")]
        [EmailAddress(ErrorMessage = "Lutfen gecerli bir e-mail giriniz.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Sifre alani zorunludur.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Sifre en az 6 karakter icermelidir.")]
        public string Password { get; set; }
    }

    public class AuthResponse
    {
        public string UserId { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
    public class DateNotInFutureAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is DateOnly birthDate)
            {
                return birthDate <= DateOnly.FromDateTime(DateTime.UtcNow);
            }
            return false;
        }
    }
}
