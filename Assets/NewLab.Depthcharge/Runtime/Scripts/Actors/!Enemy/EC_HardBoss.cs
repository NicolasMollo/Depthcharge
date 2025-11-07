using Depthcharge.Actors.AI;
using Depthcharge.Actors.Modules;
using Depthcharge.Environment;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Depthcharge.Actors
{
    public class EC_HardBoss : BaseEnemyController
    {

        #region Runtime fields

        private BaseShootStrategy startShootStrategy = null;
        private SS_CannonShoot currentCannonShootStrategy = null;
        private Vector2 seaPosition = Vector2.zero;
        private Vector2 seaSize = Vector2.zero;
        private float startMovementSpeed = 0f;
        private float currentMovementSpeed = 0f;
        private float rotationOffset = 0f;
        private float startRotationOffset = 0f;
        private int bulletsShooted = 0;
        private int bulletsToShoot = 0;
        private ShootModuleManager cannons = null;

        #endregion
        #region Serialized settings

        [SerializeField]
        private SpriteRenderer spriteRenderer = null;
        [SerializeField]
        [Range(15.0f, 90.0f)]
        private float rotationLimit = 60.0f;
        [SerializeField]
        [Range(1.0f, 10.0f)]
        private float vulnerabilityStateDelay = 1.0f;
        [SerializeField]
        [Tooltip("Offset that will be subtracted by sea position on \"Y\" axis")]
        private float positionYOffset = 0.5f;
        [SerializeField]
        private List<SS_CannonShoot> shootStrategies = null;

        #endregion

        #region Initialization & life cycle

        private void Awake()
        {
            string message = $"=== EC_HardBoss.Awake() === Be ensure to assign sprite renderer to the boss!";
            Assert.IsNotNull(spriteRenderer, message);
            bulletsToShoot = (int)(rotationLimit / rotationOffset) * 2 + 1;
            rotationOffset = rotationLimit;
            startRotationOffset = rotationOffset;
            startShootStrategy = shootStrategy;
        }

        protected override void InternalSetUp()
        {
            startMovementSpeed = MovementModule.MovementSpeed;
            currentMovementSpeed = startMovementSpeed;
            SetSeaData();
            SetShootModules();
            MoveToTargetY();
            AddListeners();
            StartCoroutine(GoToIdleState());
        }
        private void SetSeaData()
        {
            EnvironmentRootController env = FindFirstObjectByType<EnvironmentRootController>();
            string message = $"=== EC_HardBoss.Awake() === There isn't a \"EnvironmentRootController\" in this scene!";
            Assert.IsNotNull(env, message);
            seaPosition = env.SeaPosition;
            seaSize = env.SeaSize;
        }
        private void SetShootModules()
        {
            currentCannonShootStrategy = GetCannonShootStrategy(ShootModuleManager.ShootType.FromAllExceptSides);
            ShootModule.IncreaseShootPointRotation(rotationLimit * -1);
            cannons = FindFirstObjectByType<ShootModuleManager>();
            string message = $"=== EC_HardBoss.Awake() === There isn't a \"ShootModuleManager\" in this scene!";
            Assert.IsNotNull(cannons, message);
        }

        protected override void InternalCleanUp()
        {
            RemoveListeners();
        }

        private void AddListeners()
        {
            ShootModule.OnShoot += OnShoot;
            cannons.OnShoot += OnCannonsShoot;
            HealthModule.OnReachAQuarterHealth += OnReachAQuarterHealth;
            HealthModule.OnReachHalfHealth += OnReachHalfHealth;
        }
        private void RemoveListeners()
        {
            HealthModule.OnReachAQuarterHealth -= OnReachAQuarterHealth;
            HealthModule.OnReachHalfHealth -= OnReachHalfHealth;
            cannons.OnShoot -= OnCannonsShoot;
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
            MoveToTargetY();
            StartCoroutine(GoToIdleState());
        }
        private void OnShoot()
        {
            bulletsShooted++;
            if (bulletsShooted != bulletsToShoot)
            {
                ShootModule.IncreaseShootPointRotation(rotationOffset);
                return;
            }
            bulletsShooted = 0;
            ShootModule.DisableModule();
            ShootModule.IncreaseShootPointRotation((rotationLimit * -1) * 2);
            StartCoroutine(GoToVulnerabilityState(vulnerabilityStateDelay));
        }
        private void OnCannonsShoot()
        {
            cannons.DisableShootModules();
            StartCoroutine(GoToVulnerabilityState(vulnerabilityStateDelay));
        }
        private void OnReachHalfHealth()
        {
            currentCannonShootStrategy = GetCannonShootStrategy(ShootModuleManager.ShootType.FromSides);
            float halfHealthRotationOffset = startRotationOffset / 2;
            rotationOffset = halfHealthRotationOffset;
            float halfHealthSpeed = startMovementSpeed * 2;
            MovementModule.SetMovementSpeed(halfHealthSpeed);
            currentMovementSpeed = MovementModule.MovementSpeed;
        }
        private void OnReachAQuarterHealth()
        {
            currentCannonShootStrategy = GetCannonShootStrategy(ShootModuleManager.ShootType.FromAll);
            float quarterHealthRotationOffset = startRotationOffset / 4;
            rotationOffset = quarterHealthRotationOffset;
            float quarterHealthSpeed = startMovementSpeed * 3;
            MovementModule.SetMovementSpeed(quarterHealthSpeed);
            currentMovementSpeed = MovementModule.MovementSpeed;
        }

        #endregion

        #region Helpers and internal logic

        private void SetShootMode()
        {
            float positionY = seaPosition.y - positionYOffset;
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
            bulletsToShoot = (int)(rotationLimit / rotationOffset) * 2 + 1;
            shootStrategy = startShootStrategy;
            ShootModule.Reload();
            ShootModule.EnableModule();
        }
        private void SetCannonShoot()
        {
            shootStrategy = currentCannonShootStrategy;
            cannons.ReloadShootModules();
            cannons.EnableShootModules();
        }

        private void InvertBossDirection()
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
            movementDirection *= -1.0f;
        }
        private void MoveToTargetY()
        {
            const float SEA_SIZEY_OFFSET = 1.0f;
            float halfSeaSizeY = seaSize.y * 0.5f;
            float halfBossSizeY = spriteRenderer.bounds.size.y * 0.5f;
            float topSeaY = seaPosition.y + (halfSeaSizeY - SEA_SIZEY_OFFSET) - halfBossSizeY;
            float bottomSeaY = seaPosition.y - (halfSeaSizeY + SEA_SIZEY_OFFSET) + halfBossSizeY;
            float positionY = MovementModule.Target.GetPosition().y;
            positionY--;
            if (positionY <= bottomSeaY)
            {
                positionY = topSeaY;
            }
            Vector2 position = new Vector2(MovementModule.Target.GetPosition().x, positionY);
            MovementModule.Target.MoveTo(position);
        }
        private SS_CannonShoot GetCannonShootStrategy(ShootModuleManager.ShootType shootType)
        {
            SS_CannonShoot cannonShootStrategy = null;
            foreach (SS_CannonShoot strategy in shootStrategies)
            {
                if (strategy.ShootTypes == shootType)
                {
                    cannonShootStrategy = strategy;
                    break;
                }
            }
            return cannonShootStrategy;
        }

        #endregion

        private IEnumerator GoToIdleState()
        {
            yield return new WaitUntil(() => Mathf.Abs(seaPosition.x - MovementModule.Target.GetPosition().x) <= 0.1f);
            SetShootMode();
            spriteRenderer.color = Color.gray;
            MovementModule.SetMovementSpeed(0);
            fsm.ChangeState<IdleEnemyState>();
        }

        private IEnumerator GoToVulnerabilityState(float delay)
        {
            yield return new WaitForSeconds(delay);
            spriteRenderer.color = Color.white;
            MovementModule.SetMovementSpeed(currentMovementSpeed);
            fsm.ChangeState<VulnerabilityEnemyState>();
        }

    }
}