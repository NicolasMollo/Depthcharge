using UnityEngine;
using UnityEngine.Assertions;

namespace Depthcharge.Actors.AI
{
    public class PlayerBulletIdleState : BaseState
    {

        private BulletController bullet = null;

        public override void SetUp(GameObject owner)
        {
            bullet = owner.GetComponent<BulletController>();
            string message = $"=== {owner}.PlayerBulletIdleState.SetUp() === Owner \"{owner.name}\" doesn't had a \"BulletController\" component attached!";
            Assert.IsNotNull(bullet, message);
        }

        public override void OnStateUpdate()
        {
            bullet.MovementModule.MoveTarget(bullet.MovementModule.Target.transform.up);
        }

    }
}