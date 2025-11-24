using Depthcharge.Actors.AI;
using Depthcharge.Actors.Modules;
using Depthcharge.Toolkit;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Depthcharge.Actors
{

    [DisallowMultipleComponent]
    public abstract class BaseEnemyController : MonoBehaviour
    {

        private const float DEATH_SPEED = 0.1f;
        public Action<BaseEnemyController> OnDeactivation = null;

        #region Modules

        [Header("MODULES (BASE)")]
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
        protected AnimationModule animationModule = null;
        public AnimationModule AnimationModule { get => animationModule; }
        [SerializeField]
        protected BaseCollisionModule collisionModule = null;

        #endregion
        #region Serialized settings

        [Header("SETTINGS (BASE)")]
        [SerializeField]
        protected Fsm fsm = null;
        [SerializeField]
        protected TextMeshPro scorePointsText = null;
        [SerializeField]
        protected BaseShootStrategy shootStrategy = null;
        [SerializeField]
        protected StdFadeableAdapter fadeableAdapter = null;
        public StdFadeableAdapter FadeableAdapter { get => fadeableAdapter; }

        [SerializeField]
        private string enemyName = string.Empty;
        public string EnemyName { get => enemyName; }
        [SerializeField]
        [Range(1.0f, 1000.0f)]
        private float _scorePoints = 1.0f;
        public float ScorePoints { get => _scorePoints; protected set => _scorePoints = value; }
        [SerializeField]
        [Range(0.0f, 100.0f)]
        protected float _shootDelay = 0;
        public virtual float ShootDelay { get => _shootDelay; }
        [SerializeField]
        protected Vector2 movementDirection = Vector2.zero;
        [SerializeField]
        private float fadeOutDelay = 0.2f;

        #endregion

        public virtual void Shoot()
        {
            shootStrategy.Shoot(ShootModule);
        }

        public void Deactivation()
        {
            OnDeactivation?.Invoke(this);
            this.gameObject.SetActive(false);
        }

        #region Initialization & life cycle

        private void Start()
        {
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
            _healthModule.OnDeath += OnDeath;
            animationModule.SubscribeToOnAnimationStart(AnimationController.AnimationType.Death, OnDeathAnimationStart);
            animationModule.SubscribeToOnAnimationEnd(AnimationController.AnimationType.Death, OnDeathAnimationEnd);
        }
        protected virtual void RemoveListeners()
        {
            animationModule.UnsubscribeFromOnAnimationEnd(AnimationController.AnimationType.Death, OnDeathAnimationEnd);
            animationModule.UnsubscribeFromOnAnimationStart(AnimationController.AnimationType.Death, OnDeathAnimationStart);
            _healthModule.OnDeath -= OnDeath;
        }

        #endregion

        #region Event callbacks

        protected virtual void OnDeath()
        {
            animationModule.PlayAnimation(AnimationController.AnimationType.Death);
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

        #endregion

        private IEnumerator FadeOutEnemy()
        {
            fadeableAdapter.FadeOut(fadeOutDelay);
            yield return new WaitUntil(() => fadeableAdapter.IsFadedIn == false);
            Deactivation();
        }

    }

}