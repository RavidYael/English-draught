﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Logic
{
    enum eToken
    {
        O, X
    }

    internal class Piece
    {
        private eToken m_Token;
        private bool m_IsKing = false;
        private List<Cell> m_ValidMoves;

        public Piece(eToken i_Token)
        {
            m_Token = i_Token;
        }

        public eToken Token
        {
            get
            {
                return m_Token;
            }

            set
            {
                m_Token = value;
            }
        }

        public bool IsKing
        {
            get
            {
                return m_IsKing;
            }

            set
            {
                m_IsKing = value;
            }
        }
    }
}
