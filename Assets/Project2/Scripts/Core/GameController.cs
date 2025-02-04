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
            SetLevel();
        }

        private void OnEnable()
        {
            GameEventBus.LevelFailed += LevelFailed;
            GameEventBus.LevelCompleted += LevelCompleted;
            GameEventBus.OnRestartClicked += RestartLevel;
            GameEventBus.OnNextLevelClicked += SetLevel;
        }

        private void OnDisable()
        {
            GameEventBus.LevelFailed -= LevelFailed;
            GameEventBus.LevelCompleted -= LevelCompleted;
            GameEventBus.OnRestartClicked -= RestartLevel;
            GameEventBus.OnNextLevelClicked -= SetLevel;
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

        private void LevelCompleted()
        {
            _gameStateMachine.SetState(new WinState(this));
        }
        private void LevelFailed()
        {
            _gameStateMachine.SetState(new FailState(this));
        }

        private void RestartLevel()
        {
            _gameStateMachine.SetState(new WaitingStartLevelState(this,inputHandler,levelHandler));
            platformHandler.InitializeLevel(levelHandler.GetLevelConfig());
            
        }

        private void SetLevel()
        {
            _gameStateMachine.SetState(new WaitingStartLevelState(this,inputHandler,levelHandler));
            platformHandler.InitializeLevel(levelHandler.GetLevelConfig());

        }
        
    }
}

