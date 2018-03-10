using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    /// <summary>
    /// A tic tac toe-game. In two-player mode you can choose number of rows for a bigger game board. You can also play 3x3 against the computer.
    /// Johan Sjöström 20151201
    /// </summary>
    class Game
    {
        static void Main(string[] args)
        {
            new GameLogic();
        }
    }
}
