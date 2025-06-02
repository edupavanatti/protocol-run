namespace Core.Interfaces
{
    /// <summary>
    /// Interface to be implemented by Game States, enforcing a contract for entering and exiting a state.
    /// </summary>
    public interface IGameState
    {
        void Enter();
        void Exit();
    }
}