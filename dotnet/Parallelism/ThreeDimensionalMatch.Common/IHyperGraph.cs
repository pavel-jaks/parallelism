using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreeDimensionalMatch.Common
{
    public interface IHyperGraph
    {
        ImmutableArray<IVertex> Vertices { get; }
        ImmutableArray<IHyperEdge> Edges { get; }
    }
}
