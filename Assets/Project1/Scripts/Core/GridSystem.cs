using System;
using System.Collections.Generic;
using GameGuruCase.Project1.Config;
using UnityEngine;

namespace GameGuruCase.Project1.Core
{
    /// <summary>
    /// A generic grid system that spawns and manages grid objects based on GridSystemConfig settings.
    /// </summary>
    /// <typeparam name="TGridObject">The type of grid element, must derive from MonoBehaviour.</typeparam>
    public class GridSystem<TGridObject> : IDisposable where TGridObject : MonoBehaviour
    {
        public event EventHandler<OnGridObjectChangedEventArgs> OnGridObjectChanged;

        public class OnGridObjectChangedEventArgs : EventArgs
        {
            public int X;
            public int Y;
        }
        
        private readonly GridSystemConfig _config;
        private TGridObject[,] _gridArray;
        private List<TGridObject> _allGrids = new List<TGridObject>();
        
        public GridSystem(GridSystemConfig config, Func<TGridObject> createGrid)
        {
            _config = config;
            CreateGrids(createGrid);
        }

        private void CreateGrids(Func<TGridObject> createGrid)
        {
            _gridArray = new TGridObject[_config.gridCount.x, _config.gridCount.y];

            for (int x = 0; x < _gridArray.GetLength(0); x++)
            {
                for (int y = 0; y < _gridArray.GetLength(1); y++)
                {
                    _gridArray[x, y] = createGrid();
                    _gridArray[x, y].transform.position = GetWorldPosition(x, y);
                    _allGrids.Add(_gridArray[x, y]);
                }
            }
        }

        private void TriggerGridObjectChanged(int x, int y)
        {
            OnGridObjectChanged?.Invoke(this, new OnGridObjectChangedEventArgs { X = x, Y = y });
        }

        private void GetXY(Vector3 worldPosition, out int x, out int y)
        {
            Vector3 adjustedPosition = worldPosition + StartSpace();
            x = Mathf.RoundToInt(adjustedPosition.x / (_config.gridScale + _config.spaceBetweenGrids.x));
            y = Mathf.RoundToInt(adjustedPosition.y / (_config.gridScale + _config.spaceBetweenGrids.y));
        }

        private Vector3 StartSpace()
        {
            Vector3 defStartSpace = new Vector3(
                (_config.gridScale * _config.gridCount.x + _config.spaceBetweenGrids.x * (_config.gridCount.x - 1)) / 2 - _config.gridScale / 2,
                (_config.gridScale * _config.gridCount.y + _config.spaceBetweenGrids.y * (_config.gridCount.y - 1)) / 2 - _config.gridScale / 2,
                0);
            return defStartSpace + _config.originPosition;
        }

        private Vector2Int GetGridValue(Vector3 worldPosition)
        {
            return new Vector2Int(GetX(), GetY());
        }

        public int GetX()
        {
            return _config.gridCount.x;
        }

        public int GetY()
        {
            return _config.gridCount.y;
        }

        public float GetCellSize()
        {
            return _config.gridScale;
        }

        public Vector3 GetWorldPosition(int x, int y)
        {
            Vector3 pos = new Vector3(
                x * _config.gridScale + x * _config.spaceBetweenGrids.x,
                y * _config.gridScale + y * _config.spaceBetweenGrids.y,
                _config.originPosition.z);
            return pos - StartSpace();
        }

        public void SetGridObject(int x, int y, TGridObject value)
        {
            Vector3 a = new Vector3(x, y);
            if (a.x < 0 || a.y < 0 || a.x >= _config.gridCount.x || a.y >= _config.gridCount.y) return;
            _gridArray[x, y] = value;
            TriggerGridObjectChanged(x, y);
        }

        public void SetGridObject(Vector3 worldPosition, TGridObject value)
        {
            int x, y;
            GetXY(worldPosition, out x, out y);
            SetGridObject(x, y, value);
        }

        public bool GetGridObject(int x, int y, out TGridObject value)
        {
            value = null;
            if (x >= 0 && y >= 0 && x < _config.gridCount.x && y < _config.gridCount.y)
            {
                value = _gridArray[x, y];
                return true;
            }
            return false;
        }

        public bool GetGridObject(Vector3 worldPosition, out TGridObject value)
        {
            int x, y;
            GetXY(worldPosition, out x, out y);
            return GetGridObject(x, y, out value);
        }

        public bool FindNeighbour(int x, int y, Vector2Int neighbour, out TGridObject value)
        {
            value = null;
            Vector2Int a = new Vector2Int(x, y) + neighbour;
            if (a.x >= 0 && a.y >= 0 && a.x < _config.gridCount.x && a.y < _config.gridCount.y)
            {
                value = _gridArray[a.x, a.y];
                return true;
            }
            return false;
        }

        public bool FindNeighbour(TGridObject obj, Vector2Int neighbour, out TGridObject neighbourGridObject)
        {
            neighbourGridObject = null;
            int x, y;
            GetXY(obj.transform.position, out x, out y);
            return FindNeighbour(x, y, neighbour, out neighbourGridObject);
        }

        public bool FindNeighbour(Vector2 worldPosition, Vector2Int neighbour, out TGridObject value)
        {
            int x, y;
            GetXY(worldPosition, out x, out y);
            return FindNeighbour(x, y, neighbour, out value);
        }

        /// <summary>
        /// Cleans up all spawned objects and clears references.
        /// </summary>
        public void Dispose()
        {
            foreach (var grid in _allGrids)
            {
                if (grid != null && grid.gameObject != null)
                {
                    UnityEngine.Object.Destroy(grid.gameObject);
                }
            }
            _allGrids.Clear();
        }
    }
}