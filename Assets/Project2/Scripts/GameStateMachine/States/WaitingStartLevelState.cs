using GameGuruCase.Project2.Core;

namespace GameGuruCase.Project2.GameStateMachine.States
{
    /// <summary>
    /// Waits for player input to start the level, then transitions to play state.
    /// </summary>
    public class WaitingStartLevelState : IGameState
    {
        private readonly GameController _gameController;
        private readonly InputHandler _inputHandler;
        private readonly LevelHandler _levelHandler;
    
        public WaitingStartLevelState(
            GameController gameController,
            InputHandler inputHandler,
            LevelHandler levelHandler)
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
    
        private void StartLevel(UnityEngine.Vector2 mousePosition)
        {
            GameEventBus.RaiseLevelStarted(_levelHandler.GetLevelConfig());
            _gameController.StartLevel();
        }
    }
}