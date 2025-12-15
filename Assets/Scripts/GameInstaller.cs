using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [Header("Bird movement settings")] 
    [SerializeField] private float tapForce;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float maxRotationZ;
    [SerializeField] private float minRotationZ;
    [SerializeField] private Vector3 offsetPosition;
    
    [Header("Animation settings")]
    [SerializeField] private Sprite normalSprite;
    [SerializeField] private Sprite deadSprite;
    [SerializeField] private float startAnimationDuration;
    [SerializeField] private float blinkAnimationDuration;
    
    [Header("Level Up settings")]
    [SerializeField] private int levelUpScore;
    
    [Header("Screens")]
    [SerializeField] private ShopScreen shopScreen;
    [SerializeField] private GameOverScreen gameOverScreen;
    
    [Header("PipeGenerator settings")] 
    [SerializeField] private int pipesCount;
    [SerializeField] private Pipes pipeTemplate;
    [SerializeField] private GameObject pipesContainer;
    
    [Header("CoinGenerator settings")] 
    [SerializeField] private int coinsCount;
    [SerializeField] private Coin coinTemplate;
    [SerializeField] private GameObject coinsContainer;
    
    [Header("PipeSpawner settings")]
    [SerializeField] private float secondsBetweenPipeSpawn;

    [SerializeField] private float minSecondsBetweenPipeSpawn;
    [SerializeField] private float levelUpDecreasingSeconds;
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
        InstallSignals();
        InstallUI();
        InstallBird();
        InstallScreens();
        InstallControllers();
        InstallPipes();
        InstallCoins();
    }

    private void InstallSignals()
    {
        SignalBusInstaller.Install(Container);
        Container.DeclareSignal<CoinCountChangedSignal>();
        Container.DeclareSignal<ScoreChangedSignal>();
        Container.DeclareSignal<GameStateChangedSignal>();
        Container.DeclareSignal<BirdDamagedSignal>();
    }
    
    private void InstallUI()
    {
        Container.Bind<ScoreCountLabel>()
            .FromComponentInHierarchy()
            .AsSingle();
        
        Container.Bind<CoinCountLabel>()
            .FromComponentInHierarchy()
            .AsSingle();
        
        Container.BindInterfacesAndSelfTo<ScoreTextProvider>()
            .AsSingle();
        
        Container.BindInterfacesAndSelfTo<CoinTextProvider>()
            .AsSingle();
    }

    private void InstallBird()
    {
        Container.Bind<ScoreManager>().
            AsSingle().
            WithArguments(levelUpScore);

        Container.Bind<Wallet>().
            AsSingle();
        
        Container.Bind<BirdSpriteController>().
            AsSingle().
            WithArguments(normalSprite, deadSprite);

        var movementSettings = new MovementSettings(
            tapForce, 
            rotationSpeed, 
            maxRotationZ, 
            minRotationZ,
            offsetPosition);
        
        var animationSettings = new AnimationSettings(startAnimationDuration, blinkAnimationDuration);
        
        Container.Bind<MovementController>().
            AsSingle().
            WithArguments(movementSettings);
        
        Container.BindInterfacesAndSelfTo<AnimationController>().
            AsSingle().
            WithArguments(animationSettings);
        
		Container.Bind<Bird>()
            .AsSingle();
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
        var pipeSpawnerSettings = new SpawnerSettings(
            minPipeSpawnPositionY, 
            maxPipeSpawnPositionY, 
            secondsBetweenPipeSpawn, 
            levelUpDecreasingSeconds,
            minSecondsBetweenPipeSpawn);
        
        Container.BindInterfacesAndSelfTo<PipeSpawner>().
            AsSingle().
            WithArguments(pipeSpawnerSettings);

        Container.Bind<PipeMover>().AsSingle().WithArguments(pipeMoveSpeed); 
        
        var pipeGeneratorSettings = new PipeGeneratorSettings(pipesCount, pipeTemplate, pipesContainer);
        Container.BindInterfacesAndSelfTo<PipeGenerator>().
            AsSingle().
            WithArguments(pipeGeneratorSettings);
        
    }
    
    private void InstallCoins()
    {
        
        var coinSpawnerSettings = new SpawnerSettings(
            minCoinSpawnPositionY, 
            maxCoinSpawnPositionY, 
            secondsBetweenCoinSpawn);
        
        Container.Bind<CoinSpawner>().
            AsSingle().
            WithArguments(coinSpawnerSettings);
        
        Container.Bind<CoinMover>().AsSingle().WithArguments(coinMoveSpeed);
        
        var coinGeneratorSettings = new CoinGeneratorSettings(coinsCount, coinTemplate, coinsContainer);
        Container.BindInterfacesAndSelfTo<CoinGenerator>().
            AsSingle().WithArguments(coinGeneratorSettings);
        
    }
    
}