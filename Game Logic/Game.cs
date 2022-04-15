using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Logic
{
    public class Game
    {
        private GameBoard m_board;
        private Player m_PlayerO;
        private Player m_PlayerX;

        public Game(int i_BoardSize)
        {
            m_board = new GameBoard(i_BoardSize);
        }
        public void PrintBoard()
        {
            m_board.PrintBoard();
        }
        //generate valid moves will be here
    }
}
