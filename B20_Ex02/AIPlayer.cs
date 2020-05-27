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

        public bool IsNoMove()
        {
            bool result = false;
            if(m_Moves.Count == 0)
            {
                result = true;
            }
            return result;
        }

        public void AddScore()
        {
            m_Score++;
        }

        public void AddToMemory(MattLocation i_Location, int i_CardKey)
        {
            MemoryCell cell = new MemoryCell(i_Location, i_CardKey);
            m_Memory.Add(cell);
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

        public void AddNewMove(MemoryCell i_Move1, MemoryCell i_Move2)
        {
            m_Moves.Add(i_Move1);
            m_Moves.Add(i_Move2);
        }

        public void SaveToMemory(MattLocation i_Location1, int i_CardKey1, MattLocation i_Location2, int i_CardKey2)
        {

            SaveMemoryOrAddMove(i_Location1, i_CardKey1);
            SaveMemoryOrAddMove(i_Location2, i_CardKey2);
        }

        public void SaveMemoryOrAddMove(MattLocation i_Location, int i_CardKey)
        {
            if (IsCellInMemory(i_Location) == false)
            {
                MemoryCell temp = new MemoryCell(i_Location, i_CardKey);
                if (FindMatchCell(temp) == false)
                {
                    m_Memory.Add(temp);
                }
            }
        }

        public bool FindMatchCell(MemoryCell i_Cell)
        {
            bool result = false;
            foreach(var cellMemory in m_Memory)
            {
                if (cellMemory.CardKey == i_Cell.CardKey)
                {
                    m_Moves.Add(i_Cell);
                    m_Moves.Add(cellMemory);
                    m_Memory.Remove(cellMemory);
                    result = true;
                    break;
                }
            }
            return result;
        }    
        
        public void TryFindSecondCard(int i_CardKey, out MattLocation io_Pick2)
        {
            io_Pick2 = null;

            foreach (var cellMemory in m_Memory)
            {
                if (cellMemory.CardKey == i_CardKey)
                {
                    io_Pick2 = cellMemory.Location;
                    break;
                }
            }
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
