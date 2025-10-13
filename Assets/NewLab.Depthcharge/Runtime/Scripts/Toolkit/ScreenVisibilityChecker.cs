using Codice.CM.Client.Differences;
using System;
using UnityEngine;

namespace Depthcharge.Toolkit
{
    public class ScreenVisibilityChecker : MonoBehaviour
    {

        private float startWindowX = 0.0f;
        private float endWindowX = 0.0f;
        private float halfSpriteSize = 1.0f;

        [SerializeField]
        private SpriteRenderer spriteRenderer = null;
        [SerializeField]
        [Range(0.0f, 10.0f)]
        private float spriteOffsetX = 1.0f;
        public bool IsVisible { get; private set; }
        public Action OnBecameVisible = null;
        public Action<MovementDirection> OnBecameInvisible = null;


        private void Awake()
        {
            float cameraHeight = Camera.main.orthographicSize * 2.0f;
            float screenWidth = cameraHeight * Camera.main.aspect;
            float halfScreenWidth = screenWidth * 0.5f;
            startWindowX = Screen.mainWindowPosition.x - halfScreenWidth;
            endWindowX = Screen.mainWindowPosition.x + halfScreenWidth;
            halfSpriteSize = (spriteRenderer.sprite.bounds.size.x * 0.5f) - spriteOffsetX;
        }

        private void Update()
        {
            CheckVisibility();
        }
        private void CheckVisibility()
        {
            float leftOffset = transform.position.x + halfSpriteSize;
            float rightOffset = transform.position.x - halfSpriteSize;
            if (leftOffset < startWindowX)
            {
                IsVisible = false;
                OnBecameInvisible?.Invoke(MovementDirection.Left);
            }
            else if (rightOffset > endWindowX)
            {
                IsVisible = false;
                OnBecameInvisible?.Invoke(MovementDirection.Right);
            }
            else
            {
                IsVisible = true;
                OnBecameVisible?.Invoke();
            }
        }

    }
}