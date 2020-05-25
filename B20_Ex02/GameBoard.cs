using System;
using System.Collections.Generic;
using System.Text;

namespace B20_Ex02
{
    class GameBoard<T>
    {
        private BoardCell<T>[,] m_Board;
        private int m_CouplesLeft;

        public GameBoard(int i_Rows, int i_Cols, List<T> i_CardsData)
        {
            m_Board = new BoardCell<T>[i_Rows, i_Cols];
            m_CouplesLeft = i_Rows * 
        }

        private void BuildBoard(List<T> i_CardsData)
        {
           

        }
    }
}
