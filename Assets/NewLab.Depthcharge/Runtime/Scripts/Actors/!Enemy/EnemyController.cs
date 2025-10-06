using Depthcharge.Actors.AI;
using Depthcharge.Actors.Modules;
using UnityEngine;
using TMPro;
using System;

namespace Depthcharge.Actors
{

    [DisallowMultipleComponent]
    public class EnemyController : MonoBehaviour
    {

        #region Modules

        [Header("MODULES")]

        [SerializeField]
        private MovementModule _movementModule = null;
        public MovementModule MovementModule { get => _movementModule; }

        [SerializeField]
        private HealthModule _healthModule = null;
        public HealthModule HealthModule { get => _healthModule; }

        [SerializeField]
        private ShootModule _shootModule = null;
        public ShootModule ShootModule { get => _shootModule; }

        [SerializeField]
        private BaseCollisionModule _collisionModule = null;
        public BaseCollisionModule CollisionModule { get => _collisionModule; }

        #endregion
        #region Score points text

        [Header("SCORE POINTS TEXT")]

        [SerializeField]
        private TextMeshPro scorePointsText = null;

        #endregion
        #region Fsm

        [Header("FMS")]

        [SerializeField]
        private Fsm fsm = null;

        #endregion
        #region Settings

        [Header("SETTINGS")]

        [SerializeField]
        private Vector2 movementDirection = Vector2.zero;

        [SerializeField]
        [Range(1.0f, 100.0f)]
        private float scorePoints = 1.0f;
        public float ScorePoints { get => scorePoints; }

        [Header("_SHOOT SETTINGS")]

        [SerializeField]
        private bool randomShootDelay = false;

        [SerializeField]
        [Range(1.0f, 100.0f)]
        private float minShootDelay = 1.0f;

        [SerializeField]
        [Range(1.0f, 100.0f)]
        private float maxShootDelay = 1.0f;

        [SerializeField]
        [Range(0.0f, 100.0f)]
        private float _shootDelay = 0;

        private const float AGGRESSIVENESS = 1.0f;

        [SerializeField]
        [Range(0.0f, 1.0f)]
        private float aggressivenessMultiplier = 0.0f;

        public float ShootDelay
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

        #endregion

        public Action<EnemyController> OnDeactivation = null;

        private void Start()
        {
            _healthModule.OnDeath += Deactivation;
            fsm.SetUpStates();
            fsm.SetStartState();
        }

        private void OnDestroy()
        {
            fsm.CleanUpStates();
            _healthModule.OnDeath -= Deactivation;
        }

        private void Update()
        {
            _movementModule.MoveTarget(movementDirection);
        }

        private void OnEnable()
        {
            if (_shootModule.IsModuleSetUp)
            {
                _shootModule.ResetBullets();
            }
            _healthModule.ResetHealth();
            //fsm.SetStartState();
        }

        public void SetUpEnemy(EnemyConfiguration enemyConfiguration, Vector2 movementDirection)
        {
            scorePoints = enemyConfiguration.ScorePoints;
            scorePointsText.text = Mathf.RoundToInt(scorePoints).ToString();
            this.movementDirection = movementDirection;
            _movementModule.SetMovementSpeed(enemyConfiguration.MovementSpeed);
            _healthModule.SetMaxHealth(enemyConfiguration.MaxHealth);
            _shootDelay = enemyConfiguration.ShootDelay;
            minShootDelay = enemyConfiguration.MinShootDelay;
            maxShootDelay = enemyConfiguration.MaxShootDelay;
            aggressivenessMultiplier = enemyConfiguration.AggressivenessMultiplier;
        }

        public void SetMovementDirection(Vector2 movementDirection)
        {
            this.movementDirection = movementDirection;
        }

        public void Deactivation()
        {
            OnDeactivation?.Invoke(this);
            this.gameObject.SetActive(false);
        }

    }

}