namespace JustChess.Renderers.Contracts
{
    using JustChess.Board.Contracts;

    public interface IRenderer
    {
        string RenderMainMenu();

        void RenderBoard(IBoard board);

        void PrintErrorMessage(string errorMessage);
    }
}
