using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreeDimensionalMatch.Common;

namespace ThreeDimensionalMatch.Common
{
    public class Vertex : IVertex
    {
        public Vertex(int id)
        {
            Id = id;
        }
        public int Id { get; init; }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
