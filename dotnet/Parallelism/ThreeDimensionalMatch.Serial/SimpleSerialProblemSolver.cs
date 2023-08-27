using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreeDimensionalMatch.Common;

namespace ThreeDimensionalMatch.Serial
{
    public class SimpleSerialProblemSolver : IProblemSolver
    {
        public ProblemSolverType ProblemSolverType => ProblemSolverType.Serial;

        public string Name => "Simple Serial 3DM Problem Solver";

        public string Description => "Brute force over all possible hyper edge choice of size given by the problem.";

        public Statistic Solve(IProblem problem)
        {
            var stopwatch = Stopwatch.StartNew();
            
            var result = new Statistic();
            result.TotalParallelTime = 0;
            result.ProblemSolverName = Name;
            result.ProblemSolverDescription = Description;
            result.ProblemSolverType = ProblemSolverType;
            result.VerticesCount = problem.HyperGraph.Vertices.Length;
            result.EdgesCount = problem.HyperGraph.Edges.Length;
            result.MatchCount = problem.MatchCount;

            var tempSetOfVertices = new HashSet<IVertex>();
            foreach (var edgeConfiguration in GetEdgesConfiguration(result.EdgesCount, problem.MatchCount))
            {
                tempSetOfVertices.Clear();
                var brokenConfig = false;
                for (int i = 0; i < edgeConfiguration.Length; i++)
                {
                    if (edgeConfiguration[i])
                    {
                        var broken = false;
                        var edge = problem.HyperGraph.Edges[i];
                        foreach (var vertex in edge.Vertices)
                        {
                            if (tempSetOfVertices.Add(vertex)) continue;
                            broken = true;
                            brokenConfig = true;
                            break;
                        }
                        if (broken) break;
                    }
                }
                if (!brokenConfig)
                {
                    problem.Solution = true;
                    break;
                }
            }
            if (problem.Solution != null && problem.Solution != true) problem.Solution = false;


            stopwatch.Stop();
            result.TotalSerialTime = stopwatch.Elapsed.TotalSeconds;
            result.TotalTimeInSeconds = result.TotalSerialTime;
            return result;
        }

        private static IEnumerable<bool[]> GetEdgesConfiguration(int edgeCount, int matchCount)
        {
            if (matchCount == 0)
            {
                var bools = new bool[edgeCount];
                for (var i = 0; i < edgeCount; i++)
                {
                    bools[i] = false;
                }
                yield return bools;
                yield break;
            }
            if (edgeCount == matchCount)
            {
                var bools = new bool[edgeCount];
                for (var i = 0; i < edgeCount; i++)
                {
                    bools[i] = true;
                }
                yield return bools;
                yield break;
            }

            foreach (var bools in GetEdgesConfiguration(edgeCount - 1, matchCount - 1))
            {
                var allBools = new bool[edgeCount];
                allBools[0] = true;
                for (var i = 1; i < edgeCount; i++)
                {
                    allBools[i] = bools[i - 1];
                }
                yield return allBools;
            }
            foreach (var bools in GetEdgesConfiguration(edgeCount - 1, matchCount))
            {
                var allBools = new bool[edgeCount];
                allBools[0] = false;
                for (var i = 1; i < edgeCount; i++)
                {
                    allBools[i] = bools[i - 1];
                }
                yield return allBools;
            }
        }
    }
}
