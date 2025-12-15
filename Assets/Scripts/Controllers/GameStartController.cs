using UnityEngine;
using Zenject;

public class GameStartController
{
    private PipeGenerator _pipeGenerator;
    private CoinGenerator _coinGenerator;
    private Bird _bird;
    
    [Inject]
    private void Construct(PipeGenerator pipeGenerator, CoinGenerator coinGenerator, Bird bird)
    {
        _pipeGenerator = pipeGenerator;
        _coinGenerator = coinGenerator;
        _bird = bird;
    }
    
    public void StartGame()
    {
        Time.timeScale = 1;
        ResetGenerators();
        _bird.ResetPlayer();
    }
    
    private void ResetGenerators()
    {
        _pipeGenerator.Reset();
        _coinGenerator.Reset();
    }
    
}
