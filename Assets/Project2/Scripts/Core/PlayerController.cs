using GameGuruCase.Project2.Config;
using GameGuruCase.Project2.PlayerStateMachine.States;
using UnityEngine;

namespace GameGuruCase.Project2.Core
{
    /// <summary>
    /// Manages the player character, including animations, ragdoll, and state transitions.
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        private Animator _animator;
        private Rigidbody _rb;
        private PlayerStateMachine.PlayerStateMachine _playerStateMachine;
        private Vector3 _firstPos;
        private float _speed;
        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _rb = GetComponent<Rigidbody>();
            _playerStateMachine = new PlayerStateMachine.PlayerStateMachine();
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
            GameEventBus.PlatformOnRoute += SetSpeed;
        }

        private void OnDisable()
        {
            GameEventBus.LevelStarted -= StartRun;
            GameEventBus.LevelCompleted -= LevelCompleted;
            GameEventBus.LevelFailed -= LevelFailed;
            GameEventBus.OnRestartClicked -= RestartLevel;
            GameEventBus.OnNextLevelClicked -= RestartLevel;
            GameEventBus.PlatformOnRoute -= SetSpeed;

        }

        /// <summary>
        /// Invoked when the level starts. Triggers run animation and run state.
        /// </summary>
        private void StartRun(LevelConfig level)
        {
            _animator.SetTrigger("Run");
            _playerStateMachine.SetState(new RunState(this));
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

        private void SetSpeed(PlatformRouteArgs routeArgs)
        {
            float currentZ = transform.position.z;
            float targetZ = routeArgs.Position.z;
            float difZ = targetZ - currentZ;
            float targetTime = routeArgs.TimeForRouteEnd;

            _speed = difZ / targetTime;
        }
        public float GetSpeed()
        {
            return _speed;
        }
    }
}