using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Media;

class DigitGame
{
    static public int i = 2, j = 2;
    static public int swapVar;
    static int moves = 0;                       // Counts the moves 
    static string Name;                         // Stores player's name
    static public int[,] array = new int[3, 3]; // Game board values
    static public string path = @"..\..\Images\scores.txt";

    public static void PrintScores()            // Reads the scores from the text file and displays them
    {
        Console.Clear();
        string score = System.IO.File.ReadAllText(@"..\..\Images\ScoreboardImage.txt");
        Console.Write(score);
        Console.Write("Name".PadRight(15));
        Console.WriteLine("Number of attempts: ");
        int position = 1;
        List<string> scoreList = File.ReadAllLines(path).ToList();  // Reads the scores from the text file
        foreach (string separateScore in scoreList)
        {
            if (position <= 10)                                      //... and displays the first 10 of them
            {
                Console.WriteLine(position.ToString().PadLeft(2, ' ') + ". " + separateScore);
            }

            position++;
        }
    }

    public static void ScoreRecords(int score, string name)         // Writes the results in a text file
    {
        List<string> scoreList;
        if (File.Exists(path))                                      // If the file exists
        {
            scoreList = File.ReadAllLines(path).ToList();           // reads all scores in a list -> scoreList
        }
        else
        {
            scoreList = new List<string>();                         // else creates a new list for the results -> scoreList
        }
        scoreList.Add(name.PadRight(10, ' ') + "\t" + score.ToString()); // ... writes the new result in it
        var sortedScoreList = scoreList.OrderBy(ss => int.Parse(ss.Substring(ss.LastIndexOf("\t") + 1)));  // ... sorts it
        File.WriteAllLines(path, sortedScoreList.ToArray());            //and writes it back to the text file
    }

    static public void Keys()                                       //Reads and performs the player's moves (arrows)
    {
        try                                                         //Uses Exception
        {
            for (i = 0; i < 3; i++)                                 //
            {
                for (j = 0; j < 3; j++)
                {
                    while (array[i, j] == 0)
                    {
                        ConsoleKeyInfo move;                        // the variable move stores the current key pressed
                        Console.Beep();                             //sound
                        move = Console.ReadKey();                   // Reads the player's input
                        Ends(move);                                 // Checks if the player had choosen to end the game
                        switch (move.Key)                           // if the game continues, reads the next move
                        {
                            case ConsoleKey.LeftArrow:              // <-

                                swapVar = array[i, j];
                                array[i, j] = array[i, j - 1];
                                array[i, j - 1] = swapVar;
                                j = j - 1;
                                moves++;
                                break;
                            case ConsoleKey.UpArrow:                // ^

                                swapVar = array[i, j];
                                array[i, j] = array[i - 1, j];
                                array[i - 1, j] = swapVar;
                                i = i - 1;
                                moves++;
                                break;
                            case ConsoleKey.DownArrow:              // v

                                swapVar = array[i, j];
                                array[i, j] = array[i + 1, j];
                                array[i + 1, j] = swapVar;
                                i = i + 1;
                                moves++;
                                break;
                            case ConsoleKey.RightArrow:             // ->

                                swapVar = array[i, j];
                                array[i, j] = array[i, j + 1];
                                array[i, j + 1] = swapVar;
                                j = j + 1;
                                moves++;
                                break;
                            default:
                                throw new ArgumentException("Invalid key"); // invalid key
                        }
                        Values();
                        Check();
                    }
                }
            }
        }
        catch (ArgumentException)//Exeption for invalid key
        {
            Console.WriteLine("Invalid Key.Use only Arrrow Keys.");
            Keys();
        }
        catch (IndexOutOfRangeException)//Exeption for Invalid movement
        {
            Console.WriteLine("Invalid movement.");
            Keys();
        }
    }

    static Random random = new Random();

    static void Shuffle<T>(T[] array)                       //Shuffles the numbers for the hard level
    {
        int n = array.Length;
        for (int i = 0; i < n; i++)
        {
            int r = i + (int)(random.NextDouble() * (n - i));
            T t = array[r];
            array[r] = array[i];
            array[i] = t;
        }
    }

    static public void InitializeEasy() // Sets initial values in the array for the easy level
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

