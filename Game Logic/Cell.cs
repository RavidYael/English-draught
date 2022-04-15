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

        public Cell (int i_Row, int i_Col, int i_BoardSize)
        {
            int numberOfRowsForPlayer = ((i_BoardSize / 2) - 1);
            bool playerOCell = i_Row < numberOfRowsForPlayer;
            bool playerXCell = i_Row >= numberOfRowsForPlayer + 2;
            bool differInParity = checkDifferInParity(i_Row, i_Col);
            bool emptyRow = (i_Row >= numberOfRowsForPlayer) && (i_Row < numberOfRowsForPlayer + 2);

            if(differInParity && !emptyRow)
            {
                m_IsOccupied = true;

                if(playerOCell)
                {
                    m_Piece = new Piece(eToken.O);
                }
                else if(playerXCell)
                {
                    m_Piece = new Piece(eToken.X);
                }
            }

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


    }
}
