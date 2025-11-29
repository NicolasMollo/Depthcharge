using Depthcharge.Actors.AI;
using Depthcharge.Environment;
using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

namespace Depthcharge.Actors
{
    public class EC_NormalBoss : BaseEnemyController
    {

        #region Settings

        [Header("NORMAL BOSS SETTINGS")]
        [SerializeField]
        private float vulnerabilityStateDelay = 1.0f;
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
        [Range(1, 5)]
        [Tooltip("Divider applied on \"bulletToShoot\" when enemy reached half health")]
        private int halfHealthBulletToShootDivider = 2;
        [SerializeField]
        [Range(1, 5)]
        [Tooltip("Divider applied on \"bulletToShoot\" when enemy reached a quarter health")]
        private int quarterHealthBulletToShootDivider = 1;

        #endregion

        private float seaWidth = 0f;
        private Vector2 seaPosition = Vector2.zero;
        private int bulletShooted = 0;
        private int bulletToShootDivider = 0;
        private float startMovementSpeed = 0f;
        private float currentMovementSpeed = 0f;
        private Vector2 stallPosition = Vector2.zero;


        #region Initialization & life cycle

        protected override void Awake()
        {
            bulletShooted = 0;
            bulletToShootDivider = 3;
        }

        protected override void InternalSetUp()
        {
            EnvironmentRootController env = FindFirstObjectByType<EnvironmentRootController>();
            string message = $"=== EC_NormalBoss.InternalSetUp() === Doesn't exist a EnvironmentRootController object in scene!";
            Assert.IsNotNull(env, message);
            seaWidth = env.SeaSize.x;
            seaPosition = env.SeaPosition;
            startMovementSpeed = MovementModule.MovementSpeed;
            currentMovementSpeed = startMovementSpeed;
            SetStallPosition();
            StartCoroutine(GoToWaitToShootState());
        }

        protected override void AddListeners()
        {
            base.AddListeners();
            ShootModule.OnShoot += OnShoot;
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
            ShootModule.OnShoot -= OnShoot;
            base.RemoveListeners();
        }

        private void Update()
        {
            MovementModule.MoveTarget(movementDirection);
        }

        #endregion

        #region Event callbacks

        internal override void OnCollisionWithEndOfMap()
        {
            InvertBossDirection();
            SetStallPosition();
            StartCoroutine(GoToWaitToShootState());
        }

        private void OnShoot()
        {
            bulletShooted++;
            int halfPoolSize = ShootModule.PoolSize / 2;
            if (bulletShooted == halfPoolSize / bulletToShootDivider)
            {
                StartCoroutine(GoToVulnerabilityState(vulnerabilityStateDelay));
                bulletShooted = 0;
            }
        }

        private void OnReachHalfHealth()
        {
            float speed = startMovementSpeed * halfHealthSpeedMultiplier;
            MovementModule.SetMovementSpeed(speed);
            currentMovementSpeed = MovementModule.MovementSpeed;
            bulletToShootDivider = halfHealthBulletToShootDivider;
        }
        private void OnReachAQuarterHealth()
        {
            float speed = startMovementSpeed * quarterHealthSpeedMultiplier;
            MovementModule.SetMovementSpeed(speed);
            currentMovementSpeed = MovementModule.MovementSpeed;
            bulletToShootDivider = quarterHealthBulletToShootDivider;
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

        #region Helpers and internal logic

        private void SetStallPosition()
        {
            const float SPRITE_OFFSET_X = 1.0f;
            float halfWidth = (SpriteRenderer.sprite.bounds.size.x * 0.5f) - SPRITE_OFFSET_X;
            float halfSeaWidth = seaWidth * 0.5f;
            float calculatedMinX = seaPosition.x - halfSeaWidth + halfWidth;
            float calculatedMaxX = seaPosition.x + halfSeaWidth - halfWidth;
            float positionX = Random.Range(calculatedMinX, calculatedMaxX);

            stallPosition = new Vector2(positionX, MovementModule.Target.GetPosition().y);
        }
        private void InvertBossDirection()
        {
            SpriteRenderer.flipX = !SpriteRenderer.flipX;
            movementDirection *= -1.0f;
        }

        #endregion

        private IEnumerator GoToWaitToShootState()
        {
            yield return new WaitUntil(() => (stallPosition - MovementModule.Target.GetPosition()).sqrMagnitude <= 0.01f);
            ShootModule.Reload();
            MovementModule.SetMovementSpeed(0);
            fsm.ChangeState<WaitToShootEnemyState>();
        }

        private IEnumerator GoToVulnerabilityState(float delay)
        {
            yield return new WaitForSeconds(delay);
            MovementModule.SetMovementSpeed(currentMovementSpeed);
            fsm.ChangeState<VulnerabilityEnemyState>();
        }

    }
}