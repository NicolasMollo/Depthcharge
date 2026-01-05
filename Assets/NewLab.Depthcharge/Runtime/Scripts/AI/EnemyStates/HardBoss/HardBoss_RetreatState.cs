using Depthcharge.Actors.Modules;
using Depthcharge.Environment;
using UnityEngine;
using UnityEngine.Assertions;

namespace Depthcharge.Actors.AI
{
    public class HardBoss_RetreatState : BaseState
    {

        private EC_HardBoss _boss = null;
        private MovementModule _movementModule;
        private Vector2 _seaPosition;
        private HealthModule _healthModule;


        public override void SetUp(GameObject owner)
        {
            _boss = owner.GetComponent<EC_HardBoss>();
            string message = $"=== HardBoss_PatrolState.SetUp() === Owner is not a \"EC_HardBoss\"!";
            Assert.IsNotNull(_boss, message);
            _movementModule = _boss.MovementModule;
            _healthModule = _boss.HealthModule;
            _seaPosition = EnvironmentRootController.Instance.SeaPosition;
        }

        public override void OnStateEnter()
        {
            _healthModule.SetVulnerability(true);
            _boss.SpriteRenderer.color = _boss.StartColor;
        }

        public override void OnStateUpdate()
        {
            _movementModule.MoveTarget(_boss.MovementDirection);
            if (Mathf.Abs(_movementModule.Target.GetPosition().x - _seaPosition.x) > _boss.ChangeStateTreshold)
            {
                fsm.ChangeState<HardBoss_AdvanceState>();
            }
        }

    }
}