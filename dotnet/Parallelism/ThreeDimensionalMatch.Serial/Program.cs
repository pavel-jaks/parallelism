using System.Text.Json;
using ThreeDimensionalMatch.Common;

namespace ThreeDimensionalMatch.Serial
{
    public class Program
    {
        static void Main(string[] args)
        {
            var dir = "..\\..\\..\\..\\..\\..\\data\\";
            var counter = 0;
            string[] names = { "problem_4_4_4.txt", "problem_5_6_6.txt", "problem_6_14_13.txt",
                "problem_7_17_14.txt", "problem_8_30_21.txt", "problem_9_52_4.txt" };

            foreach (var path in names)
            {
                IProblem problem = new Problem(dir + path);
                IProblemSolver problemSolver = new SimpleSerialProblemSolver();
                var result = problemSolver.Solve(problem);
                var json = JsonSerializer.Serialize(result);
                using var writer = new StreamWriter(dir + $"SimpleSerial\\result_{problem.HyperGraph.Vertices.Length}.json");
                writer.Write(json);
                Console.WriteLine($"Done {problem.HyperGraph.Vertices.Length}-vertex problem.");

                counter++;
                if (counter == 20) break;
            }
        }
    }
}