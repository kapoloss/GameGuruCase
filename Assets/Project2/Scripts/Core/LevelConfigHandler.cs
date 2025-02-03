using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfigHandler", menuName = "GridSystem/LevelConfigHandler", order = 0)]

public class LevelConfigHandler : ScriptableObject
{
    public List<LevelConfig> levelConfigs;
}