using Core.Managers;
using Events;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    /// <summary>
    /// View component for the Game Over Menu UI. Handles user input and the visual logic.
    /// </summary>
    public class GameOverMenuView : MonoBehaviour
    {
        private const string WinText = "Congratulations!\nYou won!";
        private const string LoseTest = "Too bad!\nYou lost!";

        [SerializeField] private Button backButton;
        [SerializeField] private Button retryButton;
        [SerializeField] private TextMeshProUGUI resultText;
        [SerializeField] private TextMeshProUGUI scoreText;

        private void Awake()
        {
            backButton.onClick.AddListener(OnBackClicked);
            retryButton.onClick.AddListener(OnRetryClicked);
        }

        private void OnEnable()
        {
            SetPanelTexts();
        }

        private void OnDestroy()
        {
            backButton.onClick.RemoveListener(OnBackClicked);
            retryButton.onClick.RemoveListener(OnRetryClicked);
        }

        private void SetPanelTexts()
        {
            var finalScore = ScoreManager.Instance.CurrentScore;
            resultText.text = ScoreManager.Instance.HasPlayerWon ? WinText : LoseTest;
            scoreText.text = $"Final Score: {finalScore}";
        }

        private void OnBackClicked()
        {
            GameEvents.OnBackPressed?.Invoke();
        }

        private void OnRetryClicked()
        {
            GameEvents.OnRetryPressed?.Invoke();
        }
    }
}