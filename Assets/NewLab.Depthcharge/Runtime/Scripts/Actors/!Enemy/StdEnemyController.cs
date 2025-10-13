using Depthcharge.Actors.Modules;
using System;
using UnityEngine;

namespace Depthcharge.Actors
{

    [DisallowMultipleComponent]
    public class StdEnemyController : BaseEnemyController
    {

        [Header("MODULES")]
        [SerializeField]
        private BaseCollisionModule _collisionModule = null;
        public BaseCollisionModule CollisionModule { get => _collisionModule; }

        [Header("SHOOT SETTINGS")]
        [SerializeField]
        private bool randomShootDelay = false;
        [SerializeField]
        [Range(1.0f, 100.0f)]
        private float minShootDelay = 1.0f;
        [SerializeField]
        [Range(1.0f, 100.0f)]
        private float maxShootDelay = 1.0f;
        //[SerializeField]
        //[Range(0.0f, 100.0f)]
        //private float shootDelay = 0;
        private const float AGGRESSIVENESS = 1.0f;
        [SerializeField]
        [Range(0.0f, 1.0f)]
        private float aggressivenessMultiplier = 0.0f;

        public override float ShootDelay
        {
            get
            {
                if (randomShootDelay)
                {
                    float shootDelayReduction = AGGRESSIVENESS * aggressivenessMultiplier;
                    float min = minShootDelay - shootDelayReduction;
                    float max = maxShootDelay - shootDelayReduction;
                    float finalDelay = UnityEngine.Random.Range(min, max);
                    _shootDelay = finalDelay;
                }
                return _shootDelay;
            }
        }

        private void OnEnable()
        {
            if(ShootModule.IsModuleSetUp)
            {
                ShootModule.ResetBullets();
            }
            HealthModule.ResetHealth();
        }
        private void Update()
        {
            MovementModule.MoveTarget(movementDirection);
        }

        public void SetUpEnemy(EnemyConfiguration enemyConfiguration, Vector2 movementDirection)
        {
            ScorePoints = enemyConfiguration.ScorePoints;
            scorePointsText.text = Mathf.RoundToInt(ScorePoints).ToString();
            this.movementDirection = movementDirection;
            MovementModule.SetMovementSpeed(enemyConfiguration.MovementSpeed);
            HealthModule.SetMaxHealth(enemyConfiguration.MaxHealth);
            _shootDelay = enemyConfiguration.ShootDelay;
            minShootDelay = enemyConfiguration.MinShootDelay;
            maxShootDelay = enemyConfiguration.MaxShootDelay;
            aggressivenessMultiplier = enemyConfiguration.AggressivenessMultiplier;
        }

    }

}