using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    class Human : Player
    {
        public Human(string name) : base(name)
        {
        }

        override
        public int NewTurn()
        {
            int choice = int.TryParse(Console.ReadLine(), out choice) ? choice : -1;
            return choice - 1; //convert to 0-based
        }
    }
}
