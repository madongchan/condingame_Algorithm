using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

//풀이 방법: 종료일이 가장 빠른걸 선택하고 기간이 중복되는건 제거한다
class Solution
{
    static void Main(string[] args)
    {
        int N = int.Parse(Console.ReadLine());
        var periodLists = new List<(int startDay, int endDay)>(); // ValueTuple 자료구조
        for (int i = 0; i < N; i++)
        {
            string[] inputs = Console.ReadLine().Split(' ');
            int startDay = int.Parse(inputs[0]); // 연구 시작일
            int duration = int.Parse(inputs[1]); // 연구 기간
            int endDay = startDay + duration - 1; // 연구 종료일

            periodLists.Add((startDay, endDay)); // 연구기간 리스트에 저장
        }

        foreach (var period in periodLists)
        {

        }
        Console.WriteLine("answer");
    }
}