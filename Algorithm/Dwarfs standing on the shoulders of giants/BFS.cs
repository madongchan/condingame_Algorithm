using System;
using System.Collections.Generic;

class Graph
{
    private int V; // 노드 수
    private List<int>[] adj; // 인접 리스트

    public Graph(int v)
    {
        V = v;
        adj = new List<int>[V];

        // 인접 리스트 초기화
        for (int i = 0; i < V; i++)
        {
            adj[i] = new List<int>();
        }
    }

    // 노드 u와 노드 v를 연결하는 간선 추가
    public void AddEdge(int u, int v)
    {
        adj[u].Add(v);
        //adj[v].Add(u); // 무방향 그래프일 경우 양방향으로 추가해야 함
    }

    // 노드 v와 연결된 모든 노드들 반환
    public List<int> Adj(int v)
    {
        return adj[v];
    }
}

class Program
{
    static void Main(string[] args)
    {
        Graph g = new Graph(5);

        g.AddEdge(0, 1);
        g.AddEdge(0, 4);
        g.AddEdge(1, 2);
        g.AddEdge(1, 3);
        g.AddEdge(1, 4);
        g.AddEdge(2, 3);
        g.AddEdge(3, 4);

        // 노드 1과 연결된 모든 노드 출력
        List<int> adjNodes = g.Adj(0);
        Console.Write("Node 1's adjacent nodes: ");
        foreach (int n in adjNodes)
        {
            Console.Write(n + " ");
        }
    }
}
