using Game_Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User_Interface
{
    public class UserCommunicator
    {
        public void InformWhosTurn(String i_PlayerName)
        {
            Console.WriteLine(i_PlayerName + "'s turn");
        }


        public Exception PrintBoard()
        {
            return new NotImplementedException();
        }

       
        public Game.GamePrefernces GetAndValidateGamePrefernces()
        {
            Game.GamePrefernces newGamePref = new Game.GamePrefernces();
            newGamePref.OPlayerName = getAndValidateNameFromUser();
            newGamePref.XPlayerName = "Computer";
            newGamePref.BoardSize = getAndValidateBoardSizeFromUser();
            newGamePref.NumberOfHumanPlayers = getAndValidateNumberOfPlayersFromUser();
            if(newGamePref.NumberOfHumanPlayers == 2)
            {
                newGamePref.XPlayerName = getAndValidateNameFromUser();
            }
            return newGamePref;
        }
        public string getAndValidateNameFromUser()
        {
            Console.WriteLine("Hello! please enter your name:");
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

        public void InformError(string i_errorMessage)
        {
            Console.WriteLine(i_errorMessage + "\nPlease try again");
        }

        public void InformWinner(string i_WinnerName)
        {
            Console.WriteLine("Congratulations! "+ i_WinnerName + " is THE WINNER" );
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
            Console.WriteLine("Please enter desired board size (6/8/10)");
            int desiredBoardSize;
            bool parsedSuccefull = int.TryParse(Console.ReadLine(), out desiredBoardSize);
            bool validBoardSize = desiredBoardSize == 6 || desiredBoardSize == 8 || desiredBoardSize == 10;


            while(!parsedSuccefull || !validBoardSize)
            {
                Console.WriteLine("Please enter valid board size: (6/8/10)");
                parsedSuccefull = int.TryParse(Console.ReadLine(), out desiredBoardSize);
                validBoardSize = desiredBoardSize == 6 || desiredBoardSize == 8 || desiredBoardSize == 10;
            }

            return desiredBoardSize;
        }

        public int getAndValidateNumberOfPlayersFromUser()
        {
            Console.WriteLine("Please enter number of players: (1/2)");
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
            Console.WriteLine("Make your move (For example: Ab>Cd)");
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
