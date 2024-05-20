using System.Collections.Generic;

namespace Lab6_MathBalance.Models
{
    public class DataOut
    {
        public List<PotokOut> Flows { get; set; }
        public bool IsBalanced { get; set; }
    }
}
