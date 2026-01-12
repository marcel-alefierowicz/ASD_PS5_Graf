public class Corridor
{
    public int end { get; set; }
    public int length { get; set; }
    public Corridor(int end, int length)
    {
        this.end = end;
        this.length = length;
    }
}

class Program
{

    static void printCorridors(List<Corridor>[] corridors)
    {
        int j = 0;
        foreach (List<Corridor> whatever in corridors)
        {
            System.Console.WriteLine($"hypercity {j}");
            foreach (Corridor cor in whatever)
            {
                System.Console.WriteLine($"    target: {cor.end},\n    cost: {cor.length}\n");
            }
            j++;
        }
    }
    static void Main(string[] args)
    {

        string[] lines = File.ReadAllLines(args[0]);
        string[] header = lines[0].Split(' ');

        int n, m, k;

        n = int.Parse(header[0]); // liczba hypermiast
        m = int.Parse(header[1]); // liczba korytarzy
        k = int.Parse(header[2]); // liczba max. użyć korytarzy.

        List<Corridor>[] routes = new List<Corridor>[n];

        for (int i = 0; i < n; i++)
        {
            routes[i] = new List<Corridor>();
        }

        for (int i = 1; i <= m; i++)
        {
            string[] split = lines[i].Split();
            int start = int.Parse(split[0]);
            int end = int.Parse(split[1]);
            int len = int.Parse(split[2]);
            Corridor cor = new Corridor(end, len);

            routes[start - 1].Add(cor);
        }

        printCorridors(routes);
    }
}