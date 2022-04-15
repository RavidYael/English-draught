using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Logic
{
    enum ePlayerType
    {
        Human, Machine
    }

    internal class Player
    {
        private ePlayerType m_PlayerType;
        private string m_Name;
        private int m_Score;
        private List<Piece> m_Pieces;
    }
}
