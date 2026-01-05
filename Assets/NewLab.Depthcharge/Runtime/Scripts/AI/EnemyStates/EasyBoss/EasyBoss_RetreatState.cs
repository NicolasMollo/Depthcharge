using Depthcharge.Actors.Modules;
using UnityEngine;
using UnityEngine.Assertions;

namespace Depthcharge.Actors.AI
{
    public class EasyBoss_RetreatState : BaseState
    {

        private EC_EasyBoss _boss = null;
        private SpriteRenderer _sr = null;
        private MovementModule _movementModule = null;
        private HealthModule _healthModule = null;


        public override void SetUp(GameObject owner)
        {
            _boss = owner.GetComponent<EC_EasyBoss>();
            string message = $"=== EasyBoss_AttackState.SetUp() === owner is not a \"EC_EasyBoss\"!";
            Assert.IsNotNull(_boss, message);
            _sr = _boss.SpriteRenderer;
            _movementModule = _boss.MovementModule;
            _healthModule = _boss.HealthModule;
        }

        public override void OnStateEnter()
        {
            _boss.MoveToBottomSea();
            _sr.color = _boss.StartColor;
            _healthModule.SetVulnerability(true);
            _movementModule.SetMovementSpeed(_boss.RetreatSpeed);
            AddListeners();
        }
        public override void OnStateUpdate()
        {
            _movementModule.MoveTarget(_boss.MovementDirection);
        }
        public override void OnStateExit()
        {
            RemoveListeners();
        }

        private void AddListeners()
        {
            _boss.OnCollideWithEndOfMap += OnCollideWithEndOfMap;
        }
        private void RemoveListeners()
        {
            _boss.OnCollideWithEndOfMap -= OnCollideWithEndOfMap;
        }

        private void OnCollideWithEndOfMap()
        {
            fsm.ChangeState<EasyBoss_AttackState>();
        }

    }

}