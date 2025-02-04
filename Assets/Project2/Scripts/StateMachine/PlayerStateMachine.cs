namespace GameGuruCase.Project2.PlayerStateMachine
{
    /// <summary>
    /// Manages the state transitions specific to the player character.
    /// </summary>
    public class PlayerStateMachine
    {
        private IPlayerState _currentState;
        
        public void SetState(IPlayerState newState)
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