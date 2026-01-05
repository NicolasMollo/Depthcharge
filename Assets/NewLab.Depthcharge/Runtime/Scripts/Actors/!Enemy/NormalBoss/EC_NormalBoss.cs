using Depthcharge.Actors.Modules;
using Depthcharge.Audio;
using Depthcharge.Environment;
using UnityEngine;

namespace Depthcharge.Actors
{
    public class EC_NormalBoss : BaseEnemyController
    {

        #region Fields

        [SerializeField]
        private Color _invulnerabilityColor = Color.gray;
        [SerializeField]
        private NormalBossPhaseController phaseController = null;

        private NormalBossHelper helper = null;
        private float _seaWidth = 0f;
        private Vector2 _seaPosition = Vector2.zero;
        private int _bulletToShootDivider = 0;
        private float _startSpeed = 0f;
        private Vector2 _stallPosition = Vector2.zero;
        private float _changeStateTreshold = 0.1f;

        #endregion

        public Color InvulnerabilityColor { get => _invulnerabilityColor; }
        public Vector2 SeaPosition { get => _seaPosition; }
        public float SeaWidth { get => _seaWidth; }
        public Vector2 StallPosition { get => _stallPosition; }
        public int BulletToShootDivider { get => _bulletToShootDivider; set => _bulletToShootDivider = value; }
        public float ChangeStateTreshold { get => _changeStateTreshold; }


        #region Initialization & life cycle

        protected override void Awake()
        {
            base.Awake();
            _bulletToShootDivider = 3;
            helper = new NormalBossHelper(this);
            phaseController = new NormalBossPhaseController(this);
        }

        protected override void InternalSetUp()
        {
            EnvironmentRootController env = EnvironmentRootController.Instance;
            _seaWidth = env.SeaSize.x;
            _seaPosition = env.SeaPosition;
            _startSpeed = MovementModule.MovementSpeed;
            _stallPosition = helper.CalculateStallPosition();
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
            helper.InvertBossDirection();
            _stallPosition = helper.CalculateStallPosition();
        }
        internal override void Shoot()
        {
            base.Shoot();
            AudioSource.PlayOneShot(AudioClipType.Shoot);
        }

        private void OnReachHalfHealth()
        {
            phaseController.OnHalfHealth(_startSpeed);
        }
        private void OnReachAQuarterHealth()
        {
            phaseController.OnAQuarterHealth(_startSpeed);
        }

        #endregion

    }
}