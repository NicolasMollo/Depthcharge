using Depthcharge.Actors.AI;
using Depthcharge.Actors.Modules;
using Depthcharge.Toolkit;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Depthcharge.Actors
{

    public abstract class BaseEnemyController : Actor
    {

        #region Modules

        [Header("MODULES")]
        [SerializeField]
        private HealthModule _healthModule = null;
        [SerializeField]
        private MovementModule _movementModule = null;
        [SerializeField]
        protected AnimationModule _animationModule = null;
        [SerializeField]
        private ShootModule _shootModule = null;
        [SerializeField]
        protected BaseCollisionModule collisionModule = null;

        #endregion
        #region Settings

        [Header("SETTINGS")]
        [SerializeField]
        protected Fsm fsm = null;
        [SerializeField]
        protected TextMeshPro scorePointsText = null;
        [SerializeField]
        protected BaseShootStrategy shootStrategy = null;
        [SerializeField]
        protected StdFadeableAdapter fadeableAdapter = null;
        [SerializeField]
        private string enemyName = string.Empty;
        [SerializeField]
        protected Vector2 movementDirection = Vector2.zero;
        [SerializeField]
        [Range(1.0f, 1000.0f)]
        private float _scorePoints = 1.0f;
        [SerializeField]
        [Range(0.0f, 100.0f)]
        protected float _shootDelay = 0;
        [SerializeField]
        private float fadeOutDelay = 0.2f;

        #endregion

        public MovementModule MovementModule { get => _movementModule; }
        public HealthModule HealthModule { get => _healthModule; }
        public ShootModule ShootModule { get => _shootModule; }
        public AnimationModule AnimationModule { get => _animationModule; }

        public string EnemyName { get => enemyName; }
        public float ScorePoints { get => _scorePoints; protected set => _scorePoints = value; }
        public float ShootDelay { get => _shootDelay; }
        public bool IsFadedIn { get => fadeableAdapter.IsFadedIn; }

        private const float DEATH_SPEED = 0.1f;
        public Action<BaseEnemyController> OnDeactivation = null;


        #region Initialization & life cycle

        protected override void Start()
        {
            base.Start();
            fsm.SetUpStates();
            fsm.SetStartState();
            InternalSetUp();
            AddListeners();
        }
        private void OnDestroy()
        {
            RemoveListeners();
            InternalCleanUp();
            fsm.CleanUpStates();
        }
        protected virtual void InternalSetUp() { }
        protected virtual void InternalCleanUp() { }

        protected virtual void AddListeners()
        {
            _healthModule.OnTakeDamage += OnTakeDamage;
            _healthModule.OnDeath += OnDeath;
            _animationModule.SubscribeToOnAnimationStart(AnimationController.AnimationType.Death, OnDeathAnimationStart);
            _animationModule.SubscribeToOnAnimationEnd(AnimationController.AnimationType.Death, OnDeathAnimationEnd);
        }
        protected virtual void RemoveListeners()
        {
            _animationModule.UnsubscribeFromOnAnimationEnd(AnimationController.AnimationType.Death, OnDeathAnimationEnd);
            _animationModule.UnsubscribeFromOnAnimationStart(AnimationController.AnimationType.Death, OnDeathAnimationStart);
            _healthModule.OnDeath -= OnDeath;
            _healthModule.OnTakeDamage -= OnTakeDamage;
        }

        #endregion

        internal void Shoot()
        {
            shootStrategy.Shoot(ShootModule);
        }
        internal void Deactivation()
        {
            OnDeactivation?.Invoke(this);
            this.gameObject.SetActive(false);
        }
        internal virtual void OnCollisionWithEndOfMap() { }

        protected virtual void OnDeath()
        {
            _animationModule.PlayAnimation(AnimationController.AnimationType.Death);
        }
        protected virtual void OnDeathAnimationStart()
        {
            scorePointsText.gameObject.SetActive(false);
            MovementModule.SetMovementSpeed(0);
            ShootModule.DisableModule();
            collisionModule.DisableModule();
        }
        protected virtual void OnDeathAnimationEnd()
        {
            movementDirection = Vector2.down;
            MovementModule.SetMovementSpeed(DEATH_SPEED);
            StartCoroutine(FadeOutEnemy());
        }
        protected virtual void OnTakeDamage(float health)
        {
            _animationModule.PlayAnimation(AnimationController.AnimationType.Damage);
        }

        private IEnumerator FadeOutEnemy()
        {
            fadeableAdapter.FadeOut(fadeOutDelay);
            yield return new WaitUntil(() => fadeableAdapter.IsFadedIn == false);
            Deactivation();
        }

    }

}