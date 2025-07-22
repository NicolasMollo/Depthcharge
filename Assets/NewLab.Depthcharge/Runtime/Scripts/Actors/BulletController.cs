using UnityEngine;
using Depthcharge.Actors.Modules;

namespace Depthcharge.Actors
{

    [DisallowMultipleComponent]
    public class BulletController : MonoBehaviour
    {

        #region Modules

        [Header("MODULES")]

        [SerializeField]
        private MovementModule movementModule = null;
        public MovementModule MovementModule { get => movementModule; }

        [SerializeField]
        private BaseCollisionModule collisionModule = null;
        public BaseCollisionModule CollisionModule { get => collisionModule; }

        #endregion
        #region Settings

        [Header("SETTINGS")]

        [SerializeField]
        private float _damage = 1.0f;
        public float Damage { get => _damage; }

        [SerializeField]
        private Vector2 movementDirection = Vector2.zero;

        #endregion

        private bool _isShooted = false;
        public bool IsShooted { get => _isShooted; }


        private void Start()
        {
            movementModule.SetUpModule();
            collisionModule.SetUpModule(this.gameObject);
        }

        private void FixedUpdate()
        {
            movementModule.MoveTarget(movementDirection);
        }

        private void OnEnable()
        {
            _isShooted = true;
        }
        private void OnDisable()
        {
            _isShooted = false;
        }

        public void Deactivation()
        {
            this.gameObject.SetActive(false);
        }

    }

}