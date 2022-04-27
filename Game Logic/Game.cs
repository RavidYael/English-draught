﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
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
        private eTeamBaseSide m_WhosTurn;
        private bool m_GameOnGoing = true;
        private string m_Winner;
        private GamePrefernces m_GamePrefernces;


        public class GamePrefernces
        {
            private int m_NumberOfHumanPlayers;
            private int m_BoardSize;
            private String m_OPlayerName;
            private String m_XPlayerName;

            public int NumberOfHumanPlayers
            {
                get { return m_NumberOfHumanPlayers; }
                set { m_NumberOfHumanPlayers = value; }
            }

            public int BoardSize
            {
                get { return m_BoardSize; }
                set { m_BoardSize = value; }
            }

            public String OPlayerName
            {
                get { return m_OPlayerName; }
                set { m_OPlayerName = value; }
            }

            public String XPlayerName
            {
                get { return m_XPlayerName; }
                set { m_XPlayerName = value; }
            }
        }

        public Game(GamePrefernces i_GamePrefernces)
        {
            m_GamePrefernces = i_GamePrefernces;
            m_Board = new GameBoard(m_GamePrefernces.BoardSize);
            m_PlayerO = new Player(m_GamePrefernces.OPlayerName, eTeamBaseSide.Top, ePlayerType.Human);
            m_PlayerX = new Player(m_GamePrefernces.XPlayerName, eTeamBaseSide.Buttom);
            m_WhosTurn = eTeamBaseSide.Top;

            if (m_GamePrefernces.NumberOfHumanPlayers == 2)
            {
                m_PlayerX.PlayerType = ePlayerType.Human;
            }

            initializePiecesForPlayers();
            updateValidMovesForPlayer(m_PlayerO);
            updateValidMovesForPlayer(m_PlayerX);
        }

        public GameBoard GameBoard
        {
            get { return m_Board; }
        }

        public void PlayerQuits()
        {
            Player quittingPlayer = getPlayerByToken(m_WhosTurn);
            m_Winner = getPlayerByToken(getOppositeToken(quittingPlayer.Token)).Name;
            m_GameOnGoing = false;
            calculateScoreForPlayer(getPlayerByToken(getOppositeToken(quittingPlayer.Token)));
        }

        private void updateValidMovesForPlayer(Player i_Player)
        {
            foreach (Piece piece in i_Player.Pieces)
            {
                generateValidMovesForPiece(piece);

            }
        }

        private Player getPlayerByToken(eTeamBaseSide i_Token)
        {
            Player returnedPlayer = null;
            if (i_Token == eTeamBaseSide.Top)
            {
                returnedPlayer = m_PlayerO;
            }
            else
            {
                returnedPlayer = m_PlayerX;
            }

            return returnedPlayer;
        }

        private Player getPlayerByName(string i_Name)
        {
            Player toReturn;

            if (m_PlayerO.Name == i_Name)
            {
                toReturn = m_PlayerO;
            }
            else
            {
                toReturn = m_PlayerX;
            }

            return toReturn;
        }

        private eTeamBaseSide getOppositeToken(eTeamBaseSide i_Token)
        {
            eTeamBaseSide oppositeToken;
            if (i_Token == eTeamBaseSide.Top)
            {
                oppositeToken = eTeamBaseSide.Buttom;
            }
            else
            {
                oppositeToken = eTeamBaseSide.Top;
            }

            return oppositeToken;
        }

        private void switchTurn(bool i_DoubleTurn)
        {
            if (!i_DoubleTurn)
            {
                m_WhosTurn = getOppositeToken(m_WhosTurn);
            }
        }

        private void initializePiecesForPlayers()
        {
            m_PlayerO.ClearPieces();
            m_PlayerX.ClearPieces();
            List<Piece> allPieces = m_Board.allPieces;
            foreach (Piece piece in allPieces)
            {
                if (piece.OwnerBaseSide == eTeamBaseSide.Buttom)
                {
                    m_PlayerX.addPiece(piece);
                }
                else if (piece.OwnerBaseSide == eTeamBaseSide.Top)
                {
                    m_PlayerO.addPiece(piece);
                }
            }
        }

        private void generateValidMovesForPiece(Piece i_Piece)
        {
            i_Piece.ValidMoves.Clear();

            Move.Directions.Direction[] optionalDirections = getPieceOptionalDirectionsToMove(i_Piece);

            foreach (Move.Directions.Direction optionalDirection in optionalDirections)
            {

                if (m_Board.MoveInsideBounds(i_Piece.Location, optionalDirection))
                {

                    bool regularMoveWasAdded = checkAndAddRegularMoveIfPossible(i_Piece, optionalDirection);

                    if (!regularMoveWasAdded)
                    {
                        addEatingMoveIfPossible(i_Piece, optionalDirection);
                    }
                }
            }
        }

        private bool checkAndAddRegularMoveIfPossible(Piece i_Piece, Move.Directions.Direction i_direction)
        {
            int rowToCheck = i_Piece.Location.Row + i_direction.VerticalMove;
            int columnToCheck = i_Piece.Location.Column + i_direction.HorizonalMove;

            Cell cellToCheck = m_Board.getCell(rowToCheck, columnToCheck);
            Cell currentCell = m_Board.getCell(i_Piece.Location.Row, i_Piece.Location.Column);

            bool moveWasAdded = false;

            if (cellToCheck != null && cellToCheck.Occupation == false)
            {
                i_Piece.AddMove(new Move(currentCell, cellToCheck));
                moveWasAdded = true;
            }

            return moveWasAdded;

        }

        private void addEatingMoveIfPossible(Piece i_Piece, Move.Directions.Direction i_direction)
        {
            Cell currentCell = m_Board.getCell(i_Piece.Location.Row, i_Piece.Location.Column);
            Cell optionalCellToEat = m_Board.getCell(i_Piece.Location.Row + i_direction.VerticalMove, i_Piece.Location.Column + i_direction.HorizonalMove);

            if ((optionalCellToEat != null) && optionalCellToEat.Piece.OwnerBaseSide != i_Piece.OwnerBaseSide)
            {
                int afterEatRow = optionalCellToEat.Location.Row + i_direction.VerticalMove;
                int afterEatColumn = optionalCellToEat.Location.Column + i_direction.HorizonalMove;

                Point afterEatingPoint = new Point(afterEatRow, afterEatColumn);

                if (m_Board.PointInsideBounds(afterEatingPoint))
                {
                    Cell afterEatingCell = m_Board.getCell(afterEatingPoint);

                    if (!afterEatingCell.Occupation)
                    {
                        i_Piece.AddMove(new Move(currentCell, afterEatingCell, true, optionalCellToEat));
                    }
                }
            }
        }

        private Move.Directions.Direction[] getPieceOptionalDirectionsToMove(Piece i_Piece)
        {
            Move.Directions.Direction[] optionalDirections = null;
            if (i_Piece.IsKing)
            {
                optionalDirections = Move.Directions.KingDirections;
            }
            else if (i_Piece.OwnerBaseSide == eTeamBaseSide.Buttom)
            {
                optionalDirections = Move.Directions.XPawnDirections;
            }
            else if (i_Piece.OwnerBaseSide == eTeamBaseSide.Top)
            {
                optionalDirections = Move.Directions.OPawnDirections;
            }

            return optionalDirections;
        }

        private void MakeMove(Move i_MoveToMake)
        {
            bool doubleTurn = false;

            if (i_MoveToMake.IsEatingMove)
            {
                makeEatingMove(i_MoveToMake);
                updateValidMovesForPlayer(getPlayerByToken(m_WhosTurn));
                doubleTurn = i_MoveToMake.DestCell.Piece.getEatingMove(out Move move);
            }
            else
            {
                makeRegularMove(i_MoveToMake);
            }

            checkAndUpdadeIfKing(i_MoveToMake);
            switchTurn(doubleTurn);
            continueGameDependOnStatus();

        }

        private void checkAndUpdadeIfKing(Move i_lastMove)
        {

            bool needsToBeKing = ((i_lastMove.DestCell.Location.Row == 0) || (i_lastMove.DestCell.Location.Row == m_GamePrefernces.BoardSize - 1));

            if (needsToBeKing)
            {
                i_lastMove.DestCell.Piece.makeKing();
            }
        }

        private void makeRegularMove(Move i_MoveToMake)
        {
            m_Board.MovePeice(i_MoveToMake.SrcCell, i_MoveToMake.DestCell);
        }

        private void makeEatingMove(Move i_MoveToMake)
        {
            Cell srcCell = i_MoveToMake.SrcCell;
            Cell destCell = i_MoveToMake.DestCell;
            removePieceFromPlayer(i_MoveToMake.EatenCell.Piece);
            i_MoveToMake.EatenCell.removePieceFromCell();
            m_Board.MovePeice(srcCell, destCell);
        }

        private void continueGameDependOnStatus()
        {
            checkAndUpdateIfGameFinished();

            if (m_GameOnGoing)
            {
                updateValidMovesForPlayer(getPlayerByToken(m_WhosTurn));
            }
        }

        private void checkAndUpdadeIfKing(Cell i_ToCell)
        {
            bool needsToBeKing = i_ToCell.Location.Row == 0 || i_ToCell.Location.Row == m_Board.Size;
            if (needsToBeKing)
            {
                i_ToCell.Piece.makeKing();
            }
        }

        public void MakeComputerMove()
        {
            System.Threading.Thread.Sleep(3000);
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

            if (playerOHasNoPiecesLeft || playerOHasNoMovesLeft)
            {
                m_GameOnGoing = false;
                m_Winner = m_PlayerX.Name;
                calculateScoreForPlayer(m_PlayerX);
            }
            else if (playerXHasNoPiecesLeft || playerXHasNoMovesLeft)
            {
                m_GameOnGoing = false;
                m_Winner = m_PlayerO.Name;
                calculateScoreForPlayer(m_PlayerO);
            }
        }

        private void calculateScoreForPlayer(Player i_WinningPlayer)
        {
            Player losingPlayer = getPlayerByToken(getOppositeToken(i_WinningPlayer.Token));
            int winningPlayerPiecesWorth = i_WinningPlayer.getPiecesWorth();
            int losingPlayerPiecesWorth = losingPlayer.getPiecesWorth();
            i_WinningPlayer.Score += winningPlayerPiecesWorth - losingPlayerPiecesWorth;
        }

        private void removePieceFromPlayer(Piece i_PieceToRemove)
        {
            if (i_PieceToRemove.OwnerBaseSide == eTeamBaseSide.Top)
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
            bool validMove = true;
            Move moveToValidate = null;
            if (to != null && from != null)
            {
                moveToValidate = from.Piece.getMove(to);
            }
            else
            {
                o_ErrorMessage = "Invalid move: Cell (destination/source) is out of bounds";
                validMove = false;
            }
            if (validMove)
            {
                if (!from.Occupation)
                {
                    o_ErrorMessage = "Invalid move: No piece in that given location";
                    validMove = false;
                }
                else if (from.Piece.OwnerBaseSide != m_WhosTurn)
                {
                    o_ErrorMessage = string.Format("Invalid move: It is {0}'s turn", m_WhosTurn);
                    validMove = false;
                }
                else if (!from.Piece.hasValidMoveToInputCell(to))
                {
                    o_ErrorMessage = "Invalid move: Piece can't move to destination cell";
                    validMove = false;
                }
                else if (getPlayerByToken(m_WhosTurn).getEatingMove(out Move eatingMove))
                {
                    if (!moveToValidate.IsEatingMove)
                    {
                        o_ErrorMessage = "Invalid move: Eating move must be execute";
                        validMove = false;
                    }
                }
            }

            return validMove;
        }

        public void Reset()
        {
            m_Board = new GameBoard(m_Board.Size);
            m_GameOnGoing = true;
            initializePiecesForPlayers();
            updateValidMovesForPlayer(m_PlayerO);
            updateValidMovesForPlayer(m_PlayerX);
            m_WhosTurn = eTeamBaseSide.Top;
        }

        public bool OnGoing
        {
            get { return m_GameOnGoing; }
        }

        public string WhosTurnName
        {
            get { return getPlayerByToken(m_WhosTurn).Name; }
        }

        public bool isMachineTurn()
        {
            return getPlayerByToken(m_WhosTurn).PlayerType == ePlayerType.Machine;
        }

        public string Winner
        {
            get { return getPlayerByName(m_Winner).Name; }
        }

        public int getWinnerScore()
        {
            return getPlayerByName(m_Winner).Score;
        }
    }
}
