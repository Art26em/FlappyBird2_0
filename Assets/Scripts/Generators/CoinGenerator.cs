using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

public class CoinGenerator : IDisposable
{
    private readonly ObjectPool<Coin> _pool;
    private CoinSpawner _spawner;
    private CoinMover _mover;
    private readonly Transform _container;
    
    private float _elapsedTime;
    private CancellationTokenSource _cts;
    private bool _isRunning;
    
    [Inject]
    private void Construct(CoinSpawner coinSpawner, CoinMover coinMover)
    {
        _spawner = coinSpawner;
        _mover = coinMover;
    }
    
    public CoinGenerator(CoinGeneratorSettings settings)
    {
        _pool = new ObjectPool<Coin>(settings.CoinsCount);
        _container = settings.Container.transform;
        for (int i = 0; i < settings.CoinsCount; i++)
        {
            var spawned = Object.Instantiate(settings.Template, _container);
            spawned.gameObject.SetActive(false);
            _pool.Add(spawned);
            spawned.transform.SetParent(settings.Container.transform);
        }    
    }
    
    public void Reset()
    {
        _elapsedTime = 0;
        _pool.ResetPool();
        StopTimer();
        _ = GenerateCoins();
    }
    
    private async UniTask GenerateCoins()
    {
        _isRunning = true;
        _cts = new CancellationTokenSource();
        _elapsedTime = 0f;

        try
        {
            while (_isRunning)
            {
                while (!_spawner.IsTimeToSpawn(_elapsedTime))
                {
                    _elapsedTime += Time.deltaTime;
                    await UniTask.Yield(PlayerLoopTiming.Update, _cts.Token);
                } 
        
                _elapsedTime = 0;
        
                if (_pool.TryGetObject(out var pipes))
                {
                    _spawner.SpawnObject(pipes.gameObject, _container.position);
                    _mover.StartObjectMoving(pipes.gameObject);
                }
                _pool.DisableObjectAbroadScreen();    
            }
        }
        catch (OperationCanceledException)
        {
                
        }
    }
 
    private void StopTimer()
    {
        _isRunning = false;
        _cts?.Cancel();
        _cts?.Dispose();
    }


    public void Dispose()
    {
        StopTimer();
    }
}
