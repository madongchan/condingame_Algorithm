namespace Maze
{
    /* Program.cs */
    class Program
    {
        static void Main(string[] args)
        {
            const int WAIT_TICK = 1000 / 30;

            Board board = new Board();
            Player player = new Player();

            board.InitializeBoard(25, player);
            player.InitializePlayer(1, 1, board._size - 2, board._size - 2, board);

            Console.CursorVisible = false;

            int lastTick = 0;
            while (true)
            {
                #region 프레임 관리
                int currentTick = System.Environment.TickCount;
                int deltaTick = currentTick - lastTick;
                //경과 시간이 1/30초보다 작다면?
                if (currentTick - lastTick < WAIT_TICK)
                    continue;

                lastTick = currentTick;
                #endregion

                // 1)사용자 입력 대기

                // 2)입력과 기타 로직 처리
                player.Update(deltaTick);

                // 3)렌더링
                Console.SetCursorPosition(0, 0);
                board.Render();
            }
        }
    }
}