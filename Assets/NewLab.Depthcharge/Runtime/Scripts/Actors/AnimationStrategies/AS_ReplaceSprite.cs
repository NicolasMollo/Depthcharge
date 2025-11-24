using UnityEngine;
using UnityEngine.Assertions;

namespace Depthcharge.Actors.Modules
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Actors/AnimationStrategies/AS_ReplaceSprite")]
    public class AS_ReplaceSprite : BaseAnimationStrategy
    {

        [SerializeField]
        [Tooltip("Sprite that will be replace the current sprite of SpriteRenderer")]
        private Sprite sprite = null;

        public override void Animate(GameObject owner, Animator animator = null, AnimationController animation = null)
        {
            SpriteRenderer spriteRenderer = owner.GetComponentInChildren<SpriteRenderer>();
            string message = $"=== AS_ReplaceSprite.Animate() === Owner doesn't have a \"SpriteRenderer\" component!";
            Assert.IsNotNull(spriteRenderer, message);

            spriteRenderer.sprite = sprite;
        }

    }
}