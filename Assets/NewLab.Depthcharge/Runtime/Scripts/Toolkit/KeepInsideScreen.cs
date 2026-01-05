using UnityEngine;

namespace Depthcharge.Toolkit
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class KeepInsideScreen : MonoBehaviour
    {

        private BoxCollider2D boxCollider = null;
        private float startWindowX = 0f;
        private float endWindowX = 0f;
        private float halfSizeX = 0f;


        private void Awake()
        {
            boxCollider = GetComponent<BoxCollider2D>();
        }

        private void Start()
        {
            halfSizeX = boxCollider.bounds.size.x * 0.5f;
        }
        private void Update()
        {
            SetScreenParams();
            float minPosOffsetX = transform.position.x - halfSizeX;
            float maxPosOffsetX = transform.position.x + halfSizeX;
            if (minPosOffsetX <= startWindowX)
            {
                transform.position = new Vector3(startWindowX + halfSizeX, transform.position.y);
            }
            else if (maxPosOffsetX >= endWindowX)
            {
                transform.position = new Vector3(endWindowX - halfSizeX, transform.position.y);
            }
        }

        private void SetScreenParams()
        {
            Vector2 screenCenter = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f);
            Vector2 worldCenter = Camera.main.ScreenToWorldPoint(screenCenter);
            float cameraHeight = Camera.main.orthographicSize * 2.0f;
            float screenWidth = cameraHeight * Camera.main.aspect;
            float halfScreenWidth = screenWidth * 0.5f;

            startWindowX = worldCenter.x - halfScreenWidth;
            endWindowX = worldCenter.x + halfScreenWidth;
        }

    }
}