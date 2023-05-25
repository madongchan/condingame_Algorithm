using System;
using System.Collections.Generic;

namespace myCode
{
    public class Node
    {
        public int Value { get; set; }
        public bool Visited { get; set; }
        public List<Node> Neighbors { get; set; }

        public Node(int value, bool visited)
        {
            Value = value;
            Visited = false;
            Neighbors = new List<Node>();
        }
    }

    public class Graph
    {
        public Dictionary<int, Node> Nodes { get; set; }  // 그래프의 노드들을 저장하는 딕셔너리

        public Graph()
        {
            Nodes = new Dictionary<int, Node>();
        }

        public void AddNode(int value)
        {
            Nodes.Add(value, new Node(value, false));  // 새로운 노드 추가
        }

        public void AddEdge(int from, int to)
        {
            Nodes[from].Neighbors.Add(Nodes[to]);  // 두 노드 사이에 간선 추가
            Nodes[to].Neighbors.Add(Nodes[from]);  // 양방향 그래프
        }

        public void RemoveEdge(int from, int to)
        {
            Nodes[from].Neighbors.Remove(Nodes[to]); // 두 노드 사이에 간선 제거
            Nodes[to].Neighbors.Remove(Nodes[from]);
        }

        public bool IsExitDisconnected(int exitNode) //출구와 연결된 노드가 다 끊겼는지 확인하는 함수
        {
            return Nodes[exitNode].Neighbors.Count == 0;
        }
        public List<int> FindShortestPath(int start, int goal)
        {
            Queue<Node> queue = new Queue<Node>();  // BFS에 사용할 큐 생성
            queue.Enqueue(Nodes[start]);  // 시작 노드 큐에 삽입
            Nodes[start].Visited = true;  // 시작 노드 방문 표시

            Dictionary<int, int> parent = new Dictionary<int, int>();  // 각 노드의 부모 노드를 저장하는 딕셔너리

            while (queue.Count > 0)
            {
                Node current = queue.Dequeue();  // 큐에서 노드 추출

                foreach (Node neighbor in current.Neighbors)  // 현재 노드의 인접한 노드들에 대해
                {
                    // 인접 노드가 방문하지 않은 경우
                    if (neighbor.Visited == false)
                    {
                        queue.Enqueue(neighbor);  // 인접 노드 큐에 삽입
                        Nodes[neighbor.Value].Visited = true;  // 인접 노드 방문 표시
                        parent[neighbor.Value] = current.Value;  // 부모 노드 저장
                    }
                }
            }

            // 최단 경로 구성
            List<int> path = new List<int>();
            int node = goal;
            while (node != start)
            {
                path.Insert(0, node);  // 경로의 시작점에 노드 삽입
                node = parent[node];  // 부모 노드로 이동
            }
            path.Insert(0, start);  // 시작점 삽입

            // 최단 경로를 보여주는 함수 호출
            ShortPathWrite(path);

            return path;  // 최단 경로 반환
        }

        public void ShortPathWrite(List<int> path)
        {
            Console.Error.Write($"최단 경로: ");
            for (int i = 0; i < path.Count - 1; i++)
            {
                Console.Error.Write($"{path[i]} -> ");
            }

            // 마지막 노드 출력
            Console.Error.WriteLine(path[path.Count - 1]);
        }
    }

    class Player
    {
        static void Main(string[] args)
        {
            Graph graph = new Graph();

            string[] inputs = Console.ReadLine().Split(' ');
            int N = int.Parse(inputs[0]); // the total number of nodes in the level, including the gateways
            int L = int.Parse(inputs[1]); // the number of links
            int E = int.Parse(inputs[2]); // the number of exit gateways

            for (int i = 0; i < N; i++)
            {
                graph.AddNode(i);
            }

            for (int i = 0; i < L; i++)
            {
                inputs = Console.ReadLine().Split(' ');
                int N1 = int.Parse(inputs[0]); // N1 and N2 defines a link between these nodes
                int N2 = int.Parse(inputs[1]);
                graph.AddEdge(N1, N2);
            }

            List<int> EI = new List<int>(); // the array of gateway node
            for (int i = 0; i < E; i++)
            {
                EI.Add(int.Parse(Console.ReadLine())); // the index of a gateway node
                
            }

            // game loop
            while (true)
            {
                int SI = int.Parse(Console.ReadLine()); // The index of the node on which the Bobnet agent is positioned this turn

                //출구가 여러개 일 때, 가장 짧은 경로를 찾아서 저장
                List<int> ShortestPath = null;
                foreach (int ei in EI)
                {
                    foreach (Node node in graph.Nodes.Values)
                    {
                        node.Visited = false;
                    }
                    var path = graph.FindShortestPath(SI, ei);
                    
                    if (ShortestPath == null || path.Count < ShortestPath.Count)
                    {
                        ShortestPath = path;
                    }
                }
                    
                int C1 = ShortestPath[ShortestPath.Count - 2];
                int C2 = ShortestPath[ShortestPath.Count - 1];
                Console.WriteLine($"{C1} {C2}");

                // 출력한 간선 제거
                graph.RemoveEdge(C1, C2);

                // 출구와 연결된 노드가 하나도 없으면 그 출구는 완전히 막혀있다고 판단
                // 고려 사항에서 제거
                if (graph.IsExitDisconnected(C2))
                {
                    EI.Remove(C2);
                }
            }
        }
    }
}

