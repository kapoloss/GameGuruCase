using System.Collections.Generic;
using UnityEngine;

namespace GameGuruCase.Project1.Core
{
    /// <summary>
    /// A sample win strategy that checks if the current marked grid has at least three connected neighbors.
    /// </summary>
    public class ThreeNeighboursStrategy : IWinStrategy
    {
        private readonly Vector2Int[] _neighbours = new Vector2Int[]
        {
            new Vector2Int(-1, 0),
            new Vector2Int(1, 0),
            new Vector2Int(0, -1),
            new Vector2Int(0, 1),
        };

        public bool CanWin(GridObject lastMarkedGrid, GridSystem<GridObject> grid, out List<GridObject> winningGrids)
        {
            winningGrids = new List<GridObject>();
            HashSet<GridObject> checkedGrids = new HashSet<GridObject>();
            Queue<GridObject> queue = new Queue<GridObject>();

            if (lastMarkedGrid == null || !lastMarkedGrid.GetValue()) return false;

            queue.Enqueue(lastMarkedGrid);
            checkedGrids.Add(lastMarkedGrid);

            while (queue.Count > 0)
            {
                GridObject current = queue.Dequeue();
                winningGrids.Add(current);

                foreach (var direction in _neighbours)
                {
                    if (grid.FindNeighbour(current, direction, out var neighbour) &&
                        neighbour.GetValue() &&
                        !checkedGrids.Contains(neighbour))
                    {
                        queue.Enqueue(neighbour);
                        checkedGrids.Add(neighbour);
                    }
                }
            }

            return winningGrids.Count >= 3;
        }
    }
}