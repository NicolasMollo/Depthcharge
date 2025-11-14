using Depthcharge.Actors.AI;
using Depthcharge.Actors.Modules;
using System;
using UnityEngine;

namespace Depthcharge.Actors
{

    [DisallowMultipleComponent]
    public class StdEnemyController : BaseEnemyController
    {

        private float travelledDistance = 0.0f;
        private float randomDistanceOffset = 0.0f;
        private Vector2 lastPosition = Vector2.zero;

        public enum EnemyTier { Weak, Medium, Strong, Last }
        [SerializeField]
        private EnemyTier _enemyTier = EnemyTier.Weak;
        public EnemyTier Tier { get => _enemyTier; }

        [Header("SHOOT SETTINGS")]
        [SerializeField]
        private float travelledDistanceToShoot = 0.0f;
        [SerializeField]
        [Range(1.0f, 100.0f)]
        private float minRandomDistance = 1.0f;
        [SerializeField]
        [Range(1.0f, 100.0f)]
        private float maxRandomDistance = 1.0f;

        private float CalculatedDistanceToShoot
        {
            get => travelledDistanceToShoot + randomDistanceOffset;
        }

        private void OnEnable()
        {
            if(ShootModule.IsModuleSetUp)
            {
                ShootModule.ResetBullets();
            }
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
        private void CalculateTravelledDistance()
        {
            float positionDiff = Vector3.Distance(MovementModule.Target.GetPosition(), lastPosition);
            travelledDistance += positionDiff;
            lastPosition = MovementModule.Target.GetPosition();
        }

        public void SetUpEnemy(EnemyConfiguration enemyConfiguration, Vector2 movementDirection)
        {
            _enemyTier = enemyConfiguration.EnemyTier;
            ScorePoints = enemyConfiguration.ScorePoints;
            scorePointsText.text = Mathf.RoundToInt(ScorePoints).ToString();
            this.movementDirection = movementDirection;
            MovementModule.SetMovementSpeed(enemyConfiguration.MovementSpeed);
            HealthModule.SetMaxHealth(enemyConfiguration.MaxHealth);
            _shootDelay = enemyConfiguration.ShootDelay;
            minRandomDistance = enemyConfiguration.MinRandomDistance;
            maxRandomDistance = enemyConfiguration.MaxRandomDistance;
            randomDistanceOffset = UnityEngine.Random.Range(minRandomDistance, maxRandomDistance);
            travelledDistanceToShoot = enemyConfiguration.TravelledDistanceToShoot;
        }

    }

}