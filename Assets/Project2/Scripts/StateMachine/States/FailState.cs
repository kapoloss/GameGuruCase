using GameGuruCase.Project2.Core;

namespace GameGuruCase.Project2.PlayerStateMachine.States
{
    /// <summary>
    /// Represents a fail state for the player, possibly leading to ragdoll or game over.
    /// </summary>
    public class FailState : IPlayerState
    {
        private readonly PlayerController _player;
    
        public FailState(PlayerController player)
        {
            _player = player;
        }
    
        public void OnEnter() { }
        public void Update() { }
        public void OnExit() { }
    }
}