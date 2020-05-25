using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace B20_Ex02
{
    class GameBoard
    {
        private int m_Rows;
        private int m_Cols;
        private BoardCell<char>[,] m_Board;
        private int m_CouplesLeft;

        public GameBoard(int i_Rows, int i_Cols)
        {
            m_Rows = i_Rows;
            m_Cols = i_Cols;
            m_Board = new BoardCell<char>[i_Rows, i_Cols];
            m_CouplesLeft = i_Rows * i_Cols / 2;
            buildBoard(createCardsDataArray(i_Cols * i_Rows / 2));
        }

        private List<char> createCardsDataArray(int i_NumOfCards)
        {
            List<char> cardsData = new List<char>(i_NumOfCards);
            List<char> charsForPick = new List<char>(26);
            Random random = new Random();
            char charToAdd = 'A';
            for (int i = 0; i < 26; i++)
            {
                charsForPick.Add(charToAdd);
                charToAdd++;
            }

            for (int i = 0; i < i_NumOfCards; i++)
            {
                int index = random.Next(charsForPick.Count);
                cardsData.Add(charsForPick[index]);
                charsForPick.RemoveAt(index);
            }

            return cardsData;
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

        public BoardCell<char>[,] Board
        {
            get
            {
                return m_Board;
            }
            set
            {
                m_Board = value;
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

        private void buildBoard(List<char> i_CardsData)
        {
            Random random = new Random();
            List<MattLocation> emptyPlaces = new List<MattLocation>();
            for (int row = 0; row < m_Rows; row++)  //Putting all empty places into emptyPlaces array.
            {
                for (int col = 0; col < m_Cols; col++)
                {
                    emptyPlaces.Add(new MattLocation(row, col));
                }
            }

            int key = 1;
            foreach(var card in i_CardsData)
            {
                for (int i = 1; i <= 2; i++)    //Doing shuffle location two times on each card.
                {
                    int randomEmptyIndex = random.Next(emptyPlaces.Count);  //Getting random index for empty place.
                    MattLocation location = emptyPlaces[randomEmptyIndex];  //Getting the empty random location.
                    emptyPlaces.RemoveAt(randomEmptyIndex);                 //Removing the empty place that was chosen already.
                    m_Board[location.Row, location.Col] = new BoardCell<char>(card, location, key);
                }

                key++;
            }
        }

        public void PrintBoard()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(FirstLineCols('A').ToString());
            stringBuilder.AppendLine(LineSeperate('=').ToString());
            for (int row = 0; row < Rows; row++)
            {
                stringBuilder.Append((row+1).ToString() + " |");
                for (int col = 0; col < Cols; col++)
                {
                    if(m_Board[row,col].Visible == true)
                    {
                        stringBuilder.Append(" " + m_Board[row, col].Data + " ");
                    }
                    else
                    {
                        stringBuilder.Append(' ', 3);
                    }

                    stringBuilder.Append("|");
                }

                stringBuilder.Append(Environment.NewLine);
                stringBuilder.AppendLine(LineSeperate('=').ToString());
            }

            Console.WriteLine(stringBuilder);
        }

        public StringBuilder LineSeperate(char i_SeprateCharacter)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("  ");
            for (int i = 0; i < Cols; i++)
            {
                stringBuilder.Append(i_SeprateCharacter);
                stringBuilder.Append(i_SeprateCharacter, 3);
                stringBuilder.Append(i_SeprateCharacter);
            }

            return stringBuilder;
        }

        public StringBuilder FirstLineCols(char i_ColoumsChar)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("   ");
            for (int i = 0; i < Cols; i++)
            {
                stringBuilder.Append(" " + i_ColoumsChar + " ");
                stringBuilder.Append(" ");
                i_ColoumsChar++;
            }

            return stringBuilder;
        }
    }
}
