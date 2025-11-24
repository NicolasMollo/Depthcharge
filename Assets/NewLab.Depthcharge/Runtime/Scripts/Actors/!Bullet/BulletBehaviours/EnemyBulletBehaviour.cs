using Depthcharge.Actors.Modules;
using Depthcharge.Toolkit;
using System.Collections;
using UnityEngine;

namespace Depthcharge.Actors
{

    [RequireComponent(typeof(StdFadeableAdapter))]
    internal class EnemyBulletBehaviour : BaseBulletBehaviour
    {

        private StdFadeableAdapter fadeableAdapter = null;
        [SerializeField]
        private float fadeOutDelay = 0.005f;

        private void Awake()
        {
            fadeableAdapter = GetComponent<StdFadeableAdapter>();
        }

        public override void OnBulletStart()
        {
            AddListeners();
        }
        public override void OnBulletDestroy()
        {
            RemoveListeners();
        }
        public override void OnBulletEnable()
        {
            if (owner.SpriteRenderer.color == Color.white) return;
            owner.MovementModule.EnableModule();
            owner.CollisionModule.EnableModule();
            owner.MovementContext.SpawnTime = Time.time;
            owner.MovementContext.StartPosition = owner.MovementModule.Target.GetPosition();
            owner.SpriteRenderer.color = Color.white;
            fadeableAdapter.ResetAlpha();
        }
        public override void OnBulletSetUp()
        {
            owner.MovementContext.TargetToReach = FindFirstObjectByType<PlayerController>().transform;
        }

        public override void OnBulletDeath(string endOfMapTag)
        {
            if (string.IsNullOrEmpty(endOfMapTag))
            {
                owner.Deactivation();
                return;
            }
            owner.SpriteRenderer.color = Color.black;
            owner.MovementModule.Target.SetRotation(0);
            owner.MovementModule.DisableModule();
            owner.CollisionModule.DisableModule();
            owner.AnimationModule.PlayAnimation(AnimationController.AnimationType.Death);
        }


        private void AddListeners()
        {
            owner.AnimationModule.SubscribeToOnAnimationEnd(AnimationController.AnimationType.Death, OnDeathAnimationEnd);
        }
        private void RemoveListeners()
        {
            owner.AnimationModule.UnsubscribeFromOnAnimationEnd(AnimationController.AnimationType.Death, OnDeathAnimationEnd);
        }

        private void OnDeathAnimationEnd()
        {
            StartCoroutine(FadeOutBullet());
        }
        private IEnumerator FadeOutBullet()
        {
            fadeableAdapter.FadeOut(fadeOutDelay);
            yield return new WaitUntil(() => fadeableAdapter.IsFadedIn == false);
            owner.AnimationModule.PlayAnimation(AnimationController.AnimationType.Idle);
            owner.Deactivation();
        }

    }

}