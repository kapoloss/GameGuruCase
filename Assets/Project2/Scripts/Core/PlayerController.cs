using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{ 
    private Animator _animator;
    private Rigidbody _rb;
    private PlayerStateMachine _playerStateMachine;
    private Vector3 _firstPos;
    
    private void Awake()
    {
        
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
        
        _playerStateMachine = new PlayerStateMachine();
        _playerStateMachine.SetState(new WaitingToStartState(this));
        _firstPos = transform.position;
    }

    private void Update()
    {
       _playerStateMachine.Update();
    }

    private void OnEnable()
    {
        GameEventBus.LevelStarted += StartRun;
        GameEventBus.LevelCompleted += LevelCompleted;
        GameEventBus.LevelFailed += LevelFailed;
        GameEventBus.OnRestartClicked += RestartLevel;
        GameEventBus.OnNextLevelClicked += RestartLevel;
    }

    private void OnDisable()
    {
        GameEventBus.LevelStarted -= StartRun;
        GameEventBus.LevelCompleted -= LevelCompleted;
        GameEventBus.LevelFailed -= LevelFailed;
        GameEventBus.OnRestartClicked -= RestartLevel;
        GameEventBus.OnNextLevelClicked -= RestartLevel;
    }

    private void StartRun(LevelConfig level)
    {
        _animator.SetTrigger("Run");
        _playerStateMachine.SetState(new RunState(this,level.CalculatePlayerSpeed()));
    }
    
    private void SetRagdollActive(bool enable)
    {
        if (_animator != null)
        {
            _animator.enabled = !enable;
        }

        Rigidbody[] rigidbodies = transform.GetComponentsInChildren<Rigidbody>();
        foreach (var rb in rigidbodies)
        {
            rb.isKinematic = !enable;
            rb.useGravity = enable;
        }

        Collider[] colliders = transform.GetComponentsInChildren<Collider>();
        foreach (var col in colliders)
        {
            col.enabled = enable;
        }

    }

    private void LevelCompleted()
    {
        _animator.SetTrigger("Dance");
        _playerStateMachine.SetState(new WinState(this));
    }

    private void LevelFailed()
    {
        _playerStateMachine.SetState(new FailState(this));
        SetRagdollActive(true);
    }

    private void RestartLevel()
    {
        SetRagdollActive(false);
        _animator.SetTrigger("Idle");
        transform.position = _firstPos;
        _playerStateMachine.SetState(new WaitingToStartState(this));
    }
}
