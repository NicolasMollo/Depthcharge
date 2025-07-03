using UnityEngine;
using Depthcharge.Toolkit;

namespace Depthcharge.Environment
{

    [DisallowMultipleComponent]
    [RequireComponent(typeof(SpriteRendererColorSetter))]
    public class BackgroundController : MonoBehaviour
    {

        private SpriteRendererColorSetter colorSetter = null;

        [SerializeField]
        private SO_BackgroundColorConfiguration backgroundColorConfiguration = null;

        private void Awake()
        {

            colorSetter = GetComponent<SpriteRendererColorSetter>();
            if (colorSetter == null)
            {
                Debug.LogError($"=== {this.name}.BackgroundController === SpriteRendererColorSetter is null!");
            }

            if (backgroundColorConfiguration == null)
            {
                Debug.LogError($"=== {this.name}.BackgroundController === BackgroundColorConfiguration is null!");
            }

        }

        #region API

        public void SetMorningColor()
        {

            colorSetter.SetColor(backgroundColorConfiguration.MorningColor);

        }

        public void SetAfternoonColor()
        {

            colorSetter.SetColor(backgroundColorConfiguration.AfternoonColor);

        }

        public void SetEveningColor()
        {

            colorSetter.SetColor(backgroundColorConfiguration.EveningColor);

        }

        public void SetNightColor()
        {

            colorSetter.SetColor(backgroundColorConfiguration.NightColor);

        }

        #endregion

    }

}