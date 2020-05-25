using System;
using System.Collections.Generic;
using System.Text;

namespace B20_Ex02
{
    class GameLogics<T>
    {
        private Player m_Player1;
        private Player m_Player2;
        private GameBoard<T> m_GameBoard;
        private List<T> m_CardsData;

        public GameLogics(string i_Player1Name, string i_Player2Name, int i_Rows, int i_Cols)
        {
            m_CardsData = i_CardsData;
            m_GameBoard = new GameBoard<T>(i_Rows, i_Cols, i_CardsData);
            m_Player1 = new Player(i_Player1Name);
            m_Player2 = new Player(i_Player2Name);
        }

        public List<T> CreateCardsData

        public static bool IsRowsAndColsValid(int i_Rows, int i_Cols)
        {
            bool result = true;
            if (i_Rows * i_Cols < 16 || i_Rows * i_Cols > 36)
            {
                throw new Exception("Values are not in range. MIN 4X4 MAX 6X6");
            }
            else if((i_Rows * i_Cols) % 2 == 1)
            {
                throw new Exception("Values multiplication is odd. Supposed to be even.");
            }

            return result;
        }

        public bool IsGameOver()
        {
            return m_GameBoard.CouplesLeft == 0;
        }

        public bool IsPairFound(MattLocation i_Location1, MattLocation i_Location2)
        {
            bool result = false;
            if (m_GameBoard.Board[i_Location1.Row, i_Location1.Col] == m_GameBoard.Board[i_Location2.Row, i_Location2.Col])
            {
                result = true;
            }

            return result;
        }

        public void ChooseCell(MattLocation i_Location)
        {
            

            OpenCell(i_Location);
        }

        public bool IsCellValid(string row, string col)
        {
            bool result = true;

            if(i_Location.Row > m_GameBoard.Rows || i_Location.Col > m_GameBoard.Cols || 
                i_Location.Col < 0 || i_Location.Row < 0)
            {
                result = false;
            }

            return result;
        }

        public bool IsNumeric(string )

        public void OpenCell(MattLocation i_Location)
        {
            m_GameBoard.Board[i_Location.Row, i_Location.Col].Show();
        }

        public void CloseCell(MattLocation i_Location)
        {
            m_GameBoard.Board[i_Location.Row, i_Location.Col].Hide();
        }
    }
}
