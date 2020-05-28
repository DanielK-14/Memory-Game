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
        private List<MemoryCell> m_Memory;
        private List<MemoryCell> m_Moves;

        public AIPlayer()
        {
            m_Score = 0;
            m_Memory = new List<MemoryCell>();
            m_Moves = new List<MemoryCell>();
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

        public List<MemoryCell> Memory
        {
            get
            {
                return m_Memory;
            }
        }

        public void AddScore()
        {
            m_Score++;
        }

        public bool IsCellInMemory(MattLocation i_Location)
        {
            bool result = false;
            foreach(var cell in m_Memory)
            {
                if(i_Location == cell.Location)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public bool IsCellInMoves(MattLocation i_Location)
        {
            bool result = false;
            foreach (var moveCell in m_Moves)
            {
                if (i_Location == moveCell.Location)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public void AddNewMove(MemoryCell i_Move1, MemoryCell i_Move2)
        {
            m_Moves.Add(i_Move1);
            m_Moves.Add(i_Move2);
        }

        public void GetMove(out MattLocation io_Pick)
        {
            io_Pick = null;
            if (m_Moves.Count != 0)
            {
                io_Pick = m_Moves[0].Location;
                m_Moves.RemoveAt(0);
            }
        }

        public void Reset()
        {
            if(m_Memory != default && m_Memory.Count != 0)
            {
                m_Memory.Clear();
            }
            if(m_Moves != default && m_Moves.Count != 0)
            {
                m_Moves.Clear();
            }
        }

        //public void SetMemory(int i_Rows, int i_Columns)
        //{
        //    m_Memory = new List<BoardCell>(i_Rows * i_Columns);
        //    foreach(int i in m_Memory)
        //    {
        //        m_Memory[i] = null;
        //    }
        //    m_MemorySize = i_Rows * i_Columns;
        //}


        //public void AddToAIMemory(BoardCell i_Cell, int i_MaxRows, int i_Cols)
        //{
        //    int row = i_Cell.Location.Row;
        //    int column = i_Cell.Location.Col
        //    m_Memory[(row * i_MaxRows) + column] = i_Cell;
        //}

        //public int NextEmptyPlace()
        //{
        //    int i = 0;            
        //    while(m_Memory[i] != null)
        //    {
        //        i++;
        //    }
        //    return i;
        //}

        //public int GetPair(int i_Key)
        //{
        //    bool first = false;
        //    int result;

        //    for(int i = 0; i < m_MemorySize; i++)
        //    {
        //        if(first == false && m_Memory[i] == null)
        //        {
        //            result = i;
        //            first == true;
        //        }
        //        if(m_Memory[i].Key == i_Key)
        //        {
        //            return i;
        //        }
        //    }
        //}

        //public void MakeVisible(int i_Rows, int i_Cols, int i_MaxRows)
        //{
        //    m_Memory[(i_Rows * i_MaxRows) + i_Cols].Show();
        //}
    }
}
