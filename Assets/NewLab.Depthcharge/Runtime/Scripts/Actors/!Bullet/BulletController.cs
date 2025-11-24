using Depthcharge.Actors.AI;
using Depthcharge.Actors.Modules;
using UnityEngine;

namespace Depthcharge.Actors
{

    [DisallowMultipleComponent]
    public class BulletController : MonoBehaviour
    {

        private MovementContext _movementContext = default;
        internal MovementContext MovementContext { get => _movementContext; }

        [SerializeField]
        private SpriteRenderer _spriteRenderer = null;
        internal SpriteRenderer SpriteRenderer { get => _spriteRenderer; }

        #region Modules

        [Header("MODULES")]

        [SerializeField]
        private MovementModule movementModule = null;
        public MovementModule MovementModule { get => movementModule; }
        [SerializeField]
        private BaseCollisionModule collisionModule = null;
        public BaseCollisionModule CollisionModule { get => collisionModule; }
        [SerializeField]
        private HealthModule healthModule = null;
        public HealthModule HealthModule { get => healthModule; }
        [SerializeField]
        private AnimationModule animationModule = null;
        public AnimationModule AnimationModule { get => animationModule; }

        #endregion

        [Header("FSM")]
        [SerializeField]
        private Fsm fsm = null;

        [Header("SETTINGS")]
        [SerializeField]
        private float _damage = 1.0f;
        public float Damage { get => _damage; }

        private void Awake()
        {
            _movementContext = new MovementContext();
        }
        private void OnEnable()
        {
            if (fsm.CurrentState == null) return;
            fsm.ChangeToNextState();
        }
        private void OnDisable()
        {
            healthModule.ResetHealth();
        }
        private void Start()
        {
            fsm.SetUpStates();
            fsm.SetStartState();
            healthModule.OnDeath += OnDeath;
        }
        private void OnDestroy()
        {
            healthModule.OnDeath -= OnDeath;
        }
        private void FixedUpdate()
        {
            fsm.UpdateCurrentState();
        }

        public void SetUp(ShootModule owner)
        {
            collisionModule.SetUpModule();
        }

        internal void Deactivation()
        {
            this.gameObject.SetActive(false);
        }

        private void OnDeath()
        {
            fsm.ChangeToNextState();
        }

    }

}