using UnityEngine;
using Depthcharge.Actors.Modules;

namespace Depthcharge.Actors
{

    [DisallowMultipleComponent]
    public class BulletController : MonoBehaviour
    {

        [SerializeField]
        private float _damage = 1.0f;
        public float Damage { get => _damage; }

        private bool _isShooted = false;
        public bool IsShooted { get => _isShooted; }

        [SerializeField]
        private MovementModule movementModule = null;
        public MovementModule MovementModule { get => movementModule; }

        [SerializeField]
        private CollisionModule collisionModule = null;
        public CollisionModule CollisionModule { get => collisionModule; }


        private void Start()
        {
            movementModule.SetUpModule();
            collisionModule.SetUpModule(this.gameObject);
        }

        private void FixedUpdate()
        {
            movementModule.MoveTarget();
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