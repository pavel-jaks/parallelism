using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreeDimensionalMatch.Common
{
    public class Problem : IProblem
    {
        public Problem(string pathToProblemInstance)
        {
            using var reader = new StreamReader(pathToProblemInstance);
            var firstLine = reader.ReadLine();
            if (int.TryParse(firstLine?.Trim() ?? "", out var matchCount))
            {
                MatchCount = matchCount;
            }
            else
            {
                throw new ArgumentException("File in not appropriate format");
            }
            var secondLine = reader.ReadLine() ?? "";
            if (int.TryParse(secondLine.Trim(), out var verticesCount))
            {
                var edgeLine = reader.ReadLine();
                var edgeList = new List<Tuple<int, int, int>>();
                try
                {
                    while (edgeLine != null)
                    {
                        var stringArray = edgeLine?.Trim().Split(" ");
                        var intTuple = new Tuple<int, int, int>(
                            int.Parse(stringArray?[0] ?? ""),
                            int.Parse(stringArray?[1] ?? ""),
                            int.Parse(stringArray?[2] ?? "")
                        );
                        edgeList.Add(intTuple);
                        edgeLine = reader.ReadLine();
                    }
                }
                catch (FormatException)
                {
                    throw new ArgumentException("File in not appropriate format");
                }
                HyperGraph = new HyperGraph(verticesCount, edgeList);
            }
            else
            {
                throw new ArgumentException("File in not appropriate format");
            }
        }

        public IHyperGraph HyperGraph { get; init; }

        public int MatchCount { get; init; }

        public bool? Solution { get; set; } = null;
    }
}
