using Depthcharge.Actors.Modules;
using Depthcharge.Environment;
using UnityEngine;
using UnityEngine.Assertions;

namespace Depthcharge.Actors.AI
{

    [DisallowMultipleComponent]
    public class HardBoss_AdvanceState : BaseState
    {

        private EC_HardBoss _boss = null;
        private MovementModule _movementModule = null;
        private Vector2 _seaPosition = Vector2.zero;


        public override void SetUp(GameObject owner)
        {
            _boss = owner.GetComponent<EC_HardBoss>();
            string message = $"=== HardBoss_PatrolState.SetUp() === Owner is not a \"EC_HardBoss\"!";
            Assert.IsNotNull(_boss, message);
            _movementModule = _boss.MovementModule;
            _seaPosition = EnvironmentRootController.Instance.SeaPosition;
        }

        public override void OnStateEnter()
        {
            _boss.ShootCount = 0;
        }

        public override void OnStateUpdate()
        {
            _movementModule.MoveTarget(_boss.MovementDirection);
            if (Mathf.Abs(_seaPosition.x - _movementModule.Target.GetPosition().x) <= _boss.ChangeStateTreshold)
            {
                fsm.ChangeState<HardBoss_AttackState>();
            }
        }

    }

}