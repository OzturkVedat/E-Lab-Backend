namespace E_Lab_Backend.Models
{
    public class TestResultPatient        // tahlil sınıfı
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string PatientId { get; set; }
        public UserModel Patient { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public float IgA { get; set; }
        public string IgAResult { get; set; }

        public float IgM { get; set; }
        public string IgMResult { get; set; }

        public float IgG { get; set; }
        public string IgGResult { get; set; }

        public float IgG1 { get; set; }
        public string IgG1Result { get; set; }

        public float IgG2 { get; set; }
        public string IgG2Result { get; set; }

        public float IgG3 { get; set; }
        public string IgG3Result { get; set; }

        public float IgG4 { get; set; }
        public string IgG4Result { get; set; }
    }
}
