using Depthcharge.Actors.Modules;
using UnityEngine;
using UnityEngine.Assertions;

namespace Depthcharge.Actors.AI
{
    public class NormalBoss_AttackState : BaseState
    {

        private EC_NormalBoss _boss = null;
        private SpriteRenderer _sr = null;
        private HealthModule _healthModule = null;
        private ShootModule _shootModule = null;
        private float _shootCounter = 0f;
        private int _bulletToShoot = 0;
        private int _bulletShooted = 0;


        public override void SetUp(GameObject owner)
        {
            _boss = owner.GetComponent<EC_NormalBoss>();
            string message = $"=== NormalBoss_AttackState.SetUp() === owner is not a \"EC_NormalBoss\"!";
            Assert.IsNotNull(_boss, message);
            _sr = _boss.SpriteRenderer;
            _healthModule = _boss.HealthModule;
            _shootModule = _boss.ShootModule;
        }

        public override void OnStateEnter()
        {
            _shootModule.Reload();
            _shootCounter = _boss.ShootDelay;
            _sr.color = _boss.InvulnerabilityColor;
            _healthModule.SetVulnerability(false);
            int halfAmmo = _shootModule.Ammo / 2;
            _bulletToShoot = halfAmmo / _boss.BulletToShootDivider;
            AddListeners();
        }
        public override void OnStateExit()
        {
            RemoveListeners();
            _bulletShooted = 0;
        }
        public override void OnStateUpdate()
        {
            _shootCounter -= Time.deltaTime;
            if (_shootCounter <= 0f)
            {
                _boss.Shoot();
                _shootCounter = _boss.ShootDelay;
                if (_bulletShooted == _bulletToShoot)
                {
                    fsm.ChangeState<NormalBoss_IdleState>();
                }
            }
        }

        private void AddListeners()
        {
            _shootModule.OnShoot += OnShoot;
        }
        private void RemoveListeners()
        {
            _shootModule.OnShoot -= OnShoot;
        }

        private void OnShoot()
        {
            _bulletShooted++;
        }

    }
}