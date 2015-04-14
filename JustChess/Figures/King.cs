﻿namespace JustChess.Figures
{
    using System.Collections.Generic;

    using JustChess.Common;
    using JustChess.Figures.Contracts;
    using JustChess.Movements.Contracts;

    public class King : BaseFigure, IFigure
    {
        public King(ChessColor color)
            : base(color)
        {
        }

        public override ICollection<IMovement> Move(IMovementStrategy strategy)
        {
            return strategy.GetMovements(this.GetType().Name);
        }
    }
}
