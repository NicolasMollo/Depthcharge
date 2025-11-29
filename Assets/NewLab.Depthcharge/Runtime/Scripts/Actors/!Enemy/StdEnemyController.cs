using Depthcharge.Actors.AI;
using Depthcharge.Actors.Modules;
using System;
using UnityEngine;

namespace Depthcharge.Actors
{

    public class StdEnemyController : BaseEnemyController
    {

        public enum EnemyTier { Weak, Medium, Strong, Last }

        #region Settings

        [Header("STD ENEMY SETTINGS")]
        [SerializeField]
        private EnemyTier _enemyTier = EnemyTier.Weak;
        [SerializeField]
        private float travelledDistanceToShoot = 0.0f;
        [SerializeField]
        [Range(1.0f, 100.0f)]
        [Tooltip("Minimum offset (from which the random is calculated with a maximum) that is added to the distance to be traveled to shoot")]
        private float minRandomDistance = 1.0f;
        [SerializeField]
        [Range(1.0f, 100.0f)]
        [Tooltip("Maximum offset (from which the random is calculated with a minimum) that is added to the distance to be traveled to shoot")]
        private float maxRandomDistance = 1.0f;

        #endregion

        internal EnemyTier Tier { get => _enemyTier; }

        private float CalculatedDistanceToShoot { get => travelledDistanceToShoot + randomDistanceOffset; }
        private float startMovementSpeed = 0.0f;
        private Vector2 startMovementDirection = Vector2.zero;
        private float travelledDistance = 0.0f;
        private float randomDistanceOffset = 0.0f;
        private Vector2 lastPosition = Vector2.zero;
        private bool isDeadInTheLastRun = false;


        private void OnEnable()
        {
            if (isDeadInTheLastRun) 
                ResetEnemy();
            if (!ShootModule.IsFullAmmo)
                ShootModule.Reload();
            HealthModule.ResetHealth();
            lastPosition = MovementModule.Target.GetPosition();
        }
        private void Update()
        {
            MovementModule.MoveTarget(movementDirection);
            CalculateTravelledDistance();
            if (travelledDistance >= CalculatedDistanceToShoot)
            {
                randomDistanceOffset = UnityEngine.Random.Range(minRandomDistance, maxRandomDistance);
                travelledDistance = 0.0f;
                fsm.ChangeState<WaitToShootEnemyState>();
            }
        }

        internal void SetUpEnemy(EnemyConfiguration enemyConfiguration, Vector2 movementDirection)
        {
            _enemyTier = enemyConfiguration.EnemyTier;
            ScorePoints = enemyConfiguration.ScorePoints;
            scorePointsText.text = Mathf.RoundToInt(ScorePoints).ToString();
            this.movementDirection = movementDirection;
            startMovementDirection = this.movementDirection;
            MovementModule.SetMovementSpeed(enemyConfiguration.MovementSpeed);
            startMovementSpeed = MovementModule.MovementSpeed;
            HealthModule.SetMaxHealth(enemyConfiguration.MaxHealth);
            _shootDelay = enemyConfiguration.ShootDelay;
            minRandomDistance = enemyConfiguration.MinRandomDistance;
            maxRandomDistance = enemyConfiguration.MaxRandomDistance;
            randomDistanceOffset = UnityEngine.Random.Range(minRandomDistance, maxRandomDistance);
            travelledDistanceToShoot = enemyConfiguration.TravelledDistanceToShoot;
        }
        internal override void OnCollisionWithEndOfMap()
        {
            isDeadInTheLastRun = false;
            Deactivation();
        }

        protected override void OnDeath()
        {
            base.OnDeath();
            isDeadInTheLastRun = true;
        }

        private void ResetEnemy()
        {
            collisionModule.EnableModule();
            ShootModule.EnableModule();
            movementDirection = startMovementDirection;
            MovementModule.SetMovementSpeed(startMovementSpeed);
            scorePointsText.gameObject.SetActive(true);
            fadeableAdapter.ResetAlpha();
            _animationModule.PlayAnimation(AnimationController.AnimationType.Idle);
        }
        private void CalculateTravelledDistance()
        {
            float positionDiff = Vector3.Distance(MovementModule.Target.GetPosition(), lastPosition);
            travelledDistance += positionDiff;
            lastPosition = MovementModule.Target.GetPosition();
        }

    }

}