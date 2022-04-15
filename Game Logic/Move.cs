using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Game_Logic
{
    internal class Move
    {
        private Cell m_MoveTo;
        private bool m_isEatingMove = false;
        private Cell m_CellEaten;

        internal Move() // TODO is this okay?
        {}

        internal Move(Cell i_MoveTo, bool i_IsEatingMove = false, Cell i_CellEaten = null)
        {
            m_MoveTo = i_MoveTo;
            m_isEatingMove = i_IsEatingMove;
            m_CellEaten = i_CellEaten;
        }

        internal Cell Cell
        {
            get {return m_MoveTo;}
        }

        internal bool IsEatingMove
        {
            get { return m_isEatingMove;}
        }

        internal Cell CellEaten
        {
            get {return m_CellEaten;}
        }

    }
}
