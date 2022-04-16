using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Logic
{
    internal class Cell
    {
        private bool m_IsOccupied = false;
        private Point m_Location;
        private Piece m_Piece;
// has to check with Guy if c'tor overloading is something we like in our code
        public Cell (int i_Row, int i_Col, Piece i_Piece)
        {
            m_IsOccupied = true;
            m_Location = new Point(i_Row, i_Col);
            m_Piece = i_Piece;
        }

        public Cell(int i_Row, int i_Col)
        {
            m_Location = new Point(i_Row, i_Col);
        }

        private bool checkDifferInParity(int i_Row, int i_Col)
        {
            return (i_Row % 2) != (i_Col % 2);
        }

        public bool IsOccupied
        {
            get
            {
                return m_IsOccupied;
            }

            set
            {
                m_IsOccupied = value;
            }
        }

        public Piece Piece
        {
            get
            {
                return m_Piece;
            }

            set
            {
                m_Piece = value;
            }
        }

        internal Point Location
        {
            get {return m_Location;} 
            set { m_Location = value;}
        }

        internal void removePiece()
        {
            m_IsOccupied = false;
            m_Piece = null;
        }

        internal void placePiece(Piece i_Piece)
        {
            m_IsOccupied = true;
            m_Piece = i_Piece;
            m_Piece.Location = m_Location;
        }

        internal void movePiece(Cell i_MoveTo)
        {
            i_MoveTo.IsOccupied = true;
            i_MoveTo.placePiece(m_Piece);
            m_IsOccupied = false;
        }
    }
}
