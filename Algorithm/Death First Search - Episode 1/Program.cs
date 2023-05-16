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
    public Dictionary<int, Node> Nodes { get; set; }

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

    public List<int> Find_shorted_path(int start, int goal)
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
        return path;
    }


}

class Player
{
    static void Main(string[] args)
    {
        Graph graph = new Graph();

        string[] inputs;
        inputs = Console.ReadLine().Split(' ');
        int N = int.Parse(inputs[0]); // the total number of nodes in the level, including the gateways
        for (int i = 0; i < N; i++)
        {
            graph.AddNode(i);
        }
        int L = int.Parse(inputs[1]); // the number of links
        int E = int.Parse(inputs[2]); // the number of exit gateways
        int[] EI = new int[E]; // the array of gateway node
        for (int i = 0; i < L; i++)
        {
            inputs = Console.ReadLine().Split(' ');
            int N1 = int.Parse(inputs[0]); // N1 and N2 defines a link between these nodes
            int N2 = int.Parse(inputs[1]);
            graph.AddEdge(N1, N2);
        }
        for (int i = 0; i < E; i++)
        {
            EI[i] = int.Parse(Console.ReadLine()); // the index of a gateway node
        }

        // game loop
        while (true)
        {
            // 게임 루프 시작 시 초기화
            foreach (Node node in graph.Nodes.Values)
            {
                node.Visited = false;
            }
            int SI = int.Parse(Console.ReadLine()); // The index of the node on which the Bobnet agent is positioned this turn

            List<int> shorted_path = graph.Find_shorted_path(SI, EI[0]);
            int cut_node1 = shorted_path[shorted_path.Count - 2];
            int cut_node2 = shorted_path[shorted_path.Count - 1];
            Console.WriteLine($"{cut_node1} {cut_node2}");
        }
    }
}
