using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreeDimensionalMatch.Common
{
    public interface IProblemSolver
    {
        ProblemSolverType ProblemSolverType { get; }
        Statistic Solve(IProblem problem);
        string Name { get; }
        string Description { get; }
    }
}
