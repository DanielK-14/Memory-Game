﻿using System;
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

        public GameBoard Gameboard
        {
            get
            {
                return m_GameBoard;
            }
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

        public void CheckSecondMove(out MattLocation pick2)
        {
            pick2 = null;
            if(m_PlayerAI.IsNoMove() == false)
            {
                m_PlayerAI.GetMove(out pick2);
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
                io_ErrorMsg = "- - - - Values not in range - - - -\n";
                result = false;
            }

            else if(i_Rows * i_Cols % 2 == 1)
            {
                io_ErrorMsg = "- - - - Odd amount of cells - - - -\n";
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
            if(m_GameBoard.Board[i_Rows, i_Cols].Visible == true)
            {
                result = false;
                io_ErrorMsg = "- - - - Card has been picked already - - - - \n";
            }

            return result;
        }

        public bool IsPlayerHumanTurn()
        {
            return (GetPlayerTurn != ePlayer.PlayerAI);
        }

        public static bool IsNumeric(char input, out string io_ErrorMsg)
        {
            io_ErrorMsg = string.Empty;
            bool result = true;
            if(char.IsDigit(input) == false)
            {
                result = false;
                io_ErrorMsg = "- - - - Row input not numeric - - - - \n";
            }

            return result;
        }

        public static bool IsSizeBoardCorrect(string input, out string io_ErrorMsg)
        {
            bool result = true;
            if(input.Length != 1)
            {
                result = false;
                io_ErrorMsg = "- - - - Wrong input - - - - \n";
            }

            if(IsNumeric(input[0], out io_ErrorMsg) == false)
            {
                result = false;
            }

            if(int.Parse(input) > 6 || int.Parse(input) < 4)
            {
                result = false;
                io_ErrorMsg = "- - - - Input is not in valid range - - - - \n";
            }

            return result;
        }

        public bool IsValidColumn(string input, out string io_ErrorMsg)
        {
            io_ErrorMsg = string.Empty;
            bool result = true;
            input = input.ToUpper();

            CheckIfToExitGame(input);

            if (input.Length != 2 || Convert.ToInt32(input[0]) - 65 > m_GameBoard.Cols || Convert.ToInt32(input[0]) - 65 < 0)   //input.Length == string.Empty
            {
                result = false;
                io_ErrorMsg = "- - - - Column does not exsit - - - - \n";
            }
            
            return result;
        }

        public bool IsValidRow(string input, out string io_ErrorMsg)
        {
            io_ErrorMsg = string.Empty;
            bool result = true;

            CheckIfToExitGame(input);

            if (IsNumeric(input[1], out io_ErrorMsg) == false)
            {
                result = false;
                io_ErrorMsg = "- - - - Input not numeric - - - - \n";
            }
            else if(Convert.ToInt32(input[1] - '0') > m_GameBoard.Rows || Convert.ToInt32(input[1] - '0') - 1 < 0)
            {
                result = false;
                io_ErrorMsg = "- - - - Row does not exsit - - - - \n";
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
                    io_ErrorMsg = "- - - - Wrong input - - - - \n";
                    result = false;
                    break;
            }

            return result;
        }

        public static bool IsChoiseValid(string choise, out string io_ErrorMsg)
        {
            bool result = true;
            io_ErrorMsg = string.Empty;
            if (choise != "2" && choise != "1")
            {
                io_ErrorMsg = "- - - - Invalid choise - - - - \n";
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
        }

        private void closeCards(MattLocation i_Location1, MattLocation i_Location2)
        {
            m_GameBoard.Board[i_Location1.Row, i_Location1.Col].Hide();
            m_GameBoard.Board[i_Location2.Row, i_Location2.Col].Hide();
            if (s_Opponent == ePlayer.PlayerAI)
            {
                saveToMemory(i_Location1, m_GameBoard.Board[i_Location1.Row, i_Location1.Col].Key, i_Location2, m_GameBoard.Board[i_Location2.Row, i_Location2.Col].Key);
            }
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
                if(s_Opponent == ePlayer.PlayerAI)
                {
                    System.Threading.Thread.Sleep(2000);
                    deleteFromMemoryAndMovesOfPlayerAI(i_Pick1, i_Pick2);   //Delete from AI Memory if exsits.
                }
                m_GameBoard.CouplesLeft--;
                addScore(GetPlayerTurn);
            }
        }

        //need
        private void deleteFromMemoryAndMovesOfPlayerAI(MattLocation i_Pick1, MattLocation i_Pick2)
        {
            if (m_PlayerAI.IsCellInMemory(i_Pick1) == true)
            {
                m_PlayerAI.Memory.Remove(new MemoryCell(i_Pick1, m_GameBoard.Board[i_Pick1.Row, i_Pick1.Col].Key));
            }

            if (m_PlayerAI.IsCellInMemory(i_Pick2) == true)
            {
                m_PlayerAI.Memory.Remove(new MemoryCell(i_Pick2, m_GameBoard.Board[i_Pick2.Row, i_Pick2.Col].Key));
            }

            if(m_PlayerAI.IsCellInMoves(i_Pick1) == true)
            {
                m_PlayerAI.Moves.Remove(new MemoryCell(i_Pick1, m_GameBoard.Board[i_Pick1.Row, i_Pick1.Col].Key));
            }

            if (m_PlayerAI.IsCellInMoves(i_Pick2) == true)
            {
                m_PlayerAI.Moves.Remove(new MemoryCell(i_Pick2, m_GameBoard.Board[i_Pick2.Row, i_Pick2.Col].Key));
            }
        }

        //need
        private void saveMemoryOrAddMove(MattLocation i_Location, int i_CardKey)
        {
            if (m_PlayerAI.IsCellInMemory(i_Location) == false)
            {
                MemoryCell temp = new MemoryCell(i_Location, i_CardKey);
                if (isFoundMatchCellAddBothToMoves(temp) == false)
                {
                    m_PlayerAI.Memory.Add(temp);
                }
            }
        }

        //need
        private void saveToMemory(MattLocation i_Location1, int i_CardKey1, MattLocation i_Location2, int i_CardKey2)
        {
            saveMemoryOrAddMove(i_Location1, i_CardKey1);
            saveMemoryOrAddMove(i_Location2, i_CardKey2);
        }

        //need
        private bool isFoundMatchCellAddBothToMoves(MemoryCell i_Cell)
        {
            bool result = false;
            foreach (var cellMemory in m_PlayerAI.Memory)
            {
                if (cellMemory.CardKey == i_Cell.CardKey)
                {
                    m_PlayerAI.Moves.Add(i_Cell);
                    m_PlayerAI.Moves.Add(cellMemory);
                    m_PlayerAI.Memory.Remove(cellMemory);
                    result = true;
                    break;
                }
            }
            return result;
        }

        //need
        public void GetPickForAIPlayer(out MattLocation io_Pick)
        {
            if (m_PlayerAI.IsNoMove() == false)
            {
                m_PlayerAI.GetMove(out io_Pick);
            }
            else
            {
                generateRandomPick(out io_Pick);
            }

            OpenCard(io_Pick);
        }

        //need
        public void SearchForSecondCard(int i_CardKey, MattLocation io_Pick, out MattLocation io_Pick2)
        {
            io_Pick2 = null;
            foreach (var cellMemory in m_PlayerAI.Memory)
            {
                if (cellMemory.CardKey == i_CardKey && cellMemory.Location != io_Pick)
                {
                    io_Pick2 = cellMemory.Location;
                    m_PlayerAI.Memory.Remove(cellMemory);
                    break;
                }
            }
        }

        //need
        private void generateRandomPick(out MattLocation io_Pick)
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
        }

        private void addScore(ePlayer i_PlayerType)
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
            if(input == "q" || input == "quit")
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
