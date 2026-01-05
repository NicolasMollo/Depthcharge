using Depthcharge.Actors.Modules;
using Depthcharge.Audio;
using Depthcharge.Toolkit;
using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

namespace Depthcharge.Actors.AI
{
    public class EnemyBullet_DeathState : BaseState
    {

        private BulletController _bullet = null;
        private StdFadeableAdapter _fadeableAdapter = null;
        private int _deathLayerOrder = 1;
        private int _bulletLayerOrder = 0;

        [SerializeField]
        private float _fadeOutDelay = 0.005f;


        public override void SetUp(GameObject owner)
        {
            _bullet = owner.GetComponent<BulletController>();
            string message = $"=== {owner}.EnemyBulletDeathState.SetUp() === Owner \"{owner.name}\" doesn't had a \"BulletController\" component attached!";
            Assert.IsNotNull(_bullet, message);
            _fadeableAdapter = _bullet.GetComponent<StdFadeableAdapter>();
            message = $"=== {_bullet.name}.EnemyBulletDeathState.SetUp() === Bullet {_bullet.name} doesn't had a \"StdFadeableAdapter\" component attached!";
            Assert.IsNotNull(_fadeableAdapter, message);
            _bulletLayerOrder = _bullet.SpriteRenderer.sortingOrder;
        }

        public override void OnStateEnter()
        {
            if (!_bullet.killedByEndOfMap)
            {
                _bullet.Deactivation();
                return;
            }
            AddListeners();
            _bullet.SpriteRenderer.sortingOrder = _deathLayerOrder;
            _bullet.AudioSource.PlayOneShot(AudioClipType.Death);
            _bullet.SpriteRenderer.color = Color.black;
            _bullet.MovementModule.Target.SetRotation(0);
            _bullet.MovementModule.DisableModule();
            _bullet.CollisionModule.DisableModule();
            _bullet.AnimationModule.PlayAnimation(AnimationController.AnimationType.Death);
            _bullet.killedByEndOfMap = false;
        }
        public override void OnStateExit()
        {
            RemoveListeners();
            _bullet.SpriteRenderer.sortingOrder = _bulletLayerOrder;
        }

        private void AddListeners()
        {
            _bullet.AnimationModule.SubscribeToOnAnimationEnd(AnimationController.AnimationType.Death, OnDeathAnimationEnd);
        }
        private void RemoveListeners()
        {
            _bullet.AnimationModule.UnsubscribeFromOnAnimationEnd(AnimationController.AnimationType.Death, OnDeathAnimationEnd);
        }

        private void OnDeathAnimationEnd()
        {
            StartCoroutine(FadeOutBullet());
        }
        private IEnumerator FadeOutBullet()
        {
            _fadeableAdapter.FadeOut(_fadeOutDelay);
            yield return new WaitUntil(() => _fadeableAdapter.IsFadedIn == false);
            _bullet.AnimationModule.PlayAnimation(AnimationController.AnimationType.Idle);
            _bullet.Deactivation();
        }

    }
}