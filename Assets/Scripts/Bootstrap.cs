using UnityEngine;
using Zenject;

public class Bootstrap : MonoInstaller
{
    [SerializeField] private Bird bird;
    [SerializeField] private GameManager gameManager;
    
    [Header("PipeSpawner settings")]
    [SerializeField] private float secondsBetweenPipeSpawn;
    [SerializeField] private float maxPipeSpawnPositionY; 
    [SerializeField] private float minPipeSpawnPositionY;
    
    [Header("PipeMover settings")]
    [SerializeField] private float pipeMoveSpeed = 2; 
    
    [Header("CoinSpawner settings")]
    [SerializeField] private float secondsBetweenCoinSpawn;
    [SerializeField] private float maxCoinSpawnPositionY; 
    [SerializeField] private float minCoinSpawnPositionY;
    
    [Header("CoinMover settings")]
    [SerializeField] private float coinMoveSpeed = 2; 

    
    public override void InstallBindings()
    {
        InstallBird();
        InstallGameManager();
        InstallPipes();
        InstallCoins();
        InstallSignals();
    }

    private void InstallGameManager()
    {
        Container.Bind<GameManager>().
            FromInstance(gameManager).
            AsSingle();
    }

    private void InstallBird()
    {
        Container.Bind<Bird>().
            FromInstance(bird)
            .AsSingle();
    }
    
    private void InstallPipes()
    {
        Container.Bind<PipeSpawner>().
            FromInstance(new PipeSpawner(minPipeSpawnPositionY, maxPipeSpawnPositionY, secondsBetweenPipeSpawn)).
            AsSingle();
        
        Container.Bind<PipeMover>().
            FromNewComponentOnNewGameObject().
            AsSingle().
            OnInstantiated<PipeMover>((_, mover) => 
            {
                mover.Initialize(pipeMoveSpeed);
            });    
    }
    
    private void InstallCoins()
    {
        Container.Bind<CoinSpawner>().
            FromInstance(new CoinSpawner(minCoinSpawnPositionY, maxCoinSpawnPositionY, secondsBetweenCoinSpawn)).
            AsSingle();
        
        Container.Bind<CoinMover>().
            FromNewComponentOnNewGameObject().
            AsSingle().
            OnInstantiated<CoinMover>((_, mover) => 
            {
                mover.Initialize(coinMoveSpeed);
            }); 
    }
    
    private void InstallSignals()
    {
        SignalBusInstaller.Install(Container);
        
        Container.DeclareSignal<CoinCountChangedSignal>();
        Container.DeclareSignal<ScoreChangedSignal>();
        Container.DeclareSignal<GameStateChangedSignal>();
    }
}