using Microsoft.VisualStudio.TestTools.UnitTesting;
using JustChess;
using JustChess.Engine.Initializations;
using JustChess.Engine.Contracts;
using JustChess.Engine;
using JustChess.Renderers;
using JustChess.InputProviders;
using JustChess.Common;
using JustChess.Figures.Contracts;
using JustChess.Players;
using JustChess.Players.Contracts;

namespace ChessTests
{
    [TestClass]
    public class UnitTest1
    {

        public IGameInitializationStrategy Chess960 = new Chess960StandardStartGameInitializationStrategy();
        public IGameInitializationStrategy Classic = new StandardStartGameInitializationStrategy();

        [TestMethod]
        //ensures that every chess960 game is not the same.
        public void Make_Chess960Game()
        {     
            IGameInitializationStrategy gameInitializationStrategy2;

            gameInitializationStrategy2 = new Chess960StandardStartGameInitializationStrategy();

            //assert
            Assert.AreNotSame(Chess960, gameInitializationStrategy2,"They are the same.");
        }

        [TestMethod]
        //check if there are whit and black pieces for chess960
        public void CheckArmyGamePieces_Chess960()
        {

            var chessEngine = new StandardTwoPlayerEngine();

            chessEngine.TestInitialize(Chess960);

            //Check player1
            for(int i = 0; i < 8; i++)
            {
                IFigure figure = chessEngine.GetBoard().GetFigureAtPosition(new Position(8, (char)(i + 'a')));

                Assert.IsNotNull(figure);
            }

            //Checkplayer 2
            for (int j = 0; j < 8; j++)
            {
                IFigure figure = chessEngine.GetBoard().GetFigureAtPosition(new Position(1, (char)(j + 'a')));

                Assert.IsNotNull(figure);
            }
        }

        [TestMethod]
        //Check if there is pieces for classic game 
        public void CheckArmyGamePieces_Classic()
        {
            var chessEngine = new StandardTwoPlayerEngine();
            chessEngine.TestInitialize(Classic);

            //Check player1
            for (int i = 0; i < 8; i++)
            {
                IFigure figure = chessEngine.GetBoard().GetFigureAtPosition(new Position(8, (char)(i + 'a')));

                Assert.IsNotNull(figure);
            }

            //Checkplayer 2
            for (int j = 0; j < 8; j++)
            {
                IFigure figure = chessEngine.GetBoard().GetFigureAtPosition(new Position(1, (char)(j + 'a')));

                Assert.IsNotNull(figure);
            }
        }
        // check pawn peices
        [TestMethod]
        public void CheckPawnGamePiecesPlayer1_Chess960()
        {

            var chessEngine = new StandardTwoPlayerEngine();

            chessEngine.TestInitialize(Chess960);

            int player1PawnCount = 0;
            Player player1 =  (Player) chessEngine.GetPlayers(0);
            //Check player1
            for (int i = 0; i < 8; i++)
            {
                IFigure figure = chessEngine.GetBoard().GetFigureAtPosition(new Position(7, (char)(i + 'a')));
                Assert.IsNotNull(figure);

                if(player1.GetListFigures().Contains(figure))
                {
                    player1PawnCount++;
                }
            }

            Assert.AreEqual(8, player1PawnCount, "Amount of Pawns do not match For player1");

           
        }
        [TestMethod]
        public void CheckPawnGamePiecesPlayer1_Classic()
        {

            var chessEngine = new StandardTwoPlayerEngine();

            chessEngine.TestInitialize(Classic);

            int player1PawnCount = 0;
            Player player1 = (Player)chessEngine.GetPlayers(0);
            //Check player1
            for (int i = 0; i < 8; i++)
            {
                IFigure figure = chessEngine.GetBoard().GetFigureAtPosition(new Position(7, (char)(i + 'a')));
                Assert.IsNotNull(figure);

                if (player1.GetListFigures().Contains(figure))
                {
                    player1PawnCount++;
                }
            }

            Assert.AreEqual(8, player1PawnCount, "Amount of Pawns do not match For player1");


        }

        [TestMethod]
        public void CheckPawnGamePeicesPlayer2_Chess960()
        {
            var chessEngine = new StandardTwoPlayerEngine();

            chessEngine.TestInitialize(Chess960);

            int player2PawnCount = 0;
            Player player2 = (Player)chessEngine.GetPlayers(1);
            //Checkplayer 2
            for (int j = 0; j < 8; j++)
            {
                IFigure figure = chessEngine.GetBoard().GetFigureAtPosition(new Position(2, (char)(j + 'a')));
                Assert.IsNotNull(figure);

                if (player2.GetListFigures().Contains(figure))
                {
                    player2PawnCount++;
                }
            }

            Assert.AreEqual(8, player2PawnCount, "Amount of Pawns do not match For player2");
        }

        [TestMethod]
        public void CheckPawnGamePeicesPlayer2_Classic()
        {
            var chessEngine = new StandardTwoPlayerEngine();

            chessEngine.TestInitialize(Classic);

            int player2PawnCount = 0;
            Player player2 = (Player)chessEngine.GetPlayers(1);
            //Checkplayer 2
            for (int j = 0; j < 8; j++)
            {
                IFigure figure = chessEngine.GetBoard().GetFigureAtPosition(new Position(2, (char)(j + 'a')));
                Assert.IsNotNull(figure);

                if (player2.GetListFigures().Contains(figure))
                {
                    player2PawnCount++;
                }
            }

            Assert.AreEqual(8, player2PawnCount, "Amount of Pawns do not match For player2");
        }
        //check how many are on the board 

    }
}
