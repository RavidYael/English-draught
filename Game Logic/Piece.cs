using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Logic
{
    enum eToken
    {
        O, X
    }

    internal class Piece
    {
        private eToken m_Token;
        private bool m_IsKing;
        private List<Cell> m_ValidMoves;
    }
}
