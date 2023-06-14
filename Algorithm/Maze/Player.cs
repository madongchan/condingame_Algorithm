using System.IO;

namespace Maze
{
    class Player
    {
        public int PosY { get; private set; }
        public int PosX { get; private set; }

        private Board _board;
        List<Pos> path = new List<Pos>();

        public void InitializePlayer(int posY, int posX, int destY, int destX, Board board)
        {
            PosY = posY;
            PosX = posX;

            _board = board;

            BFS();
        }

        private void BFS()
        {
            int[] dirY = new int[] { -1, 0, 1, 0 };
            int[] dirX = new int[] { 0, -1, 0, 1 };

            bool[,] found = new bool[_board._size, _board._size];
            Pos[,] parent = new Pos[_board._size, _board._size];

            Queue<Pos> q = new Queue<Pos>();
            q.Enqueue(new Pos(PosY, PosX));


            found[PosY, PosX] = true;
            parent[PosY, PosX] = new Pos(PosY, PosX);

            while (q.Count > 0)
            {
                Pos pos = q.Dequeue();

                int nowY = pos.Y;
                int nowX = pos.X;

                for (int i = 0; i < 4; i++)
                {
                    int nextY = nowY + dirY[i];
                    int nextX = nowX + dirX[i];

                    if (nextX <= 0 || nextX >= _board._size || nextY <= 0 || nextY >= _board._size)
                        continue;
                    if (_board._tile[nextY, nextX] == Board.TileType.Wall)
                        continue;
                    if (found[nextY, nextX])
                        continue;

                    q.Enqueue(new Pos(nextY, nextX));
                    found[nextY, nextX] = true;
                    parent[nextY, nextX] = new Pos(nowY, nowX);
                }
            }

            CalcPathFromParent(parent);
        }

        private void CalcPathFromParent(Pos[,] parent)
        {
            int y = _board.DestY;
            int x = _board.DestX;

            while (parent[y, x].Y != y || parent[y, x].X != x)
            {
                path.Add(new Pos(y, x));

                Pos pos = parent[y, x];
                y = pos.Y;
                x = pos.X;
            }
            path.Add(new Pos(y, x));
            path.Reverse();
        }

        const int MOVE_TICK = 100;
        private int _sumTick = 0;
        int _index = 0;

        public void Update(int deltaTick)
        {
            if (_index >= path.Count)
                return;

            _sumTick += deltaTick;

            if (_sumTick >= MOVE_TICK)
            {
                _sumTick = 0;

                PosY = path[_index].Y;
                PosX = path[_index].X;

                _index++;
            }
        }
    }
}
