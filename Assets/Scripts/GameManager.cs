using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PipeGenerator pipeGenerator;
    [SerializeField] private StartScreen startScreen;
    [SerializeField] private GameOverScreen gameOverScreen;

    private Bird _bird;

    [Inject]
    private void Construct(Bird bird)
    {
        _bird = bird;
    }
    
    private void OnEnable()
    {
        startScreen.PlayButtonClick += OnPlayButtonClick;
        gameOverScreen.RestartButtonClick += OnRestartButtonClick;
        _bird.GameOver += OnGameOver;
    }

    private void OnDisable()
    {
        startScreen.PlayButtonClick -= OnPlayButtonClick;
        gameOverScreen.RestartButtonClick -= OnRestartButtonClick;
        _bird.GameOver -= OnGameOver;
    }

    private void Start()
    {
        Time.timeScale = 0;
        startScreen.Open();
    }
    
    private void OnPlayButtonClick()
    {
        startScreen.Close();
        StartGame();
    }
    
    private void OnRestartButtonClick()
    {
        gameOverScreen.Close();
        pipeGenerator.ResetPool();
        StartGame();
    }

    private void StartGame()
    {
        Time.timeScale = 1;
        _bird.ResetPlayer();
    }

    private void OnGameOver()
    {
        Time.timeScale = 0;
        gameOverScreen.Open();
    }
    
}
