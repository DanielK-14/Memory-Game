using System;
using System.Collections.Generic;
using System.Text;

namespace B20_Ex02
{
    class GameLogics
    {
        private Player m_Player1;
        private Player m_Player2;
        private AIPlayer m_PlayerAI;
        private static ePlayer m_Opponent;
        private GameBoard m_GameBoard;
        private int m_TurnNumber;

        public enum ePlayer
        {
            Player1,
            Player2,
            PlayerAI
        }

        public GameLogics(string i_Player1Name, string i_Player2Name, int i_Rows, int i_Cols)
        {
            m_GameBoard = new GameBoard(i_Rows, i_Cols);
            m_Player1 = new Player(i_Player1Name);
            m_Player2 = new Player(i_Player2Name);
            m_Opponent = ePlayer.Player2;
            m_TurnNumber = 1;
        }

        public GameLogics(string i_Player1Name, int i_Rows, int i_Cols)
        {
            m_GameBoard = new GameBoard(i_Rows, i_Cols);
            m_Player1 = new Player(i_Player1Name);
            m_Opponent = ePlayer.PlayerAI;
            m_PlayerAI = new AIPlayer();
            m_TurnNumber = 1;
        }

        public ePlayer GetPlayerTurn
        {
            get
            {
                ePlayer playerType;
                if (m_TurnNumber % 2 == 1)
                {
                    playerType = ePlayer.Player1;
                }
                else
                {
                    playerType = m_Opponent;
                }

                return playerType;
            }
        }

        public Player FirstPlayer
        {
            get
            {
                return m_Player1;
            }
        }

        public Player SecondPlayer
        {
            get
            {
                return m_Player2;
            }
        }

        public AIPlayer ComputerPlayer
        {
            get
            {
                return m_PlayerAI;
            }
        }

        public ePlayer Opponent
        {
            get
            {
                return m_Opponent;
            }
        }

