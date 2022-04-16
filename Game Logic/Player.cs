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

        public Player(string i_Name, eToken i_Token, ePlayerType i_Type = ePlayerType.Machine)
        {
            m_Name = i_Name;
            m_PlayerType = i_Type;
            m_Token = i_Token;
            m_Pieces = new List<Piece>();
        }

        public ePlayerType PlayerType
        {
            get { return m_PlayerType; }
            set { m_PlayerType = value; }
        }

        public void AddPiece(Piece i_Piece)
        {
            m_Pieces.Add(i_Piece);
        }

        public void RemovePiece(Piece i_Piece)
        {
            m_Pieces.Remove(i_Piece);
        }
    }
}
