using Depthcharge.Actors.Modules;
using Depthcharge.Audio;
using Depthcharge.Environment;
using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace Depthcharge.Actors
{

    public class EC_EasyBoss : BaseEnemyController
    {

        #region Fields

        [SerializeField]
        private Color _invulnerabilityColor = Color.gray;
        [SerializeField]
        private float _retreatSpeed = 0.0f;
        [SerializeField]
        private EasyBossPhaseController _phaseController = null;

        private EasyBossHelper _helper = null;
        private float _startRetreatSpeed = 0.0f;
        private float _startShootDelay = 0.0f;
        private Transform _topSeaTarget = null;
        private Transform _bottomSeaTarget = null;

        #endregion

        public float RetreatSpeed { get => _retreatSpeed; set => _retreatSpeed = value; }
        public Color InvulnerabilityColor { get => _invulnerabilityColor; }

        public Action OnCollideWithEndOfMap = null;


        #region Initialization & life cycle

        protected override void Awake()
        {
            base.Awake();
            _startRetreatSpeed = _retreatSpeed;
            _startShootDelay = _shootDelay;
            _helper = new EasyBossHelper(this);
            _phaseController = new EasyBossPhaseController(this);
        }
        protected override void InternalSetUp()
        {
            EnvironmentRootController env = FindFirstObjectByType<EnvironmentRootController>();
            string message = $"=== BossEnemyController.InternalSetUp() === There isn't a EnvironmentRootController in scene!";
            Assert.IsNotNull(env, message);
            _topSeaTarget = env.TopSeaTarget;
            _bottomSeaTarget = env.BottomSeaTarget;
        }

        protected override void AddListeners()
        {
            base.AddListeners();
            HealthModule.OnReachHalfHealth += OnReachHalfHealth;
            HealthModule.OnReachAQuarterHealth += OnReachAQuarterHealth;
        }
        protected override void RemoveListeners()
        {
            HealthModule.OnReachAQuarterHealth -= OnReachAQuarterHealth;
            HealthModule.OnReachHalfHealth -= OnReachHalfHealth;
            base.RemoveListeners();
        }

        private void Update()
        {
            fsm.UpdateCurrentState();
        }

        #endregion

        #region Event callbacks

        internal override void OnCollideWithBullet(float bulletDamage)
        {
            if (!HealthModule.IsVulnerable)
            {
                AudioSource.PlayOneShot(AudioClipType.InvulnerabilityDamage);
            }
            base.OnCollideWithBullet(bulletDamage);
        }
        internal override void OnCollisionWithEndOfMap(EndOfMapContext context)
        {
            _helper.InvertBossDirection();
            OnCollideWithEndOfMap?.Invoke();
        }
        internal override void Shoot()
        {
            base.Shoot();
            AudioSource.PlayOneShot(AudioClipType.Shoot);
        }

        private void OnReachHalfHealth()
        {
            _phaseController.OnHalfHealth(_startRetreatSpeed, _startShootDelay);
        }
        private void OnReachAQuarterHealth()
        {
            _phaseController.OnAQuarterHealth(_startRetreatSpeed, _startShootDelay);
        }

        #endregion

        #region API

        public void MoveToTopSea()
        {
            _helper.MoveToTargetY(_topSeaTarget.position.y);
        }
        public void MoveToBottomSea()
        {
            _helper.MoveToTargetY(_bottomSeaTarget.position.y);
        }

        #endregion

    }
}