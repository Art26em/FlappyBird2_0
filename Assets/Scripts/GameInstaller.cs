using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [Header("Bird Settings")]
    
    [SerializeField] private Sprite normalSprite;
    [SerializeField] private Sprite deadSprite;
    
    [Header("Screens")]
    [SerializeField] private ShopScreen shopScreen;
    [SerializeField] private GameOverScreen gameOverScreen;
    
    [Header("Object generators")]
    [SerializeField] private PipeGenerator pipeGenerator;
    [SerializeField] private CoinGenerator coinGenerator;
    
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
        InstallScreens();
        InstallControllers();
        InstallPipes();
        InstallCoins();
        InstallSignals();
    }
    
    private void InstallBird()
    {
        Container.Bind<Bird>().
            AsSingle();
        
         Container.Bind<BirdSpriteController>()
             .AsSingle()
             .WithArguments(normalSprite, deadSprite);
        
    }
    
    private void InstallScreens()
    {
        Container.Bind<ShopScreen>().
            FromInstance(shopScreen).
            AsSingle();
        
        Container.Bind<GameOverScreen>().
            FromInstance(gameOverScreen).
            AsSingle();
    }

    private void InstallControllers()
    {
        Container.BindInterfacesAndSelfTo<StateController>().AsSingle();
        
        Container.Bind<GameStartController>().
            AsSingle();
        
        Container.Bind<GameResumeController>().
            AsSingle();
        
        Container.Bind<GameOverController>().
            AsSingle();
        
        Container.Bind<ShopController>().
            AsSingle();
        
    }
    
    private void InstallPipes()
    {
        Container.Bind<PipeGenerator>().
            FromInstance(pipeGenerator)
            .AsSingle();
        
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
        Container.Bind<CoinGenerator>().
            FromInstance(coinGenerator)
            .AsSingle();
        
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