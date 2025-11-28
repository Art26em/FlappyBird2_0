using System;
using UnityEngine;
using Zenject;

public class StateController : IInitializable, IDisposable
{
    private SignalBus _signalBus;
    private ShopController _shopController;
    private GameStartController _gameStartController;
    private GameResumeController _gameResumeController;
    private GameOverController _gameOverController;
    
    public GameState CurrentGameState { get; private set; }
    
    [Inject]
    public void Construct(
        ShopController shopController,  
        GameStartController gameStartController, 
        GameResumeController gameResumeController,  
        GameOverController gameOverController,
        SignalBus signalBus)
    {
        _shopController = shopController; 
        _gameStartController = gameStartController;
        _gameResumeController = gameResumeController;
        _gameOverController = gameOverController;
        _signalBus = signalBus;
    }
    
    public void Initialize()
    {
        _signalBus.Subscribe<GameStateChangedSignal>(OnGameStateChanged);
    }
    
    private void OnGameStateChanged(GameStateChangedSignal signal)
    {
        switch (signal.NewState)
        {
        case GameState.Starting:
            CurrentGameState = GameState.Starting;
            _gameStartController.StartGame();
            break;
        case GameState.LevelUp:
            CurrentGameState = GameState.LevelUp;
            _shopController.OpenShop();
            break;
        case GameState.Playing:
            CurrentGameState = GameState.Playing;
            _gameResumeController.ResumeGame();
            break;
        case GameState.GameOver: 
            CurrentGameState = GameState.GameOver;
            _gameOverController.GameOver();
            break;
        }
    }
    
    public void Dispose()
    {
        _signalBus.Unsubscribe<GameStateChangedSignal>(OnGameStateChanged);
    }
    
}