using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Logic
{
    internal class Point
    {
        private int m_Row;
        private int m_Column;

        public Point(int i_Row, int i_Col)
        {
            m_Row = i_Row;
            m_Column = i_Col;
        }

        public int Row
        {
            get { return m_Row; }
            set { m_Row = value; }
        }

        public int Column
        {
            get {return m_Column;}
            set { m_Column = value; }
        }
    }
}
