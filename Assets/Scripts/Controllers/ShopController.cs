using Zenject;

public class ShopController
{
    
    private ShopScreen _shopScreen;

    [Inject]
    private void Construct(ShopScreen shopScreen)
    {
        _shopScreen = shopScreen;
    }

    public void OpenShop()
    {
        _shopScreen.Open();
    }
    
}