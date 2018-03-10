using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    /// <summary>
    /// Creates the game board and determines when to end the game.
    /// Johan Sjöström 20151201
    /// </summary>


    enum BoardValue { X, O, Empty };

    class GameBoard
    {      
        private BoardValue[] board;
        private int rows;
        private int boardSize;

        public BoardValue GetBoardValue(int index)
        {
            return board[index];
        }

        public int GetBoardSize()
        {
            return board.Length;
        }

        public GameBoard(int rows)
        {
            this.rows = rows;
            board = new BoardValue[rows * rows];
            for (int i = 0; i < board.Count(); i++)
                board[i] = BoardValue.Empty;
            boardSize = rows * rows;
        }

        public bool Set(int input, BoardValue XO)
        {
            if (input >= boardSize || input < 0) //If input is not on the board
                return false;
            if (board[input].Equals(BoardValue.Empty)) //If not already taken
            {              
                board[input] = XO;
                return true;
            }
            return false;
        }

        override
        public string ToString()
        {
            string output = "";
            for (int i = 0; i < rows * rows; i += rows)
            {
                output += PrintEmpty(' ');
                output += PrintMiddle(i);

                if (i == rows * (rows - 1)) //the last row shouldn't contain _'s
                {
                    output += PrintEmpty(' ');
                }
                else 
                    output += PrintEmpty('_');
            }
            return output;
        }

        /// <summary>
        /// Prints a row in the game board without numbers and/or X/0. Input i either ' ' or '_' depending on which row.
        /// </summary>
        /// <param name="input"></param>
        /// 
        private string PrintEmpty(char input)
        {
            string output = "|";
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    output += input;
                }
                output += "|";
            }
            return output + "\n";
        }

        /// <summary>
        /// Prints the middle part of each row (with the numbers)
        /// </summary>
        /// <param name="index">Index of array</param>
        private string PrintMiddle(int index)
        {
            string output = "";
            for (int i = 0; i < rows; i++)
            {
                int temp;
                if (board[index + i] == BoardValue.Empty)
                {
                    temp = index + i;

                    if (temp >= 99)//more than two digits (even less whitespace)
                    {
                        output += ("| " + (temp + 1) + " ");
                    }

                    else if (temp >= 9 && temp <99) //more than one digit (less whitespace)
                    {
                        output += ("|  " + (temp + 1) + " ");
                    }
                    else
                        output += ("|  " + (temp + 1) + "  "); //one digit (more whitespace)
                }
                else
                    output += ("|  " + board[index + i].ToString() + "  "); //the X's and O's
            }
            return (output + "|\n");
        }
    }
}
