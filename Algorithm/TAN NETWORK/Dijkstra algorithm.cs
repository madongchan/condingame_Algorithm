using System;
using System.Linq;
using System.Collections.Generic;

namespace Dijkstra
{
    public class NodeInformation
    {
        public string ID; // 노드의 식별자
        public string Name; // 노드의 이름
        public double Latitude, Longitude; // 노드의 위도와 경도
        public int StopType; // 정류장 유형
        public List<NodeInformation> Neighbors { get; set; } // 인접한 노드들의 리스트

        public NodeInformation(string ID_, string Name_, double Latitude_, double Longitude_, int StopType_)
        {
            ID = ID_;
            Name = Name_;
            Latitude = Latitude_;
            Longitude = Longitude_;
            StopType = StopType_;
        }
    }

    public class Graph
    {
        int V; // 노드의 수
        Dictionary<string, List<(string vertex, double distance)>> adjList; // 인접 리스트

        public Graph(int vertices)
        {
            V = vertices;
            adjList = new Dictionary<string, List<(string, double)>>();
        }

        public void AddNode(string value)
        {
            adjList.Add(value, new List<(string vertex, double distance)>());
        }

        public void AddEdge(string start, string end, double weight)
        {
            adjList[start].Add((end, weight));
        }

        public List<string> Dijkstra(string start, string end)
        {
            Dictionary<string, double> distance = new Dictionary<string, double>(); // 노드 n의 가중치를 저장하는 딕셔너리

            Dictionary<string, string> previous = new Dictionary<string, string>(); // 최단 경로에서 각 노드의 이전 노드를 저장하는 딕셔너리


            SortedSet<(string vertex, double distance)> priorityQueue = new SortedSet<(string vertex, double distance)>
                (Comparer<(string vertex, double distance)>.Create((a, b) => a.distance.CompareTo(b.distance)));

            foreach (var item in adjList)
            {
                distance[item.Key] = double.MaxValue; // 모든 노드의 거리를 무한대로 초기화
                previous[item.Key] = string.Empty; // 모든 노드의 이전 노드를 빈 문자열로 초기화
            }

            distance[start] = 0.0; // 시작 노드의 거리를 0으로 설정

            priorityQueue.Add((start, 0.0)); // 시작 노드를 우선순위 큐에 추가

            while (priorityQueue.Count > 0)
            {
                (string current, double Dist) = priorityQueue.Min; // 우선순위 큐에서 거리가 가장 작은 노드를 선택
                priorityQueue.Remove(priorityQueue.Min); // 선택된 노드를 우선순위 큐에서 제거

                if (Dist > distance[current])
                {
                    continue; // 선택된 노드의 거리가 이미 더 작은 값으로 업데이트된 경우 건너뜀
                }

                foreach ((string neighbor, double weight) in adjList[current]) // 선택된 노드의 인접 노드들
                {
                    double newDist = distance[current] + weight; // 선택된 노드를 거쳐서 인접 노드로 가는 새로운 거리 계산
                    if (newDist < distance[neighbor]) // 새로운 거리가 더 작은 경우
                    {
                        distance[neighbor] = newDist; // 인접 노드의 거리 업데이트
                        previous[neighbor] = current; // 인접 노드의 이전 노드 업데이트
                        priorityQueue.Add((neighbor, newDist)); // 인접 노드를 우선순위 큐에 추가
                    }
                }
            }

            List<string> shortestPath = new List<string>(); // 최단 경로를 저장할 리스트
            if (previous[end] == string.Empty && !(start == end)) // 도착 노드에 이르는 경로가 없는 경우 (불가능한 경우)
            {
                shortestPath = new List<string>(); // 빈 리스트 반환
            }
            else
            {
                string current = end;
                while (current != string.Empty)
                {
                    shortestPath.Insert(0, current); // 최단 경로에 현재 노드 추가 (맨 앞에 삽입하여 역순으로 저장)
                    current = previous[current]; // 이전 노드로 이동
                }
            }

            return shortestPath; // 최단 경로 반환
        }
    }

    class Solution
    {
        static void Main(string[] args)
        {
            string startPoint = Console.ReadLine(); // 시작 지점 입력
            string endPoint = Console.ReadLine(); // 도착 지점 입력
            int N = int.Parse(Console.ReadLine()); // 정류장 수 입력

            List<NodeInformation> Stops = new List<NodeInformation>(); // 정류장 리스트 생성
            Graph graph = new Graph(N); // 그래프 객체 생성

            for (int i = 0; i < N; i++)
            {
                string[] stopName = Console.ReadLine().Split(','); // 정류장 정보 입력 (쉼표로 구분된 값들)
                string id = stopName[0]; // 정류장 식별자
                double lat = double.Parse(stopName[3]); // 위도
                double lon = double.Parse(stopName[4]); // 경도
                string name = stopName[1].Replace("\"", string.Empty); // 정류장 이름 (따옴표 제거)
                int stopType = int.Parse(stopName[7]); // 정류장 유형

                graph.AddNode(id); // Astar 객체에 정류장 추가

                Stops.Add(new NodeInformation(id, name, lat, lon, stopType)); // 정류장 리스트에 정류장 추가
            }
            int M = int.Parse(Console.ReadLine()); // 경로 수 입력
            for (int i = 0; i < M; i++)
            {
                string[] route = Console.ReadLine().Split(' '); // 경로 정보 입력 (공백으로 구분된 값들)
                double weight = CalcDist(Stops.Find(s => s.ID == route[0]), Stops.Find(s => s.ID == route[1])); // 경로의 가중치 계산

                graph.AddEdge(route[0], route[1], weight);
            }

            List<string> shortestPath = graph.Dijkstra(startPoint, endPoint); // 최단 경로 탐색

            if (shortestPath.Count <= 0) // 최단 경로가 없는 경우 (불가능한 경우)
            {
                Console.WriteLine("IMPOSSIBLE"); // "IMPOSSIBLE" 출력
            }
            else
            {
                foreach (var vertex in shortestPath) // 최단 경로 출력
                {
                    string name = Stops.Find(s => s.ID == vertex)?.Name; // 노드 식별자에 해당하는 정류장 이름 가져오기
                    Console.WriteLine(name); // 정류장 이름 출력
                }
            }
        }

        static double CalcDist(NodeInformation node1, NodeInformation node2)
        {
            double x = (node2.Longitude - node1.Longitude) * Math.Cos((node1.Latitude + node2.Latitude) / 2); // 경도 차이 계산
            double y = (node2.Latitude - node1.Latitude); // 위도 차이 계산

            return DegToRad(Math.Sqrt(x * x + y * y) * 6371); // 거리 계산하여 반환
        }

        static double DegToRad(double degrees)
        {
            return (degrees * Math.PI / 180); // 도를 라디안으로 변환
        }
    }
}
