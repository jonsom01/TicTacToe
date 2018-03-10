using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TicTacToe
{
    /// <summary>
    /// This is the "AI" for the tic tac toe-game. If it's first to move it places in one of the corners. If second in the middle. After that
    /// it check (in order) 
    /// 1: A chance to win
    /// 2: To block opponents chance to win
    /// 3: If none of the above, makes a random move.
    /// Johan Sjöström 20151201
    /// </summary>
    class CPU : Player
    {
        private Random random = new Random((int)DateTime.Now.Ticks);
        public GameBoard GameBoard { get; set; } 
        public bool HasStarted { get; set; }
        private static int[] CORNERS = { 0, 2, 6, 8 };

        public CPU() : base("CPU")
        {
        }

        private bool CheckForWinOrBlock(BoardValue symbol, out int choice)
        {

            //Horizontal
            for (int i = 0; i < 9; i += 3)
            {
                if (Check(i, i + 1, i + 2, symbol))
                {
                    choice = i + 2;
                    return true;
                }
            }
            for (int i = 0; i < 9; i += 3)
            {
                if (Check(i, i + 2, i + 1, symbol))
                {
                    choice = i + 1;
                    return true;
                }
            }
            for (int i = 0; i < 9; i += 3)
            {
                if (Check(i + 1, i + 2, i, symbol))
                {
                    choice = i;
                    return true;
                }
            }
            //Vertiacal
            for (int i = 0; i < 3; i++)
            {
                if (Check(i, i + 3, i + 6, symbol))
                {
                    choice = i + 6;
                    return true;
                }
            }
            for (int i = 0; i < 3; i++)
            {
                if (Check(i, i + 6, i + 3, symbol))
                {
                    choice = i + 3;
                    return true;
                }
            }
            for (int i = 0; i < 3; i++)
            {
                if (Check(i + 3, i + 6, i, symbol))
                {
                    choice = i;
                    return true;
                }
            }
            //Diagonal left to right
            if (Check(0, 4, 8, symbol))
            {
                choice = 8;
                return true;
            }
            if (Check(0, 8, 4, symbol))
            {
                choice = 4;
                return true;
            }
            if (Check(4, 8, 0, symbol))
            {
                choice = 0;
                return true;
            }
            //Diagonal right to left
            if (Check(2, 4, 6, symbol))
            {
                choice = 6;
                return true;
            }
            if (Check(4, 6, 2, symbol))
            {
                choice = 2;
                return true;
            }
            if (Check(2, 6, 4, symbol))
            {
                choice = 2;
                return true;
            }
            else
            {
                choice = -1;
                return false;
            }
        }

        private bool Check(int firstPos, int secondPos, int choice, BoardValue symbol)
        {
            if (GameBoard.GetBoardValue(firstPos).Equals(GameBoard.GetBoardValue(secondPos)) && GameBoard.GetBoardValue(firstPos).Equals(symbol))
                if (GameBoard.GetBoardValue(choice).Equals(BoardValue.Empty))
                    return true;
            return false;
        }

        override
        public int NewTurn()
        {
            Thread.Sleep(2000);
            if (!HasStarted) //The first move
            {
                HasStarted = true;
                if (Symbol.Equals(BoardValue.X))
                {
                    int temp = random.Next(0, 4);
                    return CORNERS[temp];
                }
                else
                {
                    if (GameBoard.GetBoardValue(4).Equals(BoardValue.Empty)) //If the middle is free
                        return 4;
                    else
                        return RandomMove();
                }
            }
            int choice;
            if (CheckForWinOrBlock(Symbol, out choice))//Check for win
                return choice;
            if (Symbol.Equals(BoardValue.X))
            {
                if (CheckForWinOrBlock(BoardValue.O, out choice))//Check for block
                    return choice;
            }
            else
            {
                if (CheckForWinOrBlock(BoardValue.X, out choice))//Check for block
                    return choice;
            }
            return RandomMove();
        }

        /// <summary>
        /// Chooses randomly from the free positions in the array
        /// </summary>
        /// <returns></returns>
        public int RandomMove()
        {

            List<int> freePositions = new List<int>();
            for (int i = 0; i < GameBoard.GetBoardSize(); i++)
            {
                if (GameBoard.GetBoardValue(i).Equals(BoardValue.Empty))
                    freePositions.Add(i);
            }
            return freePositions[random.Next(0, freePositions.Count)];
        }
    }
}
