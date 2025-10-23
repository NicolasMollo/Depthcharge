using UnityEngine;
using Depthcharge.Actors.Modules;

namespace Depthcharge.Actors
{

    [DisallowMultipleComponent]
    public class BulletController : MonoBehaviour
    {

        private ShootModule owner = null;
        private bool _isShooted = false;
        public bool IsShooted { get => _isShooted; }

        private MovementContext movementContext = default;

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

        #endregion
        #region Settings

        [Header("SETTINGS")]

        [SerializeField]
        private float _damage = 1.0f;
        public float Damage { get => _damage; }

        [SerializeField]
        private Vector2 movementDirection = Vector2.zero;

        #endregion

        private void Awake()
        {
            movementContext = new MovementContext();
        }
        public void SetUp(ShootModule owner)
        {
            this.owner = owner;
            movementContext.TargetToReach = FindFirstObjectByType<PlayerController>().transform;
            collisionModule.SetUpModule(this.gameObject);
        }

        private void FixedUpdate()
        {
            movementModule.MoveTarget(movementDirection, movementContext.TargetToReach);
        }

        private void OnEnable()
        {
            movementContext.SpawnTime = Time.time;
            movementContext.StartPosition = movementModule.Target.GetPosition();
            _isShooted = true;
            if (!collisionModule.enabled)
            {
                collisionModule.EnableModule();
            }
            healthModule.OnDeath += Deactivation;
        }
        private void OnDisable()
        {
            healthModule.OnDeath -= Deactivation;
            healthModule.ResetHealth();
            _isShooted = false;
        }

        public void Deactivation()
        {
            this.gameObject.SetActive(false);
        }

        public void ResetBulletTransform()
        {
            movementModule.MoveTarget(owner.ShootPoint.position);
            transform.SetParent(owner.BulletsParent);
        }

    }

}