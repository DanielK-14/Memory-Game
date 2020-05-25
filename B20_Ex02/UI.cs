using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Media;
using System.Text;

namespace B20_Ex02
{
    class UI
    {
        private readonly GameLogics<T> m_Logic;
        private m_LocationInBoard1 loc;
        private m_LocationInBoard2 loc;
        private string m_Player1;
        private string m_Player2;

        
        public UI()
        {
            GetPlayerName();
            ChooseOpponent();
            GetBoardSize();
        }

        public void PlayGame()
        {
            int turnNumber = 1;

            while (m_Logic.IsGameOver() == false)
            {
                for(int i = 0; i < 2; i++)
                {
                    PrintBoard();
                    Turn();
                    if(i == 0)
                    {
                        loc1.row = loc2.row;
                        loc1.column = loc2.column;
                    }
                }
                if(IsPairFound(loc1, loc2) == false)
                {
                    Threading.Thread.sleep(2000);
                    // and then need function to close the last two flips
                }
                else
                {
                    if(turnNumber % 2 == 1)
                    {
                        m_P1.AddScore();
                    }
                    else
                    {
                        m_Player2->AddScore();
                    }
                }
                turnNumber++;
            }
        }

        public static void  GetPlayerName()
        {
            Console.WriteLine("Please enter your name: ");
            m_Player1 = Console.ReadLine();
        }

        public void ChooseOpponent()
        {
            int choose;
            string name;
            Console.WriteLine("Choose your opponent: (1) for another player (2) for AI ");
            choose = IsNumeric(Console.ReadLine());
            switch(choose)
            {
                case 1:
                    Console.WriteLine("Please enter second player name: ");
                    m_Player2 = Console.ReadLine();
                    break;
                case 2:
                    m_Player2 = "AIPlayer";
                    break;
            }
        }

        public void GetBoardSize()
        {
            bool result;
            string text;
            int rows, columns;
            Console.WriteLine("Please enter board rows and then columns: ");
            do
            {
                try
                {
                    rows = Convert.ToInt32(Console.ReadLine());
                    columns = Convert.ToInt32(Console.ReadLine());
                    result = GameLogic.IsRowsAndColsValid(m_Rows, m_Columns);
                }
                catch(Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
            while (result == false);
            m_Logic = new GameLogics<T>(m_Player1, m_Player2, rows, columns);
        }

        public void PrintBoard()
        {
            StringBuilder sb2 = new StringBuilder("  =", 27);
            StringBuilder sb = new StringBuilder("   ", 27);
            for(int i = 0; i < width; i++)// width of the game board
            {
                sb.Append(" ");
                sb.Append((string)(65 + i));
                sb.Append("  ");
                sb2.Append("====");
            }
            Console.WriteLine(sb);

            for (int i = 0; i <= high * 2; i++)// high of the game board
            {
                if(i % 2 == 0)
                {
                    Console.WriteLine(sb2);
                }
                else
                {
                    sb.Remove(0, 27);
                    sb.Append((string)((i+1)/2));
                    sb.Append(" |");
                    for(int j = 0; j < width; j++)// width of the game board
                    {
                        sb.Append(" ");
                        sb.Append(GameLogics.CardDataShow((i+1)/2, j+1));// need to checks with logics if its flip place or not
                        sb.Append(" |");                   // if it does bring the data else bring (" ")
                    }
                    Console.WriteLine(sb);
                }
            }



        }

        public LocationInBoard Turn()
        {
            string row, column;
            bool result;
            do
            {
                try
                {
                    Console.WriteLine("enter row and then column to open a location: ");
                    row = Console.ReadLine();// checks valid
                    column = Console.ReadLine();
                    m_Logic.IsCelValid(row, column);
                    m_Logic.ChooseCell(roww, column);
                }
                catch(Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
            while (result == false);
            loc2.row = row;
            loc2.column = column;
            return loc;
        }

        public int IsNumeric(string i_StringToCheck)
        {
            bool isNumeric;
            do
            {
                if (i_StringToCheck >= '0' && i_StringToCheck <= '1')
                {
                    isNumeric = true;
                }
                else
                {
                    isNumeric = false;
                    Console.WriteLine("Invalid input please try again: ");
                    i_StringToCheck = Console.ReadLine();
                }
            }
            while (isNumeric == false);
            return Convert.ToInt32(i_StringToCheck);
        }

    }
}
