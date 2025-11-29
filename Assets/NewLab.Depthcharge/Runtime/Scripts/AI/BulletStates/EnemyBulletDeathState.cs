using Depthcharge.Actors.Modules;
using Depthcharge.Toolkit;
using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

namespace Depthcharge.Actors.AI
{
    public class EnemyBulletDeathState : BaseState
    {

        private BulletController bullet = null;
        private StdFadeableAdapter fadeableAdapter = null;
        private const string ENDOFMAP_LAYERNAME = "Default";
        [SerializeField]
        private float fadeOutDelay = 0.005f;

        public override void SetUp(GameObject owner)
        {
            bullet = owner.GetComponent<BulletController>();
            string message = $"=== {owner}.EnemyBulletDeathState.SetUp() === Owner \"{owner.name}\" doesn't had a \"BulletController\" component attached!";
            Assert.IsNotNull(bullet, message);
            fadeableAdapter = bullet.GetComponent<StdFadeableAdapter>();
            message = $"=== {bullet.name}.EnemyBulletDeathState.SetUp() === Bullet {bullet.name} doesn't had a \"StdFadeableAdapter\" component attached!";
            Assert.IsNotNull(fadeableAdapter, message);
        }

        public override void OnStateEnter()
        {
            if (bullet.CollisionModule.LastCollisionLayer != LayerMask.NameToLayer(ENDOFMAP_LAYERNAME))
            {
                bullet.Deactivation();
                return;
            }
            AddListeners();
            bullet.SpriteRenderer.color = Color.black;
            bullet.MovementModule.Target.SetRotation(0);
            bullet.MovementModule.DisableModule();
            bullet.CollisionModule.DisableModule();
            bullet.AnimationModule.PlayAnimation(AnimationController.AnimationType.Death);
        }
        public override void OnStateExit()
        {
            RemoveListeners();
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