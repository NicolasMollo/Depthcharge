using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Depthcharge.Actors.Modules
{

    [DisallowMultipleComponent]
    public class AnimationModule : BaseModule
    {

        [SerializeField]
        private Animator animator = null;
        [SerializeField]
        private List<AnimationController> animations = null;

        private void Awake()
        {
            string message = $"=== {this.name}.AnimationModule.Awake() === Be ensure to fill \"Owner\" field in Inspector!";
            Assert.IsNotNull(owner, message);
            message = $"=== {owner.name}.AnimationModule.Awake() === Be ensure to insert \"AnimationController\" elements in \"Animations\" list in Inspector!";
            Assert.IsTrue(animations.Count > 0);
        }

        public void PlayAnimation(AnimationController.AnimationType type)
        {
            AnimationController animation = GetAnimationByType(type);
            if (!animation.gameObject.activeSelf) return;
            animation.Animate(owner, animator);
        }

        #region Animations events

        public void SubscribeToOnAnimationStart(AnimationController.AnimationType type, Action method)
        {
            AnimationController animation = GetAnimationByType(type);
            animation.OnAnimationStart += method;
        }
        public void UnsubscribeFromOnAnimationStart(AnimationController.AnimationType type, Action method)
        {
            AnimationController animation = GetAnimationByType(type);
            animation.OnAnimationStart -= method;
        }

        public void SubscribeToOnAnimationEnd(AnimationController.AnimationType type, Action method)
        {
            AnimationController animation = GetAnimationByType(type);
            animation.OnAnimationEnd += method;
        }
        public void UnsubscribeFromOnAnimationEnd(AnimationController.AnimationType type, Action method)
        {
            AnimationController animation = GetAnimationByType(type);
            animation.OnAnimationEnd -= method;
        }

        #endregion

        private AnimationController GetAnimationByType(AnimationController.AnimationType type)
        {
            AnimationController animationController = null;
            foreach (AnimationController controller in animations)
            {
                if (controller.Type == type)
                {
                    animationController = controller;
                    break;
                }
            }
            string message = $"=== AnimationModule.GetAnimationByName() === Animation with \"{type.ToString()}\" type doesn't exist in this animation module";
            Assert.IsNotNull(animationController, message);
            return animationController;
        }

    }

}