using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Independence.Solver.Version1
{
    public static class Solver
    {
        public static IEnumerable<List<int>> Solve(List<int[]> lowerAdjacency)
        {
            var vertexCount = lowerAdjacency.Count + 1;
            
            var found = new List<int>();
            var excluded = new List<int>();
            var potential = new List<int>();
            for (int i = 0; i < vertexCount; i++)
            {
                potential.Add(i);
            }
            foreach (var solution in BronKerbosch(found, potential, excluded, lowerAdjacency))
            {
                yield return solution;
            }
        }

        public static IEnumerable<List<int>> BronKerbosch(List<int> found, List<int> potential, List<int> excluded, List<int[]> lowerAdjacency)
        {
            if (found.Count > 0) yield return found;
            if (potential.Count == 0 && excluded.Count == 0)
            {
                yield break;
            }

            while (potential.Count > 0)
            {
                var i = potential[0];
                var newFound = found.Select(x => x).ToList();
                newFound.Add(i);

                var antiNeighbors = new List<int>();
                for (int j = 0; j < lowerAdjacency.Count + 1; j ++)
                {
                    if (i == j) continue;
                    if (i < j)
                    {
                        if (lowerAdjacency[j - 1][i] == 0) antiNeighbors.Add(j);
                    }
                    if (i > j)
                    {
                        if (lowerAdjacency[i - 1][j] == 0) antiNeighbors.Add(j);
                    }
                }

                var newPotential = potential.Where(antiNeighbors.Contains).ToList();
                var newExcluded = excluded.Where(antiNeighbors.Contains).ToList();
                foreach (var solution in BronKerbosch(newFound, newPotential, newExcluded, lowerAdjacency))
                {
                    yield return solution;
                }
                potential.Remove(i);
                excluded.Add(i);
            }
        }
    }
}
