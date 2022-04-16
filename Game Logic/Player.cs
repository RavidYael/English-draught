using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Logic
{
    enum ePlayerType
    {
        Human, Machine
    }

    internal class Player
    {
        private ePlayerType m_PlayerType;
        private eToken m_Token;
        private string m_Name;
        private int m_Score = 0;
        private List<Piece> m_Pieces;

        internal Player(string i_Name, eToken i_Token, ePlayerType i_Type = ePlayerType.Machine)
        {
            m_Name = i_Name;
            m_PlayerType = i_Type;
            m_Token = i_Token;
            m_Pieces = new List<Piece>();
        }

        internal ePlayerType PlayerType
        {
            get { return m_PlayerType; }
            set { m_PlayerType = value; }
        }

        internal List<Piece> Pieces
        {
            get {return m_Pieces;}
        }

        public int Score
        {
            get {return m_Score;}
            set { m_Score = value; }
        }

        public eToken Token
        {
            get { return m_Token;}
        }

        public string Name
        {
            get {return m_Name;}
        }

        internal void addPiece(Piece i_Piece)
        {
            m_Pieces.Add(i_Piece);
        }

         internal void removePiece(Piece i_Piece)
        {
            m_Pieces.Remove(i_Piece);
        }

         internal bool IsOutOfMoves()
         {
             bool outOfMoves = true;
             foreach(Piece piece in m_Pieces)
             {
                 if(piece.ValidMoves.Count != 0)
                 {
                     outOfMoves = false;
                 }
             }

             return outOfMoves;
         }

         public int getPiecesWorth()
         {
             int worth = 0;
             foreach(Piece piece in m_Pieces)
             {
                 if(piece.IsKing)
                 {
                     worth += 4;
                 }
                 else
                 {
                     worth++;
                 }
             }

             return worth;
         }

         public Move getBestPossibleMove() // as for now, the "best possible move" would be one that eats an opponent piece.
         {
             Move bestPossibleMove = null;
             Move decentMove = null;
             bool bestMoveFound = false;
             foreach(Piece piece in m_Pieces)
             {
                 foreach(Move move in piece.ValidMoves)
                 {
                     decentMove = move;
                     if(move.IsEatingMove)
                     {
                         bestPossibleMove = move;
                         bestMoveFound = true;
                     }
                 }
             }

             if(!bestMoveFound)
             {
                 bestPossibleMove = decentMove;
             }

             return bestPossibleMove;
         }

         public bool getEatingMove(out Move i_Move)
         {
             i_Move = null;
             bool foundEatingMove = false;
             foreach(Piece piece in m_Pieces)
             {
                 foreach(Move move in piece.ValidMoves)
                 {
                     if(move.IsEatingMove)
                     {
                         i_Move = move;
                         foundEatingMove = true;
                     }
                 }
             }

             return foundEatingMove;
         }
    }
}
