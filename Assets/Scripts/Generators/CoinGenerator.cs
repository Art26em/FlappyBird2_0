using Zenject;

public class CoinGenerator : ObjectGenerator
{
    [Inject]
    private void Construct(CoinSpawner coinSpawner, CoinMover coinMover)
    {
        ObjectSpawner = coinSpawner;
        ObjectMover = coinMover;
    }
    
}
