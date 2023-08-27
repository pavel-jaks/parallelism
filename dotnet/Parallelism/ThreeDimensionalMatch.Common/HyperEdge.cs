using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreeDimensionalMatch.Common
{
    public class HyperEdge : IHyperEdge
    {
        public HyperEdge(IEnumerable<IVertex> vertices)
        {
            if (vertices == null) throw new ArgumentNullException(nameof(vertices));
            if (vertices.Count() != 3) throw new ArgumentException(nameof(vertices));
            Vertices = vertices.ToArray();
        }
        public ICollection<IVertex> Vertices { get; init; }
    }
}
