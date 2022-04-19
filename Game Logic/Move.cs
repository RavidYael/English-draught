using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Game_Logic
{
    internal class Move
    {
        private Cell m_MoveFrom;
        private Cell m_MoveTo;
        private bool m_isEatingMove = false;
        private Cell m_EatenCell;
        internal static class Directions
        {
            public static readonly Direction UP = new Direction(-1, 0);
            public static readonly Direction DOWN = new Direction(1, 0);
            public static readonly Direction RIGHT = new Direction(0, 1);
            public static readonly Direction LEFT = new Direction(0, -1);

            public static readonly Direction UP_RIGHT = new Direction(-1, 1);
            public static readonly Direction UP_LEFT = new Direction(-1, -1);
            public static readonly Direction DOWN_RIGHT = new Direction(1, 1);
            public static readonly Direction DOWN_LEFT = new Direction(1, -1);

            public static readonly Direction[] OPawnDirections;
            public static readonly Direction[] XPawnDirections;
            public static readonly Direction[] KingDirections;


            static Directions()
            {
                OPawnDirections = new Direction[2] { DOWN_RIGHT, DOWN_LEFT };
                XPawnDirections = new Direction[2] { UP_RIGHT, UP_LEFT };
                KingDirections = new Direction[8] { UP_RIGHT, DOWN_RIGHT, DOWN_LEFT, UP_LEFT, UP, RIGHT, DOWN, LEFT };
            }
            internal class Direction
            {
                private int m_HorizonalMove;
                private int m_VerticalMove;

                public Direction(int i_VerticalMove, int i_HorizonalMove)
                {
                    m_HorizonalMove = i_HorizonalMove;
                    m_VerticalMove = i_VerticalMove;
                }

                public int HorizonalMove
                {
                        get { return m_HorizonalMove; }    
                }

                public int VerticalMove
                {
                    get { return m_VerticalMove; }
                }

            }

        }
        
        internal Move(Cell i_MoveFrom, Cell i_MoveTo, bool i_IsEatingMove = false, Cell i_CellEaten = null)
        {
            m_MoveFrom = i_MoveFrom;
            m_MoveTo = i_MoveTo;
            m_isEatingMove = i_IsEatingMove;
            m_EatenCell = i_CellEaten;
        }

        internal Cell DestCell
        {
            get { return m_MoveTo; }
        }

        internal bool IsEatingMove
        {
            get { return m_isEatingMove; }
        }

        internal Cell EatenCell
        {
            get { return m_EatenCell; }
        }

        internal Cell SrcCell
        {
            get { return m_MoveFrom; }
        }

    }
}
