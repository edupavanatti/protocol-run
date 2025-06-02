using System.Collections.Generic;
using Core.Configs;
using Core.Interfaces;
using Core.States;
using Events;
using Tiles;
using UnityEngine;

namespace Core.Managers
{
    /// <summary>
    /// Uses the State Pattern to encapsulate Game Mode logic and manages State transitions.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        // Singleton pattern ensures only one instance exists.
        public static GameManager Instance { get; private set; }
        public GameStateType CurrentStateType { get; private set; }

        [Tooltip("The preset game configuration.")]
        [field: SerializeField] public GameConfig GameConfig { get; private set; }

        [Tooltip("Reference to the PlayerController object.")]
        [field: SerializeField] public PlayerController PlayerController { get; private set; }

        [Tooltip("Reference to the FloorScroller object.")]
        [field: SerializeField] public FloorScroller FloorScroller { get; private set; }

        [Tooltip("Reference to the TileSpawner object.")]
        [field: SerializeField] public TileSpawner TileSpawner { get; private set; }

        private Dictionary<GameStateType, IGameState> stateMap;
        private IGameState currentState;

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
            InitializeStates();
            SwitchState(GameStateType.MainMenu);
        }

        private void OnEnable()
        {
            GameEvents.OnPlayPressed += HandlePlay;
            GameEvents.OnRetryPressed += HandlePlay;
            GameEvents.OnBackPressed += HandleBack;
        }

        private void OnDisable()
        {
            GameEvents.OnPlayPressed -= HandlePlay;
            GameEvents.OnRetryPressed -= HandlePlay;
            GameEvents.OnBackPressed -= HandleBack;
        }

        /// <summary>
        /// Initializes and caches all Game State instances.
        /// </summary>
        private void InitializeStates()
        {
            stateMap = new Dictionary<GameStateType, IGameState>
            {
                { GameStateType.MainMenu, new MainMenuState() },
                { GameStateType.Playing, new PlayingState() },
                { GameStateType.GameOver, new GameOverState() }
            };
        }

        /// <summary>
        /// Switches the current Game State to the specified new state.
        /// </summary>
        public void SwitchState(GameStateType newState)
        {
            currentState?.Exit();
            CurrentStateType = newState;
            currentState = stateMap[newState];
            currentState.Enter();
        }

        private void HandlePlay()
        {
            SwitchState(GameStateType.Playing);
        }

        private void HandleBack()
        {
            SwitchState(GameStateType.MainMenu);
        }
    }
}