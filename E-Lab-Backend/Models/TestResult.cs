namespace E_Lab_Backend.Models
{
    public class TestResult
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public SampleTypeEnum SampleType { get; set; } = SampleTypeEnum.Serum;
        public float? IgG {  get; set; }
        public float? IgA {  get; set; }
        public float? IgM {  get; set; }
        public float? IgG1 { get; set; }
        public float? IgG2 { get; set; }
        public float? IgG3 { get; set; }
        public float? IgG4 { get; set; }
        public DateTime TestRequestTime { get; set; } = DateTime.UtcNow;
        public DateTime SampleCollectionTime {  get; set; }= DateTime.UtcNow.AddMinutes(1);
        public DateTime SampleAcceptTime {  get; set; }= DateTime.UtcNow.AddMinutes(15);
        public DateTime ExpertApproveTime { get; set; }= DateTime.UtcNow.AddMinutes(120);

        public string PatientId {  get; set; }
        public UserModel Patient { get; set; }

    }

    public enum SampleTypeEnum
    {
        Serum,      
        Saliva, 
        Uriner
    }
}
