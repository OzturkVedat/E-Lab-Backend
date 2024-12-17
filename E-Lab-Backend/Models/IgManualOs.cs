namespace E_Lab_Backend.Models
{
    public class IgManualOs
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public int AgeInMonthsUpperLimit { get; set; }
        public int AgeInMonthsLowerLimit { get; set; }
        public GenderEnum Gender { get; set; }
        public int NumOfSubjects { get; set; } = 10;
        public IgTypeEnum IgType { get; set; }
        public float ArithmeticMean { get; set; }
        public float AMStandardDeviation { get; set; }
        public float MinValue { get; set; }
        public float MaxValue { get; set; }
        public float PValue {  get; set; }
    }

   
}
