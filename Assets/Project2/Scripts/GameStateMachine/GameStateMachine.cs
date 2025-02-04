namespace GameGuruCase.Project2.GameStateMachine
{
    /// <summary>
    /// Core state machine for overall game states.
    /// </summary>
    public class GameStateMachine
    {
        private IGameState _currentState;
        
        public void SetState(IGameState newState)
        {
            _currentState?.OnExit();
            _currentState = newState;
            _currentState.OnEnter();
        }

        public void Update()
        {
            _currentState?.Update();
        }
    }
}