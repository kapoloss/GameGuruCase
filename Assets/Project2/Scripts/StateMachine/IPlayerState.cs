namespace GameGuruCase.Project2.PlayerStateMachine
{
    /// <summary>
    /// Interface for player states, defining enter, update, and exit methods.
    /// </summary>
    public interface IPlayerState
    {
        void OnEnter();
        void Update();
        void OnExit();
    }
}