﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

class DigitGame
{ 
    static public int i = 2, j = 2;
    static public int swapVar;
    static int moves = 0;
    static string Name;
    static public int[,] array = new int[3, 3];
    static public string path = "scores.txt";

    public static void PrintScores()
    {
        Console.Clear();
        string score = System.IO.File.ReadAllText(@"ScoreboardImage.txt");
        Console.Write(score);
        Console.Write("Name".PadRight(15));
        Console.WriteLine("Score");
        int position = 1;
        List<string> scoreList = File.ReadAllLines(path).ToList();
        foreach (string separateScore in scoreList)
        {
            if (position <= 10)
            {
                Console.WriteLine(position.ToString().PadLeft(2, ' ') + ". " + separateScore);
            }

            position++;
        }
    }

    public static void ScoreRecords(int score, string name)
    {
        List<string> scoreList;
        if (File.Exists(path))
        {
            scoreList = File.ReadAllLines(path).ToList();
        }
        else
        {
            scoreList = new List<string>();
        }
        scoreList.Add(name.PadRight(10, ' ') + "\t" + score.ToString());
        var sortedScoreList = scoreList.OrderBy(ss => int.Parse(ss.Substring(ss.LastIndexOf("\t") + 1)));
        File.WriteAllLines(path, sortedScoreList.ToArray());
    }

    static public void keys()
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
                        ends(move);
                        switch (move.Key)
                        {
                            case ConsoleKey.LeftArrow:

                                //Console.Clear();
                                swapVar = array[i, j];
                                array[i, j] = array[i, j - 1];
                                array[i, j - 1] = swapVar;
                                j = j - 1;
                                moves++;
                                break;
                            case ConsoleKey.UpArrow:
                                //Console.Clear();
                                swapVar = array[i, j];
                                array[i, j] = array[i - 1, j];
                                array[i - 1, j] = swapVar;
                                i = i - 1;
                                moves++;
                                break;
                            case ConsoleKey.DownArrow:
                                //Console.Clear();
                                swapVar = array[i, j];
                                array[i, j] = array[i + 1, j];
                                array[i + 1, j] = swapVar;
                                i = i + 1;
                                moves++;
                                break;
                            case ConsoleKey.RightArrow:
                                //Console.Clear();
                                swapVar = array[i, j];
                                array[i, j] = array[i, j + 1];
                                array[i, j + 1] = swapVar;
                                j = j + 1;
                                moves++;break;
                            default:
                                throw new ArgumentException("Invalid key");

                        }
                        values();
                        check();
                    }
                }
            }
        }
        catch (ArgumentException)//Exeption for invalid key
        {
            Console.WriteLine("Invalid movement");
            keys();
            //Console.Clear();
            //Console.ForegroundColor = ConsoleColor.Cyan;
            //Console.WriteLine("Invalid key ");
            //Console.ForegroundColor = ConsoleColor.DarkMagenta;
            //Console.WriteLine("\nyou  press Enter to return or PRESS 'Q' for exit.");
            //{
            //    ConsoleKeyInfo exit;
            //    exit = Console.ReadKey();
            //    if (exit.Key == ConsoleKey.Enter)
            //    {
            //        values();
            //        keys();
            //    }
            //    if (exit.Key == ConsoleKey.Q)
            //    {
            //        Environment.Exit(0);

            //    }
            //    else values(); keys();

            //}
        }
        catch(IndexOutOfRangeException)//Exeption for Invalid movement
        {
            Console.WriteLine("Invalid movement");
            keys();
            //Console.Clear();
            //Console.ForegroundColor = ConsoleColor.Cyan;
            //Console.WriteLine("Invalid Movement");
            //Console.ForegroundColor = ConsoleColor.DarkMagenta;
            //Console.WriteLine("\nyou  press Any Key to return or PRESS 'Q' for exit.");
            //{
            //    ConsoleKeyInfo exit;
            //    exit = Console.ReadKey();
            //    if (exit.Key == ConsoleKey.Enter)
            //    {
            //        values();
            //        keys();
            //    }
            //    if (exit.Key == ConsoleKey.Q)
            //    {
            //        Environment.Exit(0);

            //    }
            //    else values(); keys();

            
        }
    }
        
        static public void init() // Sets initial values in the array
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
    
    static public void check()
    {
        if (array[0, 0] == 0 && array[0, 1] == 1 && array[0, 2] == 2 && array[1, 0] == 3 && array[1, 1] == 4 && array[1, 2] == 5 && array[2, 0] == 6 && array[2, 1] == 7 && array[2, 2] == 8)
        {
            Console.Clear();
            //You Win!
            Console.ForegroundColor = ConsoleColor.Cyan;
            string text = System.IO.File.ReadAllText(@"Win.txt");
            Console.WriteLine(text);
            Console.WriteLine("\n\nCongratulations, {0}! You win!", Name);
            ScoreRecords(moves, Name);
            Console.WriteLine("\nPress Q for exit, N for new game");
        }
    }

    static void ends(ConsoleKeyInfo move)
    {
        if (move.Key == ConsoleKey.Q)
        {
            Console.WriteLine("\nDo you want to exit (Y/N)");
            move = Console.ReadKey();
            if (move.Key == ConsoleKey.Y)
            {
                PrintScores();
                Environment.Exit(0);
            }
            if (move.Key == ConsoleKey.N)
            {
                values();
            }
        }
        else if (move.Key == ConsoleKey.N)
        {
            moves = 0;
            init();
            values();
            keys();
        }
    }

    static public void show()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\nPress Q for exit, N for new game");
    }

    static public void values()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\n\n\tNumber of attempts ");
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
        show();
    }

    static void Main()
    {
        //game name title
        string title = System.IO.File.ReadAllText(@"Title.txt");
        Console.WriteLine(title);

        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("Game Rules:\nArrange the numbers from 0-8 in right order by pressing the arrow keys.");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine();
        Console.WriteLine("Please, Enter your name:");
        Console.ForegroundColor = ConsoleColor.Red;
        Name = Console.ReadLine();

        init();
        values();
        keys();
        //values();
        Console.ReadLine();
    }
}