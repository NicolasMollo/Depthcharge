using Depthcharge.Actors.AI;
using Depthcharge.Actors.Modules;
using UnityEngine;

namespace Depthcharge.Actors
{

    [DisallowMultipleComponent]
    public class EnemyController : MonoBehaviour
    {

        #region Modules

        [Header("MODULES")]

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
        #region Fsm

        [Header("FMS")]

        [SerializeField]
        private Fsm fsm = null;

        #endregion
        #region Settings

        [Header("SETTINGS")]

        [SerializeField]
        private Vector2 movementDirection = Vector2.zero;

        [SerializeField]
        [Range(1.0f, 100.0f)]
        private float scorePoints = 1.0f;

        #endregion

        private void Start()
        {

            _movementModule.SetUpModule();
            _healthModule.SetUpModule();
            _shootModule.SetUpModule();
            fsm.SetStartState();

        }

        private void Update()
        {

            _movementModule.MoveTarget(movementDirection);

        }

    }

}