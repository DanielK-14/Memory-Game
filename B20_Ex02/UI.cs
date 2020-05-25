using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Media;
using System.Text;

namespace B20_Ex02
{
    class UI
    {
        private GameLogics<T> m_Logic;
        private m_LocationInBoard loc;
        private object* m_Player2;
        private Player m_P1;

        
        public UI()
        {
            m_P1 = new Player(GetPlayerName());
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
                }
                if(Not a match)
                {
                    Threading.Thread.sleep(2000);
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

        public static string  GetPlayerName()
        {
            string name;
            Console.WriteLine("Please enter your name: ");
            name = Console.ReadLine();
            return name;
        }

        public void ChooseOpponent()
        {
            int choose;
            string name;
            Console.WriteLine("Choose your opponent: (1) for another player (2) for AI ");
            choose = Convert.ToInt32(Console.ReadLine());
            switch(choose)
            {
                case 1:
                    Console.WriteLine("Please enter second player name: ");
                    name = Console.ReadLine();
                    Player p2 = new Player(name);
                    m_Player2 = p2;
                    break;
                case 2:
                    AIPlayer AIP = new AIPlayer();
                    p2 = AIP;
                    break;
            }
        }

        public GetBoardSize()
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
                    result = GameLogic.BoardRowChecks(m_Rows, m_Columns);
                }
                catch(Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
            while (result == false);
            
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
                    m_Logic.IsCelValid
                        (row, column);
                }
                catch(Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
            while (result == false);
            loc.row = row;
            loc.column = column;
            return loc;
        }



    }
}
