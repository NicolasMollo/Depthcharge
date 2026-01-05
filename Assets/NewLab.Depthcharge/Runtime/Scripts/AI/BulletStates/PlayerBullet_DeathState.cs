using UnityEngine;
using UnityEngine.Assertions;

namespace Depthcharge.Actors.AI
{
    public class PlayerBullet_DeathState : BaseState
    {

        private BulletController _bullet = null;

        public override void SetUp(GameObject owner)
        {
            _bullet = owner.GetComponent<BulletController>();
            string message = $"=== {owner}.PlayerBulletDeathState.SetUp() === Owner \"{owner.name}\" doesn't had a \"BulletController\" component attached!";
            Assert.IsNotNull(_bullet, message);
        }

        public override void OnStateEnter()
        {
            _bullet.Deactivation();
        }

    }

}