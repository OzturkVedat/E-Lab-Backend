using E_Lab_Backend.Models;

namespace E_Lab_Backend.Dto
{
    public class TestResultDto
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string PatientId { get; set; }
        public string PatientName  { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int IgA { get; set; }
        public string IgAResult { get; set; }

        public int IgM { get; set; }
        public string IgMResult { get; set; }

        public int IgG { get; set; }
        public string IgGResult { get; set; }

        public int IgG1 { get; set; }
        public string IgG1Result { get; set; }

        public int IgG2 { get; set; }
        public string IgG2Result { get; set; }

        public int IgG3 { get; set; }
        public string IgG3Result { get; set; }

        public int IgG4 { get; set; }
        public string IgG4Result { get; set; }
    }

    public class NewTestResultDto
    {
        public string PatientId { get; set; }
        public int IgA { get; set; }
        public int IgM { get; set; }
        public int IgG { get; set; }
        public int IgG1 { get; set; }
        public int IgG2 { get; set; }
        public int IgG3 { get; set; }
        public int IgG4 { get; set; }
    }

}
