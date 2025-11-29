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
        #region Settings

        [Header("HARD BOSS SETTINGS")]
        [SerializeField]
        [Range(15.0f, 90.0f)]
        private float rotationLimit = 60.0f;
        [SerializeField]
        [Range(1.0f, 10.0f)]
        private float vulnerabilityStateDelay = 1.0f;
        [SerializeField]
        private Color invulnerabilityColor = Color.gray;
        [SerializeField]
        [Range(1.0f, 10.0f)]
        [Tooltip("Multiplier applied on \"vulnearabilitySpeed\" when enemy reached half health")]
        private float halfHealthSpeedMultiplier = 2.0f;
        [SerializeField]
        [Range(1.0f, 10.0f)]
        [Tooltip("Multiplier applied on \"vulnearabilitySpeed\" when enemy reached a quarter health")]
        private float quarterHealthSpeedMultiplier = 3.0f;
        [SerializeField]
        [Range(1.0f, 10.0f)]
        [Tooltip("Divider applied on \"rotationOffset\" when enemy reached half health")]
        private float halfHealthRotationDivider = 2.0f;
        [SerializeField]
        [Range(1.0f, 10.0f)]
        [Tooltip("Divider applied on \"rotationOffset\" when enemy reached a quarter health")]
        private float quarterHealthRotationDivider = 4.0f;
        [SerializeField]
        [Tooltip("Offset that will be subtracted by sea position on \"Y\" axis")]
        private float positionYOffset = 0.5f;
        [SerializeField]
        private List<SS_CannonShoot> shootStrategies = null;

        #endregion

        #region Initialization & life cycle

        protected override void Awake()
        {
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
            StartCoroutine(GoToWaitToShootState());
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

        protected override void AddListeners()
        {
            base.AddListeners();
            ShootModule.OnShoot += OnShoot;
            cannons.OnShoot += OnCannonsShoot;
            HealthModule.OnVulnerable += OnVulnerable;
            HealthModule.OnInvulnerable += OnInvulnerable;
            HealthModule.OnReachAQuarterHealth += OnReachAQuarterHealth;
            HealthModule.OnReachHalfHealth += OnReachHalfHealth;
        }
        protected override void RemoveListeners()
        {
            HealthModule.OnReachAQuarterHealth -= OnReachAQuarterHealth;
            HealthModule.OnReachHalfHealth -= OnReachHalfHealth;
            HealthModule.OnInvulnerable -= OnInvulnerable;
            HealthModule.OnVulnerable -= OnVulnerable;
            cannons.OnShoot -= OnCannonsShoot;
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
            MoveToTargetY();
            StartCoroutine(GoToWaitToShootState());
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
            float halfHealthRotationOffset = startRotationOffset / halfHealthRotationDivider;
            rotationOffset = halfHealthRotationOffset;
            float halfHealthSpeed = startMovementSpeed * halfHealthSpeedMultiplier;
            MovementModule.SetMovementSpeed(halfHealthSpeed);
            currentMovementSpeed = MovementModule.MovementSpeed;
        }
        private void OnReachAQuarterHealth()
        {
            currentCannonShootStrategy = GetCannonShootStrategy(ShootModuleManager.ShootType.FromAll);
            float quarterHealthRotationOffset = startRotationOffset / quarterHealthRotationDivider;
            rotationOffset = quarterHealthRotationOffset;
            float quarterHealthSpeed = startMovementSpeed * quarterHealthSpeedMultiplier;
            MovementModule.SetMovementSpeed(quarterHealthSpeed);
            currentMovementSpeed = MovementModule.MovementSpeed;
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

        #region Helpers & internal logic

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
            SpriteRenderer.flipX = !SpriteRenderer.flipX;
            movementDirection *= -1.0f;
        }
        private void MoveToTargetY()
        {
            const float SEA_SIZEY_OFFSET = 1.0f;
            float halfSeaSizeY = seaSize.y * 0.5f;
            float halfBossSizeY = SpriteRenderer.bounds.size.y * 0.5f;
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

        private IEnumerator GoToWaitToShootState()
        {
            yield return new WaitUntil(() => Mathf.Abs(seaPosition.x - MovementModule.Target.GetPosition().x) <= 0.1f);
            SetShootMode();
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