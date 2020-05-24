using System;
using System.Collections.Generic;
using System.Text;

namespace B20_Ex02
{
    class BoardCell
    {
        private object m_Data;
        private readonly int m_Key;
        private readonly int m_Location;
        private bool m_Visible;

        public BoardCell(object i_Data, int i_Key, int i_Location)
        {
            m_Data = i_Data;
            m_Key = i_Key;
            m_Location = i_Location;
        }
        public int Key
        {
            get
            {
                return m_Key;
            }
        }
        public bool Visible
        {
            get
            {
                return m_Visible;
            }
        }
        public object Data
        {
            get
            {
                if(m_Visible == true)
                {
                    return m_Data;
                }
                else
                {
                    throw new Exception("Cell was not picked yet.");
                }
            }
        }
        public int Location
        {
            get
            {
                return m_Location;
            }
        }
        public void Show()
        {
            m_Visible = true;
        }
        public void Hide()
        {
            m_Visible = false;
        }
    }
}
