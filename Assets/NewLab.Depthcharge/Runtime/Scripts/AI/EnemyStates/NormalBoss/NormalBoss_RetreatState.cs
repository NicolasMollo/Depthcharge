using Depthcharge.Actors.Modules;
using UnityEngine;
using UnityEngine.Assertions;

namespace Depthcharge.Actors.AI
{
    public class NormalBoss_RetreatState : BaseState
    {

        private EC_NormalBoss _boss = null;
        private SpriteRenderer _sr = null;
        private MovementModule _movementModule = null;
        private HealthModule _healthModule = null;


        public override void SetUp(GameObject owner)
        {
            _boss = owner.GetComponent<EC_NormalBoss>();
            string message = $"=== NormalBoss.SetUp() === owner is not \"EC_NormalBoss\"!";
            Assert.IsNotNull(_boss, message);
            _sr = _boss.SpriteRenderer;
            _movementModule = _boss.MovementModule;
            _healthModule = _boss.HealthModule;
        }

        public override void OnStateEnter()
        {
            _sr.color = _boss.StartColor;
            _healthModule.SetVulnerability(true);
        }

        public override void OnStateUpdate()
        {
            _movementModule.MoveTarget(_boss.MovementDirection);
            if (Mathf.Abs(_movementModule.Target.GetPosition().x - _boss.StallPosition.x) > _boss.ChangeStateTreshold)
            {
                fsm.ChangeState<NormalBoss_AdvanceState>();
            }
        }

    }
}