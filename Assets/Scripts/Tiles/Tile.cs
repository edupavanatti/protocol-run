using System.Collections.Generic;
using UnityEngine;

namespace Tiles
{
    /// <summary>
    /// Base component for tiles. Responsible for handling the hazard flag and obstacle spawning.
    /// TODO separate the Tile types into different subclasses.
    /// </summary>
    public class Tile : MonoBehaviour
    {
        [Tooltip("Whether this tile is a hazard (ie spike or acid) or not.")]
        [field: SerializeField] public bool IsHazard { get; private set; }

        [Tooltip("List of possible obstacle prefabs to randomly spawn on this tile.")]
        [SerializeField] private List<GameObject> obstaclePrefabs;

        [Tooltip("Chance from 0 to 100 that this tile will contain an obstacle (non-hazard tiles only).")]
        [Range(0, 100)]
        [SerializeField] private int obstacleSpawnChance = 10;

        /// <summary>
        /// Attempts to spawn an obstacle on this tile based on pre-defined probabilities.
        /// </summary>
        public bool TrySpawnObstacle(bool allowSpawn)
        {
            if (IsHazard || !allowSpawn || obstaclePrefabs == null || obstaclePrefabs.Count == 0) return false;

            // Determines if the tile will spawn with an obstacle or not.
            var roll = Random.Range(0, 100);
            if (roll >= obstacleSpawnChance) return false;

            // Chooses a random obstacle from the obstacles prefabs list.
            var index = Random.Range(0, obstaclePrefabs.Count);
            Instantiate(obstaclePrefabs[index], transform);
            return true;
        }
    }
}