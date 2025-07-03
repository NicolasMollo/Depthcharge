using UnityEngine;

namespace Depthcharge.Toolkit
{

    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteRendererColorSetter : MonoBehaviour
    {

        private SpriteRenderer spriteRenderer;

        private void Awake()
        {

            spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer == null)
            {
                Debug.LogError($"=== {this.name}.SpriteRendererColorSetter === SpriteRenderer is null!");
            }

        }

        #region API

        public void SetColor(Color color)
        {

            spriteRenderer.color = color;

        }
        public void SetColor(Vector4 rgbaColor)
        {

            spriteRenderer.color = rgbaColor;

        }
        public void SetColor(Vector3 rgbColor)
        {

            Color newColor = new Color(rgbColor.x, rgbColor.y, rgbColor.z, spriteRenderer.color.a);
            spriteRenderer.color = newColor;

        }

        #endregion

    }

}