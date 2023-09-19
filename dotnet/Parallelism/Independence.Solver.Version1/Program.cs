namespace Independence.Solver.Version1
{
    public class Program
    {
        static void Main(string[] args)
        {
            var path = "..\\..\\..\\..\\..\\..\\data\\problem_30_0,10.txt";

            var file = path;

            var fileName = Path.GetFileName(file);
            //Console.WriteLine($"Start of {fileName}");
            var lowerAdjacency = new List<int[]>();
            
            using var reader = new StreamReader(file);
            using var writer = new StreamWriter("temp.txt");

            var line = reader.ReadLine();
            while (line != null)
            {
                lowerAdjacency.Add(line.Split(' ').Select(int.Parse).ToArray());
                line = reader.ReadLine();
            }
            foreach (var solution in Solver.Solve(lowerAdjacency))
            {
                Console.WriteLine(string.Join(" ", solution));
                //writer.WriteLine(string.Join(" ", solution));
            }
            //Console.WriteLine($"End of {fileName}");
            
        }
    }
}