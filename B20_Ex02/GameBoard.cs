using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace B20_Ex02
{
    class GameBoard<T>
    {
        private int m_Rows;
        private int m_Cols;
        private BoardCell<T>[,] m_Board;
        private int m_CouplesLeft;

        public GameBoard(int i_Rows, int i_Cols, List<T> i_CardsData)
        {
            m_Rows = i_Rows;
            m_Cols = i_Cols;
            m_Board = new BoardCell<T>[i_Rows, i_Cols];
            m_CouplesLeft = i_Rows * i_Cols / 2;
            buildBoard(i_CardsData);
        }

        public int CouplesLeft
        {
            get
            {
                return m_CouplesLeft;
            }
            set
            {
                m_CouplesLeft = value;
            }
        }

        public int Rows
        {
            get
            {
                return m_Rows;
            }
        }

        public int Cols
        {
            get
            {
                return m_Cols;
            }
        }

        private void buildBoard(List<T> i_CardsData)
        {
            Random random = new Random();
            List<LocationInBoard> emptyPlaces = new List<LocationInBoard>();
            for (int row = 0, col = 0; row < m_Rows; col++)
            {
                emptyPlaces.Add(new LocationInBoard(row, col));
            }

            foreach( T card in i_CardsData)
            {
                for (int i = 1; i <= 2; i++)    //Doing shuffle location two times on each card.
                {
                    int randomEmptyIndex = random.Next(emptyPlaces.Count);
                    LocationInBoard location = emptyPlaces[randomEmptyIndex];
                    emptyPlaces.RemoveAt(randomEmptyIndex);
                    m_Board[location.Row, location.Col] = new BoardCell<T>(card, location);
                }
            }
        }
    }
}
