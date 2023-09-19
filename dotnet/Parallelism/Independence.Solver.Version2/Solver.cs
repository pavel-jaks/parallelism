using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Independence.Solver.Version2
{
    public static class Solver
    {
        public static IEnumerable<List<int>> Solve(List<int[]> lowerAdjacency)
        {
            var vertexCount = lowerAdjacency.Count + 1;

            var runDataCollection = new List<RunData>();
            var initPotential = Enumerable.Range(0, vertexCount).ToList();
            var initExcluded = new List<int>();
            while (initPotential.Count > 0)
            {
                var i = initPotential[0];
                var antiNeigbors = GetAntiNeighbors(lowerAdjacency, i);
                runDataCollection.Add(new RunData()
                {
                    Found = new List<int>() { i },
                    LowerAdjaceny = lowerAdjacency,
                    Excluded = initExcluded.ToList().GetIntersection(antiNeigbors),
                    Potential = initPotential.ToList().GetIntersection(antiNeigbors)
                });
                initPotential.Remove(i);
                initExcluded.Add(i);
            }
            return runDataCollection.AsParallel().Select(rd => BronKerbosch(rd.Found, rd.Potential, rd.Excluded, lowerAdjacency)).SelectMany(x => x);
        }
        private static IEnumerable<List<int>> BronKerbosch(List<int> found, List<int> potential, List<int> excluded, List<int[]> lowerAdjacency)
        {
            if (found.Count > 0) yield return found;
            if (potential.Count == 0 && excluded.Count == 0)
            {
                yield break;
            }

            while (potential.Count > 0)
            {
                var i = potential[0];
                List<int> newFound = GetNewFound(found, i);

                var antiNeighbors = GetAntiNeighbors(lowerAdjacency, i);
                var newPotential = potential.GetIntersection(antiNeighbors);
                var newExcluded = excluded.GetIntersection(antiNeighbors);

                foreach (var solution in BronKerbosch(newFound, newPotential, newExcluded, lowerAdjacency))
                {
                    yield return solution;
                }

                potential.Remove(i);
                excluded.Add(i);
            }


        }

        private static List<int> GetNewFound(List<int> found, int i)
        {
            var newFound = found.Select(x => x).ToList();
            newFound.Add(i);
            return newFound;
        }

        private static List<int> GetIntersection(this List<int> collection, List<int> antiNeighbors)
        {
            return collection.Where(antiNeighbors.Contains).ToList();
        }

        private static List<int> GetAntiNeighbors(List<int[]> lowerAdjacency, int i)
        {
            var antiNeighbors = new List<int>();
            for (int j = 0; j < lowerAdjacency.Count + 1; j++)
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

            return antiNeighbors;
        }
    }

    public class RunData
    {
        public List<int> Found { get; set; } = new List<int>();
        public List<int[]> LowerAdjaceny { get; set; } = new List<int[]>();
        public List<int> Potential { get; set; } = new List<int>();
        public List<int> Excluded { get; set; } = new List<int>();
    }
}
