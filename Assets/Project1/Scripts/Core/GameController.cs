using System;
using MyNamespace;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameGuruCase.Project1.Core
{
    public class GameController : MonoBehaviour
    {
        private GridSystem<GridObject> _gridSystem;
        private IWinStrategy _winStrategy;
        
        [SerializeField] private GridSystemConfig gridSystemConfig;
        [SerializeField] private InputHandler inputHandler;
        [SerializeField] private MyNamespace.CameraHandler cameraHandler;
        [SerializeField] private Transform gridParent;
        [SerializeField] private int n;
    
        private void Awake()
        {
            _winStrategy = new ThreeNeighboursStrategy();
            BuildGrid();
        }
        
        private void OnEnable()
        {
            inputHandler.OnMouseClick += HandleClick;
        }
    
        private void OnDisable()
        {
            inputHandler.OnMouseClick -= HandleClick;
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
        
        [Button("Build Grid")]
        private void BuildGrid()
        {
            DestroyGrid();
            gridSystemConfig.gridCount = new Vector2Int(n, n);
            
            _gridSystem = new GridSystem<GridObject>(gridSystemConfig, () =>
            {
                var gridObject = Instantiate(gridSystemConfig.prefab,gridParent);
                return gridObject.GetComponent<GridObject>();
            });
    
            StartCoroutine(cameraHandler.AdjustCamera(gridSystemConfig));
        }
    
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
