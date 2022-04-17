using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Game_Logic
{
    public class Game
    {
        private GameBoard m_Board;
        private Player m_PlayerO;
        private Player m_PlayerX;
        private int m_NumberOfHumanPlayers;
        private eToken m_WhosTurn;
        private bool m_GameOnGoing = true;
        private eToken m_Winner;

        public Game(int i_BoardSize, int i_NumOfPlayers, string i_PlayerName1, string i_PlayerName2 = "computer")
        {
            m_NumberOfHumanPlayers = i_NumOfPlayers;
            m_Board = new GameBoard(i_BoardSize);
            m_PlayerO = new Player(i_PlayerName1, eToken.O, ePlayerType.Human);
            m_PlayerX = new Player(i_PlayerName2, eToken.X);
            m_WhosTurn = eToken.O;

            if(i_NumOfPlayers == 2)
            {
                m_PlayerX.PlayerType = ePlayerType.Human;
            }

            initializePiecesForPlayers();
            updateValidMovesForPlayer(m_PlayerO);
            updateValidMovesForPlayer(m_PlayerX);
        }

        private void updateValidMovesForPlayer(Player i_Player)
        {
            foreach(Piece piece in i_Player.Pieces)
            {
                if (piece.IsKing)
                {
                    generateValidMovesForKingPiece(piece);
                }
                else
                {
                    generateValidMovesForPiece(piece);
                }
            }
        }

        private Player getPlayerByToken(eToken i_Token)
        {
            Player returnedPlayer = null;
            if (i_Token == eToken.O)
            {
                returnedPlayer = m_PlayerO;
            }
            else
            {
                returnedPlayer = m_PlayerX;
            }

            return returnedPlayer;
        }

        private eToken getOppositeToken(eToken i_Token)
        {
            eToken oppositeToken;
            if (i_Token == eToken.O)
            {
                oppositeToken = eToken.X;
            }
            else
            {
                oppositeToken = eToken.O;
            }

            return oppositeToken;
        }

        private void switchTurn(bool i_DoubleTurn)
        {
            if(!i_DoubleTurn)
            {
                m_WhosTurn = getOppositeToken(m_WhosTurn);
            }
        }

        private void initializePiecesForPlayers()
        {
            List<Piece> allPieces = m_Board.allPieces;
            foreach(Piece piece in allPieces)
            {
                if (piece.Token == eToken.X)
                {
                    m_PlayerX.addPiece(piece);
                }
                else if(piece.Token == eToken.O)
                {
                    m_PlayerO.addPiece(piece);
                }
            }
        }

        public void PrintBoard()
        {
            m_Board.PrintBoard();
        }

        private void generateValidMovesForPiece(Piece i_Piece)
        {
            i_Piece.ValidMoves.Clear();
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
                        generatePossibleMovesForDirection(i_Piece, -1, -1);
                    }

                    if(moveInsideRightBounds)
                    {
                        generatePossibleMovesForDirection(i_Piece, -1, 1);
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
                        generatePossibleMovesForDirection(i_Piece, 1, -1);
                    }

                    if(moveInsideRightBounds)
                    {
                        generatePossibleMovesForDirection(i_Piece, 1, 1);
                    }
                }
            }
        }

        private void generateValidMovesForKingPiece(Piece i_Piece)
        {
            int pieceRow = i_Piece.Location.Row;
            int pieceCol = i_Piece.Location.Column;
            bool moveInsideLeftBounds = pieceCol - 1 >= 0;
            bool moveInsideRightBounds = pieceCol + 1 < m_Board.Size;
            bool moveInsideRowBoundsUpwards = pieceRow - 1 >= 0;
            bool moveInsideRowBoundsDownwards = pieceRow +1 < m_Board.Size;

            if (moveInsideRowBoundsUpwards)
            {
                if(moveInsideLeftBounds)
                {
                    generatePossibleMovesForDirection(i_Piece, -1, -1);
                }

                if(moveInsideRightBounds)
                {
                    generatePossibleMovesForDirection(i_Piece,-1, 1);
                }
            }

            if (moveInsideRowBoundsDownwards)
            {
                if (moveInsideLeftBounds)
                {
                    generatePossibleMovesForDirection(i_Piece, 1, -1);
                }

                if (moveInsideRightBounds)
                {
                    generatePossibleMovesForDirection(i_Piece, 1, 1);
                }
            }
        }

        private void generatePossibleMovesForDirection(Piece i_Piece, int i_RowDirection, int i_ColumnDirection)
        {
            int rowToCheck = i_Piece.Location.Row + i_RowDirection;
            int columnToCheck = i_Piece.Location.Column + i_ColumnDirection;
            Cell cellToCheck = m_Board.getCell(rowToCheck, columnToCheck);
            Cell currentCell = m_Board.getCell(i_Piece.Location.Row, i_Piece.Location.Column);

            if(!cellToCheck.IsOccupied)
            {
                i_Piece.AddMove(new Move(currentCell, cellToCheck));
            }
            else
            {
                if(cellToCheck.Piece.Token != i_Piece.Token)
                {
                    checkAndUpdateIfEatPossible(i_Piece, cellToCheck.Location, i_RowDirection, i_ColumnDirection);
                }
            }
        }

        private void checkAndUpdateIfEatPossible(Piece i_Piece, Point i_LocationToEat, int i_EatRowDirection, int i_EatColumnDirection)
        {
            int afterEatRow = i_LocationToEat.Row + i_EatRowDirection;
            int afterEatColumn = i_LocationToEat.Column + i_EatColumnDirection;
            bool afterEatInsideRowBounds = afterEatRow < m_Board.Size && afterEatRow >= 0;
            bool afterEatInsideColumnBounds = afterEatColumn < m_Board.Size && afterEatColumn >= 0;

            if(afterEatInsideRowBounds && afterEatInsideColumnBounds)
            {
                Cell currentCell = m_Board.getCell(i_Piece.Location.Row, i_Piece.Location.Column);
                Cell cellToEat = m_Board.getCell(i_LocationToEat.Row, i_LocationToEat.Column);
                Cell afterEatCell = m_Board.getCell(afterEatRow, afterEatColumn);

                if(!afterEatCell.IsOccupied)
                {
                    i_Piece.AddMove(new Move(currentCell, afterEatCell, true, cellToEat));
                }
            }
        }

        private void MakeMove(Move i_MoveToMake)
        {
            Cell fromCell = i_MoveToMake.MoveFrom;
            Cell toCell = i_MoveToMake.MoveTo;
            bool doubleTurn = false;

            if (i_MoveToMake.IsEatingMove)
            {
                removePieceFromPlayer(i_MoveToMake.CellEaten.Piece);
                i_MoveToMake.CellEaten.removePieceFromCell();
                fromCell.movePiece(toCell);
                updateValidMovesForPlayer(getPlayerByToken(m_WhosTurn));
                doubleTurn = toCell.Piece.getEatingMove(out Move move);
            }
            else
            {
                fromCell.movePiece(toCell);
            }

            checkAndUpdadeIfKing(toCell);
            checkAndUpdateIfGameFinished();
            if (m_GameOnGoing)
            {
                switchTurn(doubleTurn);
                updateValidMovesForPlayer(getPlayerByToken(m_WhosTurn));
            }
        }

        private void checkAndUpdadeIfKing(Cell i_ToCell)
        {
            bool needsToBeKing = i_ToCell.Location.Row == 0 || i_ToCell.Location.Row == m_Board.Size;
            if(needsToBeKing)
            {
                i_ToCell.Piece.makeKing();
            }
        }

        public void MakeComputerMove()
        {
            Move moveToMake = m_PlayerX.getBestPossibleMove();
            MakeMove(moveToMake);
        }

        public void MakeHumanMove(Point i_FromLocation, Point i_ToLocation)
        {
            Cell fromCell = m_Board.getCell(i_FromLocation.Row, i_FromLocation.Column);
            Cell toCell = m_Board.getCell(i_ToLocation.Row, i_ToLocation.Column);
            Move moveToMake = fromCell.Piece.getMove(toCell);
            MakeMove(moveToMake);
        }

        private void checkAndUpdateIfGameFinished()
        {
            bool playerOHasNoPiecesLeft = m_PlayerO.Pieces.Count == 0;
            bool playerOHasNoMovesLeft = m_PlayerO.IsOutOfMoves();
            bool playerXHasNoPiecesLeft = m_PlayerX.Pieces.Count == 0;
            bool playerXHasNoMovesLeft = m_PlayerX.IsOutOfMoves();

            if(playerOHasNoPiecesLeft || playerOHasNoMovesLeft)
            {
                m_GameOnGoing = false;
                m_Winner = eToken.X;
                calculateScoreForPlayer(m_PlayerX);
            }
            else if (playerXHasNoPiecesLeft || playerXHasNoMovesLeft)
            {
                m_GameOnGoing = false;
                m_Winner = eToken.O;
                calculateScoreForPlayer(m_PlayerO);
            }
        }

        private void calculateScoreForPlayer(Player i_WinningPlayer)
        {
            Player losingPlayer = getPlayerByToken(getOppositeToken(i_WinningPlayer.Token));
            int winningPlayerPiecesWorth = i_WinningPlayer.getPiecesWorth();
            int losingPlayerPiecesWorth = losingPlayer.getPiecesWorth();
            i_WinningPlayer.Score = winningPlayerPiecesWorth - losingPlayerPiecesWorth;
        }

        private void removePieceFromPlayer(Piece i_PieceToRemove)
        {
            if(i_PieceToRemove.Token == eToken.O)
            {
                m_PlayerO.removePiece(i_PieceToRemove);
            }
            else
            {
                m_PlayerX.removePiece(i_PieceToRemove);
            }
        }

        public bool IsValidMove(Point i_FromLocation, Point i_ToLocation, out string o_ErrorMessage)
        {
            o_ErrorMessage = "";
            Cell from = m_Board.getCell(i_FromLocation.Row, i_FromLocation.Column);
            Cell to = m_Board.getCell(i_ToLocation.Row, i_ToLocation.Column);
 
          Move moveToValidate = from.Piece.getMove(to);

            bool validMove = true;
            if (!from.IsOccupied)
            {
                o_ErrorMessage = "Invalid move, no piece in that given location";
                validMove = false;
            }
            else if (from.Piece.Token != m_WhosTurn)
            {
                o_ErrorMessage = string.Format("Invalid move, it is {0}'s turn", m_WhosTurn);
                validMove = false;
            }
            else if (!from.Piece.moveContainsCell(to))
            {
                o_ErrorMessage = "Invalid move";
                validMove = false;
            }
            else if (getPlayerByToken(m_WhosTurn).getEatingMove(out Move eatingMove))
            {
                if (!moveToValidate.IsEatingMove)
                if (eatingMove.MoveTo.Piece != to.Piece)
                {
                    o_ErrorMessage = "Invalid move, you must execute eat move";
                    validMove = false;
                }
            }

            return validMove;
        }

        public bool OnGoing
        {
            get {return m_GameOnGoing;}
        }

        public string WhosTurnName
        {
            get {return getPlayerByToken(m_WhosTurn).Name;}
        }

        public int NumberOfHumanPlayers
        {
            get{ return m_NumberOfHumanPlayers;}
        }

        public bool isMachineTurn()
        {
            return getPlayerByToken(m_WhosTurn).PlayerType == ePlayerType.Machine;
        }

        
    }
}
