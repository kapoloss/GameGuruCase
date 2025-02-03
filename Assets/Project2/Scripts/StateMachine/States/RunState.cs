using DG.Tweening;
using UnityEngine;

public class RunState : IPlayerState
{
    private readonly PlayerController _player;
    private readonly float _speed;
    
    public RunState(PlayerController player,float speed)
    {
        _player = player;
        _speed = speed;
    }

    public void OnEnter()
    {
        GameEventBus.PlatformPlacedSuccessfully += CenterPlatform;
    }

    public void Update()
    {
        _player.transform.Translate(Vector3.forward * (_speed * Time.deltaTime));
    }

    public void OnExit()
    {
        GameEventBus.PlatformPlacedSuccessfully -= CenterPlatform;
    }

    private void CenterPlatform(CutPlatformResult result)
    {
        _player.transform.DOKill();
        _player.transform.DOMoveX(result.UpdatedPlatform.transform.position.x, 0.3f);
    }
    
    
}