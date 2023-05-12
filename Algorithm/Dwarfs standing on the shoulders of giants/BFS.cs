using System;
using System.Collections.Generic;

class Solution
{
    // key가 정점, value가 해당 정점에서 진행 가능한 정점들의 목록인 Dictionary
    static Dictionary<int, List<int>> connection = new Dictionary<int, List<int>>();

    // 해당 key에서 시작하여 가능한 루트를 찾아서 최대 깊이를 반환하는 함수
    static int Stack_DFS(int number)
    {
        int max = 0; // 현재 루트에서 탐색한 최대 깊이를 저장할 변수
        if (!connection.ContainsKey(number) || connection[number].Count == 0)
            // 현재 루트의 정점에 연결된 다른 정점이 없는 경우
            return 0;
        else
        {
            // 현재 루트의 정점에 연결된 다른 정점들을 모두 탐색
            foreach (int item in connection[number])
            {
                int temp = Stack_DFS(item); // 현재 정점에서부터 시작하여 탐색한 최대 깊이를 구함
                if (max < temp)
                    max = temp; // 구한 최대 깊이가 현재까지 구한 최대 깊이보다 큰 경우, 최대 깊이를 갱신
            }
        }
        return max + 1; // 현재 루트를 포함한 최대 깊이를 반환
    }

    static void Main(string[] args)
    {
        int n = int.Parse(Console.ReadLine()); // 정점 간의 관계 수
        for (int i = 0; i < n; i++)
        {
            // 두 사람 간의 관계를 입력받음 (x가 y를 영향을 미치는 관계)
            string[] inputs = Console.ReadLine().Split(' ');
            int x = int.Parse(inputs[0]);
            int y = int.Parse(inputs[1]);

            // 딕셔너리에 없는 키를 조회하려고 하면 KeyNotFoundException이 발생하기 때문에,
            // connection 딕셔너리에 x 키가 없을 경우 새로운 리스트를 할당하여 이 키에 대한 값을 추가하는 것입니다.
            if (!connection.ContainsKey(x))
                connection[x] = new List<int>();

            connection[x].Add(y); // 정점 x에서 정점 y로 가는 간선 추가
        }

        int max = 0; // 모든 루트에 대해 최대 깊이를 비교하여 가장 큰 값을 저장할 변수

        foreach (KeyValuePair<int, List<int>> item in connection) // 루트 노드가 한개 이상일 때가 있으므로 
        {
            int temp = Stack_DFS(item.Key); // 각 루트에 대해 최대 깊이를 계산
            if (max < temp)
                max = temp; // 계산한 최대 깊이가 현재까지 구한 최대 깊이보다 큰 경우, 최대 깊이를 갱신
        }

        // 최대 깊이 + 1 = 가장 긴 영향관계 연쇄에 참여한 사람 수
        Console.WriteLine(max + 1);
    }
}
