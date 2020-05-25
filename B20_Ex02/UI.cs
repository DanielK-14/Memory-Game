using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Media;
using System.Text;

namespace B20_Ex02
{
    class UI
    {
        private GameLogics<T> m_Loic;

        public static string  GetPlayerName()
        {
            string name;
            cout << "Please enter your name: \n";
            cin >> name;
            return name;
        }

        public static object ChooseOpponent()
        {
            int player;
            string name;
            cout << "Choose your opponent: (1) for another player (2) for AI \n";
            cin >> player;
            switch(player)
            {
                case 1:
                    cout << "Please enter second player name: \n";
                    cin >> name;
                    return name;
                    break;
                case 2:
                    return AIPlayer;
                    break;
            }
          
        }

        public static GetBoardSize()
        {
            int high, width;
            cout << "Please enter board size high and then width: \n";
            do
            {
                cin >> high;
                cin >> width;
            }
            while (GameLogic.BoardSizeChecks);
        }

        public static void PrintBoard()
        {
            StringBuilder sb2 = new StringBuilder("  =", 27);
            StringBuilder sb = new StringBuilder("    A  ", 27);
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
                        sb.Append(GameLogics.CardDataShow);// need to checks with logics if its flip place or not
                        sb.Append(" |");                   // if it does bring the data else bring (" ")
                    }
                    Console.WriteLine(sb);
                }
            }



        }

        public static LocationInBoard Turn()
        {
            LocationInBoard loc;
            do
            {
                cout << "enter row and then column to open: \n";
                cin >> loc.row;
                cin >> loc.column;
            }
            while (!GameLogic.TurnChecks(loc.row,loc.column));
            return loc;
        }
    }
}
