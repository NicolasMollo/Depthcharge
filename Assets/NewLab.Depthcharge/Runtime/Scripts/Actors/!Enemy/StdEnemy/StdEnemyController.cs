using Depthcharge.Actors.Modules;
using Depthcharge.Audio;
using UnityEngine;

namespace Depthcharge.Actors
{

    public class StdEnemyController : BaseEnemyController
    {

        public enum EnemyTier { Weak, Medium, Strong, Last }

        [SerializeField]
        private EnemyTier _tier = EnemyTier.Weak;

        private float _travelledDistanceToShoot = 0.0f;
        private float _minRandomDistance = 1.0f;
        private float _maxRandomDistance = 1.0f;
        private float _startSpeed = 0.0f;
        private Vector2 _startDirection = Vector2.zero;
        private bool _isDeadInTheLastRun = false;
        private int _numberOfCollision = 0;

        internal EnemyTier Tier { get => _tier; }
        public float TravelledDistanceToShoot { get => _travelledDistanceToShoot; }
        public float MinRandomDistance { get => _minRandomDistance; }
        public float MaxRandomDistance { get => _maxRandomDistance; }


        private void OnEnable()
        {
            if (_isDeadInTheLastRun)
            {
                ResetEnemy();
            }
            if (!ShootModule.IsFullAmmo)
            {
                ShootModule.Reload();
            }
            ShootModule.DisableModule();
            HealthModule.ResetHealth();
        }
        private void Update()
        {
            fsm.UpdateCurrentState();
        }

        internal void SetUpEnemy(EnemyConfiguration enemyConfiguration, Vector2 movementDirection)
        {
            _tier = enemyConfiguration.EnemyTier;
            ScorePoints = enemyConfiguration.ScorePoints;
            scorePointsText.text = Mathf.RoundToInt(ScorePoints).ToString();
            this.movementDirection = movementDirection;
            _startDirection = this.movementDirection;
            MovementModule.SetMovementSpeed(enemyConfiguration.MovementSpeed);
            _startSpeed = MovementModule.MovementSpeed;
            HealthModule.SetMaxHealth(enemyConfiguration.MaxHealth);
            _shootDelay = enemyConfiguration.ShootDelay;
            _minRandomDistance = enemyConfiguration.MinRandomDistance;
            _maxRandomDistance = enemyConfiguration.MaxRandomDistance;
            _travelledDistanceToShoot = enemyConfiguration.TravelledDistanceToShoot;
        }

        protected override void AddListeners()
        {
            base.AddListeners();
            ShootModule.OnShoot += OnShoot;
        }
        protected override void RemoveListeners()
        {
            ShootModule.OnShoot -= OnShoot;
            base.RemoveListeners();
        }

        internal override void OnCollisionWithEndOfMap(EndOfMapContext context)
        {
            _isDeadInTheLastRun = false;
            Deactivation();
        }
        internal override void OnCollisionExitWithEndOfMap(EndOfMapContext context)
        {
            _numberOfCollision++;
            if (_numberOfCollision == 2)
            {
                ShootModule.DisableModule();
                _numberOfCollision = 0;
                return;
            }
            ShootModule.EnableModule();
        }

        private void OnShoot()
        {
            AudioSource.PlayOneShot(AudioClipType.Shoot);
        }
        protected override void OnDeath()
        {
            base.OnDeath();
            _isDeadInTheLastRun = true;
        }

        private void ResetEnemy()
        {
            collisionModule.EnableModule();
            ShootModule.EnableModule();
            movementDirection = _startDirection;
            MovementModule.SetMovementSpeed(_startSpeed);
            scorePointsText.gameObject.SetActive(true);
            fadeableAdapter.ResetAlpha();
            _animationModule.PlayAnimation(AnimationController.AnimationType.Idle);
        }

    }

}