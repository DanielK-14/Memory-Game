using System;
using System.Globalization;

namespace B20_Ex02
{
    class UI
    {
        private GameLogics m_Logic;

        public void StartMemoryGame()
        {
            string firstPlayerName = getPlayerName();
            string secondPlayerName = string.Empty;
            int rows, columns;

            chooseAndSetOpponent();
            if (GameLogics.s_Opponent == GameLogics.ePlayer.Player2)
            {
                secondPlayerName = chooseOpponentName();
            }

            m_Logic = new GameLogics(firstPlayerName, secondPlayerName);
            playGame();
        }

        private void playGame()
        {
            string errorMsg;
            bool toContinue;
            int rows, columns;
            do
            {
                getBoardSize(out rows, out columns);
                m_Logic.SetNewBoard(rows, columns);
                MattLocation pick1;
                MattLocation pick2;

                while (m_Logic.IsGameOver() == false)
                {
                    if (m_Logic.IsPlayerHumanTurn())
                    {
                        Ex02.ConsoleUtils.Screen.Clear();
                        pick1 = pickCard();
                        pick2 = pickCard();
                    }
                    else
                    {
                        m_Logic.GetPicksForAIPlayer(out pick1, out pick2);
                    }
                    m_Logic.PlayTurn(pick1, pick2);
                }

                Ex02.ConsoleUtils.Screen.Clear();
                m_Logic.PrintEndGameScreen();
                do
                {
                    Console.WriteLine("Would you like to play again?");
                    string input = Console.ReadLine();
                    toContinue = m_Logic.CheckIfContinue(input, out errorMsg);
                    if (errorMsg != string.Empty)
                    {
                        Ex02.ConsoleUtils.Screen.Clear();
                        Console.WriteLine(errorMsg);
                    }
                }
                while (errorMsg != string.Empty);
                m_Logic.ResetGame();
            }
            while (toContinue == true);

            m_Logic.ExitGame();
        }

        private string getPlayerName()
        {
            Console.WriteLine("Please enter first player name: ");
            string name = Console.ReadLine();
            return name;
        }

        private void chooseAndSetOpponent()
        {
            string errorMsg;
            bool result;
            string choiseString;
            do
            {
                Console.WriteLine("Choose your opponent: (1) for second player (2) for AI player");
                choiseString = Console.ReadLine();
                result = GameLogics.IsChoiseValid(choiseString, out errorMsg);
                if (result == false)
                {
                    Console.WriteLine(errorMsg);
                }
            }
            while (result == false);

            GameLogics.SetOpponentType(int.Parse(choiseString));
        }

        private string chooseOpponentName()
        {
            string name;

            Console.WriteLine("Please enter second player name:");
            name = Console.ReadLine();

            return name;
        }

        private void getBoardSize(out int io_Rows, out int io_Columns)
        {
            string errorMsg;
            bool result;
            string rows;
            string columns = string.Empty;

            do
            {
                Console.WriteLine("Please enter board rows size and then columns size:");
                rows = Console.ReadLine();
                result = GameLogics.IsSizeBoardCorrect(rows, out errorMsg);

                if (result == true)
                {
                    columns = Console.ReadLine();
                    result = GameLogics.IsSizeBoardCorrect(columns, out errorMsg);
                    if (result == true)
                    {
                        result = GameLogics.IsBoardSizesValid(int.Parse(rows), int.Parse(columns), out errorMsg);
                    }
                }

                if (result == false)
                {
                    Ex02.ConsoleUtils.Screen.Clear();
                    Console.WriteLine(errorMsg);
                }
            }
            while (result == false);

            io_Rows = int.Parse(rows);
            io_Columns = int.Parse(columns);
        }

        public MattLocation pickCard()
        {
            string errorMsg;
            string cardLocation;
            MattLocation location = null;
            bool result = true;
            do
            {
                m_Logic.PrintBoard();
                Console.WriteLine(m_Logic.PlayerTurnInfo());
                Console.WriteLine("Please enter column character and the row number to open a card:");
                cardLocation = Console.ReadLine();
                if (m_Logic.IsValidColumn(cardLocation, out errorMsg) != false)
                {
                    if (m_Logic.IsValidRow(cardLocation, out errorMsg) != false)
                    {
                        cardLocation = cardLocation.ToUpper();
                        if (m_Logic.IsCellValid(Convert.ToInt32(cardLocation[1] - '0') - 1, Convert.ToInt32(cardLocation[0] - 'A'), out errorMsg) != false)
                        { 
                            location = new MattLocation(Convert.ToInt32(cardLocation[1] - '0') - 1, Convert.ToInt32(cardLocation[0] - 'A'));
                            m_Logic.OpenCard(location);
                        }
                    }
                }

                if (errorMsg != string.Empty)
                {
                    Ex02.ConsoleUtils.Screen.Clear();
                    result = false;
                    Console.WriteLine(errorMsg);
                }
                else
                {
                    result = true;
                }
            }
            while (result == false);

            return location;
        }
    }
}
