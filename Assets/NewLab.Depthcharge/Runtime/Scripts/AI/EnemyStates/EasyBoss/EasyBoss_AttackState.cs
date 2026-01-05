using Depthcharge.Actors.Modules;
using UnityEngine;
using UnityEngine.Assertions;

namespace Depthcharge.Actors.AI
{
    public class EasyBoss_AttackState : BaseState
    {

        private EC_EasyBoss _boss = null;
        private SpriteRenderer _sr = null;
        private MovementModule _movementModule = null;
        private HealthModule _healthModule = null;
        private ShootModule _shootModule = null;
        private float _shootCounter = 0f;
        private float _startSpeed = 0f;


        public override void SetUp(GameObject owner)
        {
            _boss = owner.GetComponent<EC_EasyBoss>();
            string message = $"=== EasyBoss_AttackState.SetUp() === owner is not a \"EC_EasyBoss\"!";
            Assert.IsNotNull(_boss, message);
            _sr = _boss.SpriteRenderer;
            _movementModule = _boss.MovementModule;
            _startSpeed = _movementModule.MovementSpeed;
            _healthModule = _boss.HealthModule;
            _shootModule = _boss.ShootModule;
        }

        public override void OnStateEnter()
        {
            _boss.MoveToTopSea();
            _shootModule.Reload();
            _movementModule.SetMovementSpeed(_startSpeed);
            _shootCounter = _boss.ShootDelay;
            _sr.color = _boss.InvulnerabilityColor;
            _healthModule.SetVulnerability(false);
            AddListeners();
        }
        public override void OnStateUpdate()
        {
            _movementModule.MoveTarget(_boss.MovementDirection);
            _shootCounter -= Time.deltaTime;
            if (_shootCounter <= 0f)
            {
                _boss.Shoot();
                _shootCounter = _boss.ShootDelay;
            }
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
            fsm.ChangeState<EasyBoss_RetreatState>();
        }

    }
}