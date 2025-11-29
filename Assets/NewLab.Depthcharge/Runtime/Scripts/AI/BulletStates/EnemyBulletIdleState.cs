using Depthcharge.Toolkit;
using UnityEngine;
using UnityEngine.Assertions;

namespace Depthcharge.Actors.AI
{
    public class EnemyBulletIdleState : BaseState
    {

        private BulletController bullet = null;
        private StdFadeableAdapter fadeableAdapter = null;
        private MovementContext movementContext = default;

        public override void SetUp(GameObject owner)
        {
            bullet = owner.GetComponent<BulletController>();
            string message = $"=== {owner}.EnemyBulletIdleState.SetUp() === Owner \"{owner.name}\" doesn't had a \"BulletController\" component attached!";
            Assert.IsNotNull(bullet, message);
            fadeableAdapter = bullet.GetComponent<StdFadeableAdapter>();
            message = $"=== {bullet.name}.EnemyBulletIdleState.SetUp() === Bullet {bullet.name} doesn't had a \"StdFadeableAdapter\" component attached!";
            Assert.IsNotNull(fadeableAdapter, message);
            movementContext = new MovementContext();
            movementContext.TargetToReach = FindFirstObjectByType<PlayerController>().transform;
        }

        public override void OnStateEnter()
        {
            if (fadeableAdapter.IsFadedIn) return;
            bullet.MovementModule.EnableModule();
            bullet.CollisionModule.EnableModule();
            movementContext.SpawnTime = Time.time;
            movementContext.StartPosition = bullet.MovementModule.Target.GetPosition();
            bullet.SpriteRenderer.color = Color.white;
            fadeableAdapter.ResetAlpha();
        }

        public override void OnStateUpdate()
        {
            bullet.MovementModule.MoveTarget(bullet.MovementModule.Target.transform.up, movementContext.TargetToReach);
        }

    }
}