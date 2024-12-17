
namespace E_Lab_Backend.Models
{
    public class UserModel 
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string FullName { get; set; } = string.Empty;
        public int TC {  get; set; }
        public GenderEnum Gender { get; set; }
        public DateOnly BirthDate { get; set; }
        public string Email {  get; set; }
        public string PasswordHashed {  get; set; }
        public string Role { get; set; } = "User";
        public RefreshToken? RefreshToken { get; set; }

        public ICollection<TestResult> TestResults { get; set; } = [];
        public int GetAgeInMonths()
        {
            DateOnly currentDate = DateOnly.FromDateTime(DateTime.UtcNow);

            int ageInYears = currentDate.Year - BirthDate.Year;
            int ageInMonths = currentDate.Month - BirthDate.Month;

            if (ageInMonths < 0)
            {
                ageInMonths += 12;
                ageInYears--;
            }
            return ageInYears * 12 + ageInMonths;
        }
    }

    public enum GenderEnum
    {
        Male,
        Female
    }

    public class RefreshToken
    {
        public string Id {  get; set; }=Guid.NewGuid().ToString();
        public string Token { get; set; } = Guid.NewGuid().ToString();
        public DateTime ExpiryDate { get; set; }= DateTime.UtcNow.AddDays(7);
        public bool IsRevoked { get; set; } = false;
        public string UserId { get; set; }
        public UserModel User { get; set; }

        public RefreshToken(string userId) { UserId= userId; } 
    }
}
