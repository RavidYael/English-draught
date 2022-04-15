using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Logic
{
    internal class Point
    {
        private char m_Row;
        private char m_Column;

        public Point(int i_Row, int i_Col)
        {
            m_Row = (char)(i_Row + 'A');
            m_Column = (char)(i_Col + 'a');
        }
    }
}
