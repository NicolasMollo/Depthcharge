using UnityEngine;
using UnityEngine.Assertions;

namespace Depthcharge.Actors.AI
{
    public class PlayerBulletDeathState : BaseState
    {

        private BulletController bullet = null;

        public override void SetUp(GameObject owner)
        {
            bullet = owner.GetComponent<BulletController>();
            string message = $"=== {owner}.PlayerBulletDeathState.SetUp() === Owner \"{owner.name}\" doesn't had a \"BulletController\" component attached!";
            Assert.IsNotNull(bullet, message);
        }

        public override void OnStateEnter()
        {
            bullet.Deactivation();
        }

    }

}