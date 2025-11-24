using Depthcharge.Actors.AI;
using Depthcharge.Actors.Modules;
using UnityEngine;

namespace Depthcharge.Actors
{

    [DisallowMultipleComponent]
    public class BulletController : MonoBehaviour
    {

        private ShootModule owner = null;
        private BaseBulletBehaviour behaviour = null;
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
        [Header("SETTINGS")]
        [SerializeField]
        private float _damage = 1.0f;
        public float Damage { get => _damage; }

        private string endOfMapTag = string.Empty;


        internal int collsionLayerMask = 0;

        [SerializeField]
        private Fsm fsm = null;

        private void Awake()
        {
            _movementContext = new MovementContext();
            behaviour = GetComponent<BaseBulletBehaviour>();
        }
        private void OnEnable()
        {
            behaviour.OnBulletEnable();
        }
        private void OnDisable()
        {
            healthModule.ResetHealth();
            behaviour.OnBulletDisable();
        }
        private void Start()
        {
            behaviour.OnBulletStart();
            healthModule.OnDeath += OnDeath;
        }
        private void OnDestroy()
        {
            healthModule.OnDeath -= OnDeath;
            behaviour.OnBulletDestroy();
        }
        private void FixedUpdate()
        {
            movementModule.MoveTarget(MovementModule.Target.transform.up, _movementContext.TargetToReach);
        }

        public void SetUp(ShootModule owner)
        {
            this.owner = owner;
            behaviour.OnBulletSetUp();
            collisionModule.SetUpModule();
        }

        public void OnCollisionWithEndOfMap(string endOfMapTag)
        {
            this.endOfMapTag = endOfMapTag;
        }

        internal void Deactivation()
        {
            this.gameObject.SetActive(false);
        }

        public void ResetBulletTransform()
        {
            movementModule.MoveTarget(owner.ShootPoint.position);
            transform.SetParent(owner.BulletsParent);
        }

        private void OnDeath()
        {
            behaviour.OnBulletDeath(endOfMapTag);
            endOfMapTag = string.Empty;
        }

    }

}