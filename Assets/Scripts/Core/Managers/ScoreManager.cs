using Core.Configs;
using UI;
using UnityEngine;

namespace Core.Managers
{
    /// <summary>
    /// Tracks the player's score and checks for the defined win condition.
    /// </summary>
    public class ScoreManager : MonoBehaviour
    {
        // Singleton pattern ensures only one instance exists.
        public static ScoreManager Instance { get; private set; }
        public int CurrentScore { get; private set; }

        [Tooltip("Reference to the HUD that displays the score.")]
        [SerializeField] private ScoreHUD scoreHUD;

        private GameConfig gameConfig;
        private float scoreTimer;
        private bool isRunning;

        public bool HasPlayerWon => CurrentScore >= gameConfig.scoreToWin;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        private void Start()
        {
            gameConfig = GameManager.Instance.GameConfig;
        }

        private void Update()
        {
            if (!isRunning) return;
            scoreTimer += Time.deltaTime;

            if (scoreTimer >= 1f)
            {
                scoreTimer -= 1f;
                AddScore(gameConfig.scorePerSecond);
            }
        }

        /// <summary>
        /// Adds to the current score every second.
        /// </summary>
        /// <param name="amount">The amount to be added to the score.</param>
        private void AddScore(int amount)
        {
            CurrentScore += amount;
            scoreHUD?.UpdateScore(CurrentScore);

            if (CurrentScore >= gameConfig.scoreToWin)
            {
                isRunning = false;
                GameManager.Instance.SwitchState(GameStateType.GameOver);
            }
        }

        public void StartScoring()
        {
            CurrentScore = 0;
            scoreHUD?.UpdateScore(CurrentScore);
            isRunning = true;
        }

        public void StopScoring()
        {
            isRunning = false;
        }
    }
}