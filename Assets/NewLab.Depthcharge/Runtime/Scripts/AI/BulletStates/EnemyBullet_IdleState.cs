using Depthcharge.Toolkit;
using UnityEngine;
using UnityEngine.Assertions;

namespace Depthcharge.Actors.AI
{
    public class EnemyBullet_IdleState : BaseState
    {

        private BulletController _bullet = null;
        private StdFadeableAdapter _fadeableAdapter = null;
        private MovementContext _movementContext = default;

        public override void SetUp(GameObject owner)
        {
            _bullet = owner.GetComponent<BulletController>();
            string message = $"=== {owner}.EnemyBulletIdleState.SetUp() === Owner \"{owner.name}\" doesn't had a \"BulletController\" component attached!";
            Assert.IsNotNull(_bullet, message);
            _fadeableAdapter = _bullet.GetComponent<StdFadeableAdapter>();
            message = $"=== {_bullet.name}.EnemyBulletIdleState.SetUp() === Bullet {_bullet.name} doesn't had a \"StdFadeableAdapter\" component attached!";
            Assert.IsNotNull(_fadeableAdapter, message);
            _movementContext = new MovementContext();
            _movementContext.TargetToReach = FindFirstObjectByType<PlayerController>().transform;
        }

        public override void OnStateEnter()
        {
            if (_fadeableAdapter.IsFadedIn) return;
            _bullet.MovementModule.EnableModule();
            _bullet.CollisionModule.EnableModule();
            _movementContext.SpawnTime = Time.time;
            _movementContext.StartPosition = _bullet.MovementModule.Target.GetPosition();
            _bullet.SpriteRenderer.color = Color.white;
            _fadeableAdapter.ResetAlpha();
        }

        public override void OnStateUpdate()
        {
            _bullet.MovementModule.MoveTarget(_bullet.MovementModule.Target.transform.up, _movementContext.TargetToReach);
        }

    }
}