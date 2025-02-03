using UnityEngine;

public class WaitingToStartState : IPlayerState
{
    private readonly PlayerController _player;
    
    public WaitingToStartState(PlayerController player)
    {
        _player = player;
    }

    public void OnEnter()
    {
        //GameEventBus.LevelStarted += StartRun;
        
    }

    public void Update() { }

    public void OnExit()
    {
        //GameEventBus.LevelStarted -= StartRun;
    }

    
}