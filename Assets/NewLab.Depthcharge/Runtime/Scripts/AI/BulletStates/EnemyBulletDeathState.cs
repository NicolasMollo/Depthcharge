using Depthcharge.Actors.Modules;
using Depthcharge.Toolkit;
using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

namespace Depthcharge.Actors.AI
{
    public class EnemyBulletDeathState : BaseBulletDeathState
    {

        private StdFadeableAdapter fadeableAdapter = null;
        [SerializeField]
        private float fadeOutDelay = 0.005f;

        public override void SetUp(GameObject owner)
        {
            base.SetUp(owner);
            fadeableAdapter = bullet.GetComponent<StdFadeableAdapter>();
            string message = $"=== {bullet.name}.EnemyBulletDeathState.SetUp() === Bullet {bullet.name} doesn't had a \"StdFadeableAdapter\" component attached!";
            Assert.IsNotNull(fadeableAdapter, message);
            AddListeners();
        }

        public override void CleanUp(GameObject owner)
        {
            RemoveListeners();
        }

        public override void OnStateEnter()
        {
            if (bullet.CollisionModule.LastCollisionLayer == LayerMask.NameToLayer("Default")) return;
            bullet.SpriteRenderer.color = Color.black;
            bullet.MovementModule.Target.SetRotation(0);
            bullet.MovementModule.DisableModule();
            bullet.CollisionModule.DisableModule();
            bullet.AnimationModule.PlayAnimation(AnimationController.AnimationType.Death);
        }

        private void AddListeners()
        {
            bullet.AnimationModule.SubscribeToOnAnimationEnd(AnimationController.AnimationType.Death, OnDeathAnimationEnd);
        }
        private void RemoveListeners()
        {
            bullet.AnimationModule.UnsubscribeFromOnAnimationEnd(AnimationController.AnimationType.Death, OnDeathAnimationEnd);
        }

        private void OnDeathAnimationEnd()
        {
            StartCoroutine(FadeOutBullet());
        }
        private IEnumerator FadeOutBullet()
        {
            fadeableAdapter.FadeOut(fadeOutDelay);
            yield return new WaitUntil(() => fadeableAdapter.IsFadedIn == false);
            bullet.AnimationModule.PlayAnimation(AnimationController.AnimationType.Idle);
            bullet.Deactivation();
        }

    }
}