using UnityEngine;

namespace  GameGuruCase.Project2.Config
{
    /// <summary>
    /// Holds configuration data for a single level: platform config, platform count, and game speed.
    /// </summary>
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "GridSystem/LevelConfig", order = 0)]
    public class LevelConfig : ScriptableObject
    {
        public PlatformConfig platformConfig;
        public int neededPlatformCountForLevelEnd;
        public float gameSpeed;

        /// <summary>
        /// Calculates the player's running speed based on gameSpeed.
        /// </summary>
        public float CalculatePlayerSpeed()
        {
            return gameSpeed;
        }

        /// <summary>
        /// Calculates how quickly platforms will move or flow horizontally.
        /// </summary>
        public float CalculatePlatformFlowSpeed()
        {
            float platformZ = platformConfig.firstPlatformScale.z;
            float time = platformZ / CalculatePlayerSpeed();
            return time * 1.75f;
        }
    }
}