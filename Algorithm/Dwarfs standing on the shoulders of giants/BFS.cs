using System;
using System.Collections.Generic;

class Graph
{
    private int V;
    private List<int>[] adj;

    public Graph(int v)
    {
        V = v;
        adj = new List<int>[V];

        for (int i = 0; i < V; i++)
        {
            adj[i] = new List<int>();
        }
    }

    public void AddEdge(int u, int v)
    {
        adj[u].Add(v);
    }

    public int Height()
    {
        int maxHeight = 0;

        for (int i = 0; i < V; i++)
        {
            int height = HeightUtil(i);
            maxHeight = Math.Max(maxHeight, height);
        }

        return maxHeight;
    }

    private int HeightUtil(int v)
    {
        int maxHeight = 0;

        foreach (int adjNode in adj[v])
        {
            int height = HeightUtil(adjNode);
            maxHeight = Math.Max(maxHeight, height);
        }

        return maxHeight + 1;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Graph g = new Graph(7);

        g.AddEdge(0, 1);
        g.AddEdge(0, 2);
        g.AddEdge(1, 3);
        g.AddEdge(1, 4);
        g.AddEdge(2, 5);
        g.AddEdge(2, 6);

        int height = g.Height();

        Console.WriteLine("Graph height: " + height);
    }
}
