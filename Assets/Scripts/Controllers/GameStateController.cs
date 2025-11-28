
public abstract class GameStateController
{
    protected PipeGenerator PipeGenerator;
    protected CoinGenerator CoinGenerator;
    protected Bird Bird;
    
    protected void ResetGenerators()
    {
        PipeGenerator.ResetPool();
        CoinGenerator.ResetPool();
    }
    
}  
