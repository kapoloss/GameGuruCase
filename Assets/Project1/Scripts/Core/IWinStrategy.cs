
using System.Collections.Generic;

public interface IWinStrategy
{
    public bool CanWin(GridObject lastMarkedGrid,GridSystem<GridObject> grid,out List<GridObject> winningGrids);
}
