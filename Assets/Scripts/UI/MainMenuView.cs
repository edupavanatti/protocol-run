using Events;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    /// <summary>
    /// View component for the Main Menu UI. Handles user input and the visual logic.
    /// </summary>
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] private Button playButton;

        private void Awake()
        {
            playButton.onClick.AddListener(OnPlayClicked);
        }

        private void OnDestroy()
        {
            playButton.onClick.RemoveListener(OnPlayClicked);
        }

        private void OnPlayClicked()
        {
            GameEvents.OnPlayPressed?.Invoke();
        }
    }
}