using UnityEngine;
using UnityEngine.Assertions;
using Depthcharge.Actors.Modules;

namespace Depthcharge.Actors.AI
{
    public class PlayerBulletIdleState : BaseState
    {

        private BulletController bullet = null;
        private const string SEALEVEL_TAG = "SeaLevel";

        [SerializeField]
        private float underwaterGravity = 1.0f;
        [SerializeField]
        private float underwaterDrag = 1.0f;

        public override void SetUp(GameObject owner)
        {
            bullet = owner.GetComponent<BulletController>();
            string message = $"=== {owner}.PlayerBulletIdleState.SetUp() === Owner \"{owner.name}\" doesn't had a \"BulletController\" component attached!";
            Assert.IsNotNull(bullet, message);
        }

        public override void OnStateEnter()
        {
            bullet.OnCollideWithEndOfMap += OnCollisionWithEndOfMap;
            bullet.AnimationModule.PlayAnimation(AnimationController.AnimationType.Idle);
        }
        public override void OnStateUpdate()
        {
            bullet.MovementModule.MoveTarget(bullet.MovementModule.Target.transform.up);
        }
        public override void OnStateExit()
        {
            bullet.MovementModule.Target.ResetGravity();
            bullet.MovementModule.Target.ResetDrag();
            bullet.OnCollideWithEndOfMap -= OnCollisionWithEndOfMap;
        }

        private void OnCollisionWithEndOfMap(string tag)
        {
            if (tag == SEALEVEL_TAG)
            {
                OnCollisionWithSea();
            }
            else
            {
                bullet.killOnCollisionWithEndOfMap = true;
            }
        }

        private void OnCollisionWithSea()
        {
            bullet.killOnCollisionWithEndOfMap = false;
            bullet.AnimationModule.PlayAnimation(Modules.AnimationController.AnimationType.UnderwaterIdle);
            bullet.MovementModule.Target.SetGravity(underwaterGravity);
            bullet.MovementModule.Target.SetDrag(underwaterDrag);
        }

    }
}