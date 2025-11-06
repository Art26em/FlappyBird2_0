using TMPro;
using UnityEngine;
using Zenject;

public class CoinManager : MonoBehaviour
{
    [SerializeField] private TMP_Text coinCountLabel;
    
    private SignalBus _signalBus;
    
    [Inject]
    public void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }
    
    private void OnEnable()
    {
        _signalBus.Subscribe<CoinCountChangedSignal>(OnCoinCountChanged);    
    }

    private void OnDisable()
    {
        _signalBus.Unsubscribe<CoinCountChangedSignal>(OnCoinCountChanged);    
    }

    private void OnCoinCountChanged(CoinCountChangedSignal signal)
    {
        coinCountLabel.text = signal.NewCoinCount.ToString();    
    }
    
}
