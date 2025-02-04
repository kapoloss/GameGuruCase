using GameGuruCase.Project1.Config;
using UnityEngine;
using Zenject;

namespace GameGuruCase.Project1.Core
{
    /// <summary>
    /// Main controller that builds and manages the grid, handles clicks, and applies winning strategies.
    /// </summary>
    public class GameController : MonoBehaviour
    {
        private GridSystem<GridObject> _gridSystem;
        private IWinStrategy _winStrategy;

        private GridSystemConfig _gridSystemConfig;
        private InputHandler _inputHandler;
        private CameraHandler _cameraHandler;
        private Transform _gridParent;

        [SerializeField] private int n;

        /// <summary>
        /// Dependencies are injected by Zenject.
        /// </summary>
        [Inject]
        public void Construct(
            GridSystemConfig gridSystemConfig,
            InputHandler inputHandler,
            CameraHandler cameraHandler,
            IWinStrategy winStrategy,
            [Inject(Id = "GridParent")] Transform gridParent
        )
        {
            _gridSystemConfig = gridSystemConfig;
            _inputHandler = inputHandler;
            _cameraHandler = cameraHandler;
            _winStrategy = winStrategy;
            _gridParent = gridParent;
        }

        private void Awake()
        {
            BuildGrid();
        }

        private void OnEnable()
        {
            _inputHandler.OnMouseClick += HandleClick;
        }

        private void OnDisable()
        {
            _inputHandler.OnMouseClick -= HandleClick;
        }

        private void HandleClick(Vector2 mousePosition)
        {
            if (!_gridSystem.GetGridObject(mousePosition, out var gridObject)) return;
            gridObject.OnClick();

            if (!_winStrategy.CanWin(gridObject, _gridSystem, out var winningGrids)) return;
            foreach (var grid in winningGrids)
            {
                grid.WinGrid();
            }
        }

        /// <summary>
        /// Builds the grid and adjusts the camera to fit the newly created grid.
        /// </summary>
        [ContextMenu("Build Grid")]
        private void BuildGrid()
        {
            DestroyGrid();
            _gridSystemConfig.gridCount = new Vector2Int(n, n);

            _gridSystem = new GridSystem<GridObject>(_gridSystemConfig, () =>
            {
                var gridObject = Instantiate(_gridSystemConfig.prefab, _gridParent);
                return gridObject.GetComponent<GridObject>();
            });

            StartCoroutine(_cameraHandler.AdjustCamera(_gridSystemConfig));
        }

        /// <summary>
        /// Disposes the existing grid.
        /// </summary>
        private void DestroyGrid()
        {
            if (_gridSystem != null)
            {
                _gridSystem.Dispose();
                _gridSystem = null;
            }
        }
    }
}