namespace E_Lab_Backend.Models
{
    public class IgManualCilvPrimer
    {
        public string Id {  get; set; }= Guid.NewGuid().ToString();
        public int AgeInMonthsUpperLimit {  get; set; }
        public int AgeInMonthsLowerLimit { get; set; }
        public float IgGLowerLimit {  get; set; }
        public float IgGUpperLimit {  get; set; }
        public float IgALowerLimit { get; set; }
        public float IgAUpperLimit { get; set; }
        public float IgMLowerLimit { get; set; }
        public float IgMUpperLimit { get; set; }
       
    }

    public class IgManualCilvSeconder
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public int AgeInMonthsUpperLimit { get; set; }
        public int AgeInMonthsLowerLimit { get; set; }
        public float IgG1LowerLimit { get; set; }
        public float IgG1UpperLimit { get; set; }
        public float IgG2LowerLimit { get; set; }
        public float IgG2UpperLimit { get; set; }
        public float IgG3LowerLimit { get; set; }
        public float IgG3UpperLimit { get; set; }
        public float IgG4LowerLimit { get; set; }
        public float IgG4UpperLimit { get; set; }
    }

}
