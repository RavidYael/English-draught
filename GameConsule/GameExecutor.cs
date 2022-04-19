using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Game_Logic;
using User_Interface;

namespace GameConsule
{
    public class GameExecutor
    {
        private Game m_Game;
        private UserCommunicator m_Communicator;

        public static void Main() 
        {
            GameExecutor executor = new GameExecutor();
            executor.execute();   
        }

        public void execute()
        {
            createGame();
            play();
        }

        private Game Checkers
        {
            get { return m_Game; }
        }

        public UserCommunicator Communicator
        {
            get { return m_Communicator; }
        }

        public GameExecutor()
        {
            m_Communicator = new UserCommunicator();
        }


        private void createGame()
        {
            Game.GamePrefernces newGamePref = m_Communicator.GetAndValidateGamePrefernces();
            m_Game = new Game(newGamePref);
        }

        private void play() // MAY BE AN ABUSE TO USE THE PROPERTIES FEATURE INSIDE THE CLASS (PROBABLY)
        {
            UserMoveInput userMove;
            string errorMessage = "";
            while (Checkers.OnGoing)
            {
                Checkers.PrintBoard();
                Communicator.InformWhosTurn(m_Game.WhosTurnName);
                if(Checkers.isMachineTurn())
                {
                    Checkers.MakeComputerMove();
                }
                else
                {
                    userMove = Communicator.getAndValidateMoveInputFromUser();
                        while (!Checkers.IsValidMove(userMove.From, userMove.To, out errorMessage))
                        {
                            Communicator.InformError(errorMessage);
                            userMove = Communicator.getAndValidateMoveInputFromUser();
                        }

                    Checkers.MakeHumanMove(userMove.From, userMove.To);
                }
            }
           
         Checkers.PrintBoard();
         Communicator.InformWinner(Checkers.Winner);
        
        }
    }
}
