namespace JustChess.Engine.Initializations
{
    using System;
    using System.Collections.Generic;

    using Board.Contracts;
    using Common;
    using Contracts;
    using Figures;
    using Figures.Contracts;
    using Players.Contracts;

    public class Chess960StandardStartGameInitializationStrategy : IGameInitializationStrategy
    {
        private const int BoardTotalRowsAndCols = 8;

        private readonly IList<Type> figureTypes;

        public Chess960StandardStartGameInitializationStrategy()
        {
            this.figureTypes = new List<Type>
            {
                typeof(Rook),
                typeof(Knight),
                typeof(Bishop),
                typeof(Queen),
                typeof(King),
                typeof(Bishop),
                typeof(Knight),
                typeof(Rook)
            };
        }

        public void Initialize(IList<IPlayer> players, IBoard board)
        {
            this.ValidateStrategy(players, board);

            var firstPlayer = players[0];
            var secondPlayer = players[1];

            this.AddArmyToBoardRow(firstPlayer, board, 8);
            this.AddPawnsToBoardRow(firstPlayer, board, 7);

            this.AddPawnsToBoardRow(secondPlayer, board, 2);
            this.AddArmyToBoardRow(secondPlayer, board, 1);
        }

        private void AddPawnsToBoardRow(IPlayer player, IBoard board, int chessRow)
        {
            for (int i = 0; i < BoardTotalRowsAndCols; i++)
            {
                var pawn = new Pawn(player.Color);
                player.AddFigure(pawn);
                var position = new Position(chessRow, (char)(i + 'a'));
                board.AddFigure(pawn, position);
            }
        }

        private void AddArmyToBoardRow(IPlayer player, IBoard board, int chessRow)
        {
            //My Dice
            Random dice = new Random();
            int[] takenPositions = new int[8] { -1, -1, -1, -1, -1, -1, -1,-1 };

            //Place the two rooks first 
            takenPositions[0] = dice.Next(0, 8);
            takenPositions[1] = ReRoll(takenPositions);

            //check to see if there is enough space for the king. If not, re-roll
            while(takenPositions[0] - takenPositions[1] == -1 || takenPositions[0] - takenPositions[1] == 1)
            {
                takenPositions[1] = ReRoll(takenPositions);
            }

            //place the rooks. 
            PlacePeice(takenPositions[0], this.figureTypes[0], player, board, chessRow);
            PlacePeice(takenPositions[1], this.figureTypes[7], player, board, chessRow);

            //When we find that they are the same, use these number to pick a space between the rooks for the king.
            if (takenPositions[0] > takenPositions[1])
            {
                int temp = dice.Next(takenPositions[1], takenPositions[0]);
                while (findInt(temp, takenPositions) == true)
                {
                    temp = dice.Next(takenPositions[1], takenPositions[0]);
                }
                takenPositions[2] = temp;
            }
            else
            {
                int temp = dice.Next(takenPositions[0], takenPositions[1] - 1);
                while (findInt(temp, takenPositions) == true)
                {
                    temp = dice.Next(takenPositions[0], takenPositions[1] - 1);
                }
                takenPositions[2] = temp;
            }
            //then place the king with the new position.
            PlacePeice(takenPositions[2], this.figureTypes[4], player, board, chessRow);

            //then randomly place the other peices 
            //Make sure they are not in the same positions as the others. 
  
            //Knight
            takenPositions[3] = ReRoll(takenPositions);
            PlacePeice(takenPositions[3], this.figureTypes[1], player, board, chessRow);

            //Bishop 
            takenPositions[4] = ReRoll(takenPositions);
            PlacePeice(takenPositions[4], this.figureTypes[2], player, board, chessRow);

            //Queen 
            takenPositions[5] = ReRoll(takenPositions);
            PlacePeice(takenPositions[5], this.figureTypes[3], player, board, chessRow);

            //Bishop 2
            takenPositions[6] = ReRoll(takenPositions);
            PlacePeice(takenPositions[6], this.figureTypes[5], player, board, chessRow);

            //Kinght 2
            takenPositions[7] = ReRoll(takenPositions);
            PlacePeice(takenPositions[7], this.figureTypes[6], player, board, chessRow);


        }
        private void PlacePeice(int pos, Type gamepeice, IPlayer player, IBoard board, int chessRow)
        {
            var figureType = gamepeice;
            var figureInstance = (IFigure)Activator.CreateInstance(figureType, player.Color);
            player.AddFigure(figureInstance);
            var position = new Position(chessRow, (char)(pos + 'a'));
            board.AddFigure(figureInstance, position);
        }
        private int ReRoll(int[] takenPos)
        {
            Random dice = new Random();
            int newPos = dice.Next(0, 8);
            
            foreach(int num in takenPos)
            {
                if(num == newPos)
                {
                    //continue re-roll 
                    while (findInt(newPos, takenPos) == true)
                    {
                       newPos = dice.Next(0, 8);
                    }
                }

            }

            return newPos;

        }
        private bool findInt(int i, int[] array)
        {
            bool found = false;

            foreach (int num in array)
            {
                if (num == i)
                {
                    found = true;
                }

            }

            return found;
        }
        private void ValidateStrategy(ICollection<IPlayer> players, IBoard board)
        {
            if (players.Count != GlobalConstants.StandardGameNumberOfPlayers)
            {
                throw new InvalidOperationException("Standard Start Game Initialization Strategy needs exactly two players!");
            }

            if (board.TotalRows != BoardTotalRowsAndCols || board.TotalCols != BoardTotalRowsAndCols)
            {
                throw new InvalidOperationException("Standard Start Game Initialization Strategy needs 8x8 board!");
            }
        }
    }
}
