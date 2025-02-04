namespace GameGuruCase.Project2.GameStateMachine
{
    /// <summary>
    /// Interface for game states, defining enter, update, and exit methods.
    /// </summary>
    public interface IGameState
    {
        void OnEnter();
        void Update();
        void OnExit();
    }
}