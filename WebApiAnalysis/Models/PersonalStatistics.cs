using System.Collections.Generic;

namespace WebApiAnalysis.Models
{
    public class PersonStatistics
    {
        public List<int> Results { get; set; } = new List<int>();
        public (double K, double B, double R)? AdditionalInfo { get; set; }

        public uint AmountOfAttempts
        {
            get
            {
                return (uint)Results.Count;
            }
        }
    }
}
