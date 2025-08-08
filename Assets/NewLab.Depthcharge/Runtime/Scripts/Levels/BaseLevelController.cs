using Depthcharge.GameManagement;
using UnityEngine;

namespace Depthcharge.LevelManagement
{

    [DisallowMultipleComponent]
    public abstract class BaseLevelController : MonoBehaviour
    {

        protected GameSystemsRoot systemsRoot = null;
        protected GameLogic gameLogic = null;
        protected LevelStats _stats = null;
        public LevelStats Stats { get => _stats; }

        protected abstract void SetUp();
        protected virtual void CleanUp() { }

        private void Start()
        {
            systemsRoot = GameSystemsRoot.Instance;
            gameLogic = GameLogic.Instance;
            _stats = new LevelStats();
            SetUp();
        }

        private void OnDestroy()
        {
            CleanUp();
        }

    }

}