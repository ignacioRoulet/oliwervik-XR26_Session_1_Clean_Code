using UnityEngine;

public interface IUIManager
{
    void UpdateScore(int score);
    void UpdateHealth(float health);
    void SetGameStatus(string status);
    void ToggleGameOverPanel(bool show);
    void UpdateTimer(float time);
}