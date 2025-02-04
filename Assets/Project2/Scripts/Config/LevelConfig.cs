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
        //public float gameSpeed;
        public float gameStartSpeed;
        public float gameEndSpeed;

        /// <summary>
        /// Calculates the player's running speed based on gameSpeed.
        /// </summary>
        private float CalculateGameSpeed(int currentPlatformIndex)
        {
            float dif = gameEndSpeed - gameStartSpeed;
            
            return gameStartSpeed + (dif / neededPlatformCountForLevelEnd) * currentPlatformIndex;
        }

        /// <summary>
        /// Calculates how quickly platforms will move or flow horizontally.
        /// </summary>
        public float CalculatePlatformFlowSpeed(int currentPlatformIndex)
        {
            float platformZ = platformConfig.firstPlatformScale.z;
            float time = platformZ / CalculateGameSpeed(currentPlatformIndex);
            return time * 1.75f;
        
        }
    }
}