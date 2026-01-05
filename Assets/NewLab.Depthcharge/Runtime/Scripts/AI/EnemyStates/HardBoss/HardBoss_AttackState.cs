using Depthcharge.Actors.Modules;
using UnityEngine;
using UnityEngine.Assertions;

namespace Depthcharge.Actors.AI
{
    public class HardBoss_AttackState : BaseState
    {

        private EC_HardBoss _boss = null;
        private HealthModule _healthModule = null;
        private float _shootCounter = 0f;


        public override void SetUp(GameObject owner)
        {
            _boss = owner.GetComponent<EC_HardBoss>();
            string message = $"=== HardBoss_AttackState.SetUp() === Owner is not a \"EC_HardBoss\"!";
            Assert.IsNotNull(_boss, message);
            _healthModule = _boss.HealthModule;
        }

        public override void OnStateEnter()
        {
            _boss.SetShootMode();
            _shootCounter = _boss.ShootDelay;
            _healthModule.SetVulnerability(false);
            _boss.SpriteRenderer.color = _boss.InvulnerabilityColor;
        }

        public override void OnStateUpdate()
        {
            _shootCounter -= Time.deltaTime;
            if (_shootCounter <= 0)
            {
                for (int i = 0; i < _boss.BulletsToShoot; i++)
                {
                    _boss.Shoot();
                }
                fsm.ChangeState<HardBoss_IdleState>();
            }
        }

    }

}