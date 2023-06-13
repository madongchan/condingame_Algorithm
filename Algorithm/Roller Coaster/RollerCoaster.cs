using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace RollerCoaster
{
    class CircularQueue<T>
    {
        private T[] queue;      // 큐의 요소를 저장하는 배열
        private int front;      // 큐의 첫 번째 요소를 가리키는 인덱스
        private int rear;       // 큐의 마지막 요소를 가리키는 인덱스
        private int count;      // 큐의 현재 요소 개수

        public CircularQueue(List<T> initialItems)
        {
            queue = new T[initialItems.Count]; // 초기 요소 개수에 맞게 배열 크기 설정
            front = 0;                         // 초기화: front를 0으로 설정
            rear = initialItems.Count - 1;     // 초기화: rear를 마지막 요소 위치로 설정
            count = initialItems.Count;        // 초기화: 요소 개수 설정

            for (int i = 0; i < initialItems.Count; i++)
            {
                queue[i] = initialItems[i];    // 초기 요소를 배열에 복사
            }
        }

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= count)
                {
                    throw new IndexOutOfRangeException("Index is out of range.");
                }

                int actualIndex = (front + index) % queue.Length;  // 실제 인덱스 계산
                return queue[actualIndex];                         // 해당 인덱스의 요소 반환
            }
            set
            {
                if (index < 0 || index >= count)
                {
                    throw new IndexOutOfRangeException("Index is out of range.");
                }

                int actualIndex = (front + index) % queue.Length;  // 실제 인덱스 계산
                queue[actualIndex] = value;                        // 해당 인덱스의 요소에 값 설정
            }
        }

        public void Enqueue(T item)
        {
            if (count == queue.Length)
            {
                Console.Error.WriteLine("Queue is full. Cannot enqueue.");
                return;
            }

            rear = (rear + 1) % queue.Length; // rear 위치를 계산하여 다음 빈 공간을 찾음
            queue[rear] = item;               // rear 위치에 새로운 원소 추가
            count++;                          // 큐의 원소 개수 증가
        }

        public T Dequeue()
        {
            if (count == 0)
            {
                Console.Error.WriteLine("Queue is empty. Cannot dequeue.");
                return default(T);  // 예외 처리를 위해 기본값 반환
            }

            T item = queue[front];                 // front 위치의 원소를 가져옴
            front = (front + 1) % queue.Length;    // front 위치를 계산하여 다음 원소로 이동
            count--;                               // 큐의 원소 개수 감소
            return item;                           // 제거된 원소 반환
        }

        public T Peek()
        {
            if (count == 0)
            {
                Console.Error.WriteLine("Queue is empty. Cannot peek.");
                return default(T);  // 예외 처리를 위해 기본값 반환
            }

            return queue[front]; // front 위치의 원소 반환 (제거하지 않고 확인만 함)
        }

        public bool IsEmpty()
        {
            return count == 0; // 원소 개수가 0인지 확인
        }

        public bool IsFull()
        {
            return count == queue.Length; // 원소 개수가 큐의 크기와 같은지 확인
        }
    }



    class RollerCoaster
    {
        // 롤러코스터의 매출을 계산하는 함수입니다.
        // L: 좌석의 총 수
        // C: 운행 횟수
        // N: 그룹의 수
        // groups: 그룹별 인원수 리스트
        static long CalculateRevenue(int L, int C, int N, List<int> groups)
        {
            CircularQueue<int> circularQueue = new CircularQueue<int>(groups);
            long totalRevenue = 0; // 총소득

            int totalPassengers = groups.Sum();
            if (totalPassengers <= L) // 좌석이 전체 대기자보다 같거나 많으면 하루 운행 횟수를 곱하여 하루 매출액을 바로 구할 수 있다.
            {
                return totalRevenue = (long)totalPassengers * C; // long으로 캐스팅하여 계산
            }
            else
            {
                // 딕셔너리 키는 시작 인덱스로 하고 딕셔너리에 저장할 값은 탑승 인원과 탑승을 마치고 난 후의 인덱스가 됩니다. 
                // 다시 말해, 시작 인덱스만 알면 탑승 인원과 탑승을 끝마친 후의 인덱스를 알 수 있습니다.
                Dictionary<int, Tuple<int, int>> rvenueHistory = new Dictionary<int, Tuple<int, int>>();
                int nextIndex = 0; // 다음 탐승할 그룹의 위치를 지정합니다.

                for (int ride = 0; ride < C; ride++)
                {
                    int num_passengers = 0;
                    if (rvenueHistory.ContainsKey(nextIndex))
                    {
                        //인덱스가 딕셔너리에 있다면 바로 다음 운행 정보와 탑승 인원을 얻어옵니다.
                        (nextIndex, num_passengers) = rvenueHistory[nextIndex];
                    }
                    else
                    {
                        int beginIndex = nextIndex;
                        while (num_passengers + circularQueue[nextIndex] <= L)
                        {
                            num_passengers += circularQueue[nextIndex];
                            nextIndex = (nextIndex + 1) % N;
                        }
                        rvenueHistory[beginIndex] = Tuple.Create(nextIndex, num_passengers);
                    }

                    totalRevenue += num_passengers;
                }
                return totalRevenue;
            }
        }
        static void Main(string[] args)
        {
            string[] inputs = Console.ReadLine().Split(' ');
            int L = int.Parse(inputs[0]);
            int C = int.Parse(inputs[1]);
            int N = int.Parse(inputs[2]);
            List<int> pi = new List<int>();
            for (int i = 0; i < N; i++)
            {
                pi.Add(int.Parse(Console.ReadLine()));
            }
            long totalEarnings = CalculateRevenue(L, C, N, pi);
            Console.WriteLine(totalEarnings);
        }
    }
}