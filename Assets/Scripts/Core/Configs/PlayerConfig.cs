using UnityEngine;

namespace Core.Configs
{
    /// <summary>
    /// ScriptableObject containing the configuration values for the player's movement and physics.
    /// </summary>
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Config/PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {
        [Header("Jump Settings")]
        [Tooltip("Initial vertical jump velocity.")]
        public float jumpForce = 800f;

        [Tooltip("Gravity applied during jump.")]
        public float gravity = -1600f;
    }
}