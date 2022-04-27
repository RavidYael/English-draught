using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Game_Logic;
using User_Interface;

namespace GameConsole
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

        public GameExecutor()
        {
            m_Communicator = new UserCommunicator();
        }

        private void createGame()
        {
            Game.GamePrefernces newGamePref = m_Communicator.GetAndValidateGamePrefernces();
            m_Game = new Game(newGamePref);
        }

        private void play()
        {
            bool keepPlaying = true;
            UserMoveInput userMove;
            string errorMessage = "";
            while (keepPlaying)
            {
                while (m_Game.OnGoing)
                {
                    m_Communicator.PrintBoard(m_Game.GameBoard);
                    m_Communicator.InformWhosTurn(m_Game.WhosTurnName);

                    if (m_Game.isMachineTurn())
                    {
                        m_Game.MakeComputerMove();
                    }
                    else
                    {
                        userMove = m_Communicator.getAndValidateMoveInputFromUser();
                        if (userMove.EndGame)
                        {
                            m_Game.PlayerQuits();
                        }
                        else
                        {
                            while (!m_Game.IsValidMove(userMove.From, userMove.To, out errorMessage)) //MAYBE STRINGBUILDER(I FOUND JUST RESONS WHY NOT TO), BUT WHAT WITH THE OUT? STRING IS IMMUTABLE AND REFERNCED TYPE ALREADY
                            {
                                m_Communicator.InformError(errorMessage);
                                userMove = m_Communicator.getAndValidateMoveInputFromUser();
                            }

                            m_Game.MakeHumanMove(userMove.From, userMove.To);
                        }
                    }
                }

                m_Communicator.PrintBoard(m_Game.GameBoard);
                // print game RESULT - winner/tie
                m_Communicator.InformWinner(m_Game.Winner);
                m_Communicator.InformWinnerScore(m_Game.getWinnerScore());
                keepPlaying = m_Communicator.CheckIfUserWantToPlayAgain();
                if (keepPlaying)
                {
                    m_Game.Reset();
                }
            }
        }
    }
}
