namespace GameGuruCase.Project2.GameStateMachine.States
{
    /// <summary>
    /// Represents a fail state in the overall game state machine.
    /// </summary>
    public class FailState : IGameState
    {
        private readonly Core.GameController _gameController;
    
        public FailState(Core.GameController gameController)
        {
            _gameController = gameController;
        }

        public void OnEnter() { }
        public void Update() { }
        public void OnExit() { }
    }
}