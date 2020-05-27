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
        private int m_MemorySize = 0;

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

        public void SetMemory(int i_Rows, int i_Columns)
        {
            m_Memory = new List<BoardCell>(i_Rows * i_Columns);
            foreach(int i in m_Memory)
            {
                m_Memory[i] = null;
            }
            m_MemorySize = i_Rows * i_Columns;
        }




        public void AddToAIMemory(BoardCell i_Cell, int i_MaxRows, int i_Cols)
        {
            int row = i_Cell.Location.Row;
            int column = i_Cell.Location.Col
            m_Memory[(row * i_MaxRows) + column] = i_Cell;
        }

        public int NextEmptyPlace()
        {
            int i = 0;            
            while(m_Memory[i] != null)
            {
                i++;
            }
            return i;
        }

        public int GetPair(int i_Key)
        {
            bool first = false;
            int result;

            for(int i = 0; i < m_MemorySize; i++)
            {
                if(first == false && m_Memory[i] == null)
                {
                    result = i;
                    first == true;
                }
                if(m_Memory[i].Key == i_Key)
                {
                    return i;
                }
            }
        }

        public void MakeVisible(int i_Rows, int i_Cols, int i_MaxRows)
        {
            m_Memory[(i_Rows * i_MaxRows) + i_Cols].Show();
        }
    }
}
