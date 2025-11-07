using Depthcharge.Actors.AI;
using Depthcharge.Environment;
using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

namespace Depthcharge.Actors
{
    public class EC_NormalBoss : BaseEnemyController
    {

        private float seaWidth = 0f;
        private Vector2 seaPosition = Vector2.zero;
        private int bulletShooted = 0;
        private int bulletToShootDivider = 0;
        private float startMovementSpeed = 0f;
        private float currentMovementSpeed = 0f;
        private Vector2 stallPosition = Vector2.zero;

        [SerializeField]
        private SpriteRenderer spriteRenderer = null;
        [SerializeField]
        private float vulnerabilityStateDelay = 1.0f;

        #region Initialization & life cycle

        private void Awake()
        {
            string message = $"=== EC_NormalBoss.Awake() === Be ensure to assign sprite renderer to the boss!";
            Assert.IsNotNull(spriteRenderer, message);
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
            AddListeners();
            SetStallPosition();
            StartCoroutine(GoToIdleState());
        }
        protected override void InternalCleanUp()
        {
            RemoveListeners();
        }

        private void AddListeners()
        {
            ShootModule.OnShoot += OnShoot;
            HealthModule.OnReachHalfHealth += OnReachHalfHealth;
            HealthModule.OnReachAQuarterHealth += OnReachAQuarterHealth;
        }
        private void RemoveListeners()
        {
            HealthModule.OnReachAQuarterHealth -= OnReachAQuarterHealth;
            HealthModule.OnReachHalfHealth -= OnReachHalfHealth;
            ShootModule.OnShoot -= OnShoot;
        }

        private void Update()
        {
            MovementModule.MoveTarget(movementDirection);
        }

        #endregion

        #region Event callbacks

        public void OnCollisionWithEndOfMap()
        {
            InvertBossDirection();
            SetStallPosition();
            StartCoroutine(GoToIdleState());
        }

        private void OnShoot()
        {
            bulletShooted++;
            if (bulletShooted == ShootModule.PoolSize / bulletToShootDivider)
            {
                StartCoroutine(GoToVulnerabilityState(vulnerabilityStateDelay));
                bulletShooted = 0;
            }
        }

        private void OnReachHalfHealth()
        {
            float speed = startMovementSpeed * 2;
            MovementModule.SetMovementSpeed(speed);
            currentMovementSpeed = MovementModule.MovementSpeed;
            bulletToShootDivider = 2;
        }
        private void OnReachAQuarterHealth()
        {
            float speed = startMovementSpeed * 3;
            MovementModule.SetMovementSpeed(speed);
            currentMovementSpeed = MovementModule.MovementSpeed;
            bulletToShootDivider = 1;
        }

        #endregion

        #region Helpers and internal logic

        private void SetStallPosition()
        {
            const float SPRITE_OFFSET_X = 1.0f;
            float halfWidth = (spriteRenderer.sprite.bounds.size.x * 0.5f) - SPRITE_OFFSET_X;
            float halfSeaWidth = seaWidth * 0.5f;
            float calculatedMinX = seaPosition.x - halfSeaWidth + halfWidth;
            float calculatedMaxX = seaPosition.x + halfSeaWidth - halfWidth;
            float positionX = Random.Range(calculatedMinX, calculatedMaxX);

            stallPosition = new Vector2(positionX, MovementModule.Target.GetPosition().y);
        }
        private void InvertBossDirection()
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
            movementDirection *= -1.0f;
        }

        #endregion

        private IEnumerator GoToVulnerabilityState(float delay)
        {
            yield return new WaitForSeconds(delay);
            MovementModule.SetMovementSpeed(currentMovementSpeed);
            spriteRenderer.color = Color.white;
            fsm.ChangeState<VulnerabilityEnemyState>();
        }

        private IEnumerator GoToIdleState()
        {
            yield return new WaitUntil(() => (stallPosition - MovementModule.Target.GetPosition()).sqrMagnitude <= 0.01f);
            ShootModule.Reload();
            MovementModule.SetMovementSpeed(0);
            spriteRenderer.color = Color.gray;
            fsm.ChangeState<IdleEnemyState>();
        }

    }
}