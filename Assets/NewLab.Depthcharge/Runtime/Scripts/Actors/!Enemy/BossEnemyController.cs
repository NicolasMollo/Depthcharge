using Depthcharge.Actors.AI;
using Depthcharge.Actors.Modules;
using Depthcharge.Environment;
using System;
using UnityEngine;

namespace Depthcharge.Actors
{

    [DisallowMultipleComponent]
    public class BossEnemyController : BaseEnemyController
    {

        private float startSpeed = 0.0f;
        private float startVulnerabilitySpeed = 0.0f;
        private float startShootDelay = 0.0f;
        private float[] vsMultipliers = null;
        private float[] sdMultipliers = null;
        private int collisionCount = 0;
        private Transform topSeaTarget = null;
        private Transform bottomSeaTarget = null;

        [SerializeField]
        private SpriteRenderer spriteRenderer = null;

        #region Settings

        [Header("SETTINGS")]
        [SerializeField]
        private float vulnerabilitySpeed = 0.0f;
        [SerializeField]
        [Range(1.0f, 10.0f)]
        [Tooltip("Vulnerability speed multiplier when HP < 50%")]
        private float vsFirstMultiplier = 0.0f;
        [SerializeField]
        [Range(1.0f, 10.0f)]
        [Tooltip("Vulnerability speed multiplier when HP < 25%")]
        private float vsSecondMultiplier = 0.0f;
        [SerializeField]
        [Range(0.0f, 1.0f)]
        [Tooltip("Shoot delay divisor when HP < 50%")]
        private float sdFirstDivisor = 0.0f;
        [SerializeField]
        [Range(0.0f, 1.0f)]
        [Tooltip("Shoot delay divisor when HP < 25%")]
        private float sdSecondDivisor = 0.0f;

        #endregion

        private void Awake()
        {
            if (spriteRenderer == null)
            {
                Debug.LogError($"=== BossEnemyController.Awake() === Be ensure to assign SpriteRenderer component to boss enemy!");
                return;
            }
            spriteRenderer.color = Color.gray;
            startVulnerabilitySpeed = vulnerabilitySpeed;
            startShootDelay = _shootDelay;
            vsMultipliers = new float[] { vsFirstMultiplier, vsSecondMultiplier };
            sdMultipliers = new float[] { sdFirstDivisor, sdSecondDivisor };
            collisionCount = 0;
        }
        protected override void InternalSetUp()
        {
            EnvironmentRootController env = FindFirstObjectByType<EnvironmentRootController>();
            if (env == null)
            {
                Debug.LogError($"=== BossEnemyController.InternalSetUp() === There isn't a EnvironmentRootController in scene!");
                return;
            }
            topSeaTarget = env.TopSeaTarget;
            bottomSeaTarget = env.BottomSeaTarget;
            MoveToTargetY(topSeaTarget.position.y);
            startSpeed = MovementModule.MovementSpeed;
            HealthModule.SetVulnerability(false);
        }

        private void Update()
        {
            MovementModule.MoveTarget(movementDirection);
        }

        public void OnCollisionWithEndOfMap()
        {
            collisionCount++;
            InvertBossDirection();
            if (collisionCount % 2 == 0)
            {
                GoToIdleState();
            }
            else
            {
                GoToVulnerabilityState();
            }
        }

        private void InvertBossDirection()
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
            movementDirection *= -1.0f;
        }
        private void GoToIdleState()
        {
            MoveToTargetY(topSeaTarget.position.y);
            ShootModule.Reload();
            ScaleValueByHealth(ref _shootDelay, startShootDelay, sdMultipliers);
            MovementModule.SetMovementSpeed(startSpeed);
            spriteRenderer.color = Color.gray;
            fsm.ChangeState<IdleEnemyState>();
        }
        private void GoToVulnerabilityState()
        {
            MoveToTargetY(bottomSeaTarget.position.y);
            ScaleValueByHealth(ref vulnerabilitySpeed, startVulnerabilitySpeed, vsMultipliers);
            MovementModule.SetMovementSpeed(vulnerabilitySpeed);
            spriteRenderer.color = Color.white;
            fsm.ChangeState<VulnerabilityEnemyState>();
        }
        private void MoveToTargetY(float targetY)
        {
            Vector2 position = new Vector2(MovementModule.Target.GetPosition().x, targetY);
            MovementModule.Target.MoveTo(position);
        }
        private void ScaleValueByHealth(ref float value, float startValue, float[] multipliers)
        {
            const float MID_HEALTH_PERCENTAGE = 0.5f;
            const float LOW_HEALTH_PERCENTAGE = 0.25f;

            if (HealthModule.HealthPercentage > LOW_HEALTH_PERCENTAGE && HealthModule.HealthPercentage <= MID_HEALTH_PERCENTAGE)
            {
                value = startValue * multipliers[0];
            }
            else if (HealthModule.HealthPercentage <= LOW_HEALTH_PERCENTAGE)
            {
                value = startValue * multipliers[1];
            }
        }

    }
}