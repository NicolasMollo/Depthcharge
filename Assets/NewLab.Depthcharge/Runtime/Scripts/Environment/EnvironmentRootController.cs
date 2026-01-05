using UnityEngine;
using UnityEngine.Assertions;


namespace Depthcharge.Environment
{

    [DisallowMultipleComponent]
    public class EnvironmentRootController : MonoBehaviour
    {

        public static EnvironmentRootController Instance { get; private set; } = null;

        [SerializeField]
        private SpriteRenderer _seaSpriteRenderer = null;
        [SerializeField]
        private Transform _topSeaTarget = null;
        [SerializeField]
        private Transform _bottomSeaTarget = null;

        public Vector2 SeaSize { get => _seaSpriteRenderer.bounds.size; }
        public Vector2 SeaPosition { get => _seaSpriteRenderer.transform.position; }
        public Transform TopSeaTarget { get => _topSeaTarget; }
        public Transform BottomSeaTarget { get => _bottomSeaTarget; }



        private void Awake()
        {
            SetSingleton();
            string message = $"=== EnvirnomentRootContoller.SeaSize === \"seaSpriteRenderer\" is null!";
            Assert.IsNotNull(_seaSpriteRenderer, message);
        }
        private void SetSingleton()
        {
            if (Instance != null)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }

    }

}