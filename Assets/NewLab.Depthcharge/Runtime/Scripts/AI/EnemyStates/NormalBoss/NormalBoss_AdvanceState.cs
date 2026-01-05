using Depthcharge.Actors.Modules;
using UnityEngine;
using UnityEngine.Assertions;

namespace Depthcharge.Actors.AI
{

    public class NormalBoss_AdvanceState : BaseState
    {

        private EC_NormalBoss _boss = null;
        private MovementModule _movementModule = null;


        public override void SetUp(GameObject owner)
        {
            _boss = owner.GetComponent<EC_NormalBoss>();
            string message = $"=== NormalBoss.SetUp() === owner is not \"EC_NormalBoss\"!";
            Assert.IsNotNull(_boss, message);
            _movementModule = _boss.MovementModule;
        }

        public override void OnStateUpdate()
        {
            _movementModule.MoveTarget(_boss.MovementDirection);
            if (Mathf.Abs(_movementModule.Target.GetPosition().x - _boss.StallPosition.x) <= _boss.ChangeStateTreshold)
            {
                fsm.ChangeState<NormalBoss_AttackState>();
            }
        }

    }

}