public class WinState : IPlayerState
{
    private readonly PlayerController _player;
    
    public WinState(PlayerController player)
    {
        _player = player;
    }

    public void OnEnter()
    { }
    
    public void Update() { }
    
    public void OnExit() { }
}