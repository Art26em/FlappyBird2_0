using UnityEngine;
using Zenject;

public class GameOverController
{
    private GameOverScreen _gameOverScreen;

    [Inject]
    private void Construct(GameOverScreen gameOverScreen)
    {
        _gameOverScreen = gameOverScreen;
    }
    
    public void GameOver()
    {
        Time.timeScale = 0;
        _gameOverScreen.Open();
    }
    
}