        public static bool IsRowsAndColsValid(int i_Rows, int i_Cols)
        {
            bool result = true;
            if (i_Rows * i_Cols < 16 || i_Rows * i_Cols > 36)
            {
                throw new Exception("Values are not in range. MIN 4X4 MAX 6X6");
            }
            else if ((i_Rows * i_Cols) % 2 == 1)
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

        public bool IsCellValid(MattLocation i_Location)
        {
            bool result = true;
            if (i_Location.Row > m_GameBoard.Rows || i_Location.Row < 0)
            {
                throw new Exception("Row value is incorrect.");
            }
            else if (i_Location.Col > m_GameBoard.Cols || i_Location.Col < 0)
            {
                throw new Exception("Column value is incorrect.");
            }

            return result;
        }

        public static bool IsPlayerHuman()
        {
            return (m_Opponent == ePlayer.Player2);
        }

        public static bool IsNumeric(string input)
        {
            bool result = true;
            foreach (var character in input)
            {
                if (char.IsLetter(character) == false)
                {
                    result = false;
                    break;
                }
            }

            return result;
        }

        public bool CheckIfContinue(string input)
        {
            bool result = true;
            switch (input.ToLower())
            {
                case "yes":
                    result = true;
                    break;

                case "no":
                    result = false;
                    break;

                    //default:

            }

            return result;
        }

        public static bool IsChoiseValid(string choise)
        {
            if (choise != "0" && choise != "1")
            {
                throw new Exception("Invalid choise.");
            }

            return true;
        }

        public static void SetOpponentType(int type)
        {
            m_Opponent = (ePlayer)type;
        }

        public void OpenCard(MattLocation i_Location)
        {
            m_GameBoard.Board[i_Location.Row, i_Location.Col].Show();
        }

        public void CloseCards(MattLocation i_Location1, MattLocation i_Location2)
        {
            m_GameBoard.Board[i_Location1.Row, i_Location1.Col].Hide();
            m_GameBoard.Board[i_Location2.Row, i_Location2.Col].Hide();
        }

        public void PlayTurn(MattLocation i_Pick1, MattLocation i_Pick2)
        {
            if (IsPairFound(i_Pick1, i_Pick2) == false)
            {
                System.Threading.Thread.Sleep(2000);
                CloseCards(i_Pick1, i_Pick2);
                m_TurnNumber++;
            }
            else
            {
                if (m_TurnNumber % 2 == 1)
                {
                    AddScore(ePlayer.Player1);
                    AddAIMemory(i_Pick1, i_Pick2);
                }
                else
                {
                    AddScore(m_Opponent);
                }
            }
       
        }

        public void AIPlayerMove(out MattLocation i_Pick1, out MattLocation i_Pick2)//////////////////////////////////////////////////////////////
        {
            int EmptyPlace = m_PlayerAI.NextEmptyPlace();
            int row = EmptyPlace / m_GameBoard.Rows;
            int col = EmptyPlace % m_GameBoard.Cols;
            int index;

            i_Pick1 = new MattLocation(row, col);

            index = m_PlayerAI.GetPair(m_GameBoard.Board[row, col]);
            row = EmptyPlace / m_GameBoard.Rows;
            col = EmptyPlace % m_GameBoard.Cols;
            i_Pick2 = new MattLocation(row, col);

        }
           
        public void AddAIMemory(MattLocation i_Pick)
        {

        }

        public void BuildAIMemory(int i_Rows, int i_columns)
        {
            m_PlayerAI.SetMemory(i_Rows, i_columns);
        }
            


        public void AddScore(ePlayer i_Player)
        {
            switch (i_Player)
            {
                case ePlayer.Player1:
                    m_Player1.AddScore();
                    break;

                case ePlayer.Player2:
                    if (m_Player2 == null)
                        throw new Exception("Player2 was not signed.");
                    m_Player2.AddScore();
                    break;

                case ePlayer.PlayerAI:
                    if (m_Player2 == null)
                        throw new Exception("PlayerAI was not signed.");
                    m_PlayerAI.AddScore();
                    break;
            }
        }

        public void PrintBoard()
        {
            m_GameBoard.Print();
        }

        public void PrintEndGameScreen()
        {
            Console.WriteLine(BuildEndGameScreenPrint());
        }

        public StringBuilder BuildEndGameScreenPrint()
        {
            StringBuilder stringBuilder = new StringBuilder();
            string opponentBody;
            string headline = @"Game ended!
The results are:";
            string body = string.Format("First player  {0}  scored is: {1}\n", m_Player1.Name, m_Player1.Score);

            if(m_Opponent == ePlayer.Player2)
            {
                opponentBody = string.Format("Second player  {0}  scored: {1}", m_Player2.Name, m_Player2.Score);
            }
            else
            {
                opponentBody = string.Format("Second player  {0}  scored: {1}", m_PlayerAI.Name, m_PlayerAI.Score);
            }

            body += opponentBody;

            stringBuilder.AppendLine(headline);
            stringBuilder.AppendLine(body);
            stringBuilder.AppendLine(buildWinnerFormat());

            return stringBuilder;
        }

        private string buildWinnerFormat()
        {
            string winnerFormat = String.Empty;
            string opponentName = String.Empty;
            int opponentScore;

            switch(m_Opponent)
            {
                case ePlayer.Player2:
                    opponentName = m_Player2.Name;
                    opponentScore = m_Player2.Score;
                    break;

                case ePlayer.PlayerAI:
                    opponentName = m_PlayerAI.Name;
                    opponentScore = m_PlayerAI.Score;
                    break;

                default:
                    throw new Exception("Wrong winner inputted");
            }

            if (m_Player1.Score > opponentScore)
            {
                winnerFormat = String.Format("{0} has won the game! Congratulations!", m_Player1.Name);
            }
            else if (m_Player1.Score < opponentScore)
            {
                winnerFormat = String.Format("{0} has won the game! Congratulations!", opponentName);
            }
            else
            {
                winnerFormat = "No one won! It's a TIE.";
            }

            return winnerFormat;
        }

        public static void ExitGame()
        {
            Environment.Exit(1);
        }
    }
}
