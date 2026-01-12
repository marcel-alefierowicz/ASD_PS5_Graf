using System;
using System.Collections.Generic;
using System.IO;

public class Corridor
{
    public int start { get; set; }
    public int end { get; set; }
    public int length { get; set; }
    public Corridor(int start, int end, int length)
    {
        this.start = start;
        this.end = end;
        this.length = length;
    }
}

class Program
{
    static void Main(string[] args)
    {

        string[] lines = File.ReadAllLines(args[0]);
        string[] header = lines[0].Split(' ');

        int n, m, k;

        n = int.Parse(header[0]); // liczba hypermiast
        m = int.Parse(header[1]); // liczba korytarzy
        k = int.Parse(header[2]); // liczba max. użyć korytarzy.

        // each index is a city and contains corridors ending in dat city??
        List<Corridor>[] evil = new List<Corridor>[n];
        for (int idx = 0; idx < n; idx++)
        {
            evil[idx] = new List<Corridor>();
        }
        // parsing korytarzy
        for (int i = 1; i <= m; i++)
        {
            string[] split = lines[i].Split();
            int start = int.Parse(split[0]);
            int end = int.Parse(split[1]);
            int len = int.Parse(split[2]);
            Corridor cor = new Corridor(start, end, len);

            evil[start - 1].Add(cor);
        }

        int j = 0;
        foreach (List<Corridor> whatever in evil)
        {
            System.Console.WriteLine($"evil {j + 1}");
            foreach (Corridor cor in whatever)
            {
                System.Console.WriteLine($"{cor.start}, {cor.end}, {cor.length}");
            }
            j++;
        }

    }
}