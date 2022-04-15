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
        private List<Move> m_ValidMoves;
        internal Piece(eToken i_Token, Point i_location)
        {
            m_Token = i_Token;
            m_Location = i_location;
            m_ValidMoves = new List<Move>();
        }

        internal eToken Token
        {
            get { return m_Token; }

            set { m_Token = value; }
        }

        internal bool IsKing
        {
            get { return m_IsKing; }

            set { m_IsKing = value; }
        }

        internal Point Location
        {
            get {return m_Location;}

            set { m_Location = value;}
        }

        internal List<Move> ValidMoves
        {
            get { return m_ValidMoves;}
        }

        internal void AddMove(Move i_MoveToAdd)
        {
            m_ValidMoves.Add(i_MoveToAdd);
        }

        internal bool moveContainsCell(Cell i_isContains)
        {
            bool contains = false;

            foreach(Move move in m_ValidMoves)
            {
                if(move.Cell == i_isContains)
                {
                    contains = true;
                }
            }

            return contains;
        }

        internal Move getMove(Cell i_Cell)
        {
            Move desiredMove = null;
            foreach(Move move in m_ValidMoves)
            {
                if(move.Cell == i_Cell)
                {
                    desiredMove = move;
                }
            }

            return desiredMove;
        }
    }
}
