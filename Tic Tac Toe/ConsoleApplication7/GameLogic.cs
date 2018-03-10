using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    class GameLogic
    {
        private int rows; //how many rows the gameboard consists of
        private Player playerOne;
        private Player playerTwo;
        private CPU cpu;

        private bool isSinglePlayer;
        private UI ui;

        private GameBoard gameBoard;

        private int numInRowToWin;
        private int winner;

        public GameLogic()
        {
            ui = new UI(out isSinglePlayer);

            if (isSinglePlayer)
            {
                playerOne = new Human(ui.SetPlayerName("Player one"));
                cpu = new CPU();
                playerTwo = cpu;
            }
            else
            {
                playerOne = new Human(ui.SetPlayerName("Player one"));
                playerTwo = new Human(ui.SetPlayerName("Player two"));
            }

            while (Game()) { }
            Console.WriteLine("\nGoodbye!");
            Console.ReadKey();
        }

        public bool Game()
        {
            Random random = new Random((int)DateTime.Now.Ticks);
            if (random.Next(0, 2) == 0)
            {
                playerOne.Symbol = BoardValue.X;
                playerTwo.Symbol = BoardValue.O;
                ui.DecideStarter(playerOne.Name);
            }
            else
            {
                playerOne.Symbol = BoardValue.O;
                playerTwo.Symbol = BoardValue.X;
                ui.DecideStarter(playerTwo.Name);
            }

            if (!isSinglePlayer)
                rows = ui.DecideRows();
            else
                rows = 3;
            gameBoard = new GameBoard(rows);

            if (rows > 4)
                numInRowToWin = 4;
            else
                numInRowToWin = rows;

            ui.gameBoard = gameBoard;
            if (isSinglePlayer)
            {
                cpu.GameBoard = gameBoard;
                cpu.HasStarted = false;
            }

            while (true)
            {
                ui.NewTurn(playerOne.Symbol == BoardValue.X ? playerOne : playerTwo);
                if (IsOver())
                {
                    ui.PrintWinner(GetWinnerName());
                    return ui.PlayAgain();
                }
                ui.NewTurn(playerOne.Symbol == BoardValue.O ? playerOne : playerTwo);
                if (IsOver())
                {
                    ui.PrintWinner(GetWinnerName());
                    return ui.PlayAgain();
                }
            }
        }

        private bool IsOver()
        {
            for (int j = 0; j <= rows - numInRowToWin; j++) //Jumps down a row for each loop
            {
                for (int i = 0; i <= rows - 1; i++) //One loop for each column
                {
                    if (Check(i + j * rows, rows)) //vertical for each column
                        return true;
                }
            }

            for (int j = 0; j <= rows - numInRowToWin; j++) //One loop for each column
            {
                for (int i = 0; i <= rows - 1; i++) //One loop for each row
                {
                    if (Check(i * rows + j, 1)) //horizontal for each row
                        return true;
                }
            }

            for (int i = 0; i <= rows - numInRowToWin; i++)
            {
                if (Check(i * rows + i, rows + 1)) //diagonal starting from top left
                    return true;
            }
            for (int i = 0; i <= rows - numInRowToWin; i++)
            {
                if (Check(rows * (i + 1) - (i + 1), rows - 1)) //diagonal starting from top right
                    return true;
            }
            return IsFull();
        }

        /// <summary>
        /// Checks for X or 0 in every position of the array. Sets winner = 0 if full.
        /// </summary>
        /// <returns>true if the board is full</returns>
        private bool IsFull()
        {
            for (int i = 0; i < gameBoard.GetBoardSize(); i++)
                if (gameBoard.GetBoardValue(i).Equals(BoardValue.Empty))
                    return false;
            winner = 0;
            return true;

        }

        /// <summary>
        /// Method to check for a winner. Sets the winner variable to 1 (if player one is the winner) or 2 (if player two)
        /// </summary>
        /// <param name="index">position to start</param>
        /// <param name="increment">position to compare</param>
        /// <returns>true if there is a winner</returns>
        private bool Check(int index, int increment)
        {
            for (int i = 0; i < numInRowToWin; i++)
            {
                if (!gameBoard.GetBoardValue(index).Equals(gameBoard.GetBoardValue(index + increment)) || gameBoard.GetBoardValue(index).Equals(BoardValue.Empty))
                    return false;
                if (i == numInRowToWin - 2) //second last loop
                    break;
                index += increment;
            }
            if (gameBoard.GetBoardValue(index).Equals(BoardValue.X))
                winner = 1;
            else
                winner = 2;
            return true;
        }

        private string GetWinnerName()
        {
            if (winner == 1)
                return playerOne.Symbol == BoardValue.X ? playerOne.Name : playerTwo.Name;
            if (winner == 2)
                return playerOne.Symbol == BoardValue.O ? playerOne.Name : playerTwo.Name;
            return null;
        }
    }
}