using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Program
    {
        public int i = 1, j = 1;
        public int swapVar;
        int moves = 0;
        static string Name;
        public int[,] array = new int[3, 3];

        public void keys()
        {
            try
            {
                for (i = 0; i < 3; i++)
                {
                    for (j = 0; j < 3; j++)
                    {
                        while (array[i, j] == 0)
                        {
                            ConsoleKeyInfo move;
                            move = Console.ReadKey();

                            switch (move.Key)
                            {
                                case ConsoleKey.LeftArrow:

                                    Console.Clear();
                                    swapVar = array[i, j];
                                    array[i, j] = array[i, j - 1];
                                    array[i, j - 1] = swapVar;
                                    j = j - 1;
                                    break;
                                case ConsoleKey.UpArrow:
                                    Console.Clear();
                                    swapVar = array[i, j];
                                    array[i, j] = array[i - 1, j];
                                    array[i - 1, j] = swapVar;
                                    i = i - 1;
                                    break;
                                case ConsoleKey.DownArrow:
                                    Console.Clear();
                                    swapVar = array[i, j];
                                    array[i, j] = array[i + 1, j];
                                    array[i + 1, j] = swapVar;
                                    i = i + 1;
                                    break;
                                case ConsoleKey.RightArrow:
                                    Console.Clear();
                                    swapVar = array[i, j];
                                    array[i, j] = array[i, j + 1];
                                    array[i, j + 1] = swapVar;
                                    j = j + 1;
                                    break;
                            }
                            moves++;

                            values();

                            check();

                            ends();
                        }
                    }
                }
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Invalid movement ");
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine("\nyou  press Enter to return or PRESS 'Q' for exit.");
                {
                    ConsoleKeyInfo exit;
                    exit = Console.ReadKey();
                    if (exit.Key == ConsoleKey.Q)
                    {
                        Console.WriteLine("\n do you want to exit (Y/N)");
                        exit = Console.ReadKey();
                        if (exit.Key == ConsoleKey.Y)
                        {
                            Environment.Exit(0);
                        }
                        if (exit.Key == ConsoleKey.N)
                        {
                            keys();
                        }
                    }

                    keys();
                }
            }
        }
        public void init()
        {
            array[0, 0] = 1;
            array[0, 1] = 4;
            array[0, 2] = 2;
            array[1, 0] = 3;
            array[1, 1] = 5;
            array[1, 2] = 8;
            array[2, 0] = 6;
            array[2, 1] = 7;
            array[2, 2] = 0;
        }

        public void check()
        {
            if (array[0, 0] == 0 && array[0, 1] == 1 && array[0, 2] == 2 && array[1, 0] == 3 && array[1, 1] == 4 && array[1, 2] == 5 && array[2, 0] == 6 && array[2, 1] == 7 && array[2, 2] == 8)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                //You win title


                Console.WriteLine("\n\tPress Q for exit");
            }
        }

        public void ends()
        {
            ConsoleKeyInfo exit;
            exit = Console.ReadKey();
            if (exit.Key == ConsoleKey.Q)
            {
                Console.WriteLine("\n do you want to exit (Y/N)");
                exit = Console.ReadKey();
                if (exit.Key == ConsoleKey.Y)
                {
                    Environment.Exit(0);
                }
                if (exit.Key == ConsoleKey.N)
                {
                    values();
                }
            }
        }

        public void show()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n Press Q for exit");
        }

        public void values()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n\n\tnumber of attempts ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\n\t\t" + moves);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n\n\t\t" + new string('-', 50) + "\n\n");
            Console.ForegroundColor = ConsoleColor.Red;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (array[i, j] == 0)
                    {

                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("{0,20}", array[i, j]);
                    }
                    else
                    {

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("{0,20}", array[i, j]);
                    }
                }
                Console.WriteLine("\n");
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n\n\t\t" + new string('-', 50));
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n\n\t\t\t\t\t\t\tAll The Best ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\n\t\t\t\t\t\t\t  " + Name);
        }



        static void Main()
        {
            //game name title


            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Game Rules: \n arrange the numbers from 0-8 in right order by double pressing the arrow keys.");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Enter your NAME:");
            Console.ForegroundColor = ConsoleColor.Red;
            Name = Console.ReadLine();

            Program p = new Program();



            p.init();
            p.values();
            p.keys();
            p.values();
            Console.ReadLine();
        }
    }
}