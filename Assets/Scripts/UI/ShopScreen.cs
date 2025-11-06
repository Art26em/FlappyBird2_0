using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ShopScreen : Screen
{
    [SerializeField] private Button buyArmorButton;
    [SerializeField] private int armorPrice = 5;
    
    private Bird _bird;

    [Inject]
    public void Construct(Bird bird)
    {
        _bird = bird;
    }
    
    private void OnEnable()
    {
        button.onClick.AddListener(OnButtonClick);
        buyArmorButton.onClick.AddListener(OnBuyButtonClick);
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(OnButtonClick);
        buyArmorButton.onClick.RemoveListener(OnBuyButtonClick);
    }
    
    protected override void OnButtonClick()
    {
        Close();
    }

    public override void Open()
    {
        canvasGroup.alpha = 1;
        button.interactable = true;
        
        buyArmorButton.interactable = _bird.Coins >= armorPrice && !_bird.isArmored;
    }

    public override void Close()
    {
        canvasGroup.alpha = 0;
        button.interactable = false;
        SignalBus.Fire(new GameStateChangedSignal(GameState.Playing));
    }

    private void OnBuyButtonClick()
    {
        _bird.DecrementCoins(armorPrice);
        _bird.isArmored = true;
        buyArmorButton.interactable = false;
    }
    
}
