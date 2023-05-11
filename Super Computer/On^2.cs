using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

//풀이 방법: 종료일이 가장 빠른걸 선택하고 기간이 중복되는건 제거한다
class Solution2
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
        //리스트 요소가 남아있는 동안
        for (int i = 0; i < periodLists.Count; i++)
        {
            var currentPeriod = periodLists[i];
            //period는 리스트의 각 요소를 나타내는 변수. 그러므로 첫번째 요소와 기간이 겹치는 모든 요소를 제거한다. 현재 요소는 제거하지 않는다.
            periodLists.RemoveAll(period => period.startDay <= currentPeriod.endDay && period.endDay >= currentPeriod.startDay && period != currentPeriod);
        }
        Console.WriteLine(periodLists.Count);
    }
}