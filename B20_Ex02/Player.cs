using System;
using System.Collections.Generic;
using System.Text;

namespace B20_Ex02
{
    class Player
    {
        private readonly string m_Name;
        private int m_Score;

        public Player(string i_Name)
        {
            m_Name = String.Copy(i_Name);
            m_Score = 0;
        }

        public void SetPlayerScore(int i_NewScore)
        {
            m_Score = i_NewScore;
        }

        public int GetPlayerScore()
        {
            return m_Score;
        }

        public string GetPlayerName()
        {
            return m_Name;
        }
    }
}
