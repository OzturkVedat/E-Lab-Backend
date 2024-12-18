namespace E_Lab_Backend.Models
{
    public class IgManualTjp
    {
        public string Id { get; set; }= Guid.NewGuid().ToString();
        public int AgeInMonthsUpperLimit {  get; set; }
        public int AgeInMonthsLowerLimit {  get; set; }
        public int NumOfSubjects {  get; set; }
        public IgTypeEnum IgType {get;set;}
        public float GeometricMean {  get; set; }
        public float GMStandardDeviation {  get; set; }
        public float MinValue {  get; set; }
        public float MaxValue { get; set; }
        public float CIUpperLimit {  get; set; }
        public float CILowerLimit { get; set; }

    }

    public class ManualTjpResult
    {
        public string ReferencedManualName { get; set; } = "ManualTjp";

        public List<IgTjpRangesResult> IgTjpRangesResults { get; set; }     // ap and cilv manuals
    }

    public class IgTjpRangesResult
    {
        public IgTypeEnum IgType { get; set; }
        public string GMResult {  get; set; }
        public string MinMaxResult {  get; set; }
        public string CIResult {  get; set; }
        
    }

    public enum IgTypeEnum
    {
        IgA,
        IgG,
        IgM,
        IgG1,
        IgG2,
        IgG3,
        IgG4
    }
}
