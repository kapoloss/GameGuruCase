using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayState : IGameState
{
    private readonly MyNamespace.GameController _gameController;
    private readonly InputHandler _inputHandler;
    private readonly PlatformHandler _platformHandler;
    
    public PlayState(MyNamespace.GameController gameController, InputHandler inputHandler,PlatformHandler platformHandler)
    {
        _gameController = gameController;
        _inputHandler = inputHandler;
        _platformHandler = platformHandler;
    }

   
    
    public void OnEnter()
    {
        _inputHandler.OnMouseClick += HandleClick;
        _platformHandler.SendNewPlatform();
    }
    
    public void Update() { }
    
    public void OnExit()
    {
        _inputHandler.OnMouseClick -= HandleClick;
    }

    private void HandleClick(Vector2 mousePosition)
    {
        GameEventBus.RaisePlacePlatformAction();
    }
   
}
