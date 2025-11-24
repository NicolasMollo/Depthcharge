using Depthcharge.Actors.AI;
using Depthcharge.Actors.Modules;
using Depthcharge.Environment;
using UnityEngine;
using UnityEngine.Assertions;

namespace Depthcharge.Actors
{

    [DisallowMultipleComponent]
    public class EC_EasyBoss : BaseEnemyController
    {

        private float startSpeed = 0.0f;
        private float startVulnerabilitySpeed = 0.0f;
        private float currentVulnerabilitySpeed = 0.0f;
        private float startShootDelay = 0.0f;
        private int collisionCount = 0;
        private Transform topSeaTarget = null;
        private Transform bottomSeaTarget = null;

        [SerializeField]
        private SpriteRenderer spriteRenderer = null;
        [SerializeField]
        private float vulnerabilitySpeed = 0.0f;

        #region Initialization & life cycle

        private void Awake()
        {
            string message = $"=== BossEnemyController.Awake() === Be ensure to assign SpriteRenderer component to boss enemy!";
            Assert.IsNotNull(spriteRenderer, message);
            spriteRenderer.color = Color.gray;
            startVulnerabilitySpeed = vulnerabilitySpeed;
            startShootDelay = _shootDelay;
            collisionCount = 0;
        }
        protected override void InternalSetUp()
        {
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
            MovementModule.MoveTarget(movementDirection);
        }

        #endregion

        #region Event callbacks

        public void OnCollisionWithEndOfMap()
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

        private void OnReachHalfHealth()
        {
            float speed = startVulnerabilitySpeed * 2;
            currentVulnerabilitySpeed = speed;
            MovementModule.SetMovementSpeed(currentVulnerabilitySpeed);
            float shootDelay = startShootDelay * 0.5f;
            _shootDelay = shootDelay;
        }
        private void OnReachAQuarterHealth()
        {
            float speed = startVulnerabilitySpeed * 3;
            currentVulnerabilitySpeed = speed;
            MovementModule.SetMovementSpeed(currentVulnerabilitySpeed);
            float shootDelay = startShootDelay * 0.25f;
            _shootDelay = shootDelay;
        }

        #endregion

        #region Helpers

        private void InvertBossDirection()
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
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
            spriteRenderer.color = Color.gray;
            fsm.ChangeState<WaitToShootEnemyState>();
        }
        private void GoToVulnerabilityState()
        {
            MoveToTargetY(bottomSeaTarget.position.y);
            MovementModule.SetMovementSpeed(currentVulnerabilitySpeed);
            spriteRenderer.color = Color.white;
            fsm.ChangeState<VulnerabilityEnemyState>();
        }

    }
}