using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game_Logic;

namespace User_Interface
{
    public class UserMoveInput
    {
        private Point m_From;
        private Point m_To;
        private bool m_EndGame = false;

        public Point From
        {
            get
            {
                return m_From;
            }

            set
            {
                m_From = value;
            }
        }

        public Point To
        {
            get
            {
                return m_To;
            }

            set
            {
                m_To = value;
            }
        }

        public bool EndGame
        {
            get { return m_EndGame;}
            set { m_EndGame = value;}
        }
    }
}
