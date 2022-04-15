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
        private int m_Size;

        public GameBoard(int i_BoardSize)
        {
            allocateBoard(i_BoardSize);
            m_Size = i_BoardSize;

            for(int i = 0; i < m_Size; i++)
            {
                for(int j = 0; j < m_Size; j++)
                {
                    m_Board[i, j] = new Cell(i, j, m_Size);
                }
            }
        }

        private void allocateBoard(int i_BoardSize)
        {
            m_Board = new Cell[i_BoardSize, i_BoardSize];

            //for(int i = 0; i < i_BoardSize; i++)
            //{
            //    m_Board.ElementAt(i) = new List<Cell>(i_BoardSize);
            //}
        }

        public void PrintBoard()
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
                    if(!m_Board[i, j].IsOccupied)
                    {
                        Console.Write("   ");
                    }

                    // WE NEED TO DO HERE SOMTHING ELSE FOR KINGS
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
                                Console.WriteLine(" U ");
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
                                Console.WriteLine(" K ");
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
    }
}
