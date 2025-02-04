using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        return levelConfigHandler.levelConfigs[_currentLevel%levelConfigHandler.levelConfigs.Count];
    }
}
