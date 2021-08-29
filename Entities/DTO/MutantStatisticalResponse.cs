using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTO
{
    public class MutantStatisticalResponse
    {
        public int CountMutantDna { get; set; }
        public int CountHumanDna { get; set; }
        public float Ratio { get; set; }
    }
}
