using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Logic
{
    public enum eTeamBaseSide
    {
        Top, Buttom
    }

    internal class Piece
    {
        private eTeamBaseSide m_OwnerBaseSide;
        private bool m_IsKing = false;
        private Point m_Location;
        private List<Move> m_ValidMoves;
        internal Piece(eTeamBaseSide i_OwnerTeamSide, Point i_location)
        {
            m_OwnerBaseSide = i_OwnerTeamSide;
            m_Location = i_location;
            m_ValidMoves = new List<Move>();
        }

        internal eTeamBaseSide OwnerBaseSide
        {
            get { return m_OwnerBaseSide; }

            set { m_OwnerBaseSide = value; }
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

        internal bool hasValidMoveToInputCell(Cell i_destCell)
        {
            bool contains = false;

            foreach(Move move in m_ValidMoves)
            {
                if(move.DestCell == i_destCell)
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
                if(move.DestCell == i_Cell)
                {
                    desiredMove = move;
                }
            }

            return desiredMove;
        }

        public bool getEatingMoveAndReturnIfFoundOne(out Move o_Move)
        {
            bool foundEatingMove = false;
            o_Move = null;
            foreach(Move move in m_ValidMoves)
            {
                if(move.IsEatingMove)
                {
                    o_Move = move;
                    foundEatingMove = true;
                }
            }

            return foundEatingMove;
        }

        internal void makeKing()
        {
            m_IsKing = true;
        }
    }
}
