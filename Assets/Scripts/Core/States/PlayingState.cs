using Core.Interfaces;
using Core.Managers;

namespace Core.States
{
    /// <summary>
    /// Represents the Playing state of the game.
    /// </summary>
    public class PlayingState : IGameState
    {
        public void Enter()
        {
            GameManager.Instance.PlayerController?.StartRunning();
            GameManager.Instance.FloorScroller?.StartScrolling();
            GameManager.Instance.TileSpawner?.InitTiles();
            ScoreManager.Instance?.StartScoring();
        }

        public void Exit()
        {
            GameManager.Instance.PlayerController?.StopRunning();
            GameManager.Instance.FloorScroller?.StopScrolling();
            GameManager.Instance.TileSpawner?.ResetTiles();
            ScoreManager.Instance?.StopScoring();
        }
    }
}