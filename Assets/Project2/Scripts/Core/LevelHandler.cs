using GameGuruCase.Project2.Config;
using UnityEngine;

namespace GameGuruCase.Project2.Core
{
    /// <summary>
    /// Handles the current level index and provides the active level configuration.
    /// </summary>
    public class LevelHandler : MonoBehaviour
    {
        [SerializeField] private LevelConfigHandler levelConfigHandler;
        private int _currentLevel;

        private void OnEnable()
        {
            GameEventBus.OnNextLevelClicked += LevelUp;
        }

        private void OnDisable()
        {
            GameEventBus.OnNextLevelClicked -= LevelUp;
        }

        private void LevelUp()
        {
            _currentLevel++;
        }

        public LevelConfig GetLevelConfig()
        {
            return levelConfigHandler.levelConfigs[_currentLevel % levelConfigHandler.levelConfigs.Count];
        }
    }
}