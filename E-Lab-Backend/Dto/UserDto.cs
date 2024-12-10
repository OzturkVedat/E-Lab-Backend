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
    
}
