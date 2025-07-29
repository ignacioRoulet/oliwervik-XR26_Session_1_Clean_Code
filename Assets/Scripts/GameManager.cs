using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, IGameStateManager
{
    public static GameManager Instance { get; private set; }

    private bool isGameOver = false;
    private float gameTime = 0f;

    [SerializeField] private UIManager uiManager;
    [SerializeField] private Player player;

    public bool IsGameOver => isGameOver;
    public float GameTime => gameTime;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        if (uiManager == null)
            uiManager = FindFirstObjectByType<UIManager>();

        if (player == null)
            player = FindFirstObjectByType<Player>();

        if (player != null)
        {
            player.OnCollected.AddListener(OnCollectibleCollected);
            player.OnDamaged.AddListener(OnPlayerDamaged);
        }

        uiManager?.SetGameStatus("Game Started!");
        uiManager?.ToggleGameOverPanel(false);
        uiManager?.SetMaxHealth(player.MaxHealth);
        uiManager?.UpdateHealth(player.CurrentHealth);
    }

    private void Update()
    {
        if (isGameOver) return;

        gameTime += Time.deltaTime;
        uiManager?.UpdateTimer(gameTime);

        if (Input.GetKeyDown(KeyCode.R))
            RestartGame();

        if (player.Score >= 30)
            WinGame();
    }

    private void OnCollectibleCollected()
    {
        uiManager?.UpdateScore(player.Score);
    }

    private void OnPlayerDamaged()
    {
        uiManager?.UpdateHealth(player.CurrentHealth);

        if (player.CurrentHealth <= 0)
            GameOver();
    }

    public void GameOver()
    {
        if (isGameOver) return;

        isGameOver = true;
        uiManager?.SetGameStatus("GAME OVER!");
        uiManager?.ToggleGameOverPanel(true);
        Invoke(nameof(RestartGame), 2f);
    }

    public void WinGame()
    {
        if (isGameOver) return;

        isGameOver = true;
        uiManager?.SetGameStatus($"YOU WIN! Score: {player.Score}");
        Invoke(nameof(RestartGame), 2f);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
