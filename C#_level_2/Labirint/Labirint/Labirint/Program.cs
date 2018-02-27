using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labirint
{

    class Program
    {
        static void Main(string[] args)
        {
            int[,] labirintField = new int[9, 8] { {-1,-1,-1,-1,-1,-1,-1,-1,},
                                                {-1,0,-1,0,0,0,0,-1},
                                                {-1,0,-1,0,-1,-1,0,-1},
                                                {-1,0,-1,0,-5,-1,0,-1 },
                                                {-1,0,-1,-1,-1,-1,0,-1},
                                                {-1,0,0,0,-1,0,0,-1},
                                                {-1,0,-1,0,0,0,-1,-1},
                                                {-1,0,-1,0,-1,0,0,-1},
                                                {-1,-1,-1,-1,-1,-1,-1,-1,} };

           int x = 3, y = 1;
            int step = 0;

            PrintField(labirintField);
            Route(x, y, step, labirintField);
            PrintField(labirintField);

            Console.ReadKey();
        }

        static void Route(int x, int y,  int step, int[,] map)
        {
            PrintField(map);
            if (map[x, y] == 0)
            {
                step++;
                if (map[x + 1, y] != -1)//&& map[x + 1, y] == 0
                {
                    
                    map[x + 1, y] = step;
                    Route(x + 1, y, step, map);
                    Route(x, y + 1, step, map);
                    Route(x, y - 1, step, map);

                }
                if (map[x - 1, y] != -1)// && map[x - 1, y] == 0
                {
                    map[x - 1, y] = step;
                    Route(x - 1, y, step, map);
                    Route(x, y + 1, step, map);
                    Route(x, y - 1, step, map);
                }
                if (map[x, y + 1] != -1)//&& map[x, y + 1] == 0
                {

                    map[x, y + 1] = step;
                    Route(x, y + 1, step, map);
                    Route(x + 1, y, step, map);
                    Route(x - 1, y, step, map);
                }
                if (map[x, y - 1] != -1)//&& map[x, y - 1] == 0
                {
                    map[x, y - 1] = step;
                    Route(x, y - 1, step, map);
                    Route(x + 1, y, step, map);
                    Route(x - 1, y, step, map);
                }
            }
        }

        /// <summary>
        /// Отображает лабиринт в консоли
        /// </summary>
        static void PrintField(int[,] labirint)
        {
            for (int y = 0; y < labirint.GetLength(0); y++)
            {
                for (int x = 0; x < labirint.GetLength(1); x++)
                {

                    if ((labirint[y, x]) > 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    if (labirint[y, x] == -1)
                    {
                        Console.Write('*');
                    }
                    else
                    {
                        Console.Write(labirint[y, x]);
                    }

                }
                Console.WriteLine();
            }
        }
    }
}
