using System;
using System.Text;

namespace B20_Ex02
{
    class UI
    {
        private GameLogics m_Logic;

        public void StartScreen()
        {
            string firstPlayerName = GetPlayerName();
            string secondPlayerName;
            int rows;
            int columns;
            GetBoardSize(out rows, out columns);

            ChooseAndSetOpponent();
            if(GameLogics.IsPlayerHuman() == true) ;
            {
                secondPlayerName = ChooseOpponentName();
            }
            if(GameLogics.IsPlayerHuman() == false)
            {
                GameLogics.BuildAIMemory(rows, columns);
            }

            PlayGame();
        }

        public void PlayGame()
        {
            bool toContinue = true;
            while (toContinue == true)
            {
                //int rows;
                //int columns;
                //GetBoardSize(out rows, out columns);// we build the game board before /the AI need to know the size of his memory

                MattLocation pick1;
                MattLocation pick2;

                while (m_Logic.IsGameOver() == false)
                {
                    if(GameLogics.IsPlayerHuman() == true)
                    {
                        pick1 = PickCard();
                        pick2 = PickCard();
                    }
                    else
                    {
                        GameLogics.AIPlayerMove(out pick1, out pick2);
                    }
                    m_Logic.PlayTurn(pick1, pick2);
                }

                m_Logic.PrintEndGameScreen();
                Console.WriteLine("Would you like to play again?");
                string input = Console.ReadLine();
                toContinue = m_Logic.CheckIfContinue(input);
            }

            GameLogics.ExitGame();
        }

        public string GetPlayerName()
        {
            Console.WriteLine("Please enter first player name: ");
            string name = Console.ReadLine();
            return name;
        }

        public void ChooseAndSetOpponent()
        {
            GameLogics.ePlayer opponent;
            string choiseString;
            Console.WriteLine("Choose your opponent: (1) for second player (2) for AI player");
            choiseString = Console.ReadLine();
            bool result = GameLogics.IsChoiseValid(choiseString);
            GameLogics.SetOpponentType(int.Parse(choiseString));
        }

        public string ChooseOpponentName()
        {
            string name;

            Console.WriteLine("Please enter second player name: ");
            name = Console.ReadLine();

            return name;
        }

        public void GetBoardSize(out int io_Rows, out int io_Columns)
        {
            bool result = true;
            string rows = string.Empty;
            string columns = string.Empty;

            do
            {
                try
                {
                    Console.WriteLine("Please enter board rows size and then columns size:");
                    rows = Console.ReadLine();
                    result = GameLogics.IsNumeric(rows);

                    columns = Console.ReadLine();
                    result = GameLogics.IsNumeric(columns);

                    result = GameLogics.IsRowsAndColsValid(int.Parse(rows), int.Parse(columns));
                }
                catch(Exception exception)
                {
                    result = false;
                    Console.WriteLine(exception.Message);
                }
            }
            while (result == false);

            io_Rows = int.Parse(rows);
            io_Columns = int.Parse(columns);
        }

        public MattLocation PickCard()
        {
            string rowString, columnString;
            MattLocation location = null;
            bool result = true;
            do
            {
                try
                {
                    m_Logic.PrintBoard();
                    Console.WriteLine("Please enter row and then column to open a card:");
                    rowString = Console.ReadLine();
                    GameLogics.IsNumeric(rowString);     //Checks if rowString is numeric. if not throws an excepetion.

                    columnString = Console.ReadLine();
                    GameLogics.IsNumeric(columnString);  //Checks if columnString is numeric. if not throws an excepetion.

                    location = new MattLocation(int.Parse(rowString), int.Parse(columnString));

                    m_Logic.IsCellValid(location);
                    m_Logic.OpenCard(location);
                    m_Logic.PrintBoard();
                }
                catch (Exception exception)
                {
                    Ex02.ConsoleUtils.Screen.Clear();
                    result = false;
                    Console.WriteLine(exception.Message);
                }
            }
            while (result = false);

            return location;
        }
    }
}
