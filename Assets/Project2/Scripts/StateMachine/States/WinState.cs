using GameGuruCase.Project2.Core;

namespace GameGuruCase.Project2.PlayerStateMachine.States
{
    /// <summary>
    /// Represents a winning state for the player, typically triggered upon level completion.
    /// </summary>
    public class WinState : IPlayerState
    {
        private readonly PlayerController _player;
    
        public WinState(PlayerController player)
        {
            _player = player;
        }

        public void OnEnter() { }
        public void Update() { }
        public void OnExit() { }
    }
}