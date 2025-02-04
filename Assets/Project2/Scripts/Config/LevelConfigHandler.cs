using System.Collections.Generic;
using UnityEngine;

namespace  GameGuruCase.Project2.Config
{
    /// <summary>
    /// Keeps a list of LevelConfig for multiple levels.
    /// </summary>
    [CreateAssetMenu(fileName = "LevelConfigHandler", menuName = "GridSystem/LevelConfigHandler", order = 0)]
    public class LevelConfigHandler : ScriptableObject
    {
        public List<LevelConfig> levelConfigs;
    }
}