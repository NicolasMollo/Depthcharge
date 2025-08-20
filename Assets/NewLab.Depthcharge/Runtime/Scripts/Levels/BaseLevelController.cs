using Depthcharge.GameManagement;
using Depthcharge.UI;
using UnityEngine;

namespace Depthcharge.LevelManagement
{

    [DisallowMultipleComponent]
    public abstract class BaseLevelController : MonoBehaviour
    {

        protected GameSystemsRoot systemsRoot = null;
        protected GameUIController UI = null;
        protected GameLogic gameLogic = null;
        protected LevelStats _stats = null;
        public LevelStats Stats { get => _stats; }

        protected virtual void SetUp()
        {
            _stats = new LevelStats();
            systemsRoot.UISystem.SetStartUIActiveness(false);
            systemsRoot.UISystem.SetGameUIActiveness(true);
            UI = systemsRoot.UISystem.GameUI;
        }
        protected virtual void CleanUp() { }

        private void Start()
        {
            systemsRoot = GameSystemsRoot.Instance;
            gameLogic = GameLogic.Instance;
            SetUp();
        }

        private void OnDestroy()
        {
            CleanUp();
        }

    }

}