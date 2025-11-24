using Depthcharge.Toolkit;
using UnityEngine;
using UnityEngine.Assertions;

namespace Depthcharge.Actors.AI
{
    public class EnemyBulletIdleState : BaseBulletIdleState
    {

        private StdFadeableAdapter fadeableAdapter = null;

        public override void SetUp(GameObject owner)
        {
            base.SetUp(owner);
            fadeableAdapter = bullet.GetComponent<StdFadeableAdapter>();
            string message = $"=== {bullet.name}.EnemyBulletIdleState.SetUp() === Bullet {bullet.name} doesn't had a \"StdFadeableAdapter\" component attached!";
            Assert.IsNotNull(fadeableAdapter, message);
            bullet.MovementContext.TargetToReach = FindFirstObjectByType<PlayerController>().transform;
        }

        public override void OnStateEnter()
        {
            if (fadeableAdapter.IsFadedIn) return;
            bullet.MovementModule.EnableModule();
            bullet.CollisionModule.EnableModule();
            bullet.MovementContext.SpawnTime = Time.time;
            bullet.MovementContext.StartPosition = bullet.MovementModule.Target.GetPosition();
            bullet.SpriteRenderer.color = Color.white;
            fadeableAdapter.ResetAlpha();
        }

    }
}