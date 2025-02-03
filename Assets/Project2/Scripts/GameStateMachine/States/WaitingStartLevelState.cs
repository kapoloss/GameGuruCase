using UnityEngine;

public class WaitingStartLevelState : IGameState
{
    private readonly MyNamespace.GameController _gameController;
    private readonly InputHandler _inputHandler;
    private readonly LevelHandler _levelHandler;
    
    public WaitingStartLevelState(MyNamespace.GameController gameController, InputHandler inputHandler, LevelHandler levelHandler)
    {
        _gameController = gameController;
        _inputHandler = inputHandler;
        _levelHandler = levelHandler;
    }

    public void OnEnter()
    {
        _inputHandler.OnMouseClick += StartLevel;
    }

    public void Update() { }
    
    public void OnExit()
    {
        _inputHandler.OnMouseClick -= StartLevel;

    }
    
    private void StartLevel(Vector2 mousePosition)
    {
        GameEventBus.RaiseLevelStarted(_levelHandler.GetLevelConfig());
        _gameController.StartLevel();
    }
}