namespace Code
{
    public interface IGameStateListeners
    {
        
    }

    public interface IStartGameListener : IGameStateListeners
    {
        void OnStartGame();
    }
    
    public interface IPauseGameListener : IGameStateListeners
    {
        void OnPauseGame();
    }
}