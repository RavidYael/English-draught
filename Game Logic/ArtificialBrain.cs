using System;
using System.Collections.Generic;

namespace Game_Logic
{
    internal class ArtificialBrain
    {
        internal void MakeIntelligentMove(Game i_game)
        {
            Player playerInTurn = i_game.getPlayerInTurn();
            Move bestMoveToExcecute = findBestValidMoveToComputer(i_game, 3);
            i_game.SetPlayerTurn(playerInTurn);
            i_game.OnGoing = true;
            i_game.MakeMove(bestMoveToExcecute);
        }

        private void MakeVirtualMove(Game i_CheckersGame, Move i_MoveToMake, out bool o_isBecomingKing)
        {

            bool doubleTurn = false;

            if (i_MoveToMake.IsEatingMove)
            {
                i_CheckersGame.makeVirtualEatingMove(i_MoveToMake);
                i_CheckersGame.UpdateValidMovesForPlayerInTurn();
                doubleTurn = i_MoveToMake.DestCell.Piece.getEatingMoveAndReturnIfFoundOne(out Move move);
            }
            else
            {
                i_CheckersGame.MakeRegularMove(i_MoveToMake);
            }

            i_CheckersGame.CheckAndUpdadeIfVirtualKing(i_MoveToMake, out o_isBecomingKing);
            i_CheckersGame.SwitchTurn(doubleTurn);
            i_CheckersGame.ContinueGameDependOnStatus();
        }

        private Move findBestValidMoveToComputer(Game i_CheckersGame, int i_Depth)
        {
            Move bestMove = null;
            GenerateMoveValueAtGivenDepth(i_CheckersGame, i_Depth, ref bestMove);
            return bestMove;
        }

        private int initializingMinMaxValueAccordingToTurn(Game i_CheckersGame)
        {
            int value = 0;

            if (i_CheckersGame.isMachineTurn())
            {
                value = Int32.MinValue;
            }
            else
            {
                value = Int32.MaxValue;
            }
            return value;
        }

        private void assigningMoveAndValueToBeBestAccordingToTheMinMax(Game i_CheckersGame, int i_CurrentMoveValue, Move i_CurrentMove, ref int o_BestMoveValue, ref Move o_BestMove, Player i_moveExecutingPlayer)
        {
            if (i_moveExecutingPlayer.PlayerType == ePlayerType.Machine)
            {
                if (i_CurrentMoveValue > o_BestMoveValue)
                {
                    o_BestMoveValue = i_CurrentMoveValue;
                    o_BestMove = i_CurrentMove;
                }
            }
            else
            {
                if (i_CurrentMoveValue < o_BestMoveValue)
                {
                    o_BestMoveValue = i_CurrentMoveValue;
                    o_BestMove = i_CurrentMove;
                }
            }
        }

        public int GenerateMoveValueAtGivenDepth(Game i_CheckersGame, int i_Depth, ref Move o_BestMove)
        {
            Move currentBestMove = null;
            List<Move> possibleMoves;
            int bestMoveValue = initializingMinMaxValueAccordingToTurn(i_CheckersGame);
            int currentMoveValue;

            if (i_Depth == 0 || i_CheckersGame.GameOn == false)
            {
                bestMoveValue = calcComputerStateValue(i_CheckersGame);
            }
            else
            {
                possibleMoves = i_CheckersGame.GetAllMovesForPlayerInTurn();

                foreach (Move possibleMove in possibleMoves)
                {
                    Player playerInTurn = i_CheckersGame.getPlayerInTurn();
                    bool isBecomingKing;

                    MakeVirtualMove(i_CheckersGame, possibleMove, out isBecomingKing);

                    currentMoveValue = GenerateMoveValueAtGivenDepth(i_CheckersGame, i_Depth - 1, ref o_BestMove);

                    assigningMoveAndValueToBeBestAccordingToTheMinMax(i_CheckersGame, currentMoveValue, possibleMove, ref bestMoveValue, ref currentBestMove, playerInTurn);

                    undoVirtualMove(i_CheckersGame, possibleMove, isBecomingKing, playerInTurn);
                }
            }

            o_BestMove = currentBestMove;

            return bestMoveValue;
        }

        private void undoVirtualMove(Game i_CheckersGame, Move i_MoveToUndo, bool i_isBecomingKingInThisMove, Player i_PlayerExecutedMove)
        {
            bool doubleTurn = false;

            if (i_MoveToUndo.IsEatingMove)
            {
                i_CheckersGame.undoVirtualEatingMove(i_MoveToUndo, m_VirtualEatenPieces, i_PlayerExecutedMove);
                i_CheckersGame.UpdateValidMovesForPlayerInTurn();
                doubleTurn = i_MoveToUndo.SrcCell.Piece.getEatingMoveAndReturnIfFoundOne(out Move move);
            }
            else
            {
                i_CheckersGame.undoRegularMove(i_MoveToUndo);
            }

            i_CheckersGame.CheckAndUpdadeIfKingDemotionNeeded(i_MoveToUndo, i_isBecomingKingInThisMove);
            i_CheckersGame.SwitchTurn(doubleTurn);
            i_CheckersGame.ContinueGameDependOnStatus();
        }


        private static int calcComputerStateValue(Game i_CheckersGame)
        {
            int computerStateValue = 0;
            Player computerPlayer = i_CheckersGame.getMachinePlayer();
            Player oppositePlayer = i_CheckersGame.getPlayerByTeamSide(i_CheckersGame.getOppositeToken(computerPlayer.BaseSide));

            foreach (Piece piece in computerPlayer.Pieces)
            {
                if (piece.IsKing)
                {
                    computerStateValue += 5;
                }
                else
                {
                    computerStateValue += 2;
                }
            }

            foreach (Piece piece in oppositePlayer.Pieces)
            {
                if (piece.IsKing)
                {
                    computerStateValue -= 5;
                }
                else
                {
                    computerStateValue -= 2;
                }
            }

            return computerStateValue;
        }

    }
}