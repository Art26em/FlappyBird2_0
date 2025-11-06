using TMPro;
using UnityEngine;
using Zenject;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private int levelUpScore = 10;
    [SerializeField] private TMP_Text scoreCountLabel;
    
    private SignalBus _signalBus;
    
    [Inject]
    private void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }
    
    private void OnEnable()
    {
        _signalBus.Subscribe<ScoreChangedSignal>(OnScoreChanged);    
    }

    private void OnDisable()
    {
        _signalBus.Unsubscribe<ScoreChangedSignal>(OnScoreChanged);    
    }

    private void OnScoreChanged(ScoreChangedSignal signal)
    {
        scoreCountLabel.text = signal.NewScore.ToString();
        if (signal.NewScore >= levelUpScore && signal.NewScore % levelUpScore == 0)
        {
            _signalBus.Fire(new GameStateChangedSignal(GameState.LevelUp));
        }
    }
    
}
