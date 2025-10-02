using UnityEngine;

namespace Depthcharge.Actors.Modules
{

    public abstract class BaseModule : MonoBehaviour
    {

        public bool IsModuleSetUp { get; protected set; }
        public virtual void SetUpModule(GameObject owner = null)
        {
            IsModuleSetUp = true;
        }
        public virtual void UpdateModule() { }
        public virtual void CleanUpModule(GameObject owner = null) { }

        public virtual void EnableModule()
        {
            enabled = true;
        }
        public virtual void DisableModule()
        {
            enabled = false;
        }

    }

}