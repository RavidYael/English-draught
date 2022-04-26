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
            Console.WriteLine("Congratulations! " + i_WinnerName + " is THE WINNER" );
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

        public void InformWinnerScore(int i_Score)
        {
            Console.WriteLine("Score:" + i_Score);
        }

        public bool newRoundPrompt()
        {

            Console.WriteLine("Would you like to play again? (y/n)");
            char yesOrNo = getAndValidateYesOrNoFromUser();
            return yesOrNo == 'y';
        }

        private char getAndValidateYesOrNoFromUser()
        {
            char yesOrNo;
            bool parsing = char.TryParse(Console.ReadLine(), out yesOrNo);
            bool validChar = yesOrNo == 'y' || yesOrNo == 'n';
            while(!parsing || !validChar)
            {
                Console.WriteLine("Invalid input, please try again");
                parsing = char.TryParse(Console.ReadLine(), out yesOrNo);
                validChar = yesOrNo == 'y' || yesOrNo == 'n';
            }

            return yesOrNo;
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
            if (userInput == "Q")
            {
                userMove.EndGame = true;
            }
            else
            {
                while (!validateUserMoveInput(userInput))
                {
                    InformError("Wrong input, input should be in following format: Ab>Cd");
                    userInput = Console.ReadLine();
                    validateUserMoveInput(userInput);
                }

                string from = userInput.Substring(0, 2);
                string to = userInput.Substring(3, 2);
                userMove.From = new Point(charToIndex(from[1]), charToIndex(from[0]));
                userMove.To = new Point(charToIndex(to[1]), charToIndex(to[0]));
            }

            return userMove;
        }

        private bool validateUserMoveInput(string i_UserInput)
        {
            bool coordinateValid = false;
            bool lengthValid = i_UserInput.Length == 5;
            if (lengthValid)
            {
                string from = i_UserInput.Substring(0, 2);
                string to = i_UserInput.Substring(3, 2);
                coordinateValid = validateMoveCoordinateInput(from) && validateMoveCoordinateInput(to);
            }

            bool seperatorSighValid = i_UserInput.ElementAt(2) == '>';

            return lengthValid && coordinateValid && seperatorSighValid;
        }

        private bool validateMoveCoordinateInput(string i_ToValidate)
        {
            return char.IsUpper(i_ToValidate[0]) && char.IsLower(i_ToValidate[1]);
        }

        private int charToIndex(char i_Ch)
        {
            int resultingIndex;
            if (char.IsLower(i_Ch))
            {
                resultingIndex = i_Ch - 'a';
            }
            else
            {
                resultingIndex = i_Ch - 'A';
            }

            return resultingIndex;
        }
    }
}
