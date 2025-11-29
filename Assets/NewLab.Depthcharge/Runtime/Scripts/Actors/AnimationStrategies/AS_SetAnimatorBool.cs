using UnityEngine;
using UnityEngine.Assertions;

namespace Depthcharge.Actors.Modules
{

    [CreateAssetMenu(menuName = "Scriptable Objects/Actors/AnimationStrategies/AS_SetAnimatorBool")]
    public class AS_SetAnimatorBool : BaseAnimationStrategy
    {

        [SerializeField]
        private bool parameterValue = false;

        public override void Animate(Actor owner, Animator animator = null, AnimationController animation = null)
        {
            string message = $"=== AS_SetAnimatorBool.Animate() === Be ensure to pass \"Animator\" parameter to this method!";
            Assert.IsNotNull(animator, message);
            message = $"=== AS_SetAnimatorBool.Animate() === Be ensure to pass \"AnimationController\" parameter to this method!";
            Assert.IsNotNull(animation, message);

            animator.SetBool(animation.AnimatorParameterID, parameterValue);
        }

    }

}