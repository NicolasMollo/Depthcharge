using Depthcharge.Actors.AI;
using Depthcharge.Actors.Modules;
using System;
using TMPro;
using UnityEngine;

namespace Depthcharge.Actors
{

    [DisallowMultipleComponent]
    public abstract class BaseEnemyController : MonoBehaviour
    {

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

        #endregion

        [Header("SCORE POINTS TEXT (BASE)")]
        [SerializeField]
        protected TextMeshPro scorePointsText = null;

        [Header("FSM (BASE)")]
        [SerializeField]
        protected Fsm fsm = null;

        [Header("SETTINGS (BASE)")]
        [SerializeField]
        private string enemyName = string.Empty;
        public string EnemyName { get => enemyName; }
        [SerializeField]
        protected Vector2 movementDirection = Vector2.zero;
        [SerializeField]
        [Range(1.0f, 1000.0f)]
        private float _scorePoints = 1.0f;
        public float ScorePoints { get => _scorePoints; protected set => _scorePoints = value; }
        [SerializeField]
        [Range(0.0f, 100.0f)]
        protected float _shootDelay = 0;
        public virtual float ShootDelay { get => _shootDelay; }

        public Action<BaseEnemyController> OnDeactivation = null;

        private void Start()
        {
            fsm.SetUpStates();
            fsm.SetStartState();
            InternalSetUp();
            _healthModule.OnDeath += Deactivation;
        }
        private void OnDestroy()
        {
            _healthModule.OnDeath -= Deactivation;
            InternalCleanUp();
            fsm.CleanUpStates();
        }
        public void Deactivation()
        {
            OnDeactivation?.Invoke(this);
            this.gameObject.SetActive(false);
        }

        protected virtual void InternalSetUp() { }
        protected virtual void InternalCleanUp() { }

        public void SetMovementDirection(Vector2 movementDirection)
        {
            this.movementDirection = movementDirection;
        }

    }

}