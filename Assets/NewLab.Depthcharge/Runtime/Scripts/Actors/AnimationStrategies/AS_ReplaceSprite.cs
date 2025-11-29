using UnityEngine;

namespace Depthcharge.Actors.Modules
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Actors/AnimationStrategies/AS_ReplaceSprite")]
    public class AS_ReplaceSprite : BaseAnimationStrategy
    {

        [SerializeField]
        [Tooltip("Sprite that will be replace the current sprite of SpriteRenderer")]
        private Sprite sprite = null;

        public override void Animate(Actor owner, Animator animator = null, AnimationController animation = null)
        {
            owner.SpriteRenderer.sprite = sprite;
        }

    }
}