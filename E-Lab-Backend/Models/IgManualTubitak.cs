namespace E_Lab_Backend.Models
{
    public class IgManualTubitak
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public int AgeInMonthsUpperLimit { get; set; }
        public int AgeInMonthsLowerLimit { get; set; }
        public int NumOfSubjects { get; set; } = 30;
        public IgTypeEnum IgType { get; set; }
        public float GeometricMean { get; set; }
        public float GMStandardDeviation { get; set; }
        public float Mean { get; set; }
        public float MeanStandardDeviation { get; set; }
        public float MinValue { get; set; }
        public float MaxValue { get; set; }
        public float CIUpperLimit { get; set; }
        public float CILowerLimit { get; set; }
    }

    public class ManualTubitakResult
    {
        public string ReferencedManualName { get; set; } = "ManualTubitak";

        public List<IgTubitakRangesResult> IgTubitakRangesResults { get; set; }
    }

    public class IgTubitakRangesResult
    {
        public IgTypeEnum IgType { get; set; }
        public string GMResult { get; set; }
        public string MeanResult {  get; set; }
        public string MinMaxResult { get; set; }
        public string CIResult { get; set; }
    }

}
