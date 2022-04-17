using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Game_Logic;

namespace UI
{
    internal class GameExecutor
    {
        private Game m_Game;
        private UserMediator m_UserMediator;

        public GameExecutor()
        {
            m_UserMediator = new UserMediator();
        }

        public void Execute()
        {
            createGame();
            play();
        }

        private void createGame()
        {
            string player1Name;
            string player2Name = "computer";
            int boardSize;
            int numberOfPlayers;
            Console.WriteLine("Hello! please enter your name:");
            player1Name = m_UserMediator.getAndValidateNameFromUser();
            Console.WriteLine("Please enter desired board size (6/8/10)");
            boardSize = m_UserMediator.getAndValidateBoardSizeFromUser();
            Console.WriteLine("Please enter number of players: (1/2)");
            numberOfPlayers = m_UserMediator.getAndValidateNumberOfPlayersFromUSer();

            if (numberOfPlayers == 2)
            {
                Console.WriteLine("Please enter second player name:");
                player2Name = m_UserMediator.getAndValidateNameFromUser();
            }

            m_Game = new Game(boardSize, numberOfPlayers, player1Name, player2Name);
        }

        private void play()
        {
            int fromRow, fromColumn;
            int toRow, toColumn;
            UserMoveInput userMove;
            string errorMessage = "";
            while (m_Game.OnGoing)
            {
                m_Game.PrintBoard();
                Console.WriteLine("{0}'s turn", m_Game.WhosTurnName);
                if(m_Game.isMachineTurn())
                {
                    m_Game.PrintBoard();
                    System.Threading.Thread.Sleep(3000);
                    m_Game.MakeComputerMove();
                }
                else
                {
                    userMove = m_UserMediator.getAndValidateMoveInputFromUser();
                    while (!m_Game.IsValidMove(userMove.From, userMove.To, out errorMessage))
                    {
                        Console.WriteLine(errorMessage + "\nplease try again");
                        userMove = m_UserMediator.getAndValidateMoveInputFromUser();
                    }

                    m_Game.MakeHumanMove(userMove.From, userMove.To);
                }

                m_Game.PrintBoard();
            }
        }
    }
}
