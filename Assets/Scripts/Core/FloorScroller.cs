using Core.Managers;
using UnityEngine;

namespace Core
{
    /// <summary>
    /// Scrolls the scenario elements (tiles and obstacles) to simulate player movement.
    /// </summary>
    [RequireComponent(typeof(RectTransform))]
    public class FloorScroller : MonoBehaviour
    {
        private RectTransform groundRoot;
        private Vector2 startPosition;
        private bool isScrolling;

        private float ScrollSpeed => GameManager.Instance.GameConfig.scenarioSpeed;

        private void Awake()
        {
            groundRoot = GetComponent<RectTransform>();
            startPosition = groundRoot.anchoredPosition;
        }

        private void Update()
        {
            if (!isScrolling) return;
            groundRoot.anchoredPosition -= new Vector2(ScrollSpeed * Time.deltaTime, 0);
        }

        public void StartScrolling()
        {
            isScrolling = true;
        }

        public void StopScrolling()
        {
            isScrolling = false;
            groundRoot.anchoredPosition = startPosition;
        }
    }
}