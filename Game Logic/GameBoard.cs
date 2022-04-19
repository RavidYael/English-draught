using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Logic
{
    internal class GameBoard
    {
        private Cell [,] m_Board;
        private List<Piece> m_AllPieces;
        private int m_Size;

        internal GameBoard(int i_BoardSize)
        {
            m_AllPieces = new List<Piece>();
            allocateBoard(i_BoardSize);
            m_Size = i_BoardSize;

            for(int i = 0; i < m_Size; i++)
            {
                for(int j = 0; j < m_Size; j++)
                {
                    m_Board[i, j] = makeCell(i, j);
                }
            }
        }

        private Cell makeCell(int i_Row, int i_Column)
        {
            Cell newCell = new Cell(i_Row, i_Column);
            Piece newPiece = null;

            int numberOfRowsForPlayer = ((m_Size / 2) - 1);
            bool playerOCell = i_Row < numberOfRowsForPlayer;
            bool playerXCell = i_Row >= numberOfRowsForPlayer + 2;
            bool differInParity = checkDifferInParity(i_Row, i_Column);
            bool emptyRow = (i_Row >= numberOfRowsForPlayer) && (i_Row < numberOfRowsForPlayer + 2);

            if(differInParity && !emptyRow)
            {
                if(playerOCell)
                {
                    newPiece = new Piece(eToken.O, newCell.Location);
                }
                else if(playerXCell)
                {
                    newPiece = new Piece(eToken.X, newCell.Location);
                }

                newCell = new Cell(i_Row, i_Column, newPiece);
                m_AllPieces.Add(newPiece);
            }

            return newCell;
        }

        private bool checkDifferInParity(int i_Row, int i_Column)
        {
            return (i_Row % 2) != (i_Column % 2);
        }

        internal int Size
        {
            get { return m_Size; }
        }

        private void allocateBoard(int i_BoardSize)
        {
            m_Board = new Cell[i_BoardSize, i_BoardSize];

            //for(int i = 0; i < i_BoardSize; i++)
            //{
            //    m_Board.ElementAt(i) = new List<Cell>(i_BoardSize);
            //}
        }

        internal Cell[,] getBoard()
        {
            return m_Board;
        }

        internal List<Piece> allPieces
        {
            get {return m_AllPieces;}
        }

        internal void PrintBoard()
        {
            Console.Write("   ");
            for (int k = 0; k < m_Size; k++)
            {
                Console.Write(" " + (char)(k + 65) + "  ");
            }

            Console.WriteLine();
            Console.Write("  ");
            for (int l = 0; l < m_Size; l++)
            {
                Console.Write("====");
            }

            Console.WriteLine("=");
            for (int i = 0; i < m_Size; i++)
            {
                Console.Write((char)(i + 97) + " ");
                for(int j = 0; j < m_Size; j++)
                {
                    Console.Write("|");
                    if(!m_Board[i, j].Occupation)
                    {
                        Console.Write("   ");
                    }

                    else
                    {
                        if(m_Board[i, j].Piece.Token == eToken.O)
                        {
                            if(!m_Board[i, j].Piece.IsKing)
                            {
                                Console.Write(" O ");
                            }
                            else
                            {
                                Console.Write(" U ");
                            }
                        }
                        else
                        {
                            if(!m_Board[i, j].Piece.IsKing)
                            {
                                Console.Write(" X ");
                            }
                            else
                            {
                                Console.Write(" K ");
                            }
                        }
                    }

                    if (j == m_Size - 1)
                    {
                        Console.Write("|");
                    }
                }

                Console.WriteLine();
                Console.Write("  ");
                for (int l = 0; l < m_Size; l++)
                {
                    Console.Write("====");
                }

                Console.WriteLine("=");
            }
        }

        internal Cell getCell(int i_Row, int i_Column)
        {
            Point destPoint = new Point(i_Row, i_Column);
            return getCell(destPoint);
        }

        internal Cell getCell(Point i_Point)
        {
            Cell desiredCell = null;
            if (PointInsideBounds(i_Point))
            {
                desiredCell = m_Board[i_Point.Row, i_Point.Column];
            }

            return desiredCell;
        }

        internal bool PointInsideBounds(Point i_destPoint)
        {
            bool insideBounds = false;
            if (insideRowBounds(i_destPoint.Row) && insideCollBounds(i_destPoint.Column))
            {
                insideBounds = true;
            }

            return insideBounds;
        }


        internal bool MoveInsideBounds(Point i_srcLocation, Move.Directions.Direction i_optionalDirection)
        {
            bool insideBounds = false;
            if (insideRowBounds(i_srcLocation.Row + i_optionalDirection.VerticalMove) && insideCollBounds(i_srcLocation.Column + i_optionalDirection.HorizonalMove))
            {
                insideBounds = true;
            }

            return insideBounds;
        }

        private bool insideRowBounds(int i_rowLocation)
        {
            bool insideRowBounds = false;
            if (i_rowLocation < m_Size && i_rowLocation >= 0)
            {
                insideRowBounds = true;
            }

            return insideRowBounds;
        }
        private bool insideCollBounds(int i_collLocation)
        {
            bool insideCollBounds = false;
            if (i_collLocation < m_Size && i_collLocation >= 0)
            {
                insideCollBounds = true;
            }

          return insideCollBounds;
        }

        internal void MovePeice(Cell i_MoveFrom, Cell i_MoveTo)
        {
            i_MoveTo.placePiece(i_MoveFrom.Piece);
            i_MoveFrom.Occupation = false;
            i_MoveFrom.Piece = null;
        }
    }
}
