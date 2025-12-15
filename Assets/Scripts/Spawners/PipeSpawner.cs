using System;
using Zenject;

public class PipeSpawner : ObjectSpawner, IInitializable, IDisposable
{
    private SignalBus _signalBus;
    
    [Inject]
    private void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;        
    }
    
    public PipeSpawner(SpawnerSettings spawnerSettings) 
        : base(spawnerSettings)
    {
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
                Settings.CurrentSecondsBetweenSpawn = Settings.StartSecondsBetweenSpawn;
                break;
            case GameState.LevelUp:
                if (Settings.CurrentSecondsBetweenSpawn - Settings.LevelUpDecreasingSpawnSeconds < Settings.MinSecondsBetweenSpawn)
                {
                    Settings.CurrentSecondsBetweenSpawn = Settings.MinSecondsBetweenSpawn;        
                }
                else
                {
                    Settings.CurrentSecondsBetweenSpawn -= Settings.LevelUpDecreasingSpawnSeconds;    
                }
                break;
        }    
    }
    
    public void Dispose()
    {
        _signalBus.Unsubscribe<GameStateChangedSignal>(OnGameStateChanged);
    }
}