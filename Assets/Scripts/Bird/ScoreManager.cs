using Zenject;

public class ScoreManager
{
    private int _scoreCount;
    private int _levelUpScoreCount;
    private SignalBus _signalBus;
    
    [Inject]    
    public void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    public ScoreManager(int levelUpScoreCount)
    {
        _scoreCount = 0;
        _levelUpScoreCount = levelUpScoreCount;
    }
    
    public void IncrementScore()
    {
        _scoreCount++;
        _signalBus.Fire(new ScoreChangedSignal(_scoreCount));
        
        if (_scoreCount >= _levelUpScoreCount && _scoreCount % _levelUpScoreCount == 0)
        {
            _signalBus.Fire(new GameStateChangedSignal(GameState.LevelUp));
        }
        
    }
    
    public void ResetScore()
    {
        _scoreCount = 0;
        _signalBus.Fire(new ScoreChangedSignal(_scoreCount));
    }
    
}
