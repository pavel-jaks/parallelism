namespace Independence.Solver.Version3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var path = "..\\..\\..\\..\\..\\..\\data\\problem_30_0,10.txt";

            var file = path;

            var fileName = Path.GetFileName(file);
            
            var lowerAdjacency = new List<int[]>();

            using var reader = new StreamReader(file);

            var line = reader.ReadLine();
            while (line != null)
            {
                lowerAdjacency.Add(line.Split(' ').Select(int.Parse).ToArray());
                line = reader.ReadLine();
            }
            Solver.Solve(lowerAdjacency);
        }
    }
}