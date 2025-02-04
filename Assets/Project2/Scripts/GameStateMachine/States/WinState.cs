using GameGuruCase.Project2.Core;

namespace GameGuruCase.Project2.GameStateMachine.States
{
    /// <summary>
    /// Represents the win state in the overall game state machine.
    /// </summary>
    public class WinState : IGameState
    {
        private readonly GameController _gameController;
    
        public WinState(GameController gameController)
        {
            _gameController = gameController;
        }

        public void OnEnter() { }
        public void Update() { }
        public void OnExit() { }
    }
}