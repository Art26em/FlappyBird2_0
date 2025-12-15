using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

public class PipeGenerator : IDisposable
{
    private readonly ObjectPool<Pipes> _pool;
    private PipeSpawner _spawner;
    private PipeMover _mover;
    private readonly Transform _container;
    
    private float _elapsedTime;
    private CancellationTokenSource _cts;
    private bool _isRunning;
    
    [Inject]
     private void Construct(PipeSpawner pipeSpawner, PipeMover pipeMover)
     {
         _spawner = pipeSpawner;
         _mover = pipeMover;
     }

    public PipeGenerator(PipeGeneratorSettings settings)
    {
        _pool = new ObjectPool<Pipes>(settings.PipesCount);
        _container = settings.Container.transform;
        for (int i = 0; i < settings.PipesCount; i++)
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
        _ = GeneratePipes();
    }
    
    private async UniTask GeneratePipes()
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
