using System.Collections;
using Core.Configs;
using Core.Managers;
using UnityEngine;

namespace Core
{
    /// <summary>
    /// Handles player jumping logic and animations. Also handles player collision with tiles and obstacles.
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    public class PlayerController : MonoBehaviour
    {
        // Animation-related triggers.
        private static readonly int Idle = Animator.StringToHash("Idle");
        private static readonly int Run = Animator.StringToHash("Run");
        private static readonly int Jump = Animator.StringToHash("Jump");
        private static readonly int Dead = Animator.StringToHash("Dead");

        [Tooltip("The preset player configuration.")]
        [SerializeField] private PlayerConfig playerConfig;

        private Vector2 velocity;
        private float verticalPos;
        private float startYPos;
        private bool isRunning;
        private bool isJumping;
        private RectTransform playerTransform;
        private Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            playerTransform = GetComponent<RectTransform>();
            startYPos = playerTransform.anchoredPosition.y;
        }

        private void Update()
        {
            if (!isRunning) return;
            // Listens for player input - the Space Key and Left Mouse Button if on Editor or touches if on Mobile.
#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
#else
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
#endif
            {
                TryJump();
            }
        }

        private void LateUpdate()
        {
            if (!isJumping || !isRunning) return;

            // Applies gravity and vertical movement.
            velocity.y += playerConfig.gravity * Time.deltaTime;
            verticalPos += velocity.y * Time.deltaTime;

            if (verticalPos <= 0f)
            {
                verticalPos = 0f;
                velocity = Vector2.zero;
                isJumping = false;
            }

            var pos = playerTransform.anchoredPosition;
            pos.y = startYPos + verticalPos;
            playerTransform.anchoredPosition = pos;
        }

        /// <summary>
        /// Checks for player collision against obstacles.
        /// </summary>
        /// <param name="other">The collider that was collided with.</param>
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!isRunning) return;

            if (other.CompareTag(tag:"Obstacle"))
            {
                StartCoroutine(KillPlayer());
            }

            return;

            // Waits for the Death animation to be fully played before switching states.
            IEnumerator KillPlayer()
            {
                animator.SetTrigger(Dead);
                isRunning = false;
                yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0).Length);
                GameManager.Instance.SwitchState(GameStateType.GameOver);
            }
        }

        /// <summary>
        /// Prepares the player to start running.
        /// </summary>
        public void StartRunning()
        {
            // TODO set the Run trigger only after the initial countdown (to be implemented).
            animator.SetTrigger(Run);
            isRunning = true;
        }

        /// <summary>
        /// Changes the player animation when stopping running.
        /// </summary>
        public void StopRunning()
        {
            animator.SetTrigger(Idle);
        }

        /// <summary>
        /// Attempts to initiate a jump if the player is not already in the air.
        /// </summary>
        private void TryJump()
        {
            if (isJumping || !isRunning) return;
            isJumping = true;
            velocity = Vector2.up * playerConfig.jumpForce;
            animator.SetTrigger(Jump);
        }
    }
}