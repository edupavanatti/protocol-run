using System.Collections.Generic;
using Core.Managers;
using UnityEngine;

namespace Tiles
{
    /// <summary>
    /// Spawns and despawns floor tiles for the scrolling scenario.
    /// </summary>
    public class TileSpawner : MonoBehaviour
    {
        [Header("References")]
        [Tooltip("Where the tiles will be parented to.")]
        [SerializeField] private RectTransform floorRoot;

        [Tooltip("Available tiles.")]
        [SerializeField] private List<Tile> tiles;

        [Header("Tile Spawn Settings")]
        [Tooltip("How many tiles to start with.")]
        [SerializeField] private int initialTileCount = 10;

        [Tooltip("Number of tiles at the beginning of the run without obstacles.")]
        [SerializeField] private int safeStartTileCount = 10;

        private readonly List<RectTransform> activeTiles = new();
        private int totalTilesSpawned;
        private float nextSpawnXPos;
        private float tileWidth;
        private float nextHazardSpawnTime;
        private bool lastTileHadObstacle;

        private Vector2 SpawnInterval => GameManager.Instance.GameConfig.spawnIntervalRange;

        private void Update()
        {
            if (activeTiles.Count == 0) return;

            // Despawn tiles offscreen.
            var leftEdge = -floorRoot.anchoredPosition.x - tileWidth;
            while (activeTiles.Count > 0 && activeTiles[0].anchoredPosition.x + tileWidth < leftEdge)
            {
                var removedTile = activeTiles[0];
                activeTiles.RemoveAt(0);
                // TODO implement a pooling system instead.
                Destroy(removedTile.gameObject);
            }

            // Spawn tiles ahead as needed.
            var rightEdge = -floorRoot.anchoredPosition.x + Screen.width + tileWidth;
            while (nextSpawnXPos < rightEdge)
            {
                SpawnTile();
            }
        }

        public void InitTiles()
        {
            nextSpawnXPos = 0f;
            nextHazardSpawnTime = Time.time + Random.Range(SpawnInterval.x, SpawnInterval.y);

            for (var i = 0; i < initialTileCount; i++)
            {
                SpawnTile();
            }
        }

        /// <summary>
        /// Method responsible for spawning tiles.
        /// </summary>
        private void SpawnTile()
        {
            var spawnHazard = Time.time >= nextHazardSpawnTime;
            var tile = GetNextTile(spawnHazard);

            if (tile == null)
            {
                Debug.LogError("TileSpawner: There's no next tile available!");
                return;
            }

            var tileTransform = Instantiate(tile, floorRoot).GetComponent<RectTransform>();
            tileTransform.anchoredPosition = new Vector2(nextSpawnXPos, 0f);
            activeTiles.Add(tileTransform);
            tileWidth = tileTransform.rect.width;
            nextSpawnXPos += tileWidth;

            // Obstacles spawn control.
            var tileComponent = tileTransform.GetComponent<Tile>();
            var isInSafeZone = totalTilesSpawned < safeStartTileCount;
            var allowObstacle = !isInSafeZone && !lastTileHadObstacle;
            var spawnedObstacle = tileComponent.TrySpawnObstacle(allowObstacle);
            lastTileHadObstacle = spawnedObstacle;
            totalTilesSpawned++;

            if (spawnHazard && tile.IsHazard)
            {
                nextHazardSpawnTime = Time.time + Random.Range(SpawnInterval.x, SpawnInterval.y);
            }
        }

        /// <summary>
        /// Randomly selects the next tile (hazardous or not).
        /// </summary>
        /// <param name="hazardOnly">If it should only select between hazardous tiles.</param>
        /// <returns>The selected tile.</returns>
        private Tile GetNextTile(bool hazardOnly)
        {
            List<Tile> filteredTiles;

            if (hazardOnly)
            {
                filteredTiles = tiles.FindAll(tile => tile.IsHazard);
                if (filteredTiles.Count != 0) return filteredTiles[Random.Range(0, filteredTiles.Count)];
                Debug.LogWarning("TileSpawner: No hazard tiles available. Falling back to any tile.");
                filteredTiles = tiles;
            }
            else
            {
                filteredTiles = tiles.FindAll(tile => !tile.IsHazard);
                if (filteredTiles.Count != 0) return filteredTiles[Random.Range(0, filteredTiles.Count)];
                Debug.LogWarning("TileSpawner: No non-hazardous tiles available. Falling back to any tile.");
                filteredTiles = tiles;
            }

            return filteredTiles[Random.Range(0, filteredTiles.Count)];
        }

        /// <summary>
        /// Resets the current spawned tiles and prepares for the next play session.
        /// </summary>
        public void ResetTiles()
        {
            foreach (var tile in activeTiles)
            {
                Destroy(tile.gameObject);
            }

            activeTiles.Clear();
            nextSpawnXPos = 0f;
            totalTilesSpawned = 0;
            lastTileHadObstacle = false;
            nextHazardSpawnTime = Time.time + Random.Range(SpawnInterval.x, SpawnInterval.y);
        }
    }
}