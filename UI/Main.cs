using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game_Logic;

namespace UI
{
    internal class Test
    {
        public static void Main(string[] args)
        {
            Game game = new (10);
            game.PrintBoard();
            Console.ReadLine();
        }
    }
}
