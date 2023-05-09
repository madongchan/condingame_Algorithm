using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/
class Solution
{
    static void Main(string[] args)
    {
        int N = int.Parse(Console.ReadLine()); // 선물 구매에 동참하는 사람 수
        int C = int.Parse(Console.ReadLine()); // 선물 가격
        int[] Budgets = new int[N]; // 예산을 담을 배열

        for (int i = 0; i < N; i++)
        {
            Budgets[i] = int.Parse(Console.ReadLine()); //각각의 예산
        }

        //예산의 합이 선물 가격보다 적으면
        if (Budgets.Sum() < C)
        {
            Console.WriteLine("IMPOSSIBLE");
            return;
        }

        //가장 예산이 적은 사람부터 차례대로 확인해야 가장 공평하게 선물 가격을 지불함
        Array.Sort(Budgets);

        //예산의 합이 선물 가격보다 크면 계속 진행
        for (int i = 0; i < Budgets.Length; i++)
        {
            // 각각의 사람이 내야할 선물의 평균 값
            // 평균은 계속 업데이트 해줘야함
            int averageCost = C / N;

            //만약 평균 값보다 예산이 적다면
            if (Budgets[i] < averageCost)
            {
                Console.WriteLine(Budgets[i]);
                C -= Budgets[i]; // 예산 전체를 낸다
            }
            else
            {
                Console.WriteLine(averageCost);
                C -= averageCost;//예산에서 평균값만 내기
            }
            //낸 사람은 제외한다.
            N--;
        }
    }
}