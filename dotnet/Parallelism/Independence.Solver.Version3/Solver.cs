using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Independence.Solver.Version3
{
    public static class Solver
    {
        public static void Solve(List<int[]> lowerAdjacency)
        {
            var vertexCount = lowerAdjacency.Count + 1;

            var initPotential = Enumerable.Range(0, vertexCount).ToList();
            var initExcluded = new List<int>();
            var initFound = new List<int>();

            var concurrentQueue = new ConcurrentQueue<RunData>();
            concurrentQueue.Enqueue(
                new RunData()
                { 
                    Found = initFound,
                    Potential = initPotential,
                    Excluded = initExcluded,
                    LowerAdjacency = lowerAdjacency
                }
            );

            ThreadPool.QueueUserWorkItem((x) => ProccessItem(concurrentQueue));

            while (!concurrentQueue.IsEmpty) Thread.Sleep(50);
        }

        private static void ProccessItem(ConcurrentQueue<RunData> concurrentQueue)
        {
            if (concurrentQueue.TryDequeue(out var runData))
            {
                //if (runData.Found.Count > 0) Console.WriteLine(string.Join(' ', runData.Found));
                if (runData.Potential.Count == 0 && runData.Excluded.Count == 0)
                {
                    Console.WriteLine(string.Join(' ', runData.Found));
                    return;
                }
                while (runData.Potential.Count > 0)
                {
                    var i = runData.Potential[0];
                    var newFound = GetNewFound(runData.Found, i);
                    var antiNeighbors = GetAntiNeighbors(runData.LowerAdjacency, i);
                    var newPotential = runData.Potential.GetIntersection(antiNeighbors);
                    var newExcluded = runData.Excluded.GetIntersection(antiNeighbors);
                    concurrentQueue.Enqueue(
                        new RunData()
                        {
                            Found = newFound,
                            Potential = newPotential,
                            Excluded = newExcluded,
                            LowerAdjacency = runData.LowerAdjacency,
                        }
                    );
                    ThreadPool.QueueUserWorkItem((x) => ProccessItem(concurrentQueue));
                    runData.Potential.Remove(i);
                    runData.Excluded.Add(i);
                }
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
        public List<int[]> LowerAdjacency { get; set; } = new List<int[]>();
        public List<int> Potential { get; set; } = new List<int>();
        public List<int> Excluded { get; set; } = new List<int>();
    }
}
