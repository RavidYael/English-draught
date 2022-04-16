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
        private bool m_IsKing = false;
        private Point m_Location;
        private List<Cell> m_ValidMoves;

        public Piece()
        {
        }

        public Piece(eToken i_Token, Point i_location)
        {
            m_Token = i_Token;
            m_Location = i_location;
        }

        public eToken Token
        {
            get { return m_Token; }

            set { m_Token = value; }
        }

        public bool IsKing
        {
            get { return m_IsKing; }

            set { m_IsKing = value; }
        }

        public Point Location
        {
            get {return m_Location;}

            set { m_Location = value;}
        }

        public void AddMove(Cell i_CellToAdd)
        {
            m_ValidMoves.Add(i_CellToAdd);
        }
    }
}
