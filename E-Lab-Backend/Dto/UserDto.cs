using System.ComponentModel.DataAnnotations;

namespace E_Lab_Backend.Dto
{
    public class UserDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateOnly BirthDate { get; set; }
    }

    public class PatientDetails
    {
        public string PatientId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
    }
    

    public class UserUpdateDto
    {
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Full name must be between 2 and 100 characters.")]
        public string FullName { get; set; }
        [EmailAddress(ErrorMessage = "Lutfen gecerli bir e-mail giriniz.")]
        public string Email { get; set; }

        [StringLength(100, MinimumLength = 6, ErrorMessage = "Sifre en az 6 karakter icermelidir.")]
        public string Password { get; set; }
    }

}
