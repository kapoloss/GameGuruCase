using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHandler : MonoBehaviour
{
    [SerializeField] private LevelConfigHandler levelConfigHandler;
    private int _currentLevel;

    public void LevelUp()
    {
        _currentLevel++;
    }

    public LevelConfig GetLevelConfig()
    {
        return levelConfigHandler.levelConfigs[_currentLevel];
    }
}
