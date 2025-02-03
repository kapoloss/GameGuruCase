using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfig", menuName = "GridSystem/LevelConfig", order = 0)]

public class LevelConfig : ScriptableObject
{
    public PlatformConfig platformConfig;
    public int neededPlatformCountForLevelEnd;
    public float gameSpeed;

    public float CalculatePlayerSpeed()
    {
        return gameSpeed;
    }

    public float CalculatePlatformFlowSpeed()
    {
        float platformZ = platformConfig.firstPlatformScale.z;  

        float time = platformZ / CalculatePlayerSpeed();

        return time * 1.75f;
    }

}