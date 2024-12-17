using E_Lab_Backend.Models;
using System.ComponentModel.DataAnnotations;

namespace E_Lab_Backend.Dto
{

    public class TestResultDto      // for listing all the results of a user
    {
        public string Id { get; set; }
        public float? IgA { get; set; }
        public float? IgM { get; set; }
        public float? IgG { get; set; }
        public float? IgG1 { get; set; }
        public float? IgG2 { get; set; }
        public float? IgG3 { get; set; }
        public float? IgG4 { get; set; }
        public DateTime ExpertApproveTime { get; set; }
    }

    public class TestResultDetails      // for listing the values and results(low, high, normal) of Igs
    {
        public string Id { get; set; }
        public DateTime ExpertApproveTime { get; set; }
        public SampleTypeEnum SampleType { get; set; }
        public int AgeInMonths {  get; set; }
        public float? IgA { get; set; }
        public string? IgAResult { get; set; }

        public float? IgM { get; set; }
        public string? IgMResult { get; set; }

        public float? IgG { get; set; }
        public string? IgGResult { get; set; }

        public float? IgG1 { get; set; }
        public string? IgG1Result { get; set; }

        public float? IgG2 { get; set; }
        public string? IgG2Result { get; set; }

        public float? IgG3 { get; set; }
        public string? IgG3Result { get; set; }

        public float? IgG4 { get; set; }
        public string? IgG4Result { get; set; }
    }

    public class NewTestResultDto
    {
        [Required(ErrorMessage ="PatientId bilgisi zorunludur.")]
        public string PatientId { get; set; }
        public SampleTypeEnum? SampleType { get; set; }
        public float? IgA { get; set; }
        public float? IgM { get; set; }
        public float? IgG { get; set; }
        public float? IgG1 { get; set; }
        public float? IgG2 { get; set; }
        public float? IgG3 { get; set; }
        public float? IgG4 { get; set; }
    }

}
