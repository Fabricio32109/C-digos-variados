using System;

namespace Sokoban
{
    class Program
    {
        public static bool gan;

        //Sprites, cambian según el número
        private static int[,] ladrillo = new int[5, 5] { { 1, 14, 1, 14, 1 }, { 14, 1, 14, 1, 14 }, { 1, 14, 1, 14, 1 }, { 14, 1, 14, 1, 14 }, { 1, 14, 1, 14, 1 } };
        private static int[,] p = new int[5, 5] { { 0, 0, 11, 0, 0 }, { 0, 11, 11, 11, 0 }, { 11, 11, 11, 11, 11 }, { 0, 11, 11, 11, 0 }, { 0, 0, 11, 0, 0 } };
        private static int[,] obj = new int[5, 5] { { 0, 0, 0, 0, 0 }, { 0, 12, 0, 12, 0 }, { 0, 0, 12, 0, 0 }, { 0, 12, 0, 12, 0 }, { 0, 0, 0, 0, 0 } };
        private static int[,] caj = new int[5, 5] { { 6, 6, 6, 6, 6 }, { 6, 7, 13, 7, 6 }, { 6, 13, 7, 13, 6 }, { 6, 7, 13, 7, 6 }, { 6, 6, 6, 6, 6 } };
        private static int[,] cajm = new int[5, 5] { { 6, 6, 6, 6, 6 }, { 6, 7, 2, 7, 6 }, { 6, 2, 7, 2, 6 }, { 6, 7, 2, 7, 6 }, { 6, 6, 6, 6, 6 } };
        private static int[,] vac = new int[5, 5] { { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 } };

        private static int[,] xubic = new int[10, 10];

        public static void Main()
        {
            Console.SetWindowSize(50, 50);
            int[,] map = CargarMapa();
            gan = false;
            while (gan == false)
            {
                Dibmap(map);
                int mov = Jugar();
                map = Mover(mov, map);
                gan = Comp(map);
            }
            Dibmap(map);
        }
        public static int[,] CargarMapa()
        {
            //0=Ladrillos,1=Personaje,2=Marca de suelo,3=Caja,4=Objetivo,10=Suelo vacío
            int[,] map = { { 0,0,0,0,0,0,0,0,0,0 }, { 0,1,10,10,10,10,10,10,10,0 }, { 0, 10, 10, 10, 10, 10, 10, 10, 10, 0 }, { 0, 10, 10, 10, 10, 10, 10, 10, 10, 0 }, { 0, 10, 10, 10, 10, 2, 10, 10, 10, 0 }
             , { 0, 10, 10, 10, 10, 3, 10, 10, 10, 0 }  , { 0, 10, 10, 10, 2, 3, 10, 10, 10, 0 }  , { 0, 10, 10, 10, 10, 10, 10, 10, 10, 0 }  , { 0, 10, 10, 10, 10, 10, 10, 10, 10, 0 }  , { 0,0,0,0,0,0,0,0,0,0 } };
            return map;
        }
        public static void Dibmap(int[,] m)
        {
            Console.Clear();
            int contx = 0, contx1 = -1, conty = 0, conty1 = 0; ;
            for (int y = 0; y < 50; y++)
            {
                for (int x = 0; x < 50; x++)
                {
                    if (contx1 < 4)
                    {
                        contx1++;
                    }
                    else
                    {
                        contx++;
                        contx1 = 0;
                    }
                    Dibcar(x, y, m, contx, conty);
                }
                Console.WriteLine();
                contx = -1;
                if (conty1 < 4)
                {
                    conty1++;
                }
                else
                {
                    conty++;
                    conty1 = 0;
                }
            }
        }
        public static void Dibcar(int x, int y, int[,] map, int contx, int conty)
        {
            int car = map[y / 5, x / 5];
            x = x - 5 * contx;
            y = y - 5 * conty;
            int[,] r = new int[5, 5];
            switch (car)
            {
                case 0:
                    r = ladrillo;
                    break;
                case 1:
                    r = p;
                    break;
                case 2:
                    r = obj;
                    break;
                case 3:
                    r = caj;
                    break;
                case 4:
                    r = cajm;
                    break;
                case 5:
                    r = vac;
                    break;
            }
            Console.BackgroundColor = (ConsoleColor)r[x, y];
            Console.Write(" ");
            Console.ResetColor();
        }
        public static int Jugar()
        {
            ConsoleKeyInfo key = Console.ReadKey(true);
            bool ex = false;
            while (ex == false)
            {
                switch (key.Key)
                {
                    case ConsoleKey.W:
                        return 1;
                    case ConsoleKey.S:
                        return 2;
                    case ConsoleKey.A:
                        return 3;
                    case ConsoleKey.D:
                        return 4;
                    default:
                        break;
                }
            }
            return 0;
        }
        public static int[,] Mover(int mov, int[,] map)
        {
            int[] dir = new int[2];
            int[] pos = new int[2];
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (map[i, j] == 1)
                    {
                        pos[0] = i;
                        pos[1] = j;
                    }
                }
            }
            int[] movida = new int[2];
            switch (mov)
            {
                case 1:
                    dir[0] = 0;
                    dir[1] = -1;
                    break;
                case 2:
                    dir[0] = 0;
                    dir[1] = 1;
                    break;
                case 3:
                    dir[0] = -1;
                    dir[1] = 0;
                    break;
                case 4:
                    dir[0] = 1;
                    dir[1] = 0;
                    break;
            }