    static public void InitializeHard() // Sets initial values in the array for the hard level
    {
        int[] values =
        {
            0,
            1,
            2,
            3,
            4,
            5,
            6,
            7,
            8
        };
        Shuffle(values);
        foreach (int value in values)
        {
            for (int i = 0; i < values.Length; i++)
            {
                array[i % 3, i / 3] = values[i];
            }
        }
    }

    static public void Check()              //Check if the player wins the game, displays Congrats screen, plays sound
    {
        if (array[0, 0] == 0 && array[0, 1] == 1 && array[0, 2] == 2 && array[1, 0] == 3 && array[1, 1] == 4 && array[1, 2] == 5 && array[2, 0] == 6 && array[2, 1] == 7 && array[2, 2] == 8)
        {
            Console.Clear();
            //You Win!
            Console.ForegroundColor = ConsoleColor.Green;
            string text = System.IO.File.ReadAllText(@"..\..\Images\Win.txt");
            //sound 
            SoundPlayer simpleSound = new SoundPlayer(@"..\..\champions.wav");
            simpleSound.Play();
            Console.WriteLine(text);
            Console.WriteLine("\n\nCongratulations, {0}! You win!", Name);
            ScoreRecords(moves, Name);
            Console.WriteLine("\nPress Q to exit or N for new game.");
        }
    }

    static void Ends(ConsoleKeyInfo move)               //Checks if the player's choice is to exit the game
    {
        if (move.Key == ConsoleKey.Q)
        {
            Console.WriteLine("\nDo you want to exit? (Y/N)");
            move = Console.ReadKey();
            if (move.Key == ConsoleKey.Y)
            {
                PrintScores();
                // Console.ReadKey();
                Environment.Exit(0);
            }
            if (move.Key == ConsoleKey.N)
            {
                Values();
            }
        }
        else if (move.Key == ConsoleKey.N)
        {
            moves = 0;
            Console.WriteLine();
            Console.WriteLine("Press E for an easy game and H for hard mode.");
            Choise();
            Values();
            Keys();
        }
    }

    static public void Show()           // Shows the text "Press Q for exit, N for new game"
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\nPress Q to exit or N for new game.");
    }

    static public void Values()            //Draws the game board
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Green;

        Console.WriteLine("\n\n\tNumber of attempts: ");
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("\n\t\t" + moves);
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("\n\n\t\t" + new string('-', 50) + "\n\n");
        Console.ForegroundColor = ConsoleColor.Red;

        for (int i = 0; i < 3; i++)                                      //Draws the game board
        {
            for (int j = 0; j < 3; j++)
            {
                if (array[i, j] == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;        // The 0 symbol in Cyan color
                    Console.Write("{0,20}", array[i, j]);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;      // The other symbols in Magenta color
                    Console.Write("{0,20}", array[i, j]);
                }
            }
            Console.WriteLine("\n");
        }
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("\n\n\t\t" + new string('-', 50));
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\n\n\t\t\t\t\t\t\tAll The Best ");           // All the best, Player!
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("\n\t\t\t\t\t\t\t  " + Name);
        Show();
    }
    static public void Choise()                                     //Reads the player's choice for level - Easy, Hard
    {
        ConsoleKeyInfo modeChoice;
        modeChoice = Console.ReadKey();
        if (modeChoice.Key == ConsoleKey.E)
        {
            InitializeEasy();
        }
        else if (modeChoice.Key == ConsoleKey.H)
        {
            InitializeHard();
        }
    }
    static void Main()                                          // Main method
    {
        //game name title
        Console.ForegroundColor = ConsoleColor.Green;
        string title = System.IO.File.ReadAllText(@"..\..\Images\Title.txt");
        Console.WriteLine(title);                           //Welcome screen
        Console.WriteLine();
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Red;    // Rules
        Console.WriteLine("Game Rules:\nArrange the numbers from 0-8 in the right order by pressing the arrow keys!");
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("Please, enter your name: ");
        Console.ForegroundColor = ConsoleColor.Red;
        Name = Console.ReadLine();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Press E for an easy game and H for hard mode.");
        Choise();                                            //Reads the player's choice for level - Easy, Hard
        Values();                                           //Draws the game board
        Keys();                                              // Reads player's input
        Console.ReadLine();
    }
}