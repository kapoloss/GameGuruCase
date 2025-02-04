using System.Collections.Generic;

namespace GameGuruCase.Project1.Core
{
    /// <summary>
    /// Interface for defining a winning condition strategy on a grid system.
    /// </summary>
    public interface IWinStrategy
    {
        bool CanWin(GridObject lastMarkedGrid, GridSystem<GridObject> grid, out List<GridObject> winningGrids);
    }
}