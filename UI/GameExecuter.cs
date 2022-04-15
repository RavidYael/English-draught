using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace UI
{
    internal class GameExecuter
    {


        public static void Execute()
        {
            string Player1Name;
            string Player2Name;
            int boardSize;
            int numberOfPlayers;
            Console.WriteLine("Hello! please enter your name:");
            getAndValidateNameFromUser(out Player1Name);
            Console.WriteLine("Please enter desired board size (6/8/10)");
            getAndValidateBoardSizeFromUser(out boardSize);
            Console.WriteLine("Please enter number of players: (1/2)");
            getAndValidateNumberOfPlayersFromUSer(out numberOfPlayers);
            if (numberOfPlayers == 2)
            {
                Console.WriteLine("Please enter second player name:");
                Player2Name = getAndValidateNameFromUser(out o_Player2Name);
            }
        }

        private static void getAndValidateNameFromUser(out string o_PlayerName)
        {
            o_PlayerName = Console.ReadLine();
            bool allChars = isOnlyLetters(o_PlayerName);
            bool lengthUnder20 = o_PlayerName.Length < 20;
            while (!allChars || !lengthUnder20)
            {
                Console.WriteLine("Name must be no longer than 20 characters long, and contain only english letters. Please try again");
                o_PlayerName = Console.ReadLine();
            }
        }

        private static bool isOnlyLetters(string i_PlayerName)
        {
            bool allLetters = true;
            foreach (char character in i_PlayerName)
            {
                if (!char.IsLetter(character))
                {
                    allLetters = false;
                }
            }

            return allLetters;
        }
    }
}
