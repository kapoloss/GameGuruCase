using System;
using UnityEngine;

namespace MyNamespace
{
    public class GameController : MonoBehaviour
    {
        private GameStateMachine _gameStateMachine;
        private GameObject _currentPlatform;
        
        [SerializeField] private LevelHandler levelHandler;
        [SerializeField] private PlatformHandler platformHandler;
        [SerializeField] private InputHandler inputHandler;
        [SerializeField] private PlayerController playerController;

        private void Awake()
        {
            _gameStateMachine = new GameStateMachine();
            _gameStateMachine.SetState(new WaitingStartLevelState(this,inputHandler,levelHandler));
            platformHandler.InitializeLevel(levelHandler.GetLevelConfig());
        }

        private void OnEnable()
        {
            GameEventBus.PlatformPlacedUnsuccessfully += LevelFailed;
        }

        private void OnDisable()
        {
            GameEventBus.PlatformPlacedUnsuccessfully -= LevelFailed;
        }

        private void Update()
        {
            _gameStateMachine.Update();
        }

        public void StartLevel()
        {
            LevelConfig levelConfig = levelHandler.GetLevelConfig();
            _gameStateMachine.SetState(new PlayState(this,inputHandler,platformHandler));
        }

        private void LevelFailed(CutPlatformResult result)
        {
            _gameStateMachine.SetState(new FailState(this));
        }
        
    }
}

