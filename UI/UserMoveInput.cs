﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game_Logic;

namespace UI
{
    internal class UserMoveInput
    {
        private Point m_From;
        private Point m_To;

        public Point From
        {
            get {return m_From;}
            set {m_From = value;}
        }

        public Point To
        {
            get {return m_To;}
            set {m_To = value;}
        }
    }
}
