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

    public class TestResultDetails     
    {
        public string Id { get; set; }
        public DateTime ExpertApproveTime { get; set; }
        public SampleTypeEnum SampleType { get; set; }
        public int AgeInMonths {  get; set; }
        public float? IgA { get; set; }
        public float? IgM { get; set; }
        public float? IgG { get; set; }
        public float? IgG1 { get; set; }
        public float? IgG2 { get; set; }
        public float? IgG3 { get; set; }
        public float? IgG4 { get; set; }
    }

    public class IgResultsDto
    {
        public string ReferencedManualName { get; set; } = string.Empty;
        public string? IgAResult { get; set; }
        public string? IgMResult { get; set; }
        public string? IgGResult { get; set; }
        public string? IgG1Result { get; set; }
        public string? IgG2Result { get; set; }
        public string? IgG3Result { get; set; }
        public string? IgG4Result { get; set; }
    }

    public class TestResultsFromManuals
    {
        public TestResultDetails Details { get; set; }
        public IgResultsDto ManualApResults {  get; set; }
        public IgResultsDto ManualCilvResults { get; set; }
        public ManualOsResult ManualOsResults {  get; set; }
        public ManualTjpResult ManualTjpResults { get; set; }
        public ManualTubitakResult ManualTubitakResults {  get; set; }
    }

    public class NewTestResultDto
    {
        [Required(ErrorMessage = "PatientId bilgisi zorunludur.")]
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

    public class CheckManualDto
    {
        public int AgeInMonths { get; set; }
        public float? IgA { get; set; }
        public float? IgM { get; set; }
        public float? IgG { get; set; }
        public float? IgG1 { get; set; }
        public float? IgG2 { get; set; }
        public float? IgG3 { get; set; }
        public float? IgG4 { get; set; }
    }

}
