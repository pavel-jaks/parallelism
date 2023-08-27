using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreeDimensionalMatch.Common
{
    public class HyperGraph : IHyperGraph
    {
        public HyperGraph(int verticesCount, IEnumerable<Tuple<int, int, int>> edges)
        {
            Vertices = ImmutableArray.Create<IVertex>(Enumerable.Range(0, verticesCount).Select(id => new Vertex(id)).ToArray());
            Edges = ImmutableArray.Create<IHyperEdge>(
                edges.Select(
                    e => new HyperEdge(
                        new int[] { e.Item1, e.Item2, e.Item3 }.Select(i => Vertices[i])
                    )
                ).ToArray()
            );
        }
        public ImmutableArray<IVertex> Vertices { get; private set; }

        public ImmutableArray<IHyperEdge> Edges { get; private set; }
    }
}
