using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TicTacToe
{
    /// <summary>
    /// User Interface. Contains methods to print and read from the console.
    /// Johan Sjöström 20151201
    /// </summary>
    class UI
    {
        public GameBoard gameBoard;

        /// <summary>
        /// Starts the application
        /// </summary>
        ///
        public UI(out bool isSinglePlayer)
        {
            Console.WriteLine("\nWelcome to Tic-tac-toe!\n");

            while (true)
            {
                string input;
                Console.Write("Enter number of players (1/2): ");
                input = Console.ReadLine().ToLower();
                if (input.Equals("1") || input.Equals("one"))
                {
                    isSinglePlayer = true;
                    break;
                }
                else if (input.Equals("2") || input.Equals("two"))
                {
                    isSinglePlayer = false;
                    break;
                }
                else
                {
                    Console.WriteLine("\nWrong input! Try again. (Press any key to continue.)");
                    Console.ReadKey();
                }
            }
        }

        public string SetPlayerName(string name)
        {
            string newName;
            Console.Write("\n" + name + " please enter your name:");
            while (true)
            {
                newName = Console.ReadLine();
                if (!String.IsNullOrEmpty(newName))
                    return newName;
            }
        }

        public void DecideStarter(string name)
        {
            Console.Write("\nTossing coin to decide who will start.");
            for (int i = 0; i < 3; i++)
            {
                Thread.Sleep(500);
                Console.Write(".");
            }
            Console.WriteLine("\n" + name + " will start...");
        }

        public int DecideRows()
        {
            while (true)
            {
                int rows;

                Console.WriteLine("\nHow many rows (2-9)?");
                if (int.TryParse(Console.ReadLine(), out rows))
                {

                    if (rows >= 2 && rows <= 9)
                    {
                        return rows;
                    }
                }
                Console.WriteLine("Invalid input. Try again!");
            }
        }

        public void NewTurn(Player player)
        {
            Console.WriteLine(gameBoard.ToString());
            while (true)
            {
                Console.Clear();
                if (player.Name.EndsWith("s"))
                    Console.WriteLine("\nIt's " + player.Name + "' turn. Choose a free number.\n");
                else
                    Console.WriteLine("\nIt's " + player.Name + "'s turn. Choose a free number.\n");
                Console.WriteLine(gameBoard.ToString());
                if (!gameBoard.Set(player.NewTurn(), player.Symbol))
                {
                    Console.WriteLine("There was something wrong with your input. Try again! (Press any key to continue)");
                    Console.ReadKey();
                }
                else
                    break;
            }
        }

        public void PrintWinner(string winner)
        {
            Console.Clear();
            if (!String.IsNullOrEmpty(winner))
                Console.WriteLine("\nGame over! The winner is: " + winner);
            else
                Console.WriteLine("\nGame over! It's a draw.");
            Console.WriteLine("\n" + gameBoard.ToString());
        }

        public bool PlayAgain()
        {
            while (true)
            {
                Console.Write("\nDo you want to play again? (y/n)");
                char answer;
                if (char.TryParse(Console.ReadLine().ToLower(), out answer))
                {
                    if (answer.Equals('y'))
                        return true;
                    if (answer.Equals('n'))
                        return false;
                }
                Console.WriteLine("\nThere was something wrong with your input. Try again! (Press any key to continue...)");
                Console.ReadKey();
            }
        }
    }
}

