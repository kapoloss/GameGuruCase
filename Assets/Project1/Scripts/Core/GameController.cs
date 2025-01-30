using System;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private GridSystem<GridObject> _gridSystem;
    [SerializeField] private GridSystemConfig gridSystemConfig;
    [SerializeField] private InputHandler inputHandler;
    
    void Start()
    {
        _gridSystem = new GridSystem<GridObject>(gridSystemConfig, () =>
        {
            var gridObject = Instantiate(gridSystemConfig.prefab);
            return gridObject.GetComponent<GridObject>();
        });
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
        if (_gridSystem.GetGridObject(mousePosition, out var gridObject))
        {
            gridObject.OnClick();
        }
    }
}
