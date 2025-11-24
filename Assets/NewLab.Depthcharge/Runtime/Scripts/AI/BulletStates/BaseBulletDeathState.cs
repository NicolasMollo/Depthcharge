using UnityEngine;
using UnityEngine.Assertions;

namespace Depthcharge.Actors.AI
{
    public abstract class BaseBulletDeathState : BaseState
    {

        protected BulletController bullet = null;

        public override void SetUp(GameObject owner)
        {
            bullet = owner.GetComponent<BulletController>();
            string message = $"=== {owner}.BulletDeathState.SetUp() === Owner \"{owner.name}\" doesn't had a \"BulletController\" component attached!";
            Assert.IsNotNull(bullet, message);
        }

    }
}