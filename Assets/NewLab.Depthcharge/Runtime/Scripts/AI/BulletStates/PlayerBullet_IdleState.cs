using UnityEngine;
using UnityEngine.Assertions;
using Depthcharge.Actors.Modules;

namespace Depthcharge.Actors.AI
{
    public class PlayerBullet_IdleState : BaseState
    {

        private BulletController _bullet = null;
        private const string SEALEVEL_TAG = "SeaLevel";

        [SerializeField]
        private float _underwaterGravity = 1.0f;
        [SerializeField]
        private float _underwaterDrag = 3.0f;


        public override void SetUp(GameObject owner)
        {
            _bullet = owner.GetComponent<BulletController>();
            string message = $"=== {owner}.PlayerBulletIdleState.SetUp() === Owner \"{owner.name}\" doesn't had a \"BulletController\" component attached!";
            Assert.IsNotNull(_bullet, message);
        }

        public override void OnStateEnter()
        {
            _bullet.OnCollideWithEndOfMap += OnCollisionWithEndOfMap;
            _bullet.AnimationModule.PlayAnimation(AnimationController.AnimationType.Idle);
        }
        public override void OnStateUpdate()
        {
            _bullet.MovementModule.MoveTarget(_bullet.MovementModule.Target.transform.up);
        }
        public override void OnStateExit()
        {
            _bullet.MovementModule.Target.ResetGravity();
            _bullet.MovementModule.Target.ResetDrag();
            _bullet.OnCollideWithEndOfMap -= OnCollisionWithEndOfMap;
        }

        private void OnCollisionWithEndOfMap(string tag)
        {
            if (tag == SEALEVEL_TAG)
            {
                OnCollisionWithSea();
            }
            else
            {
                _bullet.killOnCollisionWithEndOfMap = true;
            }
        }
        private void OnCollisionWithSea()
        {
            _bullet.killOnCollisionWithEndOfMap = false;
            _bullet.AnimationModule.PlayAnimation(AnimationController.AnimationType.UnderwaterIdle);
            _bullet.MovementModule.Target.SetGravity(_underwaterGravity);
            _bullet.MovementModule.Target.SetDrag(_underwaterDrag);
        }

    }
}