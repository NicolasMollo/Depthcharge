using Depthcharge.Actors.AI;
using Depthcharge.Actors.Modules;
using Depthcharge.Environment;
using UnityEngine;
using UnityEngine.Assertions;
using Depthcharge.Audio;

namespace Depthcharge.Actors
{

    public class EC_EasyBoss : BaseEnemyController
    {

        #region Settings

        [Header("EASY BOSS SETTINGS")]
        [SerializeField]
        private float vulnerabilitySpeed = 0.0f;
        [SerializeField]
        private Color invulnerabilityColor = Color.gray;
        [SerializeField]
        [Range(1.0f, 10.0f)]
        [Tooltip("Multiplier applied on \"vulnearabilitySpeed\" when enemy reached half health")]
        private float halfHealthSpeedMultiplier = 1.5f;
        [SerializeField]
        [Range(1.0f, 10.0f)]
        [Tooltip("Multiplier applied on \"vulnearabilitySpeed\" when enemy reached a quarter health")]
        private float quarterHealthSpeedMultiplier = 2.0f;
        [SerializeField]
        [Range(0.0f, 1.0f)]
        [Tooltip("Multiplier applied on \"shootDelay\" when enemy reached half health")]
        private float halfHealthShootDelayMultiplier = 0.5f;
        [SerializeField]
        [Range(0.0f, 1.0f)]
        [Tooltip("Multiplier applied on \"shootDelay\" when enemy reached a quarter health")]
        private float quarterHealthShootDelayMultiplier = 0.25f;

        #endregion

        private float startSpeed = 0.0f;
        private float startVulnerabilitySpeed = 0.0f;
        private float currentVulnerabilitySpeed = 0.0f;
        private float startShootDelay = 0.0f;
        private int collisionCount = 0;
        private Transform topSeaTarget = null;
        private Transform bottomSeaTarget = null;


        #region Initialization & life cycle

        protected override void Awake()
        {
            base.Awake();
            startVulnerabilitySpeed = vulnerabilitySpeed;
            startShootDelay = _shootDelay;
            collisionCount = 0;
        }
        protected override void InternalSetUp()
        {
            SpriteRenderer.color = invulnerabilityColor;
            EnvironmentRootController env = FindFirstObjectByType<EnvironmentRootController>();
            string message = $"=== BossEnemyController.InternalSetUp() === There isn't a EnvironmentRootController in scene!";
            Assert.IsNotNull(env, message);
            topSeaTarget = env.TopSeaTarget;
            bottomSeaTarget = env.BottomSeaTarget;
            MoveToTargetY(topSeaTarget.position.y);
            startSpeed = MovementModule.MovementSpeed;
            startVulnerabilitySpeed = vulnerabilitySpeed;
            currentVulnerabilitySpeed = startVulnerabilitySpeed;
            HealthModule.SetVulnerability(false);
        }

        protected override void AddListeners()
        {
            base.AddListeners();
            HealthModule.OnVulnerable += OnVulnerable;
            HealthModule.OnInvulnerable += OnInvulnerable;
            HealthModule.OnReachHalfHealth += OnReachHalfHealth;
            HealthModule.OnReachAQuarterHealth += OnReachAQuarterHealth;
        }
        protected override void RemoveListeners()
        {
            HealthModule.OnReachAQuarterHealth -= OnReachAQuarterHealth;
            HealthModule.OnReachHalfHealth -= OnReachHalfHealth;
            HealthModule.OnInvulnerable -= OnInvulnerable;
            HealthModule.OnVulnerable -= OnVulnerable;
            base.RemoveListeners();
        }

        private void Update()
        {
            MovementModule.MoveTarget(movementDirection);
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
            collisionCount++;
            InvertBossDirection();
            if (collisionCount % 2 == 0)
            {
                GoToWaitToShootState();
            }
            else
            {
                GoToVulnerabilityState();
            }
        }
        internal override void Shoot()
        {
            base.Shoot();
            AudioSource.PlayOneShot(AudioClipType.Shoot);
        }

        private void OnReachHalfHealth()
        {
            float speed = startVulnerabilitySpeed * halfHealthSpeedMultiplier;
            currentVulnerabilitySpeed = speed;
            MovementModule.SetMovementSpeed(currentVulnerabilitySpeed);
            float shootDelay = startShootDelay * halfHealthShootDelayMultiplier;
            _shootDelay = shootDelay;
        }
        private void OnReachAQuarterHealth()
        {
            float speed = startVulnerabilitySpeed * quarterHealthSpeedMultiplier;
            currentVulnerabilitySpeed = speed;
            MovementModule.SetMovementSpeed(currentVulnerabilitySpeed);
            float shootDelay = startShootDelay * quarterHealthShootDelayMultiplier;
            _shootDelay = shootDelay;
        }

        private void OnVulnerable()
        {
            SpriteRenderer.color = StartColor;
        }
        private void OnInvulnerable()
        {
            SpriteRenderer.color = invulnerabilityColor;
        }

        #endregion

        #region Helpers

        private void InvertBossDirection()
        {
            SpriteRenderer.flipX = !SpriteRenderer.flipX;
            movementDirection *= -1.0f;
        }

        private void MoveToTargetY(float targetY)
        {
            Vector2 position = new Vector2(MovementModule.Target.GetPosition().x, targetY);
            MovementModule.Target.MoveTo(position);
        }

        #endregion

        private void GoToWaitToShootState()
        {
            MoveToTargetY(topSeaTarget.position.y);
            ShootModule.Reload();
            MovementModule.SetMovementSpeed(startSpeed);
            fsm.ChangeState<WaitToShootEnemyState>();
        }
        private void GoToVulnerabilityState()
        {
            MoveToTargetY(bottomSeaTarget.position.y);
            MovementModule.SetMovementSpeed(currentVulnerabilitySpeed);
            fsm.ChangeState<VulnerabilityEnemyState>();
        }

    }
}