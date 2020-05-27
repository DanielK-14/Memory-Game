using System;
using System.Collections.Generic;
using System.Text;

namespace B20_Ex02
{
    class GameLogics
    {
        public static ePlayer s_Opponent;
        private Player m_Player1;
        private Player m_Player2;
        private AIPlayer m_PlayerAI;
        private GameBoard m_GameBoard;
        private List<MattLocation> m_PossibleMovesForAI;
        private int m_TurnNumber;

        public enum ePlayer
        {
            Player1,
            Player2,
            PlayerAI
        }

        public GameLogics(string i_Player1Name, string i_Player2Name)
        {
            m_Player1 = new Player(i_Player1Name);
            if (i_Player2Name != string.Empty)
            {
                m_Player2 = new Player(i_Player2Name);
                m_PlayerAI = null;
                s_Opponent = ePlayer.Player2;
            }
            else
            {
                m_PlayerAI = new AIPlayer();
                m_Player2 = null;
                s_Opponent = ePlayer.PlayerAI;
            }
            m_TurnNumber = 1;
        }

        public void SetNewBoard(int i_Rows, int i_Cols)
        {
            m_GameBoard = new GameBoard(i_Rows, i_Cols);
        }

        public ePlayer GetPlayerTurn
        {
            get
            {
                ePlayer playerType;
                if(m_TurnNumber % 2 == 1)
                {
                    playerType = ePlayer.Player1;
                }
                else
                {
                    playerType = s_Opponent;
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
                return s_Opponent;
            }
        }

        public static bool IsBoardSizesValid(int i_Rows, int i_Cols, out string io_ErrorMsg)
        {
            bool result = true;
            io_ErrorMsg = string.Empty;
            if (i_Rows < 4 || i_Rows > 6 || i_Cols < 4 || i_Cols > 6)
            {
                io_ErrorMsg = "Values not in range";
                result = false;
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

        public bool IsCellValid(int i_Rows, int i_Cols, out string io_ErrorMsg)
        {
            bool result = true;
            io_ErrorMsg = string.Empty;
            if (i_Rows > m_GameBoard.Rows || i_Rows < 0)
            {
                result = false;
                io_ErrorMsg = "Row incorrect";
            }
            else if(i_Cols > m_GameBoard.Cols || i_Cols < 0)
            {
                result = false;
                io_ErrorMsg = "Column incorrect";
            }
            else if(m_GameBoard.Board[i_Rows, i_Cols].Visible == true)
            {
                result = false;
                io_ErrorMsg = "Card has been picked already";
            }

            return result;
        }

        public bool IsPlayerHumanTurn()
        {
            return (GetPlayerTurn != ePlayer.PlayerAI);
        }

        public static bool IsNumeric(string input, out string io_ErrorMsg)
        {
            io_ErrorMsg = string.Empty;
            bool result = true;
            foreach(var character in input)
            {
                if(char.IsDigit(character) == false)
                {
                    result = false;
                    io_ErrorMsg = "Not numeric input";
                    break;
                }
            }

            return result;
        }

        public bool IsValidColumn(string input, out string io_ErrorMsg)
        {
            io_ErrorMsg = string.Empty;
            bool result = true;
            input = input.ToUpper();

            CheckIfToExitGame(input);

            if (input.Length > 1 || input == string.Empty || int.Parse(io_ErrorMsg) - 65 > m_GameBoard.Cols || int.Parse(io_ErrorMsg) - 65 < 0)
            {
                result = false;
                io_ErrorMsg = "Column does not exsit";
            }
            
            return result;
        }

        public bool IsValidRow(string input, out string io_ErrorMsg)
        {
            io_ErrorMsg = string.Empty;
            bool result = true;

            CheckIfToExitGame(input);

            if (IsNumeric(input, out io_ErrorMsg) == false)
            {
                result = false;
                io_ErrorMsg = "Input not numeric";
            }
            else if(input == string.Empty || int.Parse(io_ErrorMsg) > m_GameBoard.Rows || int.Parse(io_ErrorMsg) - 1 < 0)
            {
                result = false;
                io_ErrorMsg = "Wrong input for row";
            }

            return result;
        }

        public int ConvertCharToIntLocation(string i_Character)
        {
            i_Character = i_Character.ToUpper();
            return (int.Parse(i_Character) - 65);
        }

        public bool CheckIfContinue(string input, out string io_ErrorMsg)
        {
            io_ErrorMsg = string.Empty;
            bool result;
            switch(input.ToLower())
            {
                case "yes":
                    result = true;
                    break;

                case "no":
                    result = false;
                    break;

                default:
                    io_ErrorMsg = "Wrong input";
                    result = false;
                    break;
            }

            return result;
        }

        public static bool IsChoiseValid(string choise, out string io_ErrorMsg)
        {
            bool result = true;
            io_ErrorMsg = string.Empty;
            if (choise != "0" && choise != "1")
            {
                io_ErrorMsg = "Invalid choise";
                result = false;
            }

            return result;
        }

        public static void SetOpponentType(int type)
        {
            s_Opponent = (ePlayer)type;
        }

        public void OpenCard(MattLocation i_Location)
        {
            m_GameBoard.Board[i_Location.Row, i_Location.Col].Show();
            Ex02.ConsoleUtils.Screen.Clear();
            m_GameBoard.Print();
        }

        public void closeCards(MattLocation i_Location1, MattLocation i_Location2)
        {
            m_GameBoard.Board[i_Location1.Row, i_Location1.Col].Hide();
            m_GameBoard.Board[i_Location2.Row, i_Location2.Col].Hide();
            m_PlayerAI.SaveToMemory(i_Location1, m_GameBoard.Board[i_Location1.Row, i_Location1.Col].Key, i_Location2, m_GameBoard.Board[i_Location2.Row, i_Location2.Col].Key);
        }

        public void PlayTurn(MattLocation i_Pick1, MattLocation i_Pick2)
        {
            if (IsPairFound(i_Pick1, i_Pick2) == false)
            {
                System.Threading.Thread.Sleep(2000);
                Ex02.ConsoleUtils.Screen.Clear();
                closeCards(i_Pick1, i_Pick2);
                m_TurnNumber++;
            }
            else
            {
                m_GameBoard.CouplesLeft--;
                AddScore(GetPlayerTurn);
            }
        }

        public void GetPicksForAIPlayer(out MattLocation io_Pick1, out MattLocation io_Pick2)
        {
            if (m_PlayerAI.IsNoMove() == true)
            {
                GenerateRandomPick(out io_Pick1);
                m_PlayerAI.TryFindSecondCard(m_GameBoard.Board[io_Pick1.Row, io_Pick1.Col].Key, out io_Pick2);
                if (io_Pick2 == null)
                {
                    GenerateRandomPick(out io_Pick2);
                }
            }
            else
            {
                m_PlayerAI.GetMove(out io_Pick1);
                m_PlayerAI.GetMove(out io_Pick2);
            }
        }

        private void GenerateRandomPick(out MattLocation io_Pick)
        {
            int randomIndexInPossibleMoves;
            List<MattLocation> possibleMoves = new List<MattLocation>();
            Random random = new Random();
            bool stop = true;

            foreach (var cell in m_GameBoard.Board)
            {
                if(cell.Visible == false)
                {
                    possibleMoves.Add(cell.Location);
                }
            }

            do
            {
                randomIndexInPossibleMoves = random.Next(possibleMoves.Count);
                if(m_PlayerAI.IsCellInMemory(possibleMoves[randomIndexInPossibleMoves]) == true)
                {
                    possibleMoves.RemoveAt(randomIndexInPossibleMoves);
                    stop = false;
                }
            } while (stop == false);

            io_Pick = possibleMoves[randomIndexInPossibleMoves];
            OpenCard(io_Pick);
        }

        public void AddScore(ePlayer i_PlayerType)
        {
            switch (i_PlayerType)
            {
                case ePlayer.Player1:
                    m_Player1.AddScore();
                    break;

                case ePlayer.Player2:
                        m_Player2.AddScore();
                    break;

                case ePlayer.PlayerAI:
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

            if(s_Opponent == ePlayer.Player2)
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
            string winnerFormat;
            string opponentName = String.Empty;
            int opponentScore = 0;

            switch(s_Opponent)
            {
                case ePlayer.Player2:
                    opponentName = m_Player2.Name;
                    opponentScore = m_Player2.Score;
                    break;

                case ePlayer.PlayerAI:
                    opponentName = m_PlayerAI.Name;
                    opponentScore = m_PlayerAI.Score;
                    break;
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

        public void ResetGame()
        {
            m_Player1.Reset();
            if (s_Opponent == ePlayer.Player2)
            {
                m_Player2.Reset();
            }
            else
            {
                m_PlayerAI.Reset();
            }
        }

        public void CheckIfToExitGame(string input)
        {
            input = input.ToLower();
            if(input == "q")
            {
                ExitGame();
            }
        }

        public void ExitGame()
        {
            Environment.Exit(1);
        }
    }
}
