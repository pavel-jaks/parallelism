using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreeDimensionalMatch.Common
{
    public interface IProblem
    {
        IHyperGraph HyperGraph { get; }
        int MatchCount { get; }
        bool? Solution { get; set; }
    }
}
