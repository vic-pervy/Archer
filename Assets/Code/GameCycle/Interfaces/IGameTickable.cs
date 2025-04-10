namespace Code
{
    public interface IGameTickable
    {
        public bool enabled { get; }
        void GameTick();
    }
}