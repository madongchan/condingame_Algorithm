public class Node
{
    public int Value { get; set; }
    public List<Node> Neighbors { get; set; }

    public Node(int value)
    {
        Value = value;
        Neighbors = new List<Node>();
    }
}

public class Graph
{
    public List<Node> Nodes { get; set; }

    public Graph()
    {
        Nodes = new List<Node>();
    }

    public void AddNode(int value)
    {
        Nodes.Add(new Node(value));
    }

    public void AddEdge(int from, int to)
    {
        Nodes[from].Neighbors.Add(Nodes[to]);
        Nodes[to].Neighbors.Add(Nodes[from]);
    }

    public void BFS(int start, int goal)
    {
        int maxValue = Nodes.Max(node => node.Value);
        bool[] visited = new bool[maxValue + 1];
        Queue<Node> queue = new Queue<Node>();
        queue.Enqueue(Nodes[start]);
        visited[start] = true;

        List<int> path = new List<int>();
        Dictionary<int, int> parent = new Dictionary<int, int>();

        while (queue.Count > 0)
        {
            Node current = queue.Dequeue();

            foreach (Node neighbor in current.Neighbors)
            {
                if (!visited[neighbor.Value])
                {
                    queue.Enqueue(neighbor);
                    visited[neighbor.Value] = true;
                    parent[neighbor.Value] = current.Value;

                    if (neighbor.Value == goal)
                    {
                        // 목표 노드에 도달한 경우 경로 기록을 종료
                        break;
                    }
                }
            }
        }

        // 최단 경로 구성
        int node = goal;
        while (node != start)
        {
            path.Insert(0, node);
            node = parent[node];
        }
        path.Insert(0, start);

        for (int i = 0; i < path.Count - 1; i++)
        {
            Console.Error.Write($"{path[i]} -> ");
        }

        // 마지막 노드 출력
        Console.Error.Write(path[path.Count - 1]);

    }


}

class Player
{
    static void Main(string[] args)
    {
        Graph graph = new Graph();

        // 그래프에 노드 추가
        graph.AddNode(0);
        graph.AddNode(1);
        graph.AddNode(2);
        graph.AddNode(3);

        // 그래프에 간선 추가
        graph.AddEdge(0, 1);
        graph.AddEdge(0, 2);
        graph.AddEdge(2, 3);

        // BFS 수행
        Console.WriteLine("BFS Path:");
        graph.BFS(0, 3);
    }
}
