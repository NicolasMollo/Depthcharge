using Depthcharge.Actors.Modules;
using Depthcharge.Audio;
using UnityEngine;
using UnityEngine.Assertions;

namespace Depthcharge.Actors
{

    [DisallowMultipleComponent]
    [RequireComponent(typeof(AudioSourceController))]
    public abstract class Actor : MonoBehaviour
    {

        private Color _startColor = default;
        protected AudioSourceController _audioSource = null;

        [SerializeField]
        private SpriteRenderer _spriteRenderer = null;

        internal Color StartColor { get => _startColor; }
        internal SpriteRenderer SpriteRenderer { get => _spriteRenderer; }
        internal AudioSourceController AudioSource { get => _audioSource; }


        protected virtual void Awake()
        {
            _audioSource = GetComponent<AudioSourceController>();
            string message = $"=== {gameObject.name}.Actor.Awake() === Be ensure to fill \"spriteRenderer\" field in Inspector!";
            Assert.IsNotNull(_spriteRenderer, message);
        }
        protected virtual void Start()
        {
            _startColor = _spriteRenderer.color;
        }

        internal virtual void OnCollisionWithEndOfMap(EndOfMapContext context) { }
        internal virtual void OnCollisionStayWithEndOfMap(EndOfMapContext context) { }
        internal virtual void OnCollisionExitWithEndOfMap(EndOfMapContext context) { }

    }
}