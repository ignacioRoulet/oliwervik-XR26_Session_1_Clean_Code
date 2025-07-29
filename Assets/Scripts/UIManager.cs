using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour, IUIManager
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Slider healthBar;
    [SerializeField] private TextMeshProUGUI statusText;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI timerText;

    public void SetMaxHealth(float max)
    {
        if (healthBar != null)
            healthBar.maxValue = max;
    }
    public void UpdateHealth(float health)
    {
        if (healthBar != null)
        {
            healthBar.value = health;
        }
    }
    public void UpdateScore(int score)
    {
        if (scoreText != null)
            scoreText.text = $"Score: {score}";
    }

    public void SetGameStatus(string status)
    {
        if (statusText != null)
            statusText.text = status;
    }

    public void ToggleGameOverPanel(bool show)
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(show);
    }

    public void UpdateTimer(float time)
    {
        if (timerText != null)
            timerText.text = $"Time: {Mathf.FloorToInt(time)}s";
    }
}
