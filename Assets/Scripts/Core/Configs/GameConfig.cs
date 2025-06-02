using UnityEngine;

namespace Core.Configs
{
    /// <summary>
    /// ScriptableObject that stores configurations for the gameplay session.
    /// </summary>
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Config/GameConfig")]
    public class GameConfig : ScriptableObject
    {
        [Tooltip("Score required to win the game.")]
        public int scoreToWin = 100;

        [Tooltip("Score gained per second while alive.")]
        public int scorePerSecond = 10;

        [Tooltip("Range for the obstacles spawn interval (x = min, y = max).")]
        public Vector2 spawnIntervalRange = new(1f, 2.5f);

        [Tooltip("Speed at which the scenario scrolls.")]
        public float scenarioSpeed = 500f;
    }
}