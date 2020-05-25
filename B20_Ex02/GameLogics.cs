using System;
using System.Collections.Generic;
using System.Text;

namespace B20_Ex02
{
    class GameLogics<T>
    {
        private Player m_Player1;
        private Player m_Player2;
        private GameBoard m_GameBoard;

        public GameLogics(string i_Player1Name, string i_Player2Name, int i_Rows, int i_Cols)
        {
            m_GameBoard = new GameBoard(i_Rows, i_Cols);
            m_Player1 = new Player(i_Player1Name);
            m_Player2 = new Player(i_Player2Name);
        }

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

        public void ChooseCell(int row, int col)
        {
            OpenCell(row, col);
        }

        public bool IsCellValid(int i_Row, int i_Col)
        {
            bool result = true;
            if(i_Row > m_GameBoard.Rows || i_Row < 0)
            {
                throw new Exception("Row value is incorrect.");
            }
            else if(i_Col > m_GameBoard.Cols || i_Col < 0)
            {
                throw new Exception("Columns value is incorrect.");
            }

            return result;
        }

        public bool IsNumeric(string input)
        {
            bool result = true;
            foreach(var character in input)
            {
                if(char.IsLetter(character) == false)
                {
                    result = false;
                    break;
                }
            }

            return result;
        }

        public void OpenCell(int row, int col)
        {
            m_GameBoard.Board[row, col].Show();
        }

        public void CloseCell(int row, int col)
        {
            m_GameBoard.Board[row, col].Hide();
        }
    }
}
