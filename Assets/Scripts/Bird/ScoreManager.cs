using Zenject;

public class ScoreManager
{
    private int _scoreCount;
    private SignalBus _signalBus;
    
    [Inject]
    private void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }
    
    public void IncrementScore()
    {
        _scoreCount++;
        _signalBus.Fire(new ScoreChangedSignal(_scoreCount));
    }
    
    public void ResetScore()
    {
        _scoreCount = 0;
        _signalBus.Fire(new ScoreChangedSignal(_scoreCount));
    }
    
}