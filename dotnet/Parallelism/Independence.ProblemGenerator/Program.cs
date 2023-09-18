namespace Independence.ProblemGenerator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            for (int i = 3; i < 25; i++)
            {
                Console.WriteLine(i);
                CreateAndWriteProblems(i);
                Console.WriteLine($"Done doing a {i}-vertex problem.");
            }
        }

        private static void CreateAndWriteProblems(int vertexCount)
        {
            var maximum_occupation = (double)(vertexCount * Math.Min(3, vertexCount - 1)) / 2 / (vertexCount * (vertexCount - 1) / 2);
            for (var i = 0.1; i <= maximum_occupation + 0.1; i += 0.1)
            {
                CreateAndWriteProblem(vertexCount, i);
            }
        }

        private static void CreateAndWriteProblem(int vertexCount, double greedy)
        {
            int[] degrees = new int[vertexCount];
            for (int i = 0; i < vertexCount; i++)
            {
                degrees[i] = 0;
            }
            var adjacency_matrix = new List<int[]>();
            for (int i = 1; i < vertexCount; i++)
            {
                adjacency_matrix.Add(new int[i]);
                for (int j = 0; j < i; j++)
                {
                    if (degrees[i] == 3 || degrees[j] == 3)
                    {
                        adjacency_matrix[i - 1][j] = 0;
                    }
                    else
                    {
                        var toAddOrNotToAdd = Random.Shared.NextDouble() < greedy;
                        if (toAddOrNotToAdd)
                        {
                            adjacency_matrix[i - 1][j] = 1;
                            degrees[i]++;
                            degrees[j]++;
                        }
                        else
                        {
                            adjacency_matrix[i - 1][j] = 0;
                        }
                    }
                }
            }
            using var writer = new StreamWriter($"..\\..\\..\\..\\..\\..\\data\\problem_{vertexCount}_{greedy:0.00}.txt");
            foreach (var adjacency_row in adjacency_matrix)
            {
                writer.WriteLine(string.Join(" ", adjacency_row));
            }
        }
    }
}