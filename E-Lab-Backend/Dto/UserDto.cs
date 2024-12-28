using E_Lab_Backend.Models;
using System.ComponentModel.DataAnnotations;

namespace E_Lab_Backend.Dto
{
    public class UserDto
    {
        public string FullName { get; set; }
        public string Tckn { get; set; }
        public DateOnly BirthDate { get; set; }
    }

    public class PatientDetails
    {
        public string PatientId { get; set; }
        public string FullName { get; set; }
        public string Tckn { get; set; }
        public GenderEnum Gender { get; set; }
    }
    
    public class UserUpdateDto
    {
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Full name must be between 2 and 100 characters.")]
        public string FullName { get; set; }
        public DateOnly BirthDate { get; set; }
    }

}
