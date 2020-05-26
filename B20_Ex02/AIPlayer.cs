using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace B20_Ex02
{
    class AIPlayer
    {
        private const string m_Name = "AI Player";
        private int m_Score;
        private List<BoardCell> m_Memory;
        private List<int> m_Moves;

        public AIPlayer()
        {
            m_Score = 0;
        }

        public string Name
        {
            get
            {
                return m_Name;
            }
        }
        public int Score
        {
            get
            {
                return m_Score;
            }
        }
        public void AddScore()
        {
            m_Score++;
        }
        public void AddToMemory(BoardCell cell)
        {
            m_Memory.Add(cell);
        }
        public bool IsCellInMemory(BoardCell cell)
        {
            return m_Memory.Contains(cell);
        }
        public void AddNewMove(int i_Move)
        {
            m_Moves.Add(i_Move);
        }
        public int GetMove()
        {
     
        }
    }
}
