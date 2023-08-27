using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreeDimensionalMatch.Common
{
    public class Statistic
    {
        public ProblemSolverType ProblemSolverType { get; set; }
        public string ProblemSolverName { get; set; } = string.Empty;
        public string ProblemSolverDescription { get; set; } = string.Empty;
        public double TotalTimeInSeconds { get; set; }
        public double TotalSerialTime { get; set; }
        public double TotalParallelTime { get; set; }
        public int VerticesCount { get; set; }
        public int EdgesCount { get; set; }
        public int MatchCount { get; set; }
    }
}
