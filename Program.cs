// https://www.geeksforgeeks.org/dsa/dijkstras-shortest-path-algorithm-greedy-algo-7/
using System.Diagnostics;

class Program
{
    static void addEdge(List<List<(int, int)>> list, int start, int end, int weight)
    {
        list[start].Add((end, weight));
        list[end].Add((start, weight));
    }

    static int modifiedDjikstra(List<List<(int, int)>> adj, int start, int target, int k)
    {
        PriorityQueue<(int node, int usedZeroEdges), int> pq = new(); // MinHeap, lower value takes prio
        // storing tuples in the pq containing the node & the number of zero-weight edges used at that given point.

        int n = adj.Count;
        int[,] dist = new int[n, k + 1];
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j <= k; j++)
            {
                dist[i, j] = int.MaxValue;
            }
        }

        dist[start, 0] = 0;
        pq.Enqueue((start, 0), 0);

        int bestTarget = int.MaxValue;

        while (pq.Count > 0)
        {
            pq.TryDequeue(out (int node, int usedZeroEdges) state, out int d);
            int u = state.node; // current node
            int usedZero = state.usedZeroEdges; // used zeroes at this point

            if (d > dist[u, usedZero]) continue;
            foreach ((int, int) edge in adj[u])
            {
                (int v, int weight) = edge;
                int nextUsedZero = usedZero + (weight == 0 ? 1 : 0);
                if (nextUsedZero > k) continue;

                int nd = d + weight;
                if (nd < dist[v, nextUsedZero])
                {
                    dist[v, nextUsedZero] = nd;
                    pq.Enqueue((v, nextUsedZero), nd);
                    if (v == target && nd < bestTarget) bestTarget = nd;
                }
            }
        }

        if (bestTarget == int.MaxValue) return -1;
        return bestTarget;
    }

    static void Main(string[] args)
    {

        string[] lines = File.ReadAllLines(args[0]);
        string[] header = lines[0].Split(' ');

        int n = int.Parse(header[0]); // liczba hypermiast
        int m = int.Parse(header[1]); // liczba korytarzy
        int k = int.Parse(header[2]); // liczba max. użyć korytarzy.

        Stopwatch sw = new();
        sw.Start();
        List<List<(int, int)>> adj = new List<List<(int, int)>>(n);
        for (int i = 0; i < n; i++)
        {
            adj.Add(new List<(int, int)>());
        }

        for (int i = 1; i <= m; i++)
        {
            string[] split = lines[i].Split();
            int start = int.Parse(split[0]) - 1;
            int end = int.Parse(split[1]) - 1;
            int len = int.Parse(split[2]);


            addEdge(adj, start, end, len);
        }

        int shortest = modifiedDjikstra(adj, 0, n - 1, k);
        sw.Stop();
        System.Console.WriteLine($"czas:\n   {sw.ElapsedMilliseconds}ms");
        System.Console.WriteLine($"wynik:\n   1 --> {n}: {shortest}");
    }


}