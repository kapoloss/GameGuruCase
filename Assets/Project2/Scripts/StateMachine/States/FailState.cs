public class FailState : IPlayerState
{
    private readonly PlayerController _player;
    
    public FailState(PlayerController player)
    {
        _player = player;
    }
    
    public void OnEnter()
    { }
    public void Update() { }

    public void OnExit() { }
}