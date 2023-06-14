using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Maze
{
    /* Board */
    class Board
    {
        const char CIRCLE = '\u25cf';
        public int _size;
        public TileType[,] _tile;
        Player _player;
        public int DestY { get; private set; }
        public int DestX { get; private set; }
        public enum TileType
        {
            Empty,
            Wall,
        }

        public void InitializeBoard(int size, Player player)
        {
            if (size % 2 == 0)
                return;

            _size = size;
            DestY = size - 2;
            DestX = size - 2;
            _tile = new TileType[_size, _size];
            _player = player;

            GenerateBySideWinder();
        }

        private void GenerateBySideWinder()
        {
            #region Fill Empty
            for (int y = 0; y < _size; y++)
            {
                for (int x = 0; x < _size; x++)
                {
                    if (x % 2 == 0 || y % 2 == 0)
                        _tile[y, x] = TileType.Wall;
                    else
                        _tile[y, x] = TileType.Empty;
                }
            }
            #endregion

            #region SideWinder Algorithm
            for (int y = 0; y < _size; y++)
            {
                int count = 1;

                for (int x = 0; x < _size; x++)
                {
                    if (x % 2 == 0 || y % 2 == 0)
                        continue;

                    if (x == _size - 2 && y == _size - 2)
                        continue;

                    if (x == _size - 2)
                    {
                        _tile[y + 1, x] = TileType.Empty;
                        continue;
                    }

                    if (y == _size - 2)
                    {
                        _tile[y, x + 1] = TileType.Empty;
                        continue;
                    }

                    Random rnd = new Random();

                    if (rnd.Next(0, 2) == 0)
                    {
                        _tile[y, x + 1] = TileType.Empty;
                        count++;
                    }
                    else
                    {
                        int idx = rnd.Next(0, count);
                        _tile[y + 1, x - idx * 2] = TileType.Empty;
                        count = 1;
                    }
                }
            }
            #endregion
        }

        private ConsoleColor GetTileColor(TileType type)
        {
            switch (type)
            {
                case TileType.Empty:
                    return ConsoleColor.Green;
                case TileType.Wall:
                    return ConsoleColor.Red;
                default:
                    return ConsoleColor.Green;
            }
        }

        public void Render()
        {
            ConsoleColor prevColor = Console.ForegroundColor;

            for (int y = 0; y < _size; y++)
            {
                for (int x = 0; x < _size; x++)
                {
                    //플레이어 좌표가 (y, x)라면 플레이어 색으로 칠해준다.
                    if (y == _player.PosY && x == _player.PosX)
                        Console.ForegroundColor = ConsoleColor.Blue;
                    else if (y == DestY && x == DestX)
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    else
                        Console.ForegroundColor = GetTileColor(_tile[y, x]);

                    Console.Write(CIRCLE+" ");
                }
                Console.WriteLine();
            }

            Console.ForegroundColor = prevColor;
        }
    }
}
