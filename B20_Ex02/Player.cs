using System;
using System.Collections.Generic;
using System.Text;

namespace B20_Ex02
{
    class Player
    {
        private string m_Name;
        private int m_Score;

        public Player(string i_Name)
        {
            m_Name = i_Name;
            m_Score = 0;
        }
        public string Name
        {
            get
            {
                return m_Name;
            }
            set
            {
                if(m_Name.Equals(value) == false)
                {
                    m_Name = value;
                }
            }
        }
        public void AddScore()
        {
            m_Score++;
        }
    }
}
