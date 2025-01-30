
public interface IWinStrategy
{
    public bool CanWin(GridSystem<GridObject> grid,out GridObject[] winningGrids);
}
