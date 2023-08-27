namespace ThreeDimensionalMatch.ProblemGenerator
{
    public class Program
    {
        static void Main(string[] args)
        {
            for (int i = 3; i < 1001;  i++)
            {
                Console.WriteLine(i);
                CreateAndWriteProblem(i);
                Console.WriteLine($"Done doing a {i}-vertex problem.");
            }
        }

        static void CreateAndWriteProblem(int vertexCount)
        {
            var hyperEdgeCount = 0;
            var edgeList = new List<Tuple<int, int, int>>();
            for (int i = 0; i < vertexCount; i++)
            {
                for (int j = i + 1; j < vertexCount; j++)
                {
                    for (var k = j + 1; k < vertexCount; k++)
                    {
                        if (Random.Shared.Next() % 2 == 0)
                        {
                            edgeList.Add(new Tuple<int, int, int>(i, j, k ));
                            hyperEdgeCount++;
                        }
                    }
                }
            }
            if (hyperEdgeCount == 0) return;
            var matchCount = Random.Shared.Next() % hyperEdgeCount + 1;
            var path = $"..\\..\\..\\..\\..\\..\\data\\problem_{vertexCount}_{hyperEdgeCount}_{matchCount}.txt";
            using var writer = new StreamWriter(path);
            writer.WriteLine(matchCount);
            writer.WriteLine(vertexCount);
            foreach (var edge in edgeList)
            {
                writer.WriteLine($"{edge.Item1} {edge.Item2} {edge.Item3}");
            }
        }
    }
}