using System;

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
                        pick1 = pickCard();
                        pick2 = pickCard();
                    }
                    else
                    {
                        m_Logic.GetPicksForAIPlayer(out pick1, out pick2);
                    }
                    m_Logic.PlayTurn(pick1, pick2);
                }

                m_Logic.PrintEndGameScreen();
                do
                {
                    Ex02.ConsoleUtils.Screen.Clear();
                    Console.WriteLine("Would you like to play again?");
                    string input = Console.ReadLine();
                    toContinue = m_Logic.CheckIfContinue(input, out errorMsg);
                    if (errorMsg != string.Empty)
                    {
                        Console.WriteLine(errorMsg);
                    }
                }
                while (errorMsg != string.Empty);
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
                result = GameLogics.IsNumeric(rows, out errorMsg);

                if (result == true)
                {
                    columns = Console.ReadLine();
                    result = GameLogics.IsNumeric(columns, out errorMsg);
                    if (result == true)
                    {
                        result = GameLogics.IsBoardSizesValid(int.Parse(rows), int.Parse(columns), out errorMsg);
                    }
                }

                if (result == false)
                {
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
            string rowString, columnString;
            MattLocation location = null;
            bool result = true;
            do
            {
                m_Logic.PrintBoard();
                Console.WriteLine("Please enter column character and the row number to open a card:");
                columnString = Console.ReadLine();
                if (m_Logic.IsValidColumn(columnString, out errorMsg) != false)
                {
                    rowString = Console.ReadLine();
                    if (m_Logic.IsValidRow(rowString, out errorMsg) != false)
                    {
                        if (m_Logic.IsCellValid(int.Parse(rowString), int.Parse(columnString), out errorMsg) != false)
                        {
                            location = new MattLocation(int.Parse(rowString) - 1, int.Parse(columnString));
                            m_Logic.OpenCard(location);
                        }
                    }
                }

                if (errorMsg != string.Empty)
                {
                    result = false;
                    Console.WriteLine(errorMsg);
                }
            }
            while (result == false);

            return location;
        }
    }
}
