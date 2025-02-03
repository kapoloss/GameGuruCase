using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{ 
    private Animator _animator;
    private Rigidbody _rb;
    private PlayerStateMachine _playerStateMachine;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
        
        _playerStateMachine = new PlayerStateMachine();
        _playerStateMachine.SetState(new WaitingToStartState(this));
    }

    private void Update()
    {
       _playerStateMachine.Update();
    }

    private void OnEnable()
    {
        GameEventBus.LevelStarted += StartRun;
    }

    private void OnDisable()
    {
        GameEventBus.LevelStarted -= StartRun;
    }

    private void StartRun(LevelConfig level)
    {
        //_animator.SetTrigger("Run");
        _playerStateMachine.SetState(new RunState(this,level.CalculatePlayerSpeed()));
    }
}
