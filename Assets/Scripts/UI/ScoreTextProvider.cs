using System;
using TMPro;
using Zenject;

public class ScoreTextProvider : IInitializable, IDisposable
{
    private ScoreCountLabel _scoreCountLabel;
    private TMP_Text _scoreCountText;
    private SignalBus _signalBus;
    
    [Inject]
    private void Construct(SignalBus signalBus, ScoreCountLabel scoreCountLabel)
    {
        _signalBus = signalBus;
        _scoreCountLabel = scoreCountLabel;
    }
    
    public void Initialize()
    {
        _signalBus.Subscribe<ScoreChangedSignal>(OnScoreChanged);
        _scoreCountText = _scoreCountLabel.GetComponent<TMP_Text>();
    }
    
    private void OnScoreChanged(ScoreChangedSignal signal)
    {
        _scoreCountText.text = signal.NewScore.ToString();
    }
    
    public void Dispose()
    {
        _signalBus?.Unsubscribe<ScoreChangedSignal>(OnScoreChanged);
    }
}
