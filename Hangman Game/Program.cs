using System;
using System.Collections.Generic;

namespace HangmanGame
{
    class Program
    {
        public static string Word { get; set; }

        public static int Attempts_left { get; set; }

        public static string Guessed { get; set; }

        public static List<char> Mistakes { get; set; }

        static string StartGame()
        {
            List<string> suggestions = new List<string>();
            suggestions.Add("CSHARP");
            suggestions.Add("SUMMER");

            Random random = new Random();
            Word = suggestions[random.Next(suggestions.Count())];

            Attempts_left = Word.Length;

            Guessed = "";
            for (int i = 0; i < Word.Length; i++)
            {
                Guessed += "_";
            }

            Mistakes = new List<char>();

            return Word;
        }

        static bool Draw()
        {
            Console.Clear();

            Console.WriteLine(Guessed);
            Console.WriteLine();
            Console.WriteLine("Attempts left: " + Program.Attempts_left);
            Console.WriteLine();
            Console.WriteLine("---------------------------");
            Console.WriteLine();

            if (Mistakes.Count > 0)
            {
                Console.Write("Used: ");

                for (int i = 0; i < Mistakes.Count(); i++)
                {
                    if (i == Mistakes.Count() - 1)
                    {
                        Console.Write(Mistakes[i]);
                    }
                    else
                    {
                        Console.Write(Mistakes[i] + ", ");
                    }
                }

                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("No mistakes");
            }

            Console.WriteLine();
            Console.WriteLine(" +====+  ");
            Console.Write(" |    "); if(Attempts_left < 6) Console.Write("0  "); Console.WriteLine();
            Console.Write(" |   "); if (Attempts_left < 5) Console.Write("/"); if (Attempts_left < 4) Console.Write("|"); if (Attempts_left < 3) Console.Write("\\ "); Console.WriteLine();
            Console.Write(" |   "); if (Attempts_left < 2) Console.Write("/"); if (Attempts_left == 0) Console.Write(" \\ "); Console.WriteLine();
            Console.WriteLine("===      ");
            Console.WriteLine();
            if (Attempts_left == 0) return false;
            return true;
        }

        static void Main(string[] args)
        {
            StartGame();

            bool correct = true;

            do
            {
                char attempt;
                bool validation = false;

                do
                {
                    Draw();

                    Console.Write("Take a guess: ");
                    Char.TryParse(Console.ReadLine(), out attempt);

                    if (attempt.Equals(""))
                    {
                        Console.WriteLine("---------------------------");
                        Console.WriteLine("  You need to input data!  ");
                        Console.WriteLine(" Press any key to continue ");
                        Console.WriteLine("---------------------------");
                        Console.ReadLine();
                        continue;
                    }

                    validation = false;
                    if (!Char.IsLetter(attempt))
                    {
                        validation = true;
                        Console.WriteLine("---------------------------------");
                        Console.WriteLine(" You need to write letters only! ");
                        Console.WriteLine("    Press any key to continue    ");
                        Console.WriteLine("---------------------------------");
                        Console.ReadLine();
                        continue;
                    }
                } while (validation || attempt.Equals(""));

                correct = false;

                for (int i = 0; i < Word.Length; i++)
                {
                    if (Word[i] == attempt && Guessed[i].Equals('_'))
                    {
                        Guessed = Guessed.Substring(0, i) + attempt + Guessed.Substring(i + 1);
                        correct = true;
                    }
                    else if (Word[i] == attempt && !Guessed[i].Equals('_'))
                    {
                        Console.WriteLine("---------------------------");
                        Console.WriteLine(" You tried this one before ");
                        Console.WriteLine(" Press any key to continue ");
                        Console.WriteLine("---------------------------");
                        Console.ReadLine();
                        break;
                    }

                    if(i == Word.Length - 1 && !correct && !Mistakes.Contains(attempt))
                    {
                        Attempts_left--;
                        Mistakes.Add(attempt);
                        break;
                    }

                }

                if (Word == Guessed) break;

            } while (Attempts_left != 0);

            if(Attempts_left == 0)
            {
                Draw();
                Console.WriteLine("---------------------------");
                Console.WriteLine("         You lost!         ");
                Console.WriteLine(" Press any key to continue ");
                Console.WriteLine("---------------------------");
                Console.ReadLine();
            }
            else
            {
                Draw();
                Console.WriteLine("---------------------------");
                Console.WriteLine("          You won!         ");
                Console.WriteLine(" Press any key to continue ");
                Console.WriteLine("---------------------------");
                Console.ReadLine();
            }
        }
    }
}