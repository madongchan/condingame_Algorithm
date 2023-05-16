public class Node
{
    public int Value { get; set; }
    public bool Visited { get; set; }
    public List<Node> Neighbors { get; set; }

    public Node(int value)
    {
        Value = value;
        Visited = false;
        Neighbors = new List<Node>();
    }
}

public class Graph
{
    Dictionary<int, Node> Nodes { get; set; }

    public Graph()
    {
        Nodes = new Dictionary<int, Node> ();
    }

    public void AddNode(int value)
    {
        Nodes.Add(value, new Node(value));
    }

    public void AddEdge(int from, int to)
    {
        Nodes[from].Neighbors.Add(Nodes[to]);
        Nodes[to].Neighbors.Add(Nodes[from]);
    }

    public void BFS(int start, int goal)
    {
        Queue<Node> queue = new Queue<Node>();
        queue.Enqueue(Nodes[start]);
        Nodes[start].Visited = true;

        List<int> path = new List<int>();
        Dictionary<int, int> parent = new Dictionary<int, int>();

        while (queue.Count > 0)
        {
            Node current = queue.Dequeue();

            foreach (Node neighbor in current.Neighbors)
            {
                // 인접 노드가 방문하지 않은 경우
                if (neighbor.Visited == false)
                {
                    // 인접 노드 큐에 삽입
                    queue.Enqueue(neighbor);

                    // 방문 여부 표시
                    Nodes[neighbor.Value].Visited = true;

                    // 부모 노드 저장
                    parent[neighbor.Value] = current.Value;

                    // 목표 노드에 도달한 경우
                    if (neighbor.Value == goal)
                    {
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
        string[] inputs;
        inputs = Console.ReadLine().Split(' ');
        int N = int.Parse(inputs[0]); // the total number of nodes in the level, including the gateways
        int L = int.Parse(inputs[1]); // the number of links
        int E = int.Parse(inputs[2]); // the number of exit gateways
        for (int i = 0; i < L; i++)
        {
            inputs = Console.ReadLine().Split(' ');
            int N1 = int.Parse(inputs[0]); // N1 and N2 defines a link between these nodes
            int N2 = int.Parse(inputs[1]);
        }
        for (int i = 0; i < E; i++)
        {
            int EI = int.Parse(Console.ReadLine()); // the index of a gateway node
        }

        // game loop
        while (true)
        {
            int SI = int.Parse(Console.ReadLine()); // The index of the node on which the Bobnet agent is positioned this turn

            // Write an action using Console.WriteLine()
            // To debug: Console.Error.WriteLine("Debug messages...");


            // Example: 0 1 are the indices of the nodes you wish to sever the link between
            Console.WriteLine("0 1");
        }
    }
}
class Test
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
