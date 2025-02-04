using GameGuruCase.Project2.Core;

namespace GameGuruCase.Project2.GameStateMachine.States
{
    /// <summary>
    /// Represents the main gameplay state, handling platform placement via user input.
    /// </summary>
    public class PlayState : IGameState
    {
        private readonly GameController _gameController;
        private readonly InputHandler _inputHandler;
        private readonly PlatformHandler _platformHandler;
    
        public PlayState(GameController gameController, InputHandler inputHandler, PlatformHandler platformHandler)
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

        private void HandleClick(UnityEngine.Vector2 mousePosition)
        {
            GameEventBus.RaisePlacePlatformAction();
        }
    }
}