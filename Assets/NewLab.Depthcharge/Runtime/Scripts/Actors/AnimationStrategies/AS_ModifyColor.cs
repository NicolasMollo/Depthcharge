using System.Collections;
using UnityEngine;

namespace Depthcharge.Actors.Modules
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Actors/AnimationStrategies/AS_ModifyColor")]
    public class AS_ModifyColor : BaseAnimationStrategy
    {

        [SerializeField]
        private Color color = default;
        [SerializeField]
        [Tooltip("Time when color remain setted")]
        private float delay = 0.0f;

        public override void Animate(Actor owner, Animator animator = null, AnimationController animation = null)
        {
            owner.StartCoroutine(SetColor(owner.SpriteRenderer, owner.StartColor));
        }

        private IEnumerator SetColor(SpriteRenderer sr, Color startColor)
        {
            sr.color = color;
            yield return new WaitForSeconds(delay);
            if (sr.color == color)
            {
                sr.color = startColor;
            }
        }

    }
}