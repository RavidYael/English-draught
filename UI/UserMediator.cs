using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game_Logic;

namespace UI
{
    internal class UserMediator
    {
        public string getAndValidateNameFromUser()
        {
            string playerName;
            playerName = Console.ReadLine();
            bool allChars = isOnlyLetters(playerName);
            bool lengthUnder20 = playerName.Length < 20;
            while (!allChars || !lengthUnder20)
            {
                Console.WriteLine("Name must be no longer than 20 characters long, and contain only english letters. Please try again");
                playerName = Console.ReadLine();
                allChars = isOnlyLetters(playerName);
                lengthUnder20 = playerName.Length < 20;
            }

            return playerName;
        }

        private bool isOnlyLetters(string i_PlayerName)
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

        public int getAndValidateBoardSizeFromUser()
        {
            int desiredBoardSize;
            bool parsedSuccefull = int.TryParse(Console.ReadLine(), out desiredBoardSize);
            bool validBoardSize = desiredBoardSize == 6 || desiredBoardSize == 8 || desiredBoardSize == 10;
            bool firstInput = true;

            while(!parsedSuccefull || !validBoardSize)
            {
                Console.WriteLine("Please enter valid board size: (6/8/10)");
                parsedSuccefull = int.TryParse(Console.ReadLine(), out desiredBoardSize);
                validBoardSize = desiredBoardSize == 6 || desiredBoardSize == 8 || desiredBoardSize == 10;
            }

            return desiredBoardSize;
        }

        public int getAndValidateNumberOfPlayersFromUSer()
        {
            int numberOfPlayers;
            bool parsedSuccefull = int.TryParse(Console.ReadLine(), out numberOfPlayers);
            bool validNumberOfPlayers = numberOfPlayers == 1 || numberOfPlayers == 2;

            while(!parsedSuccefull || !validNumberOfPlayers)
            {
                Console.WriteLine("Please enter valid number of players");
                parsedSuccefull = int.TryParse(Console.ReadLine(), out numberOfPlayers);
                validNumberOfPlayers = numberOfPlayers == 1 || numberOfPlayers == 2;
            }

            return numberOfPlayers;
        }

        public UserMoveInput getAndValidateMoveInputFromUser()
        {
            Console.WriteLine("Make your move (For example: Ab>Cd");
            UserMoveInput userMove = new UserMoveInput();
            string userInput = Console.ReadLine();
            //TODO validation
            string from = userInput.Substring(0, 2);
            string to = userInput.Substring(3, 2);
            userMove.From = new Point(charToIndex(from[1]), charToIndex(from[0]));
            userMove.To = new Point(charToIndex(to[1]), charToIndex(to[0]));
            return userMove;
        }

        private int charToIndex(char i_ch)
        {
            int resultingIndex;
            if(char.IsLower(i_ch))
            {
                resultingIndex = i_ch - 'a';
            }
            else
            {
                resultingIndex = i_ch - 'A';
            }

            return resultingIndex;
        }

    }
}
