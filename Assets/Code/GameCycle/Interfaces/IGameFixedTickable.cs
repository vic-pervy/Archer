namespace Code
{
    public interface IGameFixedTickable
    {
        public bool enabled { get; }
        void FixedGameTick();
    }
}