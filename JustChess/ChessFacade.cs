namespace JustChess
{
    using System;

    using Engine;
    using Engine.Initializations;
    using InputProviders;
    using JustChess.Engine.Contracts;
    using Renderers;

    public static class ChessFacade
    {
        public static void Start()
        {
            var renderer = new ConsoleRenderer();
            
            int gameMode = int.Parse(renderer.RenderMainMenu());

            var inputProvider = new ConsoleInputProvider();

            var chessEngine = new StandardTwoPlayerEngine(renderer, inputProvider);

            IGameInitializationStrategy gameInitializationStrategy;

            switch (gameMode)
            {
                case 1:
                   gameInitializationStrategy = new StandardStartGameInitializationStrategy();
                   chessEngine.Initialize(gameInitializationStrategy);
                   chessEngine.Start();
                   break;

                case 2:
                    gameInitializationStrategy = new Chess960StandardStartGameInitializationStrategy();
                    chessEngine.Initialize(gameInitializationStrategy);
                    chessEngine.Start();
                    break;
            }


            

            Console.ReadLine();
        }
    }
}
