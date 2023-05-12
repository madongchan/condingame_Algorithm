using System;
using System.Collections.Generic;

class Solution
{
    static Dictionary<int, List<int>> connection = new Dictionary<int, List<int>>();

    static int LetsCount(int number)
    {
        Stack<int> stack = new Stack<int>(); // 스택을 생성
        stack.Push(number); // 시작 정점을 스택에 push
        int max = 0;

        while (stack.Count > 0)
        {
            int current = stack.Pop(); // 스택에서 정점을 pop

            if (!connection.ContainsKey(current) || connection[current].Count == 0)
                // 현재 정점에 연결된 다른 정점이 없는 경우
                continue;

            foreach (int item in connection[current])
            {
                stack.Push(item); // 이웃 정점을 스택에 push
            }

            int depth = stack.Count; // 스택에 있는 정점 수가 현재 탐색한 깊이
            if (depth > max)
                max = depth; // 최대 깊이를 갱신
        }

        return max; // 최대 깊이 반환
    }

    static void Main(string[] args)
    {
        int n = int.Parse(Console.ReadLine());
        for (int i = 0; i < n; i++)
        {
            string[] inputs = Console.ReadLine().Split(' ');
            int x = int.Parse(inputs[0]);
            int y = int.Parse(inputs[1]);

            if (!connection.ContainsKey(x))
                connection[x] = new List<int>();

            connection[x].Add(y);
        }

        int max = 0;
        foreach (KeyValuePair<int, List<int>> item in connection)
        {
            int temp = LetsCount(item.Key);
            if (max < temp)
                max = temp;
        }

        Console.WriteLine(max);
    }
}
