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
        gameObject.SetActive(true);
        button.interactable = true;
        buyArmorButton.interactable = _bird.GetCoinsAmount() >= armorPrice && !_bird.IsArmored;
    }

    public override void Close()
    {
        gameObject.SetActive(false);
        button.interactable = false;
        SignalBus.Fire(new GameStateChangedSignal(GameState.Playing));
    }

    private void OnBuyButtonClick()
    {
        _bird.OnItemPurchase(armorPrice);
        _bird.IsArmored = true;
        buyArmorButton.interactable = false;
    }
    
}
