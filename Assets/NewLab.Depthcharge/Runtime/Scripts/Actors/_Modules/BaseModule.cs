using UnityEngine;

namespace Depthcharge.Actors.Modules
{

    public abstract class BaseModule : MonoBehaviour
    {

        [SerializeField]
        protected Actor owner = null;

        public virtual void SetUpModule() { }
        public virtual void UpdateModule() { }
        public virtual void CleanUpModule() { }

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