using UnityEngine;
using UnityEngine.Assertions;

namespace Depthcharge.Actors
{

    [DisallowMultipleComponent]
    public abstract class Actor : MonoBehaviour
    {

        private Color _startColor = default;
        [SerializeField]
        private SpriteRenderer _spriteRenderer = null;

        internal Color StartColor { get => _startColor; }
        internal SpriteRenderer SpriteRenderer { get => _spriteRenderer; }

        protected virtual void Awake()
        {
            string message = $"=== {gameObject.name}.Actor.Awake() === Be ensure to fill \"spriteRenderer\" field in Inspector!";
            Assert.IsNotNull(_spriteRenderer, message);
        }
        protected virtual void Start()
        {
            _startColor = _spriteRenderer.color;
        }

    }
}