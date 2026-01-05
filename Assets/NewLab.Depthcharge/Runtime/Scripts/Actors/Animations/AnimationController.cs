using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

namespace Depthcharge.Actors.Modules
{

    public class AnimationController : MonoBehaviour
    {

        public enum AnimationType : byte { Idle, Death, Damage, UnderwaterIdle }

        private int animatorStateID = 0;

        [SerializeField]
        private string animatorParameterName = string.Empty;
        [SerializeField]
        private string animatorStateName = string.Empty;
        [SerializeField]
        private AnimationType _type = AnimationType.Idle;
        [SerializeField]
        private BaseAnimationStrategy strategy = null;
        [SerializeField]
        private bool isDummyAnimation = false;

        public AnimationType Type { get => _type; }
        public int AnimatorParameterID { get; private set; }
        public Action OnAnimationStart = null;
        public Action OnAnimationEnd = null;


        private void Awake()
        {
            string message = $"=== AnimationController.Awake() === Be ensure to correctly fill \"Strategy\" in Inspector!";
            Assert.IsNotNull(strategy, message);
            AnimatorParameterID = Animator.StringToHash(animatorParameterName);
            animatorStateID = Animator.StringToHash(animatorStateName);
        }

        public void Animate(Actor owner, Animator animator = null)
        {
            if (isDummyAnimation)
            {
                AnimateWithoutAnimator(owner);
            }
            else
            {
                AnimateWithAnimator(owner, animator);
            }
        }

        private void AnimateWithAnimator(Actor owner, Animator animator)
        {
            strategy.Animate(owner, animator, this);
            StartCoroutine(CheckAnimationState(animator));
        }
        private void AnimateWithoutAnimator(Actor owner)
        {
            strategy.Animate(owner, null, this);
        }

        private IEnumerator CheckAnimationState(Animator animator)
        {
            while (animator.GetCurrentAnimatorStateInfo(0).shortNameHash != animatorStateID) yield return null;
            OnAnimationStart?.Invoke();
            while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f) yield return null;
            OnAnimationEnd?.Invoke();
        }

    }

}