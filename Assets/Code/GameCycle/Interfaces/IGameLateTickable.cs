namespace Code
{
    public interface IGameLateTickable
    {
        public bool enabled { get; }
        void LateGameTick();
    }
}