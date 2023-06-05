using System;
using System.Linq;
using System.Collections.Generic;
using static System.Formats.Asn1.AsnWriter;

namespace graph
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


        public List<string> Astar(string start, string end, List<NodeInformation> Stops)
        { 
            double Heuristic(string cur, string end)
            {
                NodeInformation currentNode = Stops.Find(s => s.ID == cur); // 현재 노드
                NodeInformation endNode = Stops.Find(s => s.ID == end); // 도착 노드

                // 직선 거리(유클리드 거리)를 사용한 휴리스틱 계산
                double dx = endNode.Latitude - currentNode.Latitude;
                double dy = endNode.Longitude - currentNode.Longitude;
                double approximateWeight = Math.Sqrt(dx * dx + dy * dy);

                // 다른 노드와 이어져 있지 않다면 무한대를 반환
                if (adjList[cur].Count <= 0)
                {
                    return double.MaxValue;
                }

                return approximateWeight;
            }



            Dictionary<string, double> G = new Dictionary<string, double>(); // 출발 노드로부터 노드 n까지의 경로 가중치 합
            Dictionary<string, double> H = new Dictionary<string, double>(); // 노드 n에서 도착 노드까지의 예상 거리의 합
            Dictionary<string, double> F = new Dictionary<string, double>(); // F = G + H

            Dictionary<string, string> previous = new Dictionary<string, string>(); // 최단 경로에서 각 노드의 이전 노드를 저장하는 딕셔너리

            //Open List
            SortedSet<(string vertex, double distance)> openList = new SortedSet<(string vertex, double distance)>
                (Comparer<(string vertex, double distance)>.Create((a, b) => a.distance.CompareTo(b.distance)));

            //Closed List
            HashSet<string> closedList = new HashSet<string>();


            //Dictionary init
            foreach (var item in adjList)
            {
                G[item.Key] = double.MaxValue; // 모든 노드의 거리를 무한대로 초기화
                H[item.Key] = Heuristic(item.Key, end); // 노드 n에서 도착 노드까지의 예상 거리를 초기화
                previous[item.Key] = string.Empty; // 모든 노드의 이전 노드를 빈 문자열로 초기화
            }

            G[start] = 0.0; // 시작 노드의 거리를 0으로 설정
            F[start] = G[start] + H[start];
            openList.Add((start, F[start]));
            while (openList.Count > 0)
            {
                (string current, double Dist) = openList.Min; // Open List의 최소 거리 노드
                openList.Remove(openList.Min); // Open List에서 최소 거리 노드를 제거

                if (current == end) // 도착 노드에 도달한 경우
                    break;

                closedList.Add(current); // Closed List에 도착 노드를 추가

                foreach ((string neighbor, double distance) in adjList[current]) // 현재 노드의 이웃 노드
                {
                    if (closedList.Contains(neighbor)) // Closed List에 있는 노드는 건너뜀
                        continue;

                    double newDist = G[current] + distance; // 현재 노드에서 이웃 노드까지의 거리 계산

                    if (!openList.Any(x => x.vertex == neighbor) || newDist < G[neighbor]) // 이웃 노드가 Open List에 포함되지 않은 경우 or 현재 노드에서 이웃 노드까지의 거리가 G[neighbor]보다 작으면
                    {
                        G[neighbor] = newDist; // G[neighbor]를 현재 노드에서 이웃 노드까지의 거리로 갱신
                        F[neighbor] = G[neighbor] + H[neighbor];
                        openList.Add((neighbor, F[neighbor])); // 이웃 노드를 Open List에 추가
                        previous[neighbor] = current; // 이웃 노드의 이전 노드를 현재 노드로 갱신
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
            Graph graph = new Graph(N); // Astar 객체 생성

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
                double weight = CalcDist(Stops.Find(s => s.ID == route[0]), Stops.Find(s => s.ID == route[1])); // 경로의 가중치
                graph.AddEdge(route[0], route[1], weight);
            }

            List<string> shortestPath = graph.Astar(startPoint, endPoint, Stops); // 최단 경로 탐색

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
