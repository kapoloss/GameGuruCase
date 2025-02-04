using DG.Tweening;
using GameGuruCase.Project2.Core;
using MyNamespace;
using UnityEngine;

namespace GameGuruCase.Project2.PlayerStateMachine.States
{
    /// <summary>
    /// Player state for running forward, checking if the player is on a platform, and centering after each cut.
    /// </summary>
    public class RunState : IPlayerState
    {
        private readonly PlayerController _player;
        private readonly float _speed;
        private float _fallTimer;
    
        public RunState(PlayerController player, float speed)
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
            CheckUnderPlatform();
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

        private void CheckUnderPlatform()
        {
            Ray ray = new Ray(_player.transform.position + Vector3.up, Vector3.down);
            LayerMask mask = LayerMask.GetMask("Default");
            if (Physics.Raycast(ray, out var hit, 5, mask))
            {
                if (hit.collider.CompareTag("Platform"))
                {
                    return;
                }
                else if (hit.collider.CompareTag("Finish"))
                {
                    GameEventBus.RaiseLevelCompleted();
                }
                else
                {
                    _fallTimer += Time.deltaTime;
                    if (_fallTimer >= 0.3f)
                    {
                        GameEventBus.RaiseLevelFailed();
                        _fallTimer = 0;
                    }
                }
            }
            else
            {
                _fallTimer += Time.deltaTime;
                if (_fallTimer >= 0.3f)
                {
                    GameEventBus.RaiseLevelFailed();
                    _fallTimer = 0;
                }
            }
        }
    }
}