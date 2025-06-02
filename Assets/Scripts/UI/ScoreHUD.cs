using TMPro;
using UnityEngine;

namespace UI
{
    /// <summary>
    /// Displays the player's score on the screen.
    /// </summary>
    public class ScoreHUD : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;

        /// <summary>
        /// Updates the score display.
        /// </summary>
        /// <param name="score">The current score value to show.</param>
        public void UpdateScore(int score)
        {
            scoreText.text = $"Score: {score}";
        }
    }
}