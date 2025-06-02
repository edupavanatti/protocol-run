using UnityEngine;

namespace Core.Managers
{
    /// <summary>
    /// Manages the visibility of UI elements across different Game States.
    /// </summary>
    public class UIManager : MonoBehaviour
    {
        // Singleton pattern ensures only one instance exists.
        public static UIManager Instance { get; private set; }

        [SerializeField] private GameObject mainMenuPanel;
        [SerializeField] private GameObject gameOverPanel;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }

        /// <summary>
        /// Enables or disables the Main Menu UI panel.
        /// </summary>
        public void ShowMainMenu(bool show)
        {
            // TODO fade the menu in/out instead.
            mainMenuPanel.SetActive(show);
        }

        public void ShowGameOverMenu(bool show)
        {
            // TODO fade the menu in/out instead.
            gameOverPanel.SetActive(show);
        }
    }
}