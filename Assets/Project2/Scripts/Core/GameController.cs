using GameGuruCase.Project2.GameStateMachine.States;
using UnityEngine;
using Zenject;

namespace GameGuruCase.Project2.Core
{
    /// <summary>
    /// Manages the overall game flow, handles level start, fail, win states, and transitions.
    /// </summary>
    public class GameController : MonoBehaviour
    {
        private GameStateMachine.GameStateMachine _gameStateMachine;
        private GameObject _currentPlatform;
        
        private LevelHandler _levelHandler;
        private PlatformHandler _platformHandler;
        private InputHandler _inputHandler;

        /// <summary>
        /// Receives needed dependencies from Zenject.
        /// </summary>
        [Inject]
        public void Construct(
            GameStateMachine.GameStateMachine gameStateMachine,
            LevelHandler levelHandler,
            PlatformHandler platformHandler,
            InputHandler inputHandler)
        {
            _gameStateMachine = gameStateMachine;
            _levelHandler = levelHandler;
            _platformHandler = platformHandler;
            _inputHandler = inputHandler;
        }

        /// <summary>
        /// Initializes the first level state.
        /// </summary>
        private void Awake()
        {
            SetLevel();
        }

        /// <summary>
        /// Subscribes to game events.
        /// </summary>
        private void OnEnable()
        {
            GameEventBus.LevelFailed += LevelFailed;
            GameEventBus.LevelCompleted += LevelCompleted;
            GameEventBus.OnRestartClicked += RestartLevel;
            GameEventBus.OnNextLevelClicked += SetLevel;
        }

        /// <summary>
        /// Unsubscribes from game events.
        /// </summary>
        private void OnDisable()
        {
            GameEventBus.LevelFailed -= LevelFailed;
            GameEventBus.LevelCompleted -= LevelCompleted;
            GameEventBus.OnRestartClicked -= RestartLevel;
            GameEventBus.OnNextLevelClicked -= SetLevel;
        }

        /// <summary>
        /// Updates the current game state machine.
        /// </summary>
        private void Update()
        {
            _gameStateMachine.Update();
        }

        /// <summary>
        /// Prepares the game to start playing.
        /// </summary>
        public void StartLevel()
        {
            _gameStateMachine.SetState(new PlayState(this, _inputHandler, _platformHandler));
        }

        /// <summary>
        /// Called when the level is completed successfully.
        /// </summary>
        private void LevelCompleted()
        {
            _gameStateMachine.SetState(new WinState(this));
        }
        
        /// <summary>
        /// Called when the level fails.
        /// </summary>
        private void LevelFailed()
        {
            _gameStateMachine.SetState(new FailState(this));
        }

        /// <summary>
        /// Restarts the level, resetting states and re-initializing the platform.
        /// </summary>
        private void RestartLevel()
        {
            _gameStateMachine.SetState(new WaitingStartLevelState(this, _inputHandler, _levelHandler));
            _platformHandler.InitializeLevel(_levelHandler.GetLevelConfig());
        }

        /// <summary>
        /// Sets the initial state for the level.
        /// </summary>
        private void SetLevel()
        {
            _gameStateMachine.SetState(new WaitingStartLevelState(this, _inputHandler, _levelHandler));
            _platformHandler.InitializeLevel(_levelHandler.GetLevelConfig());
        }
    }
}