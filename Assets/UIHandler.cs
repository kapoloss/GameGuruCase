using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    public Button restartButton;
    public Button nextLevelButton;
    
    public GameObject restartPanel;
    public GameObject nextLevelPanel;
    
    private void OnEnable()
    {
        GameEventBus.LevelCompleted += OpenNextLevelPanel;
        GameEventBus.LevelFailed += OpenRestartPanel;
        restartButton.onClick.AddListener(HandleRestartClicked);
        nextLevelButton.onClick.AddListener(HandleNextLevelClicked);
    }

    private void OnDisable()
    {
        GameEventBus.LevelCompleted -= OpenNextLevelPanel;
        GameEventBus.LevelFailed -= OpenRestartPanel;
        restartButton.onClick.RemoveListener(HandleRestartClicked);
        nextLevelButton.onClick.RemoveListener(HandleNextLevelClicked);
    }

    private void HandleRestartClicked()
    {
        GameEventBus.RaiseOnRestartClicked();
        ResetUI();
    }

    private void HandleNextLevelClicked()
    {
        GameEventBus.RaiseOnNextLevelClicked();
        ResetUI();
    }

    private void OpenRestartPanel()
    {
        restartPanel.SetActive(true);
    }

    private void OpenNextLevelPanel()
    {
        nextLevelPanel.SetActive(true);
    }

    private void ResetUI()
    {
        restartPanel.SetActive(false);
        nextLevelPanel.SetActive(false);
    }
}
