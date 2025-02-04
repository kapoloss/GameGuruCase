using GameGuruCase.Project2.Core;

namespace GameGuruCase.Project2.PlayerStateMachine.States
{
    /// <summary>
    /// Player state waiting for the game to start, idle until the level is kicked off.
    /// </summary>
    public class WaitingToStartState : IPlayerState
    {
        private readonly PlayerController _player;
    
        public WaitingToStartState(PlayerController player)
        {
            _player = player;
        }

        public void OnEnter() { }
        public void Update() { }
        public void OnExit() { }
    }
}