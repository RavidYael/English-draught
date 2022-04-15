using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Logic
{
    public class Game
    {
        private GameBoard m_Board;
        private Player m_PlayerO;
        private Player m_PlayerX;

        public Game(int i_BoardSize, int i_NumOfPlayers, string i_PlayerName1, string i_PlayerName2 = "computer")
        {
            m_Board = new GameBoard(i_BoardSize);
            m_PlayerO = new Player(i_PlayerName1, eToken.O, ePlayerType.Human);
            m_PlayerX = new Player(i_PlayerName2, eToken.X);

            if(i_NumOfPlayers == 2)
            {
                m_PlayerX.PlayerType = ePlayerType.Human;
            }

            initializePiecesForPlayers();
        }

        private void initializePiecesForPlayers()
        {
            Cell[,] board = m_Board.getBoard();

        }

        public void PrintBoard()
        {
            m_Board.PrintBoard();
        }

        public void UpdateValidMoves()
        {

        }

        private void generateValidMovesForPiece(Piece i_Piece)
        {
            Cell[,] board = m_Board.getBoard();
            int pieceRow = i_Piece.Location.Row;
            int pieceCol = i_Piece.Location.Column;
            Cell cellToValidate = board[i_Piece.Location.Row, i_Piece.Location.Column];
            bool moveInsideLeftBounds = pieceCol - 1 >= 0;
            bool moveInsideRightBounds = pieceCol + 1 < m_Board.Size;

            // TODO if piece is a king?
            if(i_Piece.Token == eToken.X)
            {
                bool moveInsideRowBounds = pieceRow - 1 >= 0;

                if(moveInsideRowBounds)
                {
                    if(moveInsideLeftBounds)
                    {
                        updatePossibleMovesForDirection(i_Piece, -1, -1);
                    }

                    if(moveInsideRightBounds)
                    {
                        updatePossibleMovesForDirection(i_Piece, -1, 1);
                    }
                }

            }

            else if(i_Piece.Token == eToken.O)
            {
                bool moveInsideRowBounds = pieceRow + 1 < m_Board.Size;

                if(moveInsideRowBounds)
                {
                    if(moveInsideLeftBounds)
                    {
                        updatePossibleMovesForDirection(i_Piece, 1, -1);
                    }

                    if(moveInsideRightBounds)
                    {
                        updatePossibleMovesForDirection(i_Piece, 1, 1);
                    }
                }
            }
        }

        private void updatePossibleMovesForDirection(Piece i_Piece, int i_RowDirection, int i_ColumnDirection)
        {
            int rowToCheck = i_Piece.Location.Row + i_RowDirection;
            int columnToCheck = i_Piece.Location.Column + i_ColumnDirection;
            Cell cellToCheck = m_Board.getCell(rowToCheck, columnToCheck);

            if(!cellToCheck.IsOccupied)
            {
                i_Piece.AddMove(cellToCheck);
            }

            else
            {
                if(cellToCheck.Piece.Token != i_Piece.Token)
                {
                    checkAndUpdateIfEatPossible(i_Piece, cellToCheck.Location, i_RowDirection, i_ColumnDirection);
                }
            }
        }

        private void checkAndUpdateIfEatPossible(
            Piece i_Piece,
            Point i_LocationToEat,
            int i_EatRowDirection,
            int i_EatColumnDirection)
        {
            int afterEatRow = i_LocationToEat.Row + i_EatRowDirection;
            int afterEatColumn = i_LocationToEat.Column + i_EatColumnDirection;
            bool afterEatInsideRowBounds = afterEatRow < m_Board.Size && afterEatRow >= 0;
            bool afterEatInsideColumnBounds = afterEatColumn < m_Board.Size && afterEatColumn >= 0;

            if(afterEatInsideRowBounds && afterEatInsideColumnBounds)
            {
                Cell afterEatCell = m_Board.getCell(afterEatRow, afterEatColumn);

                if(!afterEatCell.IsOccupied)
                {
                    i_Piece.AddMove(afterEatCell);
                }
            }
        }
    }
}
