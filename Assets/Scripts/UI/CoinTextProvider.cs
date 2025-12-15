using System;
using TMPro;
using Zenject;

public class CoinTextProvider: IInitializable, IDisposable
{
    private SignalBus _signalBus;
    private CoinCountLabel _coinCountLabel;
    private TMP_Text _coinCountText;
    
    [Inject]
    public void Construct(SignalBus signalBus, CoinCountLabel coinCountLabel)
    {
        _signalBus = signalBus;
        _coinCountLabel = coinCountLabel;
    }
    
    public void Initialize()
    {
        _signalBus.Subscribe<CoinCountChangedSignal>(OnCoinCountChanged);
        _coinCountText = _coinCountLabel.GetComponent<TMP_Text>();
    }
    
    private void OnCoinCountChanged(CoinCountChangedSignal signal)
    {
        _coinCountText.text = signal.NewCoinCount.ToString();    
    }
    
    public void Dispose()
    {
        _signalBus.Unsubscribe<CoinCountChangedSignal>(OnCoinCountChanged);
    }
}
