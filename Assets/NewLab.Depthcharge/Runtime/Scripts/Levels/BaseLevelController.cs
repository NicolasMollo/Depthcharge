using Codice.CM.Client.Differences.Graphic;
using Depthcharge.Actors;
using UnityEngine;

namespace Depthcharge.LevelManagement
{

    [DisallowMultipleComponent]
    public abstract class BaseLevelController : MonoBehaviour
    {

        protected GameSystemsRoot systemsRoot = null;
        protected abstract void SetUp();
        protected virtual void CleanUp() { }
        protected abstract bool WinCondition();

        private void Start()
        {
            systemsRoot = GameSystemsRoot.Instance;
            SetUp();
        }

        private void OnDestroy()
        {
            CleanUp();
        }

    }

}