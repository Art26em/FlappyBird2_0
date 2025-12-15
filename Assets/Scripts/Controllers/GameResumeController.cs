using UnityEngine;
using Zenject;

public class GameResumeController
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
    
    public void ResumeGame()
    {
        ResetGenerators();
        Time.timeScale = 1;
        _bird.ResetPlayer(false);
    }  
    
    private void ResetGenerators()
    {
        _pipeGenerator.Reset();
        _coinGenerator.Reset();
    }
    
}