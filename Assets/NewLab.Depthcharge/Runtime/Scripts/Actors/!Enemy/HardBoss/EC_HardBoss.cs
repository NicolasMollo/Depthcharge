using Depthcharge.Actors.Modules;
using Depthcharge.Audio;
using Depthcharge.Environment;
using UnityEngine;

namespace Depthcharge.Actors
{
    public class EC_HardBoss : BaseEnemyController
    {

        #region Serialized fields

        [SerializeField]
        private CannonShootController cannonShootController = null;
        [SerializeField]
        private HardBossPhaseController phaseController = null;
        [SerializeField]
        [Range(15.0f, 90.0f)]
        private float _rotationLimit = 45.0f;
        [SerializeField]
        private Color _invulnerabilityColor = Color.gray;
        [SerializeField]
        [Tooltip("Offset that will be subtracted by sea position on \"Y\" axis")]
        private float _positionYOffset = 0.5f;

        #endregion

        #region Runtime fields

        private HardBossHelper _helper = null;
        private float _changeStateTreshold = 0.1f;
        private BaseShootStrategy _startShootStrategy = null;
        private Vector2 _seaPosition = Vector2.zero;
        private Vector2 _seaSize = Vector2.zero;
        private float _startMovementSpeed = 0f;
        private float _rotationOffset = 0f;
        private float _startRotationOffset = 0f;
        private int _bulletsShooted = 0;
        private int _bulletsToShoot = 0;
        private int _shootCount = 0;

        #endregion

        public float ChangeStateTreshold { get => _changeStateTreshold; }
        public Vector2 SeaPosition { get => _seaPosition; }
        public Vector2 SeaSize { get => _seaSize; }
        public float RotationOffset { get => _rotationOffset; set => _rotationOffset = value; }
        public int BulletsToShoot { get => _bulletsToShoot; }
        public int ShootCount { get => _shootCount; set => _shootCount = value; }
        public Color InvulnerabilityColor { get => _invulnerabilityColor; }


        #region Initialization & life cycle

        protected override void Awake()
        {
            base.Awake();
            _bulletsToShoot = (int)(_rotationLimit / _rotationOffset) * 2 + 1;
            _rotationOffset = _rotationLimit;
            _startRotationOffset = _rotationOffset;
            _startShootStrategy = shootStrategy;
        }

        protected override void InternalSetUp()
        {
            cannonShootController.SetUp(FindFirstObjectByType<ShootModuleManager>());
            _startMovementSpeed = MovementModule.MovementSpeed;
            MovementModule.SetMovementSpeed(_startMovementSpeed);
            cannonShootController.SetCurrentCannonShootStrategy(ShootModuleManager.ShootType.FromAllExceptSides);
            ShootModule.IncreaseShootPointRotation(_rotationLimit * -1);
            _seaPosition = EnvironmentRootController.Instance.SeaPosition;
            _seaSize = EnvironmentRootController.Instance.SeaSize;
            phaseController.SetUp(this, cannonShootController);
            _helper = new HardBossHelper(this);
            _helper.MoveToTargetY();
        }
        protected override void AddListeners()
        {
            base.AddListeners();
            ShootModule.OnShoot += OnShoot;
            cannonShootController.SubscribeOnCannonsShoot(OnCannonsShoot);
            HealthModule.OnReachAQuarterHealth += OnReachAQuarterHealth;
            HealthModule.OnReachHalfHealth += OnReachHalfHealth;
        }
        protected override void RemoveListeners()
        {
            HealthModule.OnReachAQuarterHealth -= OnReachAQuarterHealth;
            HealthModule.OnReachHalfHealth -= OnReachHalfHealth;
            cannonShootController.UnsubscribeFromCannonsShoot(OnCannonsShoot);
            ShootModule.OnShoot -= OnShoot;
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
            _helper.MoveToTargetY();
        }
        internal override void Shoot()
        {
            base.Shoot();
            if (_shootCount == 0)
            {
                AudioSource.PlayOneShotCurrentClip();
            }
            _shootCount++;
        }

        private void OnShoot()
        {
            _bulletsShooted++;
            if (_bulletsShooted != _bulletsToShoot)
            {
                ShootModule.IncreaseShootPointRotation(_rotationOffset);
                return;
            }
            _bulletsShooted = 0;
            ShootModule.DisableModule();
            ShootModule.IncreaseShootPointRotation((_rotationLimit * -1) * 2);
        }
        private void OnCannonsShoot()
        {
            cannonShootController.DisableCannons();
        }
        private void OnReachHalfHealth()
        {
            phaseController.OnHalfHealth(_startRotationOffset, _startMovementSpeed);
        }
        private void OnReachAQuarterHealth()
        {
            phaseController.OnAQuarterHealth(_startRotationOffset, _startMovementSpeed);
        }

        #endregion

        #region Shoot mode

        public void SetShootMode()
        {
            float positionY = _seaPosition.y - _positionYOffset;
            if (MovementModule.Target.GetPosition().y > positionY)
            {
                SetCannonShoot();
            }
            else
            {
                SetStdShoot();
            }
        }
        private void SetStdShoot()
        {
            _bulletsToShoot = (int)(_rotationLimit / _rotationOffset) * 2 + 1;
            shootStrategy = _startShootStrategy;
            ShootModule.Reload();
            ShootModule.EnableModule();
            AudioSource.SetCurrentClip(AudioClipType.Shoot);
        }
        private void SetCannonShoot()
        {
            shootStrategy = cannonShootController.CurrentCannonShootStrategy;
            cannonShootController.ReloadCannons();
            cannonShootController.EnableCannons();
            AudioSource.SetCurrentClip(AudioClipType.CannonShoot);
        }

        #endregion

    }
}