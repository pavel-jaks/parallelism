using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreeDimensionalMatch.Common
{
    public interface IHyperEdge
    {
        ICollection<IVertex> Vertices { get; }
    }
}
