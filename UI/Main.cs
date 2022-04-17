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
            GameExecutor executor = new GameExecutor();
            executor.Execute();
        }
    }
}
