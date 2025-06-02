using Core.Interfaces;
using Core.Managers;

namespace Core.States
{
    /// <summary>
    /// Represents the Main Menu state of the game.
    /// </summary>
    public class MainMenuState : IGameState
    {
        public void Enter()
        {
            UIManager.Instance.ShowMainMenu(true);
        }

        public void Exit()
        {
            UIManager.Instance.ShowMainMenu(false);
        }
    }
}