using Core.Interfaces;
using Core.Managers;

namespace Core.States
{
    /// <summary>
    /// Represents the Game Over state of the game.
    /// </summary>
    public class GameOverState : IGameState
    {
        public void Enter()
        {
            UIManager.Instance.ShowGameOverMenu(true);
        }

        public void Exit()
        {
            UIManager.Instance.ShowGameOverMenu(false);
        }
    }
}