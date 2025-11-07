using Depthcharge.Actors.AI;
using Depthcharge.Environment;
using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

namespace Depthcharge.Actors
{
    public class EC_NormalBoss : BaseEnemyController
    {

        private float seaWidth = default;
        private int bulletShooted = 0;
        private int bulletToShootDivider = 0;
        private float startMovementSpeed = 0;
        private Vector2 stallPosition = Vector2.zero;

        [SerializeField]
        private SpriteRenderer spriteRenderer = null;
        [SerializeField]
        private float vulnerabilityStateDelay = 1.0f;

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
            startMovementSpeed = MovementModule.MovementSpeed;
            ShootModule.OnShoot += OnShoot;
            SetStallPosition();
            StartCoroutine(GoToVulnerabilityState(0));
        }
        protected override void InternalCleanUp()
        {
            ShootModule.OnShoot -= OnShoot;
        }

        private void Update()
        {
            MovementModule.MoveTarget(movementDirection);
        }

        public void OnCollisionWithEndOfMap()
        {
            SetStallPosition();
            InvertBossDirection();
        }
        private void SetStallPosition()
        {
            const float SPRITE_OFFSET_X = 1.0f;
            float halfWidth = (spriteRenderer.sprite.bounds.size.x * 0.5f) - SPRITE_OFFSET_X;
            float halfSeaWidth = seaWidth * 0.5f;
            float calculatedMinX = Screen.mainWindowPosition.x - halfSeaWidth + halfWidth;
            float calculatedMaxX = Screen.mainWindowPosition.x + halfSeaWidth - halfWidth;
            float positionX = Random.Range(calculatedMinX, calculatedMaxX);

            stallPosition = new Vector2(positionX, MovementModule.Target.GetPosition().y);
        }
        private void InvertBossDirection()
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
            movementDirection *= -1.0f;
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

        private IEnumerator GoToVulnerabilityState(float delay)
        {
            yield return new WaitForSeconds(delay);
            SetMovementSpeed();
            MovementModule.EnableModule();
            spriteRenderer.color = Color.white;
            fsm.ChangeState<VulnerabilityEnemyState>();
            yield return new WaitUntil(() => (stallPosition - MovementModule.Target.GetPosition()).sqrMagnitude > 0.01f);
            StartCoroutine(GoToIdleState());
        }
        private void SetMovementSpeed()
        {
            float[] valuesToAssign = new float[3];
            valuesToAssign[0] = startMovementSpeed * 2;
            valuesToAssign[1] = startMovementSpeed * 3;
            valuesToAssign[2] = startMovementSpeed;
            float temporarySpeed = 0.0f;
            ScaleValueByHealth(ref temporarySpeed, valuesToAssign);
            MovementModule.SetMovementSpeed(temporarySpeed);
        }

        private IEnumerator GoToIdleState()
        {
            yield return new WaitUntil(() => (stallPosition - MovementModule.Target.GetPosition()).sqrMagnitude <= 0.01f);
            SetBulletToShootDivider();
            ShootModule.Reload();
            MovementModule.SetMovementSpeed(0);
            // HealthModule.SetVulnerability(false);
            spriteRenderer.color = Color.gray;
            fsm.ChangeState<IdleEnemyState>();
        }
        private void SetBulletToShootDivider()
        {
            float[] valuesToAssign = new float[3];
            valuesToAssign[0] = 2;
            valuesToAssign[1] = 1;
            valuesToAssign[2] = 3;
            float temporaryDivider = 0.0f;
            ScaleValueByHealth(ref temporaryDivider, valuesToAssign);
            bulletToShootDivider = (int)temporaryDivider;
        }

        private void ScaleValueByHealth(ref float value, float[] valuesToAssign)
        {
            const float MID_HEALTH_PERCENTAGE = 0.5f;
            const float LOW_HEALTH_PERCENTAGE = 0.25f;

            if (HealthModule.HealthPercentage > LOW_HEALTH_PERCENTAGE && HealthModule.HealthPercentage <= MID_HEALTH_PERCENTAGE)
            {
                value = valuesToAssign[0];
            }
            else if (HealthModule.HealthPercentage <= LOW_HEALTH_PERCENTAGE)
            {
                value = valuesToAssign[1];
            }
            else
            {
                value = valuesToAssign[2];
            }
        }

    }
}