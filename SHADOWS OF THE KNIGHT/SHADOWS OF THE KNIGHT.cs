class Player
{
    static void Main(string[] args)
    {
        string[] inputs;
        inputs = Console.ReadLine().Split(' ');
        int W = int.Parse(inputs[0]); // width of the building.
        int H = int.Parse(inputs[1]); // height of the building.

        int xStart = 0, xEnd = W - 1, yStart = H - 1, yEnd = 0; //이진 탐색으로 폭탄의 위치를 찾기 위해 추측 범위를 좁혀나가야함 이때 필요한 범위 값

        int N = int.Parse(Console.ReadLine()); // 최대 이동 수
        inputs = Console.ReadLine().Split(' ');

        int X0 = int.Parse(inputs[0]); //배트맨의 현재 위치
        int Y0 = int.Parse(inputs[1]);

        // game loop
        while (true)
        {
            string bombDir = Console.ReadLine(); // the direction of the bombs from batman's current location (U, UR, R, DR, D, DL, L or UL)

            if (bombDir.Any(c => c == 'U'))
            {
                yStart = Y0 - 1; // 0으로 갈 수록 위쪽이라서 yStart 값에 배트맨의 Y위치 -1을 해줬다.
            }
            else if (bombDir.Any(c => c == 'D'))
            {
                yEnd = Y0 + 1;
            }
            else
            {
                yStart = yEnd = Y0;
            }

            if (bombDir.Any(c => c == 'R'))
            {
                xStart = X0 + 1; // Y 좌표와는 반대로 0으로부터 멀어질 수록 오른쪽 이라서 xStart 값에 배트맨의 Y위치 -1을 해줬다.
            }
            else if (bombDir.Any(c => c == 'L'))
            {
                xEnd = X0 - 1;
            }
            else
            {
                xStart = xEnd = X0;
            }

            //배트맨의 위치를 이진 탐색을 위해 추측 범위의 중간으로 옮겨야 함
            X0 = (xStart + xEnd) / 2;
            Y0 = (yStart + yEnd) / 2;
            // the location of the next window Batman should jump to.
            Console.WriteLine(X0 + " " + Y0);
        }
    }
}