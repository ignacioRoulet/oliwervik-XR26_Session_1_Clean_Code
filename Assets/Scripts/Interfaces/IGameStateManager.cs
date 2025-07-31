public interface IGameStateManager
{
    void GameOver();
    void WinGame();
    void RestartGame();
    bool IsGameOver { get; }
    float GameTime { get; }
}