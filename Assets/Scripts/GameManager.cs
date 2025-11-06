using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PipeGenerator pipeGenerator;
    [SerializeField] private CoinGenerator coinGenerator;
    [SerializeField] private StartScreen startScreen;
    [SerializeField] private ShopScreen shopScreen;
    [SerializeField] private GameOverScreen gameOverScreen;

    private Bird _bird;
    private SignalBus _signalBus;
    
    public GameState CurrentGameState { get; private set; }
    
    [Inject]
    private void Construct(Bird bird, SignalBus signalBus)
    {
        _bird = bird;
        _signalBus = signalBus;
    }
    
    private void OnEnable()
    {
        _signalBus.Subscribe<GameStateChangedSignal>(OnGameStateChanged);
    }

    private void OnDisable()
    {
        _signalBus.Unsubscribe<GameStateChangedSignal>(OnGameStateChanged);
    }

    private void Start()
    {
        Time.timeScale = 0;
        CurrentGameState = GameState.Starting;
        startScreen.Open();
    }
    
    private void OnGameStateChanged(GameStateChangedSignal signal)
    {
        switch (signal.NewState)
        {
            case GameState.Starting:
                StartGame();
                break;
            case GameState.LevelUp:
                OpenShop();
                break;
            case GameState.Playing:
                ResumeGame();
                break;
            case GameState.GameOver:
                GameOver();
                break;
        }
        
    }
    
    private void StartGame()
    {
        pipeGenerator.ResetPool();
        coinGenerator.ResetPool();
        
        Time.timeScale = 1;
        CurrentGameState = GameState.Playing;
        _bird.ResetPlayer();
    }
    
    private void ResumeGame()
    {
        pipeGenerator.ResetPool();
        coinGenerator.ResetPool();
        
        Time.timeScale = 1;
        CurrentGameState = GameState.Playing;
        _bird.ResetPlayer(false);
    }

    private void OpenShop()
    {
        Time.timeScale = 0;
        CurrentGameState = GameState.LevelUp;
        shopScreen.Open();
    }

    private void GameOver()
    {
        Time.timeScale = 0;
        CurrentGameState = GameState.GameOver;
        gameOverScreen.Open();
    }
    
}
