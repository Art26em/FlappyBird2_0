
public struct GameStateChangedSignal
{
    public readonly GameState NewState;

    public GameStateChangedSignal(GameState newState)
    {
        NewState = newState;
    }
}

