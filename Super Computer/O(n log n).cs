using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

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
        //과제 종료일이 가장 빠른 것부터 처리하기 위해 정렬함
        periodLists = periodLists.OrderBy(x => x.endDay).ToList();
        foreach (var period in periodLists)
        {
            Console.Error.WriteLine(period);
        }
        int maxCount = 0;
        int currentEndDay = -1; // 마지막으로 선택한 연구 종료일
        foreach (var period in periodLists)
        {
            // 기간이 겹치지 않는 경우
            if (period.startDay > currentEndDay)
            {
                maxCount++;
                currentEndDay = period.endDay;
            }
        }
        Console.WriteLine(maxCount);
    }
}
