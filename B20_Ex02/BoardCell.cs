using System;
using System.Collections.Generic;
using System.Text;

namespace B20_Ex02
{
    class BoardCell<T>
    {
        private T m_Data;
        private readonly LocationInBoard m_Location;
        private bool m_Visible;

        public BoardCell(T i_Data, LocationInBoard i_Location)
        {
            m_Data = i_Data;
            m_Location = i_Location;
        }
        public bool Visible
        {
            get
            {
                return m_Visible;
            }
        }
        public T Data
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
        public LocationInBoard Location
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