            if (map[pos[0] + dir[1], pos[1] + dir[0]] != 0 && map[pos[0] + dir[1], pos[1] + dir[0]] != 3 && map[pos[0] + dir[1], pos[1] + dir[0]] != 4)
            {
                if (map[pos[0] + dir[1], pos[1] + dir[0]] == 2)
                {
                    xubic[pos[0] + dir[1], pos[1] + dir[0]] = 1;
                    map[pos[0] + dir[1], pos[1] + dir[0]] = 1;
                    map[pos[0], pos[1]] = 5;
                }
                else
                {
                    map[pos[0] + dir[1], pos[1] + dir[0]] = 1;
                    map[pos[0], pos[1]] = 5;
                }
            }
            if (map[pos[0] + dir[1], pos[1] + dir[0]] == 3 || map[pos[0] + dir[1], pos[1] + dir[0]] == 4)
            {
                if (map[pos[0] + 2 * dir[1], pos[1] + 2 * dir[0]] != 0 && map[pos[0] + 2 * dir[1], pos[1] + 2 * dir[0]] != 3 && map[pos[0] + 2 * dir[1], pos[1] + 2 * dir[0]] != 4)
                {
                    if (map[pos[0] + 2 * dir[1], pos[1] + 2 * dir[0]] == 2)
                    {
                        map[pos[0] + 2 * dir[1], pos[1] + 2 * dir[0]] = 4;
                        map[pos[0] + dir[1], pos[1] + dir[0]] = 1;
                        map[pos[0], pos[1]] = 5;
                        xubic[pos[0] + 2 * dir[1], pos[1] + 2 * dir[0]] = 1;
                    }
                    else
                    {
                        map[pos[0] + 2 * dir[1], pos[1] + 2 * dir[0]] = 3;
                        map[pos[0] + dir[1], pos[1] + dir[0]] = 1;
                        map[pos[0], pos[1]] = 5;
                    }
                }

            }
            for (int i = 1; i < 9; i++)
            {
                for (int j = 1; j < 9; j++)
                {
                    if (xubic[i, j] == 1)
                    {
                        if (map[i, j] == 5)
                        {
                            map[i, j] = 2;
                        }
                    }
                }
            }
            return map;
        }
        public static bool Comp(int[,] map)
        {
            for (int i = 1; i < 9; i++)
            {
                for (int j = 1; j < 9; j++)
                {
                    if (map[i, j] == 3)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
