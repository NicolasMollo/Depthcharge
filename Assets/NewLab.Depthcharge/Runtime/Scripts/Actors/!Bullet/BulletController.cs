using Depthcharge.Actors.AI;
using Depthcharge.Actors.Modules;
using System;
using UnityEngine;

namespace Depthcharge.Actors
{

    public class BulletController : Actor
    {

        #region Modules

        [Header("MODULES")]
        [SerializeField]
        private HealthModule _healthModule = null;
        [SerializeField]
        private MovementModule _movementModule = null;
        [SerializeField]
        private AnimationModule _animationModule = null;
        [SerializeField]
        private BaseCollisionModule _collisionModule = null;

        #endregion
        #region Settings

        [Header("SETTINGS")]
        [SerializeField]
        private Fsm fsm = null;
        [SerializeField]
        private float _damage = 1.0f;

        #endregion

        public HealthModule HealthModule { get => _healthModule; }
        public MovementModule MovementModule { get => _movementModule; }
        public AnimationModule AnimationModule { get => _animationModule; }
        public BaseCollisionModule CollisionModule { get => _collisionModule; }

        public float Damage { get => _damage; }

        internal Action<string> OnCollideWithEndOfMap = null;
        internal bool killOnCollisionWithEndOfMap = true;


        private void OnEnable()
        {
            if (fsm.CurrentState == null) return;
            SpriteRenderer.color = StartColor;
            _healthModule.ResetHealth();
            _collisionModule.EnableModule();
            fsm.ChangeToNextState();
        }
        protected override void Start()
        {
            base.Start();
            _collisionModule.SetUpModule();
            fsm.SetUpStates();
            fsm.SetStartState();
            AddListeners();
        }
        private void OnDestroy()
        {
            RemoveListeners();
            fsm.CleanUpStates();
        }
        private void FixedUpdate()
        {
            fsm.UpdateCurrentState();
        }

        internal void OnCollisionWithEndOfMap(string tag)
        {
            if (!_collisionModule.IsEnable) return;
            OnCollideWithEndOfMap?.Invoke(tag);
            if (killOnCollisionWithEndOfMap)
            {
                HealthModule.TakeMaxDamage();
                CollisionModule.DisableModule();
            }
        }
        internal void Deactivation()
        {
            this.gameObject.SetActive(false);
        }

        private void AddListeners()
        {
            _healthModule.OnTakeDamage += OnTakeDamage;
            _healthModule.OnDeath += OnDeath;
        }
        private void RemoveListeners()
        {
            _healthModule.OnDeath -= OnDeath;
            _healthModule.OnTakeDamage -= OnTakeDamage;
        }
        private void OnTakeDamage(float health)
        {
            if (health <= 0.0f) return;
            _animationModule.PlayAnimation(AnimationController.AnimationType.Damage);
        }
        private void OnDeath()
        {
            fsm.ChangeToNextState();
        }

    }

}